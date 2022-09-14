using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace ApiProyectoPotenteV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpPost]
        [Route("Enviar")]
        public IActionResult Enviar([FromBody] Email cuerpa, [FromServices] IEmailService bue)
        {
            bue.Enviar(cuerpa);
           
            return Ok();
        }
    }
}
