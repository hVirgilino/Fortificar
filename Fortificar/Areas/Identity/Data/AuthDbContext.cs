using Fortificar.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fortificar.Models;
using System.Reflection.Emit;

namespace Fortificar.Data;

public class AuthDbContext : IdentityDbContext<FortificarUser>
{
	//MIGRATION
	public DbSet<Projeto> Projeto { get; set; }
	public DbSet<FortificarUser> FortificarUser { get; set; }
	public DbSet<Proponente> Proponente { get; set; }
	public DbSet<ResponsavelLegal> ResponsavelLegal { get; set; }
	public DbSet<ResponsavelTecnico> ResponsavelTecnico { get; set; }
    public DbSet<MembroEquipe> MembroEquipe { get; set; }
    public DbSet<CronogramaMeta> CronogramaMeta { get; set; }
    public DbSet<PlanoAplicacaoItem> PlanoAplicacaoItem { get; set; }
    public DbSet<EquipeExecucaoProjeto> EquipeExecucaoProjeto { get; set; }
    public DbSet<Anexo> Anexo { get; set; }
    public DbSet<Parametro> Parametro { get; set; }
    public DbSet<ODS> ODS { get; set; }
    public DbSet<ProjetoODS> ProjetoODS { get; set; }
    public DbSet<PublicoBeneficiario> PublicoBeneficiario { get; set; }
    public DbSet<ProjetoPublicoBeneficiario> ProjetoPublicoBeneficiario { get; set; }
    public DbSet<Situacao> Situacao { get; set; }
 

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
	{

		builder.Entity<Projeto>()
			.HasMany(p => p.EquipeExecucao)
			.WithOne(m => m.Projeto)
			.HasForeignKey(m => m.ProjetoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Projeto>()
			.HasMany(p => p.CronogramaMeta)
			.WithOne(c => c.Projeto)
			.HasForeignKey(m => m.ProjetoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Projeto>()
			.HasMany(p => p.PlanoAplicacao)
			.WithOne(pl => pl.Projeto)
			.HasForeignKey(m => m.ProjetoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Projeto>()
            .HasOne(p => p.ResponsavelLegal)
            .WithMany()
            .HasForeignKey(p => p.ResponsavelLegalId)
            .OnDelete(DeleteBehavior.Restrict);
		builder.Entity<Projeto>()
            .HasOne(p => p.ResponsavelTecnico)
            .WithMany()
            .HasForeignKey(p => p.ResponsavelTecnicoId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
        builder.Entity<ODS>().HasData(
            new ODS { Id = 1, Nome = "Erradicação da pobreza", Descricao = "Acabar com a pobreza em todas as suas formas, em todos os lugares.", IsSelected = false },
            new ODS { Id = 2, Nome = "Erradicação da fome", Descricao = "Acabar com a fome e a inanição, alcançar a segurança alimentar, melhorar a nutrição e promover a agricultura sustentável.", IsSelected = false  },
            new ODS { Id = 3, Nome = "Saúde e Bem-Estar", Descricao = "Assegurar uma vida saudável e promover o bem-estar para todos, em todas as idades.", IsSelected = false  },
            new ODS { Id = 4, Nome = "Educação de qualidade", Descricao = "Assegurar a educação inclusiva e equitativa de qualidade, e promover oportunidades de aprendizagem ao longo da vida para todos.", IsSelected = false  },
            new ODS { Id = 5, Nome = "Igualdade de gênero", Descricao = "Alcançar a igualdade de gênero e empoderar todas as mulheres e meninas.", IsSelected = false  },
            new ODS { Id = 6, Nome = "Água Potável e Saneamento", Descricao = "Assegurar a disponibilidade e a gestão sustentável da água e saneamento para todos.", IsSelected = false  },
            new ODS { Id = 7, Nome = "Energia acessível e limpa", Descricao = "Assegurar o acesso confiável, sustentável, moderno e a preço acessível à energia para todos.", IsSelected = false  },
            new ODS { Id = 8, Nome = "Trabalho decente e crescimento econômico", Descricao = "Promover o crescimento econômico sustentado, inclusivo e sustentável, o emprego pleno e produtivo e o trabalho decente para todos.", IsSelected = false  },
            new ODS { Id = 9, Nome = "Inovação e infraestrutura", Descricao = "Construir infraestruturas resilientes, promover a industrialização inclusiva e sustentável e fomentar a inovação.", IsSelected = false  },
            new ODS { Id = 10, Nome = "Redução das desigualdades", Descricao = "Reduzir a desigualdade dentro dos países e entre eles.", IsSelected = false  },
            new ODS { Id = 11, Nome = "Cidades e comunidades sustentáveis", Descricao = "Tornar as cidades e os assentamentos humanos inclusivos, seguros, resilientes e sustentáveis.", IsSelected = false  },
            new ODS { Id = 12, Nome = "Consumo e produção responsáveis", Descricao = "Assegurar padrões de produção e de consumo sustentáveis.", IsSelected = false  },
            new ODS { Id = 13, Nome = "Ação contra a Mudança Global do Clima", Descricao = "Tomar medidas urgentes para combater a mudança do clima e seus impactos.", IsSelected = false  },
            new ODS { Id = 14, Nome = "Vida na Água", Descricao = "Conservar e promover o uso sustentável dos oceanos, dos mares e dos recursos marinhos para o desenvolvimento sustentável.", IsSelected = false  },
            new ODS { Id = 15, Nome = "Vida Terrestre", Descricao = "Proteger, recuperar e promover o uso sustentável dos ecossistemas terrestres, gerir de forma sustentável as florestas, combater a desertificação, deter e reverter a degradação da terra e deter a perda.", IsSelected = false  },
            new ODS { Id = 16, Nome = "Paz, Justiça e Instituições Eficazes", Descricao = "Promover sociedades pacíficas e inclusivas para o desenvolvimento sustentável, proporcionar o acesso à justiça para todos e construir instituições eficazes, responsáveis e inclusivas em todos os níveis.", IsSelected = false  },
            new ODS { Id = 17, Nome = "Parcerias e Meios de Implementação", Descricao = "Fortalecer os meios de implementação e revitalizar a parceria global para o desenvolvimento sustentável.", IsSelected = false  }
        );
        builder.Entity<PublicoBeneficiario>().HasData(
            new PublicoBeneficiario { Id = 1, Nome = "Criança", IsSelected = false },
            new PublicoBeneficiario { Id = 2, Nome = "Adolescente", IsSelected = false },
            new PublicoBeneficiario { Id = 3, Nome = "Jovens", IsSelected = false },
            new PublicoBeneficiario { Id = 4, Nome = "Adulto", IsSelected = false },
            new PublicoBeneficiario { Id = 5, Nome = "Idosos", IsSelected = false },
            new PublicoBeneficiario { Id = 6, Nome = "Criança e Adolescente", IsSelected = false },
            new PublicoBeneficiario { Id = 7, Nome = "Criança, Adolescentes, Jovens, Adultos e Idosos", IsSelected = false },
            new PublicoBeneficiario { Id = 8, Nome = "Adultos e Idosos", IsSelected = false },
            new PublicoBeneficiario { Id = 9, Nome = "Mulheres", IsSelected = false },
            new PublicoBeneficiario { Id = 10, Nome = "PCD", IsSelected = false },
            new PublicoBeneficiario { Id = 11, Nome = "Dependentes de substâncias psicoativas", IsSelected = false }
        );
        
        builder.Entity<Situacao>().HasData(
            new Situacao { Id = 1, Nome = "Em edição", Descricao = "O projeto ainda está sendo criado pela instituição."},
            new Situacao { Id = 2, Nome = "Enviado", Descricao = "O projeto foi enviado à FORTES."},
            new Situacao { Id = 3, Nome = "Em andamento", Descricao = "O projeto foi aprovado e está em andamento."},
            new Situacao { Id = 4, Nome = "Aguardando documentação", Descricao = "O projeto está pendente de algum documento."},
            new Situacao { Id = 5, Nome = "Recusado", Descricao = "O projeto foi recusado."},
            new Situacao { Id = 6, Nome = "Concluido", Descricao = "O projeto foi concluído."},
            new Situacao { Id = 7, Nome = "Em análise", Descricao = "O projeto está em análise."}
        );
    }



	
}
