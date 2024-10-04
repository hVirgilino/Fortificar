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
        public async Task<IActionResult> Index()
        {
              return _context.Projeto != null ? 
                          View(await _context.Projeto.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.Projeto'  is null.");
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
			 // Associa o Proponente ao Projeto
				viewModel.Projeto.ProponenteId = viewModel.Proponente.Id;
				viewModel.Projeto.ResponsavelLegalId = viewModel.ResponsavelLegal.Id;
				viewModel.Projeto.ResponsavelTecnicoId = viewModel.ResponsavelTecnico.Id;

				_context.Add(viewModel.Projeto);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(viewModel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarProjeto(ProjetoViewModel projetoViewModel)
        {
            if (ModelState.IsValid)
            {//MIGRATION
                // Criar novos objetos com os dados do ViewModel
                var proponente = new Proponente
                {
                    Nome = projetoViewModel.Proponente.Nome,
                    CNPJ = projetoViewModel.Proponente.CNPJ,
                    Logradouro = projetoViewModel.Proponente.Logradouro,
                    Bairro = projetoViewModel.Proponente.Bairro,
                    Cidade = projetoViewModel.Proponente.Cidade,
                    CEP = projetoViewModel.Proponente.CEP,
                    Email = projetoViewModel.Proponente.Email,
                    SiteDivulgacao = projetoViewModel.Proponente.SiteDivulgacao,
                    Telefone1 = projetoViewModel.Proponente.Telefone1,
                    Telefone2 = projetoViewModel.Proponente.Telefone2,
                    Telefone3 = projetoViewModel.Proponente.Telefone3,
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

                // Agora que o Proponente foi salvo, podemos associá-lo ao Projeto
                var projeto = new Projeto
                {
                    Objeto = projetoViewModel.Projeto.Objeto,
                    ObjetivoGeral = projetoViewModel.Projeto.ObjetivoGeral  ,
                    ObjetivosEspecificos = projetoViewModel.Projeto.ObjetivosEspecificos,
                    PublicoBeneficiario = projetoViewModel.Projeto.PublicoBeneficiario  ,
                    Justificativa = projetoViewModel.Projeto.Justificativa  ,
                    ProponenteId = proponente.Id,  // Associando o ID do Proponente ao Projeto
                    ResponsavelLegalId = responsavelLegal.Id , // Associando o ID do ResponsavelLegal ao Projeto

					ResponsavelTecnicoId = responsavelTecnico.Id  // Associando o ID do ResponsavelTecnico ao Projeto
                                                  
                };

                // Salvar o Projeto no banco
                _context.Projeto.Add(projeto);
                await _context.SaveChangesAsync();

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








        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile anexo)
        {
            try
            {
                if (anexo != null && anexo.Length > 0)
                {
                    // Validação da extensão do arquivo
                    var fileExtension = Path.GetExtension(anexo.FileName);
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

                    if (!allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = "Tipo de arquivo não permitido. Selecione uma imagem ou PDF." });
                    }

                    // Gera um nome aleatório usando GUID e mantém a extensão original do arquivo
                    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Define o caminho completo do arquivo e o diretório
                    string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // Verifica se o diretório existe, se não, cria o diretório
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    // Define o caminho final do arquivo
                    string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                    // Salva o arquivo no caminho especificado
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await anexo.CopyToAsync(stream);
                    }

                    // Retorna o nome do arquivo como resposta de sucesso
                    return Json(new { success = true, message = uniqueFileName + " foi anexado com sucesso!" });
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

        public IActionResult VisualizarAnexo(string caminho)
        {
            var caminhoArq = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", caminho);

            if (!System.IO.File.Exists(caminhoArq))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(caminhoArq);
            var contentType = "application/octet-stream"; // tipo genérico

            // Verifica a extensão do arquivo para definir o tipo de conteúdo adequado
            var extension = Path.GetExtension(caminhoArq).ToLower();
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
            {
                contentType = "image/png"; // ou "image/png"
            }

            return File(fileBytes, contentType);
        }


    }
}
