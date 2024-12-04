using Fortificar.Areas.Identity.Data;
using Fortificar.Models;
using Fortificar.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fortificar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<FortificarUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, UserManager<FortificarUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> index()
        {
            // Resgata o nome do usuário conectado
            var userName = _userManager.GetUserName(this.User);
            ViewData["UserNome"] = userName;

            // Resgata o usuário atual usando o UserManager
            var user = await _userManager.GetUserAsync(this.User);

            var pastaDoc = Path.Combine(_webHostEnvironment.WebRootPath, "pdf");

            if (Directory.Exists(pastaDoc))
            {
                // Verifica se há arquivos na pasta
                var arquivos = Directory.GetFiles(pastaDoc, "*.pdf");
                if (arquivos.Length > 0)
                {
                    // Pega o primeiro arquivo encontrado
                    var caminho = arquivos[0];
                    var nomeArq = Path.GetFileName(caminho);

                    // Armazena as informações no ViewData
                    var caminhoArq = $"/pdf/{nomeArq}";
                    ViewData["CaminhoDocAnexo"] = caminhoArq;
                    ViewData["NomeDocAnexo"] = nomeArq;
                }
                else
                {
                    // Caso a pasta esteja vazia
                    ViewData["CaminhoDocAnexo"] = null;
                    ViewData["NomeDocAnexo"] = null;
                }
            }
            else
            {
                // Caso a pasta não exista
                ViewData["CaminhoDocAnexo"] = null;
                ViewData["NomeDocAnexo"] = null;
            }

            // Redireciona para a Index padrão se o tipo não for encontrado
            ViewData["Tipo"] = user.Tipo;
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AnexarDocIndex(IFormFile anexo)
        {
            // Resgata o usuário atual usando o UserManager
            var user = await _userManager.GetUserAsync(this.User);
            ViewData["Tipo"] = user.Tipo;

            if (anexo == null || anexo.Length == 0 || Path.GetExtension(anexo.FileName).ToLower() != ".pdf")
            {
                ViewData["Erro"] = "Por favor, selecione um arquivo PDF válido.";
                return View("Index");
            }

            try
            {
                // Define o caminho da pasta wwwroot/pdf
                var pdfFolder = Path.Combine(_webHostEnvironment.WebRootPath, "pdf");

                // Apaga todos os arquivos na pasta
                if (Directory.Exists(pdfFolder))
                {
                    foreach (var filePath in Directory.GetFiles(pdfFolder))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(pdfFolder);
                }

                // Define o nome único do arquivo para evitar conflitos
                var nomeArq = Guid.NewGuid().ToString() + ".pdf";

                // Caminho completo para salvar o arquivo
                var caminho = Path.Combine(pdfFolder, nomeArq);

                // Salva o arquivo na pasta
                using (var fileStream = new FileStream(caminho, FileMode.Create))
                {
                    anexo.CopyTo(fileStream);
                }

                // Armazena os dados no ViewData
                var caminhoArq = $"/pdf/{nomeArq}"; // Caminho relativo
                ViewData["CaminhoDocAnexo"] = caminhoArq;
                ViewData["NomeDocAnexo"] = nomeArq;

                ViewData["Ok"] = "Arquivo carregado com sucesso!";
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro ao carregar o arquivo: {ex.Message}";
            }

            // Redireciona para a Index padrão se o tipo não for encontrado
            
            return View("Index");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}