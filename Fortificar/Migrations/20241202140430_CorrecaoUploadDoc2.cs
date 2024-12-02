using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoUploadDoc2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anexo_Projeto_ProjetoId",
                table: "Anexo");

            migrationBuilder.DropIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Anexo");

            migrationBuilder.AddColumn<int>(
                name: "AnexoId",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_AnexoId",
                table: "Projeto",
                column: "AnexoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Anexo_AnexoId",
                table: "Projeto",
                column: "AnexoId",
                principalTable: "Anexo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Anexo_AnexoId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_AnexoId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "AnexoId",
                table: "Projeto");

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Anexo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Anexo_Projeto_ProjetoId",
                table: "Anexo",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
