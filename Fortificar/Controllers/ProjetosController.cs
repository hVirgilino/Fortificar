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
using System.ComponentModel.DataAnnotations;
using ODS = Fortificar.Models.ODS;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Busca o ProponenteId baseado no usuário logado
            var proponenteId = _context.FortificarUser
                .Where(fu => fu.Id == userId)
                .Select(fu => fu.ProponenteId)
                .FirstOrDefault();

            // Caso não encontre o ProponenteId, redireciona para uma página de erro
            if (proponenteId == null)
            {
                return RedirectToAction("Error");
            }

            // Busca o proponente no banco de dados
            var proponente = _context.Proponente
                .FirstOrDefault(p => p.Id == proponenteId);

            // Caso não encontre o proponente, redireciona para uma página de erro
            if (proponente == null)
            {
                return RedirectToAction("Error");
            }



            // Busca o Responsável Legal, se houver
            var responsavelLegal = _context.ResponsavelLegal
                .FirstOrDefault(rl => rl.Id == proponente.ResponsavelLegalId);

            // Preenche o ViewModel com os dados do proponente e responsável legal (se existir)
            viewModel = new ProjetoViewModel
            {
                Projetos = await _context.Projeto
                                 .Include(p => p.Proponente) 
                                 .ToListAsync(),

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
                    Site = proponente.Site,

                    Historico = proponente.Historico,
                    PrincipaisAcoes = proponente.PrincipaisAcoes,
                    PublicoAlvo = proponente.PublicoAlvo,
                    RegioesAtendimento = proponente.RegioesAtendimento,
                    Infraestrutura = proponente.Infraestrutura,
                    EquipeMultidisciplinar = proponente.EquipeMultidisciplinar

                },
                ResponsavelLegal = responsavelLegal != null ? new ResponsavelLegal
                {
                    Nome = responsavelLegal.Nome,
                    CPF = responsavelLegal.CPF,
                    RG = responsavelLegal.RG,
                    OrgaoExpedidor = responsavelLegal.OrgaoExpedidor,
                    CargoOSC = responsavelLegal.CargoOSC,
                    MandatoVigente = responsavelLegal.MandatoVigente,
                    Endereco = responsavelLegal.Endereco,
                    Telefone1 = responsavelLegal.Telefone1,
                    Telefone2 = responsavelLegal.Telefone2,
                    Telefone3 = responsavelLegal.Telefone3
                } : new ResponsavelLegal(),

                EquipeExecucao = new List<MembroEquipe> { new MembroEquipe() },
                Cronograma = new List<CronogramaMeta> { new CronogramaMeta() },
                PlanoAplicacao = new List<PlanoAplicacaoItem> { new PlanoAplicacaoItem() }
            };

            // Retorna a view preenchida com o ViewModel
            return View("Create", viewModel);
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
        public IActionResult DadosGeraisNav()
        {
            return View();
        }
        public IActionResult ResponsavelLegalNav()
        {
            return View();
        }
        public IActionResult ResponsavelTecnicoNav()
        {
            return View();
        }
        public IActionResult DadosProjetoNav()
        {
            return View();
        }
        public IActionResult PlanoAplicacaoNav()
        {
            return View();
        }
        public IActionResult ProponenteNav()
        {
            return View();
        }
        public IActionResult FotosNav()
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

            Console.WriteLine("entrei create");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Busca o ProponenteId baseado no usuário logado
            var proponenteId = _context.FortificarUser
                .Where(fu => fu.Id == userId)
                .Select(fu => fu.ProponenteId)
                .FirstOrDefault();

            // Busca o proponente no banco de dados
            var proponente = _context.Proponente
                .FirstOrDefault(p => p.Id == proponenteId);

            // Caso não encontre o proponente, redireciona para uma página de erro
            if (proponente == null)
            {
                return RedirectToAction("Error");
            }

            // Preenche o ViewModel com os dados do proponente
            var viewModel2 = new ProjetoViewModel
            {
                ODS = new List<ODS>
            {
                new ODS { Id = 1, Nome = "Erradicação da pobreza", IsSelected = false },
                new ODS { Id = 2, Nome = "Fome zero e agricultura sustentável", IsSelected = false },
                new ODS { Id = 3, Nome = "Saúde e Bem-Estar", IsSelected = false },
                new ODS { Id = 4, Nome = "Educação de qualidade", IsSelected = false },
                new ODS { Id = 5, Nome = "Igualdade de gênero", IsSelected = false },
                new ODS { Id = 6, Nome = "Água potável e saneamento", IsSelected = false },
                new ODS { Id = 7, Nome = "Energia limpa e acessível", IsSelected = false },
                new ODS { Id = 8, Nome = "Trabalho decente e crescimento econômico", IsSelected = false },
                new ODS { Id = 9, Nome = "Indústria, inovação e infraestrutura", IsSelected = false },
                new ODS { Id = 10, Nome = "Redução das desigualdades", IsSelected = false },
                new ODS { Id = 11, Nome = "Cidades e comunidades sustentáveis", IsSelected = false },
                new ODS { Id = 12, Nome = "Consumo e produção responsáveis", IsSelected = false },
                new ODS { Id = 13, Nome = "Ação contra a mudança global do clima", IsSelected = false },
                new ODS { Id = 14, Nome = "Vida na água", IsSelected = false },
                new ODS { Id = 15, Nome = "Vida terrestre", IsSelected = false },
                new ODS { Id = 16, Nome = "Paz, Justiça e Instituições Eficazes", IsSelected = false },
                new ODS { Id = 17, Nome = "Parcerias e meios de implementação", IsSelected = false }
            },


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

            // Retorna a view preenchida com o ViewModel
            return View(viewModel2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarProjeto(ProjetoViewModel projetoViewModel, IFormFile? anexo)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Busca o ProponenteId baseado no usuário logado
                var proponenteId = _context.FortificarUser
                    .Where(fu => fu.Id == userId)
                    .Select(fu => fu.ProponenteId)
                    .FirstOrDefault();

                var proponente = _context.Proponente
                    .FirstOrDefault(p => p.Id == proponenteId);

                var responsavelLegal = _context.ResponsavelLegal
                    .FirstOrDefault(rl => rl.Id == proponente.ResponsavelLegalId);

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

                _context.ResponsavelTecnico.Add(responsavelTecnico);
                await _context.SaveChangesAsync();

                var projeto = new Projeto
                {
                    Objeto = projetoViewModel.Projeto.Objeto,
                    ObjetivoGeral = projetoViewModel.Projeto.ObjetivoGeral,
                    ObjetivosEspecificos = projetoViewModel.Projeto.ObjetivosEspecificos,
                    PublicoBeneficiario = projetoViewModel.Projeto.PublicoBeneficiario,
                    Justificativa = projetoViewModel.Projeto.Justificativa,
                    ProponenteId = proponente.Id,
                    ResponsavelLegalId = responsavelLegal.Id,
                    ResponsavelTecnicoId = responsavelTecnico.Id
                };

                _context.Projeto.Add(projeto);
                await _context.SaveChangesAsync();

                // Salvando a equipe de execução
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
                        _context.MembroEquipe.Add(_membro);
                    }
                    await _context.SaveChangesAsync();
                }

                // Salvando o cronograma
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
                        _context.CronogramaMeta.Add(_cronograma);
                    }
                    await _context.SaveChangesAsync();
                }

                // Salvando o plano de aplicação
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
                        _context.PlanoAplicacaoItem.Add(_plano);
                    }
                    await _context.SaveChangesAsync();
                }

                

                // Salvando os ODS selecionados
                if (projetoViewModel.ODS != null)
                {
                    foreach (var odsId in projetoViewModel.ODS.Where(ods => ods.IsSelected).Select(ods => ods.Id))
                    {
                        var projetoODS = new ProjetoODS
                        {
                            ProjetoId = projeto.Id,
                            ODSId = odsId
                        };

                        _context.ProjetoODS.Add(projetoODS);
                    }

                    await _context.SaveChangesAsync();
                }


                // Salvando o anexo, se existir
                if (anexo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        anexo.CopyTo(memoryStream);
                        var arquivo = memoryStream.ToArray();

                        var novoAnexo = new Anexo
                        {
                            Nome = anexo.FileName,
                            Tipo = anexo.ContentType,
                            ProjetoId = projeto.Id,
                            Imagem = arquivo
                        };

                        _context.Anexo.Add(novoAnexo);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

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


        

        [HttpGet]
        public IActionResult Parametros()
        {
            // Busca todos os parâmetros no banco de dados
            var parametros = _context.Parametro.ToList();

            // Se a lista for nula, inicializa uma lista vazia para evitar NullReferenceException
            if (parametros == null)
            {
                parametros = new List<Parametro>();
            }

            return View(parametros);
        }



        [HttpPost]
        public async Task<IActionResult> ConfigurarParametros(List<Parametro> parametros)
        {
            foreach (var parametro in parametros)
            {
                // Ignorar parâmetros vazios (sem nome, descrição e valor)
                if (string.IsNullOrEmpty(parametro.Nome) && string.IsNullOrEmpty(parametro.Descricao) && string.IsNullOrEmpty(parametro.ValorMin) && string.IsNullOrEmpty(parametro.ValorMax) && string.IsNullOrEmpty(parametro.Ativo))
                {
                    continue;
                }

                if (parametro.Id == 0)
                {
                    // Adicionar novo parâmetro
                    _context.Parametro.Add(parametro);
                }
                else
                {
                    // Atualizar parâmetro existente
                    var parametroExistente = await _context.Parametro.FindAsync(parametro.Id);
                    if (parametroExistente != null)
                    {
                        // Verificar se houve alterações antes de atualizar
                        if (parametroExistente.Nome != parametro.Nome ||
                            parametroExistente.Descricao != parametro.Descricao ||
                            parametroExistente.ValorMin != parametro.ValorMin ||
                            parametroExistente.ValorMax != parametro.ValorMax)
                        {
                            parametroExistente.Nome = parametro.Nome;
                            parametroExistente.Descricao = parametro.Descricao;
                            parametroExistente.ValorMin = parametro.ValorMin;
                            parametroExistente.ValorMax = parametro.ValorMax;
                        }
                    }
                }
            }

            // Salvar alterações para garantir que IDs sejam atribuídos aos novos parâmetros
            await _context.SaveChangesAsync();

            // Obter todos os IDs dos parâmetros existentes no banco
            var todosIdsExistentes = _context.Parametro.Select(p => p.Id).ToList();

            // Deletar parâmetros que não estão na lista recebida (considerando que foram removidos)
            var idsRecebidos = parametros.Where(p => p.Id != 0).Select(p => p.Id).ToList();
            var parametrosParaDeletar = _context.Parametro
                .Where(p => !idsRecebidos.Contains(p.Id) && todosIdsExistentes.Contains(p.Id))
                .ToList();

            _context.Parametro.RemoveRange(parametrosParaDeletar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Parametros");
        }



    }
}
