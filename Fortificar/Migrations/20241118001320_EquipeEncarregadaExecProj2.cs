using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class EquipeEncarregadaExecProj2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EquipeExecucaoProjeto_ProjetoId",
                table: "EquipeExecucaoProjeto",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeExecucaoProjeto_Projeto_ProjetoId",
                table: "EquipeExecucaoProjeto",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipeExecucaoProjeto_Projeto_ProjetoId",
                table: "EquipeExecucaoProjeto");

            migrationBuilder.DropIndex(
                name: "IX_EquipeExecucaoProjeto_ProjetoId",
                table: "EquipeExecucaoProjeto");
        }
    }
}
