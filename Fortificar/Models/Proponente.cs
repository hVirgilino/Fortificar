using System.ComponentModel.DataAnnotations;

namespace Fortificar.Models
{
    public class Proponente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Email { get; set; }

		[Display(Name = "Sítio eletrônico de divulgação de parceria")]
		public string SiteDivulgacao { get; set; }
		[Display(Name = "Telefone 1")]
		public string Telefone1 { get; set; }

		[Display(Name = "Telefone 2")]
		public string Telefone2 { get; set; }

		[Display(Name = "Telefone 3")]
		public string Telefone3 { get; set; }

		// Histórico e informações da 

		[Display(Name = "CONHECENDO O PROPONENTE")]
		public string Historico { get; set; }

		[Display(Name = "PRINCIPAIS AÇÕES DESENVOLVIDAS PELA PROPONENTE")]
		public string PrincipaisAcoes { get; set; }

		[Display(Name = "PÚBLICO ALVO DE ATENDIMENTO DA PROPONENTE")]
		public string PublicoAlvo { get; set; }

		[Display(Name = "REGIÕES DE ALCANCE DAS AÇÕES (BAIRROS)")]
		public string RegioesAtendimento { get; set; }

		[Display(Name = "INFRAESTRUTURA DA PROPONENTE")]
		public string Infraestrutura { get; set; }

		// Lista de Equipe Multidisciplinar
		[Display(Name = "EQUIPE MULTIDISCIPLINAR DO PROPONENTE")]
		public string EquipeMultidisciplinar { get; set; }
		//public List<Projeto> Projetos { get;  set; }
	}

}
