using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Fortificar.Services
{
    public class EmailService
    {
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _smtpHost;
        private readonly int _smtpPort;

        public EmailService(IConfiguration configuration)
        {
            _fromEmail = "henriquevoliveira5@gmail.com";
            _fromPassword = "qnzz dtvc etmk bjge";
            _smtpHost = "smtp.gmail.com";
            _smtpPort = 587;
        }

        public async Task EnviarEmail(string para, string assunto, string corpo)
        {
            var fromAddress = new MailAddress(_fromEmail, "Fortificar");
            var toAddress = new MailAddress(para);

            var smtp = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = assunto,
                Body = corpo
            })
            {
                smtp.Send(message);
            }


        }
    }
}
