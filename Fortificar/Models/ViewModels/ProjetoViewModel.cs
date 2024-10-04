using Fortificar.Models;

namespace Fortificar.Models.ViewModels
{
	public class ProjetoViewModel
	{//MIGRATION
		public Projeto Projeto { get; set; }
		public Proponente Proponente { get; set; }
		public ResponsavelLegal ResponsavelLegal { get; set; }
		public ResponsavelTecnico ResponsavelTecnico { get; set; }

		
	}
}
