// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Fortificar.Areas.Identity.Data;
using Fortificar.Data;
using Fortificar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fortificar.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<FortificarUser> _userManager;
        private readonly SignInManager<FortificarUser> _signInManager;
        private readonly AuthDbContext _context;

        public IndexModel(
            UserManager<FortificarUser> userManager,
            SignInManager<FortificarUser> signInManager, AuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Email de login")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        [BindProperty]
        public Proponente Proponente { get; set; } = new();

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        private async Task LoadAsync(FortificarUser user)
        {

            try
            {
                Proponente = await _context.Proponente
                    .Include(p => p.ResponsavelLegal)
                    .FirstOrDefaultAsync(p => p.Id == user.ProponenteId);

            }
            catch (Exception ex)
            {
                // Tratamento genérico para outras exceções
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }



            if (Proponente == null)
            {
                // Criar um novo ResponsavelLegal se necessário
                var novoResponsavelLegal = new ResponsavelLegal();
                _context.ResponsavelLegal.Add(novoResponsavelLegal);
                await _context.SaveChangesAsync();

                // Criar um novo Proponente e associar o ResponsavelLegal
                Proponente = new Proponente
                {
                    ResponsavelLegalId = novoResponsavelLegal.Id,
                    // Preencha outros campos do Proponente conforme necessário
                };
                _context.Proponente.Add(Proponente);
                await _context.SaveChangesAsync();

                // Associar o ProponenteId ao usuário
                user.ProponenteId = Proponente.Id;
                await _userManager.UpdateAsync(user);
            }


        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var proponente = await _context.Proponente
            .Include(p => p.ResponsavelLegal)
            .FirstOrDefaultAsync(p => p.Id == user.ProponenteId);

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            // Atualizar os dados do Proponente
            var proponente = await _context.Proponente
                .FirstOrDefaultAsync(p => p.Id == user.ProponenteId);

            if (proponente != null)
            {
                proponente.NomeFantasia = Proponente.NomeFantasia;
                proponente.CNPJ = Proponente.CNPJ;
                proponente.Endereco = Proponente.Endereco;
                // Atualize outros campos conforme necessário

                _context.Update(proponente);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}