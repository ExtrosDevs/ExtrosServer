using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ExtrosServer.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ExtrosServer.Services;
using ExtrosServer.Data;
namespace ExtrosServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly EmailService _emailService;
        private readonly ApplicationDBContext _context;
        private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(5);
        private readonly string _secretKey = "your_very_long_and_secure_secret_key_32_bytes";


        public AuthController(IMemoryCache memoryCache, EmailService emailService, ApplicationDBContext context)
        {
            _memoryCache = memoryCache;
            _emailService = emailService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from auth!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister model)
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
                LastLoginDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                BOD = DateTime.UtcNow,
                Verified = false,
                PhoneNumber = 999,
                UserImage = "default.png",
                Bio = "I am a new user",
                IsAdmin = false,
                PostalCode = 123456,
                UserFieldId = null
            };

            // store user profile in cache
            _memoryCache.Set(model.Email, userProfile, _expirationTime);

            var verificationCode = new Random().Next(1000, 9999).ToString();

            // store codes in cache
            _memoryCache.Set($"{model.Email}_verificationCode", verificationCode, _expirationTime);

            Console.WriteLine($"Verification code for {model.Email}: {verificationCode}");

            // send verification code
            var emailSubject = "Your Verification Code | Extros";
            var emailBody = $@"
        <html>
        <body>
            <h3>Hello {userProfile.Username},</h3>
            <p>Thank you for registering with Extros!</p>
            <p>Your verification code is <strong>{verificationCode}</strong>.</p>
            <p>Please use this code within {_expirationTime / TimeSpan.FromMinutes(1)} minutes to complete your registration process.</p>
            <p>Best regards,<br/>The Extros Team</p>
        </body>
        </html>";
            await _emailService.SendEmailAsync(model.Email, emailSubject, emailBody);


            return Ok("Verification code sent to email.");
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] UserActivation model)
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
                    if (_memoryCache.TryGetValue(model.Email, out User userProfile))
                    {
                        if (userProfile != null)
                        {
                            userProfile.UserFieldId = null;
                            userProfile.UserField = null;
                            userProfile.LastLoginDate = DateTime.UtcNow;
                            Console.WriteLine($"User {userProfile.Email}, {userProfile.Username}, {userProfile.Password}, {userProfile.UserId}.");

                            if (_context != null && _context.Users != null)
                            {
                                _context.Users.Add(userProfile);
                                await _context.SaveChangesAsync();

                                // Remove user from cache after successful database insertion
                                _memoryCache.Remove(model.Email);

                                // Send activation email
                                var emailBody = $@"
                    <html>
                    <body>
                        <p>Hello there,</p>
                        <p>Your account has been activated successfully.</p>
                        <p>Welcome to Extros!</p>
                        <p>Best regards,<br/>The Extros Team</p>
                    </body>
                    </html>";
                                // await _emailService.SendEmailAsync(model.Email, "Account Activated Successfully", emailBody);

                                return Ok("Account activated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Database context or Users DbSet is null");
                                return StatusCode(500, "Internal server error: Database context is not properly initialized.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("User profile is null after retrieval from cache");
                            return BadRequest("User profile not found in cache.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve user profile from cache");
                        return BadRequest("User profile not found in cache.");
                    }
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
