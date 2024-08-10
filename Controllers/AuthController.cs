using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using ExtrosServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExtrosServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static ConcurrentDictionary<string, User> PendingRegistrations = new ConcurrentDictionary<string, User>();
        private static ConcurrentDictionary<string, string> VerificationCodes = new ConcurrentDictionary<string, string>();


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

            PendingRegistrations[model.Email] = userProfile;

            var verificationCode = new Random().Next(100000, 999999).ToString();
            VerificationCodes[model.Email] = verificationCode;

            //will use email service later (mailgun or mailkit)
            Console.WriteLine($"Verification code for {model.Email}: {verificationCode}");

            return Ok("Verification code sent to email.");
        }

    }
}