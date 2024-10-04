using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class ListaMembroProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelLegal_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.CreateTable(
                name: "MembroEquipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargaHorariaSemanal = table.Column<int>(type: "int", nullable: true),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembroEquipe_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_ProjetoId",
                table: "MembroEquipe",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembroEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto",
                column: "ResponsavelLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_ResponsavelLegal_ResponsavelLegalId",
                table: "Projeto",
                column: "ResponsavelLegalId",
                principalTable: "ResponsavelLegal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId",
                principalTable: "ResponsavelTecnico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
