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
	public DbSet<Proponente> Proponente { get; set; }
	public DbSet<ResponsavelLegal> ResponsavelLegal { get; set; }
	public DbSet<ResponsavelTecnico> ResponsavelTecnico { get; set; }
	public DbSet<MembroEquipe> MembroEquipe { get; set; }

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

		base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }



	
}
