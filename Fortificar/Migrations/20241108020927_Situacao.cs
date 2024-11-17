using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class Situacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.AddColumn<int>(
                name: "SituacaoId",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Situacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacao", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Situacao",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "O projeto ainda está sendo criado pela instituição.", "Em edição" },
                    { 2, "O projeto foi enviado à FORTES e está em análise.", "Em análise" },
                    { 3, "O projeto foi aprovado.", "Aprovado" },
                    { 4, "O projeto está pendente de algum documento.", "Aguardando documentação" },
                    { 5, "O projeto foi recusado.", "Recusado" },
                    { 6, "O projeto foi concluído.", "Concluido" }
                });
            /*
            migrationBuilder.CreateIndex(
                name: "IX_Projeto_SituacaoId",
                table: "Projeto",
                column: "SituacaoId");
            */
            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId",
                principalTable: "ResponsavelTecnico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            /*
            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Situacao_SituacaoId",
                table: "Projeto",
                column: "SituacaoId",
                principalTable: "Situacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto");
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Situacao_SituacaoId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_SituacaoId",
                table: "Projeto");
            */
            migrationBuilder.DropTable(
                name: "Situacao");

            migrationBuilder.DropColumn(
                name: "SituacaoId",
                table: "Projeto");

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
