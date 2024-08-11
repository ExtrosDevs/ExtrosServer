using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ExtrosServer.Models;

namespace ExtrosServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(5);

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
    }
}
