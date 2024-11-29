using Fortificar.Models;
using Fortificar.Service;
using Fortificar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fortificar.Controllers
{
    public class EmailsController : Controller
    {
        private readonly EmailService _emailService;

        // Injeta o EmailService através do construtor
        public EmailsController(EmailService emailService)
        {
            _emailService = emailService;
        }

        // GET: Email/Index
        public IActionResult Index()
        {
            return View();
        }

        // P


        [HttpPost]
        public async Task<IActionResult> Enviar(string Destinatario, string Texto, string Assunto)
        {
            try
            {
                EmailDados _Email = new(Destinatario, Assunto, Texto);
                await _emailService.EnviarEmail(_Email.Para, _Email.Assunto, _Email.Corpo);

                return Ok("Projeto aprovado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar o e-mail: " + ex.ToString());
                return StatusCode(500, "Falha ao enviar o e-mail: " + ex.Message);
            }
        }
    }
}
