using System.Net.Mail;
using System.Net;
using FluentEmail.Razor;

namespace Fortificar.Models
{
    public class EmailDados
    {
        public string Para { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        
        public EmailDados(string para, string assunto, string corpo = "")
        {
            Para = para;
            Assunto = assunto;
            Corpo = corpo;
        }
    }

}
