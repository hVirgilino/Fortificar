using Fortificar.Areas.Identity.Pages.Account;

namespace Fortificar.Models.ViewModels
{
    public class RegistroViewModel
    {
        public RegisterModel? Registro { get; set; }
        public Proponente? Proponente { get; set; }
        public ResponsavelLegal? ResponsavelLegal { get; set; }
    }
}
