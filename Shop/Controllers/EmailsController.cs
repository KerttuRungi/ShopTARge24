using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.Email;

namespace Shop.Controllers
{
    // [ApiController]
    //[Route("api/[controller]")]
    [Route("[controller]")]
    public class EmailsController : Controller
        {
            private readonly IEmailServices _emailServices;

            public EmailsController(IEmailServices emailService)
            {
                _emailServices = emailService;
            }

            public IActionResult Index() {
                return View();
            }

            [HttpPost("send")]
            [Consumes("multipart/form-data")]
            public IActionResult SendEmail(EmailViewModel vm)
            {
            var files = Request.Form.Files.Any() ? Request.Form.Files.ToList() : new List<IFormFile>();


            var emailDto = new EmailDto
                {
                    To = vm.To,
                    Subject = vm.Subject,
                    Body = vm.Body,
                    Attachment = files
                };
            _emailServices.SendEmail(emailDto);


            return RedirectToAction(nameof(Index));
            //return Ok(response);
        }
        }
}



