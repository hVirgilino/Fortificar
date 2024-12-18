﻿// Licensed to the .NET Foundation under one or more agreements.
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
        private readonly AuthDbContext _context;

        public RegisterModel(
            UserManager<FortificarUser> userManager,
            IUserStore<FortificarUser> userStore,
            SignInManager<FortificarUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, AuthDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

       

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

            public Proponente? Proponente { get; set; }
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

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var user = CreateUser();


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.Tipo = 1;                  

                
                


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                    var auxRespLegal = new ResponsavelLegal
                    {
                        Nome = Input.Proponente.ResponsavelLegal.Nome,
                        CPF = Input.Proponente.ResponsavelLegal.CPF,
                        RG = Input.Proponente.ResponsavelLegal.RG,
                        Endereco = Input.Proponente.ResponsavelLegal.Endereco,
                        MandatoVigente = Input.Proponente.ResponsavelLegal.MandatoVigente,
                        OrgaoExpedidor = Input.Proponente.ResponsavelLegal.OrgaoExpedidor,
                        Telefone1 = Input.Proponente.ResponsavelLegal.Telefone1,
                        Telefone2 = Input.Proponente.ResponsavelLegal.Telefone2,
                        Telefone3 = Input.Proponente.ResponsavelLegal.Telefone3
                    };

                    _context.ResponsavelLegal.Add(auxRespLegal);
                    await _context.SaveChangesAsync();
                    var proponente = new Proponente
                    {
                        RazaoSocial = Input.Proponente.RazaoSocial,
                        NomeFantasia = Input.Proponente.NomeFantasia,
                        CNPJ = Input.Proponente.CNPJ,
                        InscricaoEstadual = Input.Proponente.InscricaoEstadual,
                        InscricaoMunicipal = Input.Proponente.InscricaoMunicipal,
                        Endereco = Input.Proponente.Endereco,
                        Numero = Input.Proponente.Numero,
                        Complemento = Input.Proponente.Complemento,
                        Bairro = Input.Proponente.Bairro,
                        Cidade = Input.Proponente.Cidade,
                        Estado = Input.Proponente.Estado,
                        CEP = Input.Proponente.CEP,
                        Site = Input.Proponente.Site,
                        Telefone1 = Input.Proponente.Telefone1,
                        Telefone2 = Input.Proponente.Telefone2,
                        Telefone3 = Input.Proponente.Telefone3,
                        Banco = Input.Proponente.Banco,
                        Agencia = Input.Proponente.Agencia,
                        Conta = Input.Proponente.Conta,
                        TipoConta = Input.Proponente.TipoConta,
                        ResponsavelLegalId = auxRespLegal.Id,
                        Historico = Input.Proponente.Historico,
                        PrincipaisAcoes = Input.Proponente.PrincipaisAcoes,
                        PublicoAlvo = Input.Proponente.PublicoAlvo,
                        RegioesAtendimento = Input.Proponente.RegioesAtendimento,
                        Infraestrutura = Input.Proponente.Infraestrutura,
                        EquipeMultidisciplinar = Input.Proponente.EquipeMultidisciplinar
                    };
                    // Salvar objetos no banco
                    _context.Proponente.Add(proponente);
                    await _context.SaveChangesAsync();

                    user.ProponenteId = proponente.Id;

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
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
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
