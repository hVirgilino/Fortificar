using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDemaisListas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InicioExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TerminoExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CronogramaMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Indicadores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etapas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CronogramaMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CronogramaMeta_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanoAplicacaoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<float>(type: "real", nullable: false),
                    ValorTotal = table.Column<float>(type: "real", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAplicacaoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoAplicacaoItem_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CronogramaMeta_ProjetoId",
                table: "CronogramaMeta",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAplicacaoItem_ProjetoId",
                table: "PlanoAplicacaoItem",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CronogramaMeta");

            migrationBuilder.DropTable(
                name: "PlanoAplicacaoItem");

            migrationBuilder.DropColumn(
                name: "InicioExecucao",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "TerminoExecucao",
                table: "Projeto");
        }
    }
}
