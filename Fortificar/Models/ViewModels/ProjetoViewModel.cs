using Fortificar.Models;

namespace Fortificar.Models.ViewModels
{
	public class ProjetoViewModel
	{//MIGRATION
		public Projeto Projeto { get; set; }
		public Proponente Proponente { get; set; }
		public ResponsavelLegal ResponsavelLegal { get; set; }
		public ResponsavelTecnico ResponsavelTecnico { get; set; }
		public Anexo? Anexo { get; set; }
        public List<MembroEquipe> EquipeExecucao { get; set; } = new List<MembroEquipe>();
        public List<CronogramaMeta> Cronograma { get; set; } = new List<CronogramaMeta>();
        public List<PlanoAplicacaoItem> PlanoAplicacao { get; set; } = new List<PlanoAplicacaoItem>();


    }
}
