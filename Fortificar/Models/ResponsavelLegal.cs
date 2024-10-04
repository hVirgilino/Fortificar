using System.ComponentModel.DataAnnotations;

namespace Fortificar.Models
{
    public class ResponsavelLegal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
		[Display(Name = "Órgão Expedidor")]
		public string OrgaoExpedidor { get; set; }
		[Display(Name = "Cargo na OSC")]
		public string CargoOSC { get; set; }
		[Display(Name = "Mandato vigente")]
		public DateTime MandatoVigente { get; set; }
		[Display(Name = "Endereço")]
		public string Endereco { get; set; }
		[Display(Name = "Telefone 1")]
		public string Telefone1 { get; set; }
		[Display(Name = "Telefone 2")]
		public string Telefone2 { get; set; }
		[Display(Name = "Telefone 3")]
		public string Telefone3 { get; set; }

		
	}
}
