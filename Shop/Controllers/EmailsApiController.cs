using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.Accounts;

namespace Shop.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class EmailsApiController : ControllerBase
        {
            private readonly IEmailServices _emailServices;
            private readonly UserManager<ApplicationUser> _userManager;

        public EmailsApiController(IEmailServices emailServices, UserManager<ApplicationUser> userManager)
            {
                _emailServices = emailServices;
                _userManager = userManager;
        }

        [HttpPost("send")] // <- Add this
        [Consumes("multipart/form-data")]
        public IActionResult SendEmail([FromForm] EmailDto dto)
        {
            var response = new
            {
                To = dto.To,
                Subject = dto.Subject,
                Body = dto.Body,
                Attachments = dto.Attachment.Select(f => new { f.FileName, f.Length }),
                Message = "Email processed (mock)"
            };

            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Name = vm.Name,
                City = vm.City
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
                return BadRequest(new
                {
                    Message = "Registration failed",
                    Errors = result.Errors.Select(e => e.Description)
                });

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Build fake link
            var confirmUrl = $"https://localhost/Confirm?userId={user.Id}&token={token}";

            // Build DTO
            var emailDto = new EmailTokenDto
            {
                To = user.Email,
                Subject = "Confirm Your Account",
                Body = $"Click here to confirm: <a href=\"{confirmUrl}\">Confirm</a>",
                Token = token
            };

            // Call your existing void method
            _emailServices.SendEmailToken(emailDto, token);

            // Return info for Postman
            return Ok(new
            {
                Message = "User created successfully",
                UserId = user.Id,
                EmailPreview = emailDto
            });
        }
    }
}


