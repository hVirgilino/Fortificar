namespace Fortificar.Models.ViewModels
{
    public class ProjetoViewModel
	{//MIGRATION
		public Projeto Projeto { get; set; }		
        public List<ODS> ODS { get; set; } = new List<ODS>();
        public List<PublicoBeneficiario> PublicoBeneficiario { get; set; } = new List<PublicoBeneficiario>();

        public IEnumerable<Projeto>? Projetos { get; set; }
    }
}
