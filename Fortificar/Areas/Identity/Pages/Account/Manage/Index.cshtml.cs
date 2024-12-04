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
        [BindProperty]
        public ResponsavelLegal ResponsavelLegal { get; set; } = new();

        [BindProperty]
        public Desembolso Desembolso { get; set; } = new();

        [BindProperty]
        public List<Parametro> Parametros { get; set; } = new();

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        private async Task LoadAsync(FortificarUser user)
        {        

            if (user.Tipo == 0)
            {
                try
                {
                    var _parametro = _context.Parametro
         .FirstOrDefault(p => p.Nome.ToLower() == "desembolso");

                    Desembolso = new Desembolso
                    {
                        ValorTotal = _context.Projeto
        .Where(p => p.SituacaoId == 4 || p.SituacaoId == 6)
        .Sum(p => p.Orcamento),
                        ValorMin = _parametro.ValorMin,
                        ValorMax = _parametro.ValorMax
                    };

                    // Busca todos os parâmetros no banco de dados
                    var parametros = _context.Parametro.ToList();

                    // Se a lista for nula, inicializa uma lista vazia para evitar NullReferenceException
                    if (parametros == null)
                    {
                        parametros = new List<Parametro>();
                    }

                    Parametros = parametros;

                }
                catch (Exception ex)
                {
                    // Tratamento genérico para outras exceções
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }else if(user.Tipo == 1)
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


            ViewData["Tipo"] = user.Tipo;
        }

       
        public async Task<IActionResult> ConfigurarParametros(List<Parametro> parametros)
        {
            foreach (var parametro in parametros)
            {
                var parametroExistente = await _context.Parametro
                    .FirstOrDefaultAsync(p => p.Id == parametro.Id);

                if (parametroExistente != null)
                {
                    parametroExistente.ValorMin = parametro.ValorMin;
                    parametroExistente.ValorMax = parametro.ValorMax;
                    parametroExistente.Ativo = parametro.Ativo;
                }
            }

            
            await _context.SaveChangesAsync();

            return Page();
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


            
            var proponente = await _context.Proponente
                .Include(p => p.ResponsavelLegal)
                .FirstOrDefaultAsync(p => p.Id == user.ProponenteId);

            if (proponente != null)
            {
                proponente.NomeFantasia = Proponente.NomeFantasia;
                proponente.CNPJ = Proponente.CNPJ;
                proponente.Endereco = Proponente.Endereco;
                proponente.Bairro = Proponente.Bairro;
                proponente.Cidade = Proponente.Cidade;
                proponente.CEP = Proponente.CEP;
                proponente.InformacoesRelevantes = Proponente.InformacoesRelevantes;
                proponente.EmailEmpresa = Proponente.EmailEmpresa;
                proponente.Site = Proponente.Site;
                proponente.Telefone1 = Proponente.Telefone1;
                proponente.Telefone2 = Proponente.Telefone2;
                proponente.Telefone3 = Proponente.Telefone3;

                proponente.ResponsavelLegal.Nome = ResponsavelLegal.Nome;
                proponente.ResponsavelLegal.CPF = ResponsavelLegal.CPF;
                proponente.ResponsavelLegal.RG = ResponsavelLegal.RG;
                proponente.ResponsavelLegal.OrgaoExpedidor = ResponsavelLegal.OrgaoExpedidor;
                proponente.ResponsavelLegal.CargoOSC = ResponsavelLegal.CargoOSC;
                proponente.ResponsavelLegal.MandatoVigente = ResponsavelLegal.MandatoVigente;
                proponente.ResponsavelLegal.Endereco = ResponsavelLegal.Endereco;
                proponente.ResponsavelLegal.Telefone1 = ResponsavelLegal.Telefone1;
                proponente.ResponsavelLegal.Telefone2 = ResponsavelLegal.Telefone2;
                proponente.ResponsavelLegal.Telefone3 = ResponsavelLegal.Telefone3;
                
                proponente.Historico = Proponente.Historico;
                proponente.PrincipaisAcoes = Proponente.PrincipaisAcoes;
                proponente.PublicoAlvo = Proponente.PublicoAlvo;
                proponente.RegioesAtendimento = Proponente.RegioesAtendimento;
                proponente.Infraestrutura = Proponente.Infraestrutura;
                proponente.EquipeMultidisciplinar = Proponente.EquipeMultidisciplinar;


                _context.Update(proponente);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage();
        }
    }
}