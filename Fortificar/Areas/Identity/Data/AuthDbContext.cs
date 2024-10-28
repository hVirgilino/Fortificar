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
    public DbSet<Anexo> Anexo { get; set; }
    public DbSet<Parametro> Parametro { get; set; }
    public DbSet<ODS> ODS { get; set; }
    public DbSet<ProjetoODS> ProjetoODS { get; set; }

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
			.HasMany(p => p.Cronograma)
			.WithOne(c => c.Projeto)
			.HasForeignKey(m => m.ProjetoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Projeto>()
			.HasMany(p => p.PlanoAplicacao)
			.WithOne(pl => pl.Projeto)
			.HasForeignKey(m => m.ProjetoId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Projeto>()
			.HasMany(p => p.ODS);

		base.OnModelCreating(builder);
        builder.Entity<ODS>().HasData(
            new ODS { Id = 1, Nome = "Erradicação da pobreza", Descricao = "Acabar com a pobreza em todas as suas formas, em todos os lugares." },
            new ODS { Id = 2, Nome = "Erradicação da fome", Descricao = "Acabar com a fome e a inanição, alcançar a segurança alimentar, melhorar a nutrição e promover a agricultura sustentável." },
            new ODS { Id = 3, Nome = "Saúde e Bem-Estar", Descricao = "Assegurar uma vida saudável e promover o bem-estar para todos, em todas as idades." },
            new ODS { Id = 4, Nome = "Educação de qualidade", Descricao = "Assegurar a educação inclusiva e equitativa de qualidade, e promover oportunidades de aprendizagem ao longo da vida para todos." },
            new ODS { Id = 5, Nome = "Igualdade de gênero", Descricao = "Alcançar a igualdade de gênero e empoderar todas as mulheres e meninas." },
            new ODS { Id = 6, Nome = "Água Potável e Saneamento", Descricao = "Assegurar a disponibilidade e a gestão sustentável da água e saneamento para todos." },
            new ODS { Id = 7, Nome = "Energia acessível e limpa", Descricao = "Assegurar o acesso confiável, sustentável, moderno e a preço acessível à energia para todos." },
            new ODS { Id = 8, Nome = "Trabalho decente e crescimento econômico", Descricao = "Promover o crescimento econômico sustentado, inclusivo e sustentável, o emprego pleno e produtivo e o trabalho decente para todos." },
            new ODS { Id = 9, Nome = "Inovação e infraestrutura", Descricao = "Construir infraestruturas resilientes, promover a industrialização inclusiva e sustentável e fomentar a inovação." },
            new ODS { Id = 10, Nome = "Redução das desigualdades", Descricao = "Reduzir a desigualdade dentro dos países e entre eles." },
            new ODS { Id = 11, Nome = "Cidades e comunidades sustentáveis", Descricao = "Tornar as cidades e os assentamentos humanos inclusivos, seguros, resilientes e sustentáveis." },
            new ODS { Id = 12, Nome = "Consumo e produção responsáveis", Descricao = "Assegurar padrões de produção e de consumo sustentáveis." },
            new ODS { Id = 13, Nome = "Ação contra a Mudança Global do Clima", Descricao = "Tomar medidas urgentes para combater a mudança do clima e seus impactos." },
            new ODS { Id = 14, Nome = "Vida na Água", Descricao = "Conservar e promover o uso sustentável dos oceanos, dos mares e dos recursos marinhos para o desenvolvimento sustentável." },
            new ODS { Id = 15, Nome = "Vida Terrestre", Descricao = "Proteger, recuperar e promover o uso sustentável dos ecossistemas terrestres, gerir de forma sustentável as florestas, combater a desertificação, deter e reverter a degradação da terra e deter a perda." },
            new ODS { Id = 16, Nome = "Paz, Justiça e Instituições Eficazes", Descricao = "Promover sociedades pacíficas e inclusivas para o desenvolvimento sustentável, proporcionar o acesso à justiça para todos e construir instituições eficazes, responsáveis e inclusivas em todos os níveis." },
            new ODS { Id = 17, Nome = "Parcerias e Meios de Implementação", Descricao = "Fortalecer os meios de implementação e revitalizar a parceria global para o desenvolvimento sustentável." }
        );
    }



	
}
