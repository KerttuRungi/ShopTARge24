using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;

namespace Shop.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class EmailsApiController : ControllerBase
        {
            private readonly IEmailServices _emailServices;

            public EmailsApiController(IEmailServices emailServices)
            {
                _emailServices = emailServices;
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
    }
    }

