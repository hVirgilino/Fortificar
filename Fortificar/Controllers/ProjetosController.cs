using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fortificar.Data;
using Fortificar.Models;
using Fortificar.Models.ViewModels;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Fortificar.Migrations;
using System.Drawing;
using Microsoft.CodeAnalysis;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Build.Evaluation;
using System.Security.Claims;

namespace Fortificar.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly AuthDbContext _context;

        public ProjetosController(AuthDbContext context)
        {
            _context = context;
        }


        // GET: Projetos
        public async Task<IActionResult> Index(ProjetoViewModel viewModel)
        {
            return View(viewModel);
        }

        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // GET: Projetos/Create
        public IActionResult Create()
        {
            return View();
        }

		// POST: Projetos/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProjetoViewModel viewModel)
		{
			if (ModelState.IsValid)
			{//MIGRATION
				viewModel.Projeto.ProponenteId = viewModel.Proponente.Id;
				viewModel.Projeto.ResponsavelLegalId = viewModel.ResponsavelLegal.Id;
				viewModel.Projeto.ResponsavelTecnicoId = viewModel.ResponsavelTecnico.Id;

				_context.Add(viewModel.Projeto);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            
            var proponente = _context.Proponente.FirstOrDefault(p => p.FortificarUserId == userId);

            if (proponente == null)
            {
                return RedirectToAction("Error");
            }

            var view = new ProjetoViewModel
            {
                Proponente = new Proponente
                {
                    RazaoSocial = proponente.RazaoSocial,
                    NomeFantasia = proponente.NomeFantasia,
                    CNPJ = proponente.CNPJ,
                    InscricaoEstadual = proponente.InscricaoEstadual,
                    InscricaoMunicipal = proponente.InscricaoMunicipal,
                    Endereco = proponente.Endereco,
                    Numero = proponente.Numero,
                    Complemento = proponente.Complemento,
                    Bairro = proponente.Bairro,
                    Cidade = proponente.Cidade,
                    Estado = proponente.Estado,
                    CEP = proponente.CEP,
                    Telefone1 = proponente.Telefone1,
                    Telefone2 = proponente.Telefone2,
                    Telefone3 = proponente.Telefone3,
                    Site = proponente.Site
                }
            };
            return View(view);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarProjeto(ProjetoViewModel projetoViewModel, IFormFile? anexo)
        {
            if (ModelState.IsValid)
            {//MIGRATION
             
                var proponente = new Proponente
                {
                    RazaoSocial = projetoViewModel.Proponente.RazaoSocial,
                    NomeFantasia = projetoViewModel.Proponente.NomeFantasia,
                    CNPJ = projetoViewModel.Proponente.CNPJ,
                    InscricaoEstadual = projetoViewModel.Proponente.InscricaoEstadual,
                    InscricaoMunicipal = projetoViewModel.Proponente.InscricaoMunicipal,

                    Endereco = projetoViewModel.Proponente.Endereco,
                    Numero = projetoViewModel.Proponente.Numero,
                    Complemento = projetoViewModel.Proponente.Complemento,
                    Bairro = projetoViewModel.Proponente.Bairro,
                    Cidade = projetoViewModel.Proponente.Cidade,
                    Estado = projetoViewModel.Proponente.Estado,
                    CEP = projetoViewModel.Proponente.CEP,

                    Site = projetoViewModel.Proponente.Site,
                    Telefone1 = projetoViewModel.Proponente.Telefone1,
                    Telefone2 = projetoViewModel.Proponente.Telefone2,
                    Telefone3 = projetoViewModel.Proponente.Telefone3,

                    Banco = projetoViewModel.Proponente.Banco,
                    Agencia = projetoViewModel.Proponente.Agencia,
                    ContaCorrente = projetoViewModel.Proponente.ContaCorrente,
                    TipoConta = projetoViewModel.Proponente.TipoConta,

                    RepresentanteLegalId = projetoViewModel.Proponente.RepresentanteLegalId,

                    Historico = projetoViewModel.Proponente.Historico,
                    PrincipaisAcoes = projetoViewModel.Proponente.PrincipaisAcoes,
                    PublicoAlvo = projetoViewModel.Proponente.PublicoAlvo,
                    RegioesAtendimento = projetoViewModel.Proponente.RegioesAtendimento,
                    Infraestrutura = projetoViewModel.Proponente.Infraestrutura,
                    EquipeMultidisciplinar = projetoViewModel.Proponente.EquipeMultidisciplinar

                }; 

                var responsavelLegal = new ResponsavelLegal
                {
                    Nome = projetoViewModel.ResponsavelLegal.Nome,
                    CPF = projetoViewModel.ResponsavelLegal.CPF,
                    RG = projetoViewModel.ResponsavelLegal.RG,
                    Endereco = projetoViewModel.ResponsavelLegal.Endereco,
                    MandatoVigente = projetoViewModel.ResponsavelLegal.MandatoVigente,
                    OrgaoExpedidor = projetoViewModel.ResponsavelLegal.OrgaoExpedidor,
                    Telefone1 = projetoViewModel.ResponsavelLegal.Telefone1,
                    Telefone2 = projetoViewModel.ResponsavelLegal.Telefone2,
                    Telefone3 = projetoViewModel.ResponsavelLegal.Telefone3
                };

                var responsavelTecnico = new ResponsavelTecnico
                {
                    Nome = projetoViewModel.ResponsavelTecnico.Nome,
                    CPF = projetoViewModel.ResponsavelTecnico.CPF,
                    RG = projetoViewModel.ResponsavelTecnico.RG,
                    Endereco = projetoViewModel.ResponsavelTecnico.Endereco,
                    MandatoVigente = projetoViewModel.ResponsavelTecnico.MandatoVigente,
                    OrgaoExpedidor = projetoViewModel.ResponsavelTecnico.OrgaoExpedidor,
                    Telefone1 = projetoViewModel.ResponsavelTecnico.Telefone1,
                    Telefone2 = projetoViewModel.ResponsavelTecnico.Telefone2,
                    Telefone3 = projetoViewModel.ResponsavelTecnico.Telefone3
                };

                // Salvar objetos no banco
                _context.Proponente.Add(proponente);
                await _context.SaveChangesAsync();
                
                _context.ResponsavelLegal.Add(responsavelLegal);
                await _context.SaveChangesAsync();
                
                _context.ResponsavelTecnico.Add(responsavelTecnico);
                await _context.SaveChangesAsync();

                
                var projeto = new Projeto
                {
                    Objeto = projetoViewModel.Projeto.Objeto,
                    ObjetivoGeral = projetoViewModel.Projeto.ObjetivoGeral  ,
                    ObjetivosEspecificos = projetoViewModel.Projeto.ObjetivosEspecificos,
                    PublicoBeneficiario = projetoViewModel.Projeto.PublicoBeneficiario  ,
                    Justificativa = projetoViewModel.Projeto.Justificativa  ,

                    ProponenteId = proponente.Id,  
                    ResponsavelLegalId = responsavelLegal.Id , 
					ResponsavelTecnicoId = responsavelTecnico.Id  
                                                  
                };

                // Salvar o Projeto no banco
                _context.Projeto.Add(projeto);
                await _context.SaveChangesAsync();

                if (projetoViewModel.EquipeExecucao != null)
                {
                    foreach (var membro in projetoViewModel.EquipeExecucao)
                    {

                        var _membro = new MembroEquipe
                        {
                            Nome = membro.Nome,
                            Formacao = membro.Formacao,
                            Funcao = membro.Funcao,
                            CargaHorariaSemanal = membro.CargaHorariaSemanal,
                            ProjetoId = projeto.Id 
                        };
                        _context.MembroEquipe.Add(_membro); // Adicionar cada membro individualmente
                        await _context.SaveChangesAsync();
                    }
                }

                if (projetoViewModel.Cronograma != null)
                {
                    foreach (var cronograma in projetoViewModel.Cronograma)
                    {

                        var _cronograma = new CronogramaMeta
                        {

                            Meta = cronograma.Meta,
                            ValorMeta = cronograma.ValorMeta,
                            ValorEtapa = cronograma.ValorEtapa,
                            Indicadores = cronograma.Indicadores,
                            Etapas = cronograma.Etapas,
                            Inicio = cronograma.Inicio,
                            Termino = cronograma.Termino,
                            ProjetoId = projeto.Id 
                        };
                        _context.CronogramaMeta.Add(_cronograma); // Adicionar cada membro individualmente
                        await _context.SaveChangesAsync();
                    }
                }

                if (projetoViewModel.PlanoAplicacao != null)
                {
                    foreach (var plano in projetoViewModel.PlanoAplicacao)
                    {
                        var _plano = new PlanoAplicacaoItem
                        {
                            Especificacao = plano.Especificacao,
                            Unidade = plano.Unidade,
                            Quantidade = plano.Quantidade,
                            ValorUnitario = plano.ValorUnitario,
                            ValorTotal = plano.ValorTotal,
                            ProjetoId = projeto.Id 
                        };
                        _context.PlanoAplicacaoItem.Add(_plano); // Adicionar cada membro individualmente
                        await _context.SaveChangesAsync();
                    }
                }

                if (anexo != null)
                {
                    Anexo imagem;
                    using (var memoryStream = new MemoryStream())
                    {
                        anexo.CopyTo(memoryStream);
                        var arquivo = memoryStream.ToArray(); // Convertendo para byte[]

                        var novoAnexo = new Anexo
                        {
                            Nome = anexo.FileName,
                            Tipo = anexo.ContentType,
                            ProjetoId = projeto.Id, 
                            Imagem = arquivo
                        };

                        imagem = novoAnexo;

                        // Salvar no BD
                        _context.Anexo.Add(novoAnexo);
                        _context.SaveChanges();
                    }
                }
                


                // Redirecionar para a Index após salvar o projeto
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            // Caso o ModelState não seja válido, voltar para a View Create
            return View("Create", projetoViewModel);
        }


        [HttpGet]
        public IActionResult ExibirImagem(int id)
        {
            var anexo = _context.Anexo.Find(id); // Buscando a imagem no banco
            if (anexo != null)
            {
                return File(anexo.Imagem, "image/png"); // Retornando a imagem
            }

            return NotFound();
        }





        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile anexo)
        {//APENAS VERIFICAÇÃO, A FOTO É SALVA QUANDO O PROJETO É CADASTRADO
            try
            {
                if (anexo != null && anexo.Length > 0)
                {
                    var fileExtension = Path.GetExtension(anexo.FileName); //Verifica extensão
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                    if (!allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = "Tipo de arquivo não permitido. Selecione uma imagem." });
                    }
                    
                    // Retorna o nome do arquivo como resposta de sucesso
                    return Json(new { success = true, message = "A foto foi anexada com sucesso!"});
                }
                else
                {
                    return Json(new { success = false, message = "Nenhum arquivo foi selecionado." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ocorreu um erro ao anexar o arquivo: {ex.Message}" });
            }
        }








        // GET: Projetos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        // POST: Projetos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Orcamento,Status,ObjetivoGeral,ObjetivosEspecificos,PublicoBeneficiario,Justificativa,InicioExecucao,TerminoExecucao,FotosProjeto")] Projeto projeto)
        {
            if (id != projeto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projeto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetoExists(projeto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        // GET: Projetos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projeto == null)
            {
                return Problem("Entity set 'AuthDbContext.Projeto'  is null.");
            }
            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto != null)
            {
                _context.Projeto.Remove(projeto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjetoExists(int id)
        {
          return (_context.Projeto?.Any(e => e.Id == id)).GetValueOrDefault();
        }

       


    }
}
