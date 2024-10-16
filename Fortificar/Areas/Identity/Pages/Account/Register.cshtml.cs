// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Fortificar.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Fortificar.Models;
using Fortificar.Models.ViewModels;
using Fortificar.Data;

namespace Fortificar.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<FortificarUser> _signInManager;
        private readonly UserManager<FortificarUser> _userManager;
        private readonly IUserStore<FortificarUser> _userStore;
        private readonly IUserEmailStore<FortificarUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<FortificarUser> userManager,
            IUserStore<FortificarUser> userStore,
            SignInManager<FortificarUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        private readonly AuthDbContext _context;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        public Proponente Proponente { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Nome")]
            public string Name { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Sobrenome")]
            public string LastName { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "A {0} precisa ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "A senha e a confirmação não são iguais.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/");
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null, RegistroViewModel registroViewModel = null)
        {


            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            registroViewModel ??= new RegistroViewModel
            {
                Registro = this,
                Proponente = new Proponente(),
                ResponsavelLegal = new ResponsavelLegal()
            };
            if (ModelState.IsValid)
            {
                var user = CreateUser();


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                
                var responsavelLegal = new ResponsavelLegal
                {
                    Nome = registroViewModel.ResponsavelLegal.Nome,
                    CPF = registroViewModel.ResponsavelLegal.CPF,
                    RG = registroViewModel.ResponsavelLegal.RG,
                    Endereco = registroViewModel.ResponsavelLegal.Endereco,
                    MandatoVigente = registroViewModel.ResponsavelLegal.MandatoVigente,
                    OrgaoExpedidor = registroViewModel.ResponsavelLegal.OrgaoExpedidor,
                    Telefone1 = registroViewModel.ResponsavelLegal.Telefone1,
                    Telefone2 = registroViewModel.ResponsavelLegal.Telefone2,
                    Telefone3 = registroViewModel.ResponsavelLegal.Telefone3
                };

                var proponente = new Proponente
                {
                    RazaoSocial = registroViewModel.Proponente.RazaoSocial,
                    NomeFantasia = registroViewModel.Proponente.NomeFantasia,
                    CNPJ = registroViewModel.Proponente.CNPJ,
                    InscricaoEstadual = registroViewModel.Proponente.InscricaoEstadual,
                    InscricaoMunicipal = registroViewModel.Proponente.InscricaoMunicipal,
                    Endereco = registroViewModel.Proponente.Endereco,
                    Numero = registroViewModel.Proponente.Numero,
                    Complemento = registroViewModel.Proponente.Complemento,
                    Bairro = registroViewModel.Proponente.Bairro,
                    Cidade = registroViewModel.Proponente.Cidade,
                    Estado = registroViewModel.Proponente.Estado,
                    CEP = registroViewModel.Proponente.CEP,
                    Site = registroViewModel.Proponente.Site,
                    Telefone1 = registroViewModel.Proponente.Telefone1,
                    Telefone2 = registroViewModel.Proponente.Telefone2,
                    Telefone3 = registroViewModel.Proponente.Telefone3,
                    Banco = registroViewModel.Proponente.Banco,
                    Agencia = registroViewModel.Proponente.Agencia,
                    Conta = registroViewModel.Proponente.Conta,
                    TipoConta = registroViewModel.Proponente.TipoConta,
                    ResponsavelLegalId = responsavelLegal.Id,
                    Historico = registroViewModel.Proponente.Historico,
                    PrincipaisAcoes = registroViewModel.Proponente.PrincipaisAcoes,
                    PublicoAlvo = registroViewModel.Proponente.PublicoAlvo,
                    RegioesAtendimento = registroViewModel.Proponente.RegioesAtendimento,
                    Infraestrutura = registroViewModel.Proponente.Infraestrutura,
                    EquipeMultidisciplinar = registroViewModel.Proponente.EquipeMultidisciplinar
                };

                // Salvar objetos no banco
                _context.Proponente.Add(proponente);
                await _context.SaveChangesAsync();

                _context.ResponsavelLegal.Add(responsavelLegal);
                await _context.SaveChangesAsync();

                
                //user.Tipo = registroViewModel.Tipo;
                user.ProponenteId = proponente.Id;


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        private FortificarUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<FortificarUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(FortificarUser)}'. " +
                    $"Ensure that '{nameof(FortificarUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<FortificarUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<FortificarUser>)_userStore;
        }
    }
}
