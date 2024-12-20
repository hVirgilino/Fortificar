﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fortificar.Models
{
    public class Projeto
    {
        public int Id { get; set; }

        public Proponente? Proponente { get; set; }
        public ResponsavelLegal? ResponsavelLegal { get; set; }
        public ResponsavelTecnico? ResponsavelTecnico { get; set; }
        public int ProponenteId { get; set; }
		public int ResponsavelLegalId { get; set; }
		public int ResponsavelTecnicoId { get; set; }

		// Informações do projeto
		public string? Objeto { get; set; }
		[Display(Name = "Objetivo Geral")]
		public string? ObjetivoGeral { get; set; }
		[Display(Name = "Objetivos Específicos")]
		public string? ObjetivosEspecificos { get; set; }
        public string? Justificativa { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Início em:")]
        public DateTime? InicioExecucao { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Término em:")]
        public DateTime? TerminoExecucao { get; set; }

        // Lista de profissionais da execução do projeto
        public List<MembroEquipe>? EquipeExecucao { get; set; } = new List<MembroEquipe>();

        // Cronograma físico-financeiro e metas
        public List<CronogramaMeta>? CronogramaMeta { get; set; } = new List<CronogramaMeta>();

        // Plano de aplicação
        public List<PlanoAplicacaoItem>? PlanoAplicacao { get; set; } = new List<PlanoAplicacaoItem>();    
        
        // Equipe encarregada pela execução do projeto
        public List<EquipeExecucaoProjeto>? EquipeEncarregada { get; set; } = new List<EquipeExecucaoProjeto>();      
        
        // ODS
        public List<ODS>? ODS { get; set; } = new List<ODS>();
        // PB
        public List<PublicoBeneficiario>? PublicoBeneficiario { get; set; } = new List<PublicoBeneficiario>();

        [Display(Name = "Orçamento do Projeto")]
        public float Orcamento { get; set; }

        [Display(Name = "Situação")]
        public Situacao? Situacao { get; set; }
        public int SituacaoId { get; set; }

        public int AnexoId { get; set; }
        public Anexo? Anexo { get; set; }

        //Cronograma
        public string? Cronograma { get; set; }
        public float? ValorMeta { get; set; }
        public string? Indicadores { get; set; }

        public ICollection<ProjetoODS>? ProjetoODS { get; set; }
        public ICollection<ProjetoPublicoBeneficiario>? ProjetoPublicoBeneficiario { get; set; }
    }

    public class Situacao
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }

   
    public class MembroEquipe
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        [Display(Name = "Formação")]
        public string? Formacao { get; set; }
        [Display(Name = "Função")]
        public string? Funcao { get; set; }
        [Display(Name = "Cárga Horária Semanal")]
        public int? CargaHorariaSemanal { get; set; }

        public int? ProjetoId { get; set; }
        [NotMapped]
        public Projeto? Projeto { get; set; }
    }
 
    public class CronogramaMeta
    {
        public int Id { get; set; }
        public float? ValorEtapa { get; set; }
        public string? Etapas { get; set; }

        [DataType(DataType.Date)]  
        [Display(Name = "Início")]
        public DateTime? Inicio { get; set; }


        [Display(Name = "Término")]
        [DataType(DataType.Date)]
        public DateTime? Termino { get; set; }

        public int ProjetoId { get; set; }
        [NotMapped]
        public Projeto? Projeto { get; set; }
    }

    public class PlanoAplicacaoItem
    {
        public int Id { get; set; }
        [Display(Name = "Especificação")]
        public string? Especificacao { get; set; }
        public string? Unidade { get; set; }
        public int? Quantidade { get; set; }
        [Display(Name = "Valor Unitário")]
        public float? ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public float? ValorTotal { get; set; }

        public int ProjetoId { get; set; }
        [NotMapped]
        public Projeto? Projeto { get; set; }
    }
    public class EquipeExecucaoProjeto
    {
        public int Id { get; set; }
        [Display(Name = "Especificação")]
        public string? Especificacao { get; set; }
        public string? Unidade { get; set; }
        public int? Quantidade { get; set; }
        [Display(Name = "Valor Unitário")]
        public float? ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public float? ValorTotal { get; set; }

        public int ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }
    }
}
