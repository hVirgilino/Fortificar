using Fortificar.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Fortificar.Models
{
    public class Proponente
    {
        public int Id { get; set; }
        // Informações básicas
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public string? CNPJ { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? InscricaoMunicipal { get; set; }

        // Endereço
        public string? Endereco { get; set; }
        [Display(Name = "Número")]
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }

        // Contato
        [Display(Name = "Sítio eletrônico de divulgação de parceria")]
        public string? Site { get; set; }
        [Display(Name = "Telefone 1")]
        public string? Telefone1 { get; set; }

        [Display(Name = "Telefone 2")]
        public string? Telefone2 { get; set; }

        [Display(Name = "Telefone 3")]
        public string? Telefone3 { get; set; }

        // Informações Financeiras (para desembolso)
        public string? Banco { get; set; }

        [Display(Name = "Agência")]
        public string? Agencia { get; set; }

        [Display(Name = "Conta Corrente")]
        public string? ContaCorrente { get; set; }

        [Display(Name = "Tipo da conta")]
        public string? TipoConta { get; set; }

        // Representante Legal

        public int RepresentanteLegalId{ get; set; }



		// Seção de projetos

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

        //Usuário
        public string FortificarUserId { get; set; }
        public FortificarUser Usuario { get; set; }
    }

}
