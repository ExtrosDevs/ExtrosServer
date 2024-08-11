using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ExtrosServer.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace ExtrosServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(5);
        private readonly string _secretKey = "your_very_long_and_secure_secret_key_32_bytes";


        public AuthController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from auth!");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegister model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request body.");
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("All fields are required.");
            }

            var userProfile = new User
            {
                UserId = Guid.NewGuid(),
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
            };

            // Store the user profile in the cache with expiration
            _memoryCache.Set(model.Email, userProfile, _expirationTime);

            var verificationCode = new Random().Next(1000, 9999).ToString();

            // Store the verification code in the cache with expiration
            _memoryCache.Set($"{model.Email}_verificationCode", verificationCode, _expirationTime);

            // Simulate sending the verification code via email
            Console.WriteLine($"Verification code for {model.Email}: {verificationCode}");

            return Ok("Verification code sent to email.");
        }

        [HttpPost("activate")]
        public IActionResult Activate([FromBody] UserActivation model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request body.");
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.VerificationCode))
            {
                return BadRequest("Email and verification code are required.");
            }

            // check if the verification code matches
            if (_memoryCache.TryGetValue($"{model.Email}_verificationCode", out string storedVerificationCode))
            {
                if (storedVerificationCode == model.VerificationCode)
                {
                    // verification code is correct, remove it from cache
                    _memoryCache.Remove($"{model.Email}_verificationCode");

                    return Ok("Account activated successfully.");
                }
                else
                {
                    return BadRequest("Invalid verification code.");
                }
            }
            else
            {
                return BadRequest("Verification code has expired or is not found.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request body.");
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email and password are required.");
            }

            if (_memoryCache.TryGetValue(model.Email, out User userProfile))
            {
                if (userProfile.Password == model.Password)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_secretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Name, userProfile.Username)
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return BadRequest("Invalid password.");
                }
            }
            else
            {
                return BadRequest("User not found.");
            }
        }
    }
}
