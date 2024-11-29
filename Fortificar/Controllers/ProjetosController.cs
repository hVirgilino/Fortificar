using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fortificar.Data;
using Fortificar.Models;
using Fortificar.Models.ViewModels;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using ODS = Fortificar.Models.ODS;
using Microsoft.AspNetCore.Identity;
using Fortificar.Areas.Identity.Data;

namespace Fortificar.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<FortificarUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjetosController(AuthDbContext context, UserManager<FortificarUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;

            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Projetos
        public async Task<IActionResult> Index(List<Projeto> projetos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userName = _userManager.GetUserName(this.User);
            ViewData["UserNome"] = userName;

            var user = await _userManager.GetUserAsync(this.User);
            //var projetos = _context.Projeto.ToList();

            if (user.Tipo == 1)
            {
                projetos = _context.Projeto
                    .Include(p => p.Proponente)
                    .Include(p => p.Situacao)
                    .Where(p => p.ProponenteId == user.ProponenteId)
                    .ToList();
            }
            else if(user.Tipo == 0)
            {

                projetos = _context.Projeto
                    .Include(p => p.Proponente)
                    .Include(p => p.Situacao)
                    .Where(p => p.SituacaoId == 2 || p.SituacaoId == 3 || p.SituacaoId == 4)
                    .ToList();    
                


            }

                ViewData["Tipo"] = user.Tipo;
                return View("Index", projetos);
        }



        // GET: Projetos/Details/5
        public async Task<IActionResult> Detalhes(int id)
        {

            var user = await _userManager.GetUserAsync(this.User);

            var projeto = await _context.Projeto
                .Include(p => p.Proponente)
                .Include(p => p.ResponsavelLegal)
                .Include(p => p.ResponsavelTecnico)
                .Include(p => p.Situacao)
                .Include(p => p.Anexo)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (projeto == null)
            {
                return NotFound();
            }

            bool ataEleicaoPendente = true;
            bool estatutoPendente = false;
            bool cnpjPendente = false;
            bool cpfPendente = false;
            bool rgPendente = false;
            bool dadosBancariosPendente = false;

            if (projeto.SituacaoId == 4)
            {
                // Busca o anexo relacionado ao ProponenteId
                var anexo = await _context.Anexo
                    .FirstOrDefaultAsync(a => a.ProjetoId == projeto.Id);

                // Verificação de pendências de documentos
                ataEleicaoPendente = anexo?.AtaEleicao == null;
                estatutoPendente = anexo?.Estatuto == null;
                cnpjPendente = anexo?.CNPJ == null;
                cpfPendente = anexo?.CPFRespLegal == null;
                rgPendente = anexo?.RGRespLegal == null;
                dadosBancariosPendente = anexo?.DadosBancarios == null;
            }


            // Cria um objeto com o status dos documentos pendentes
            var anexosPendentes = new
            {
                AtaEleicaoPendente = ataEleicaoPendente,
                EstatutoPendente = estatutoPendente,
                CNPJPendente = cnpjPendente,
                CPFPendente = cpfPendente,
                RGPendente = rgPendente,
                DadosBancariosPendente = dadosBancariosPendente
            };

            ViewData["AnexosPendentes"] = anexosPendentes;

            
            ViewData["_Situacao"] = projeto.SituacaoId;

            ViewData["Tipo"] = user.Tipo;

            /*
            var odsDisponiveis = await (from po in _context.ProjetoODS
                                        join ods in _context.ODS on po.ODSId equals ods.Id
                                        where po.ProjetoId == id
                                        select ods)
                                        .ToListAsync();

            var pbDisponiveis = await (from ppb in _context.ProjetoPublicoBeneficiario
                                       join pb in _context.PublicoBeneficiario on ppb.PublicoBeneficiarioId equals pb.Id
                                       where ppb.ProjetoId == id
                                       select pb)
                                        .ToListAsync();
            */
            var detalhesViewModel = new ProjetoViewModel
            {
                Projeto = projeto,
                //ODS = odsDisponiveis,
                //PublicoBeneficiario = pbDisponiveis
            };

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

            return View(detalhesViewModel);
        }

        

		public async Task<IActionResult> Create(ProjetoViewModel viewmodelCreate)
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

            var ods = _context.ODS.OrderBy(a => a.Id);
            var pb = _context.PublicoBeneficiario.OrderBy(a => a.Id);

            // Preenche o ViewModel com os dados do proponente
            viewmodelCreate = new ProjetoViewModel
            {

                ODS = ods.Select(ods => new ODS
                {
                    Id = ods.Id,
                    Nome = ods.Nome
                }).ToList(),

                PublicoBeneficiario = pb.Select(pb => new PublicoBeneficiario
                {
                    Id = pb.Id,
                    Nome = pb.Nome
                }).ToList(),

                Projetos = await _context.Projeto
                                 .Include(p => p.Proponente)
                                    .ThenInclude(r => r.ResponsavelLegal)
                                 .ToListAsync(),

                Projeto = new Projeto
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
                    CronogramaMeta = new List<CronogramaMeta> { new CronogramaMeta() },
                    PlanoAplicacao = new List<PlanoAplicacaoItem> { new PlanoAplicacaoItem() }
                }

                
            };

            // Retorna a view preenchida com o ViewModel
            return View("Create", viewmodelCreate);
        }

 public async Task<IActionResult> SalvarProjeto(ProjetoViewModel viewmodelProjeto, IFormFile? anexo)
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
                    Nome = viewmodelProjeto.Projeto.ResponsavelTecnico.Nome,
                    CPF = viewmodelProjeto.Projeto.ResponsavelTecnico.CPF,
                    RG = viewmodelProjeto.Projeto.ResponsavelTecnico.RG,
                    Endereco = viewmodelProjeto.Projeto.ResponsavelTecnico.Endereco,
                    MandatoVigente = viewmodelProjeto.Projeto.ResponsavelTecnico.MandatoVigente,
                    OrgaoExpedidor = viewmodelProjeto.Projeto.ResponsavelTecnico.OrgaoExpedidor,
                    Telefone1 = viewmodelProjeto.Projeto.ResponsavelTecnico.Telefone1,
                    Telefone2 = viewmodelProjeto.Projeto.ResponsavelTecnico.Telefone2,
                    Telefone3 = viewmodelProjeto.Projeto.ResponsavelTecnico.Telefone3
                };

                _context.ResponsavelTecnico.Add(responsavelTecnico);
                await _context.SaveChangesAsync();

                var projeto = new Projeto
                {
                    Objeto = viewmodelProjeto.Projeto.Objeto,
                    ObjetivoGeral = viewmodelProjeto.Projeto.ObjetivoGeral,
                    ObjetivosEspecificos = viewmodelProjeto.Projeto.ObjetivosEspecificos,
                    PublicoBeneficiario = viewmodelProjeto.Projeto.PublicoBeneficiario,
                    Justificativa = viewmodelProjeto.Projeto.Justificativa,
                    Cronograma = viewmodelProjeto.Projeto.Cronograma,
                    ValorMeta = viewmodelProjeto.Projeto.ValorMeta,
                    Indicadores = viewmodelProjeto.Projeto.Indicadores,
                    Orcamento = viewmodelProjeto.Projeto.Orcamento,

                    ProponenteId = proponente.Id,
                    ResponsavelLegalId = responsavelLegal.Id,
                    ResponsavelTecnicoId = responsavelTecnico.Id
                };

                _context.Projeto.Add(projeto);
                await _context.SaveChangesAsync();

                // Salvando a equipe de execução
                if (viewmodelProjeto.Projeto.EquipeExecucao != null)
                {
                    foreach (var membro in viewmodelProjeto.Projeto.EquipeExecucao)
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
                if (viewmodelProjeto.Projeto.CronogramaMeta != null)
                {
                    foreach (var cronograma in viewmodelProjeto.Projeto.CronogramaMeta)
                    {
                        var _cronograma = new CronogramaMeta
                        {
                            ValorEtapa = cronograma.ValorEtapa,
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
                if (viewmodelProjeto.Projeto.PlanoAplicacao != null)
                {
                    foreach (var plano in viewmodelProjeto.Projeto.PlanoAplicacao)
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
                if (viewmodelProjeto.ODS != null)
                {
                    foreach (var odsId in viewmodelProjeto.ODS.Where(ods => ods.IsSelected).Select(ods => ods.Id))
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

                // Salvando os PublicoBeneficiario selecionados
                if (viewmodelProjeto.PublicoBeneficiario != null)
                {
                    foreach (var publicoBeneficiario in viewmodelProjeto.PublicoBeneficiario.Where(pb => pb.IsSelected))
                    {
                        var projetoPublicoBeneficiario = new ProjetoPublicoBeneficiario
                        {
                            ProjetoId = projeto.Id,
                            PublicoBeneficiarioId = publicoBeneficiario.Id
                        };

                        _context.ProjetoPublicoBeneficiario.Add(projetoPublicoBeneficiario);
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

            return View("Create", viewmodelProjeto);
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
        public async Task<IActionResult> Aprovar(int? id)
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


        

        


        [HttpPost]
        public async Task<IActionResult> EnviarProjeto(int id)
        {
            var projeto = _context.Projeto
                    .FirstOrDefault(p => p.Id == id);
            var _situacao = 2;

            //Aqui os parâmetros vão analisar 
            //Se ok = 2 (Enviado), senão = 7 (Em análise)

            var parametroOrcamento = _context.Parametro
                    .FirstOrDefault(p => p.Nome == "Orçamento");

            if (parametroOrcamento.Ativo == "S")
            {
                bool abaixoMin = parametroOrcamento.ValorMin is not null && projeto.Orcamento < parametroOrcamento.ValorMin;
                bool acimaMax = parametroOrcamento.ValorMax is not null && projeto.Orcamento > parametroOrcamento.ValorMax;

                if (abaixoMin || acimaMax)
                {
                    _situacao = 7;
                }

            }

            ViewData["Situacao"] = _situacao;

            await MudarSituacaoProjeto(id, _situacao);


            return RedirectToAction("Detalhes");
        }

        [HttpGet]
        public async Task<IActionResult> Aprovar(int id)
        {
            var projeto = _context.Projeto
                    .Include(p => p.Proponente)
                    .FirstOrDefault(p => p.Id == id);
            //var _situacao = 3; // Em andamento


            //ViewData["Situacao"] = _situacao;

            //await MudarSituacaoProjeto(id, _situacao);


            return View("Aprovar", projeto);
        }
        [HttpPost]
        public async Task<IActionResult> MudarSituacaoProjeto(int id, int _situacao)
        {
            var projeto = _context.Projeto
                    .Include(p => p.Proponente)
                    .FirstOrDefault(p => p.Id == id);            

            projeto.SituacaoId = _situacao;

            _context.Projeto.Add(projeto);
            await _context.SaveChangesAsync();

            ViewData["Situacao"] = _situacao;

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Recusados()
        {
           

            var projetosRecusados = _context.Projeto
                    .Include(p => p.Proponente)
                    .Where(p => p.SituacaoId == 7)
                    .ToList();



            return View("Recusados", projetosRecusados);
        }


    }
}
