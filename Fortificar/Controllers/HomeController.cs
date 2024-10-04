using Fortificar.Areas.Identity.Data;
using Fortificar.Models;
using Fortificar.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        public HomeController(ILogger<HomeController> logger, UserManager<FortificarUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Resgata o nome do usuário conectado
            var userName = _userManager.GetUserName(this.User);
            ViewData["UserNome"] = userName;

            // Resgata o usuário atual usando o UserManager
            var user = await _userManager.GetUserAsync(this.User);

            // Supondo que o "Tipo" esteja na entidade de usuário
            if (user != null)
            {
                // Verifica o valor da coluna "Tipo"
                if (user.Tipo == 0) // Se o tipo for 0 (Empresa)
                {
                    return View("IndexEmpresa");
                }
                else if (user.Tipo == 1) // Se o tipo for 1 (Administrador)
                {
                    return View("Index");
                }
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