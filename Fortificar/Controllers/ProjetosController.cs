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
using System;
using Microsoft.Build.Evaluation;
using System.Reflection.Metadata;

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

            int ataEleicaoAprovada = 0;
            int estatutoAprovado = 0;
            int cnpjAprovado = 0;
            int cpfAprovado = 0;
            int rgAprovado = 0;
            int dadosBancariosAprovados = 0;

            if (projeto.SituacaoId == 4 || projeto.SituacaoId == 2)
            {

                var anexo = await _context.Anexo
            .FirstOrDefaultAsync();

                if (anexo != null)
                {
                    ataEleicaoAprovada = anexo.AprovadoAtaEleicao == "S" ? 1 : 0;
                    estatutoAprovado = anexo.AprovadoEstatuto == "S" ? 1 : 0;
                    cnpjAprovado = anexo.AprovadoCNPJ == "S" ? 1 : 0;
                    cpfAprovado = anexo.AprovadoCPFRespLegal == "S" ? 1 : 0;
                    rgAprovado = anexo.AprovadoRGRespLegal == "S" ? 1 : 0;
                    dadosBancariosAprovados = anexo.AprovadoDadosBancarios == "S" ? 1 : 0;
                }
            }

            // Preenche os valores no ViewData
            ViewData["AprovarAtaEleicao"] = ataEleicaoAprovada;
            ViewData["AprovarEstatuto"] = estatutoAprovado;
            ViewData["AprovarCNPJ"] = cnpjAprovado;
            ViewData["AprovarCPF"] = cpfAprovado;
            ViewData["AprovarRG"] = rgAprovado;
            ViewData["AprovarDadosBancarios"] = dadosBancariosAprovados;

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

            var pastaDoc = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                
                var documentos = new[] { "AtaEleicao", "Estatuto", "CNPJ", "CPFRespLegal", "RGRespLegal", "DadosBancarios" };
            if (Directory.Exists(pastaDoc))
            {

                foreach (var documento in documentos)
                {

                    var padraoBusca = $"{projeto.Id}_{documento}*.pdf";

                    
                    var arquivosEncontrados = Directory.GetFiles(pastaDoc, padraoBusca);
                    if (arquivosEncontrados.Any())
                    {
                        var caminhoArquivo = arquivosEncontrados.First();
                        var nomeArquivo = Path.GetFileName(caminhoArquivo);

                        
                        ViewData[$"{documento}-env"] = $"/uploads/{nomeArquivo}";
                    }
                    else
                    {
                        // Se o arquivo não existir, armazena null
                        ViewData[$"{documento}-env"] = null;
                    }

                    
                    var aprovacaoStatus = "";

                    switch (documento)
                    {
                        case "AtaEleicao":
                            aprovacaoStatus = projeto.Anexo.AprovadoAtaEleicao;
                            break;
                        case "Estatuto":
                            aprovacaoStatus = projeto.Anexo.AprovadoEstatuto;
                            break;
                        case "CNPJ":
                            aprovacaoStatus = projeto.Anexo.AprovadoCNPJ;
                            break;
                        case "CPFRespLegal":
                            aprovacaoStatus = projeto.Anexo.AprovadoCPFRespLegal;
                            break;
                        case "RGRespLegal":
                            aprovacaoStatus = projeto.Anexo.AprovadoRGRespLegal;
                            break;
                        case "DadosBancarios":
                            aprovacaoStatus = projeto.Anexo.AprovadoDadosBancarios;
                            break;
                        default:
                            aprovacaoStatus = null;
                            break;
                    }

                    ViewData[$"{documento}-Aprovado"] = aprovacaoStatus;
                }
            }
            else
            {
                
                foreach (var documento in documentos)
                {
                    ViewData[$"{documento}-env"] = null;
                    ViewData[$"{documento}-Aprovado"] = null;
                }
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
                    PlanoAplicacao = new List<PlanoAplicacaoItem> { new PlanoAplicacaoItem() },
                    EquipeEncarregada = new List<EquipeExecucaoProjeto> { new EquipeExecucaoProjeto() }
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


                var Anexo = new Anexo
                {
                    AprovadoAtaEleicao = "N",
                    AprovadoEstatuto = "N",
                    AprovadoCNPJ = "N",
                    AprovadoCPFRespLegal = "N",
                    AprovadoRGRespLegal = "N",
                    AprovadoDadosBancarios = "N"

                };

                _context.Anexo.Add(Anexo);
                await _context.SaveChangesAsync();

                if (anexo != null)
                {
                    using (var memoryStream = new MemoryStream())
                        {
                            anexo.CopyTo(memoryStream);
                            var arquivo = memoryStream.ToArray();

                            Anexo = new Anexo
                            {
                                Nome = anexo.FileName,
                                Tipo = anexo.ContentType,
                                Imagem = arquivo
                            };

                            _context.Anexo.Update(Anexo);
                            await _context.SaveChangesAsync();
                        }
                }


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
                    SituacaoId = 1,

                    ProponenteId = proponente.Id,
                    ResponsavelLegalId = responsavelLegal.Id,
                    ResponsavelTecnicoId = responsavelTecnico.Id,
                    AnexoId = Anexo.Id
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

                // Salvando a equipe encarregada
                if (viewmodelProjeto.Projeto.EquipeEncarregada != null)
                {
                    foreach (var equipe in viewmodelProjeto.Projeto.EquipeEncarregada)
                    {
                        var _equipe = new EquipeExecucaoProjeto
                        {
                            Especificacao = equipe.Especificacao,
                            Unidade = equipe.Unidade,
                            Quantidade = equipe.Quantidade,
                            ValorUnitario = equipe.ValorUnitario,
                            ValorTotal = equipe.ValorTotal,
                            ProjetoId = projeto.Id
                        };
                        _context.EquipeExecucaoProjeto.Add(_equipe);
                    }
                    await _context.SaveChangesAsync();
                }

                var ODSselecionadas = Request.Form["ODSSelecionadas"].ToString(); 

                if (!string.IsNullOrEmpty(ODSselecionadas))
                {
                    var odsIds = ODSselecionadas.Split(',').Select(int.Parse).ToList();

                    
                    foreach (var odsId in odsIds)
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

                // PÚBLICO-ALVO
                var PAselecionados = Request.Form["PASelecionados"].ToString(); 

                if (!string.IsNullOrEmpty(PAselecionados))
                {
                    var paIds = PAselecionados.Split(',').Select(int.Parse).ToList();

                    
                    foreach (var paId in paIds)
                    {
                        var projetoPublicoBeneficiario = new ProjetoPublicoBeneficiario
                        {
                            ProjetoId = projeto.Id,
                            PublicoBeneficiarioId = paId
                        };

                        _context.ProjetoPublicoBeneficiario.Add(projetoPublicoBeneficiario);
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




                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View("Create", viewmodelProjeto);
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocumento(IFormFile arquivo, string tipoDocumento, int projetoId)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return Json(new { sucesso = false, mensagem = "Por favor, selecione um arquivo válido." });
            }

            // Caminho base da pasta 'uploads'
            var pastaUploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            // Cria a pasta 'uploads' caso ela não exista
            if (!Directory.Exists(pastaUploads))
            {
                Directory.CreateDirectory(pastaUploads);
            }

            // Gera um nome para o arquivo com base no ID do projeto e no tipo de documento
            var nomeArqUp = $"{projetoId}_{tipoDocumento}{Path.GetExtension(arquivo.FileName)}";
            var caminhoArqUp = Path.Combine(pastaUploads, nomeArqUp);


            // Salva o arquivo no diretório
            using (var stream = new FileStream(caminhoArqUp, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Monta o caminho relativo (que será salvo no banco)
            var caminhoRelativo = $"/uploads/{nomeArqUp}";

            // Atualiza o banco de dados na tabela `Anexo`
            var anexo = _context.Anexo.FirstOrDefault();
            

            // Atualiza o campo correspondente ao tipo de documento
            switch (tipoDocumento)
            {
                case "AtaEleicao":
                    anexo.AprovadoAtaEleicao = "E";
                    anexo.AtaEleicao = caminhoRelativo;
                    break;
                case "Estatuto":
                    anexo.Estatuto = caminhoRelativo;
                    anexo.AprovadoEstatuto = "E";
                    break;
                case "CNPJ":
                    anexo.CNPJ = caminhoRelativo;
                    anexo.AprovadoCNPJ = "E";
                    break;
                case "CPFRespLegal":
                    anexo.CPFRespLegal = caminhoRelativo;
                    anexo.AprovadoCPFRespLegal = "E";
                    break;
                case "RGRespLegal":
                    anexo.RGRespLegal = caminhoRelativo;
                    anexo.AprovadoRGRespLegal = "E";
                    break;
                case "DadosBancarios":
                    anexo.DadosBancarios = caminhoRelativo;
                    anexo.AprovadoDadosBancarios = "E";
                    break;
                default:
                    return Json(new { sucesso = false, mensagem = "Tipo de documento inválido." });
            }
            _context.Anexo.Update(anexo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar no banco: {ex.Message}");
                return Json(new { sucesso = false, mensagem = "Erro ao salvar no banco de dados." });
            }


            return Json(new { sucesso = true, mensagem = "Upload realizado com sucesso.", caminho = caminhoRelativo });
        }




        [HttpPost]
        public async Task<IActionResult> AprovarDocumento(int idProjeto, string tipoDocumento)
        {
            var projeto = _context.Projeto.Include(p => p.Anexo).FirstOrDefault(p => p.Id == idProjeto);

            if (projeto != null && projeto.Anexo != null)
            {
                switch (tipoDocumento)
                {
                    case "AtaEleicao":
                        projeto.Anexo.AprovadoAtaEleicao = "S";
                        ViewData["AprovarAtaEleicao"] = 1;
                        ViewData["StatusAtaEleicao"] = "Documento: Ata de eleição já aprovado!";
                        break;
                    case "Estatuto":
                        projeto.Anexo.AprovadoEstatuto = "S";
                        ViewData["AprovarEstatuto"] = 1;
                        ViewData["StatusEstatuto"] = "Documento: Estatuto ou Contrato Social já aprovado!";
                        break;
                    case "CNPJ":
                        projeto.Anexo.AprovadoCNPJ = "S";
                        ViewData["AprovarCNPJ"] = 1;
                        ViewData["StatusCNPJ"] = "Documento: Cartão do CNPJ já aprovado!";
                        break;
                    case "CPF":
                        projeto.Anexo.AprovadoCPFRespLegal = "S";
                        ViewData["AprovarCPF"] = 1;
                        ViewData["StatusCPF"] = "Documento: Cópia do CPF do Responsável Legal já aprovado!";
                        break;
                    case "RG":
                        projeto.Anexo.AprovadoRGRespLegal = "S";
                        ViewData["AprovarRG"] = 1;
                        ViewData["StatusRG"] = "Documento: Cópia do RG do Responsável Legal já aprovado!";
                        break;
                    case "DadosBancarios":
                        projeto.Anexo.AprovadoDadosBancarios = "S";
                        ViewData["AprovarDadosBancarios"] = 1;
                        ViewData["StatusDadosBancarios"] = "Documento: Dados bancários já aprovado!";
                        break;
                    default:
                        return BadRequest("Tipo de documento inválido.");
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detalhes", new { id = idProjeto });
        }



        [HttpPost]
        public async Task<IActionResult> RecusarDoc(int idProjeto, string tipoDocumento)
        {
            var projeto = _context.Projeto.Include(p => p.Anexo).FirstOrDefault(p => p.Id == idProjeto);

            if (projeto != null && projeto.Anexo != null)
            {
                switch (tipoDocumento)
                {
                    case "AtaEleicao":
                        projeto.Anexo.AprovadoAtaEleicao = "N";
                        ViewData["AprovarAtaEleicao"] = 0;
                        ViewData["StatusAtaEleicao"] = "";
                        break;
                    case "Estatuto":
                        projeto.Anexo.AprovadoEstatuto = "N";
                        ViewData["AprovarEstatuto"] = 0;
                        ViewData["StatusEstatuto"] = "";
                        break;
                    case "CNPJ":
                        projeto.Anexo.AprovadoCNPJ = "N";
                        ViewData["AprovarCNPJ"] = 0;
                        ViewData["StatusCNPJ"] = "";
                        break;
                    case "CPF":
                        projeto.Anexo.AprovadoCPFRespLegal = "N";
                        ViewData["AprovarCPF"] = 0;
                        ViewData["StatusCPF"] = "";
                        break;
                    case "RG":
                        projeto.Anexo.AprovadoRGRespLegal = "N";
                        ViewData["AprovarRG"] = 0;
                        ViewData["StatusRG"] = "";
                        break;
                    case "DadosBancarios":
                        projeto.Anexo.AprovadoDadosBancarios = "N";
                        ViewData["AprovarDadosBancarios"] = 0;
                        ViewData["StatusDadosBancarios"] = "";
                        break;
                    default:
                        return BadRequest("Tipo de documento inválido.");
                }
                
                var caminhoUpload = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                
                string nomePadrao = $"{idProjeto}_{tipoDocumento}.pdf"; // Adicione a extensão, se aplicável
                string caminhoArqRecusado = Path.Combine(caminhoUpload, nomePadrao);


                if (!string.IsNullOrEmpty(caminhoArqRecusado) && System.IO.File.Exists(caminhoArqRecusado))
                {
                    try
                    {
                        System.IO.File.Delete(caminhoArqRecusado);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao deletar arquivo: {ex.Message}");
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detalhes", new { id = idProjeto });
        }

        [HttpPost]
        public async Task<IActionResult> ConcluirProjeto(int idProjeto)
        {
            var projeto = _context.Projeto.FirstOrDefault(p => p.Id == idProjeto);

            if (projeto != null)
            {
                projeto.SituacaoId = 6;
            }
            await _context.SaveChangesAsync();
            

            return RedirectToAction("Detalhes", new { id = idProjeto });
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
