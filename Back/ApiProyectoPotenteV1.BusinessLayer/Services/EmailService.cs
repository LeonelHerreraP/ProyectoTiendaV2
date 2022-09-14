using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace ApiProyectoPotenteV1.BusinessLayer.Services
{
    public class EmailService : IEmailService
    {

        public EmailService()
        {

        }

        public void Enviar(Email cuerpa)
        {
            /*var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("eliktienda@hotmail.com"));
            email.To.Add(MailboxAddress.Parse(cuerpa.para));
            email.Subject = "Compra en Elik Tienda";
            email.Body = new TextPart(TextFormat.Html) { Text = cuerpa.cuerpo };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("eliktienda@hotmail.com", "velake123@");
            smtp.Send(email);
            smtp.Disconnect(true);*/
        }
    }
}
