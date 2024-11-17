using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class Detalhes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "PublicoBeneficiario",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Indicadores",
                table: "CronogramaMeta");

            migrationBuilder.DropColumn(
                name: "Meta",
                table: "CronogramaMeta");

            migrationBuilder.DropColumn(
                name: "ValorMeta",
                table: "CronogramaMeta");

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "PublicoBeneficiario",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TerminoExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Objeto",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ObjetivosEspecificos",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ObjetivoGeral",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Cronograma",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Indicadores",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ValorMeta",
                table: "Projeto",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 6,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 7,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 8,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 9,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 10,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 11,
                column: "ProjetoId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_PublicoBeneficiario_ProjetoId",
                table: "PublicoBeneficiario",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto",
                column: "ResponsavelLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_ResponsavelLegal_ResponsavelLegalId",
                table: "Projeto",
                column: "ResponsavelLegalId",
                principalTable: "ResponsavelLegal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId",
                principalTable: "ResponsavelTecnico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelLegal_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario");

            migrationBuilder.DropIndex(
                name: "IX_PublicoBeneficiario_ProjetoId",
                table: "PublicoBeneficiario");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "PublicoBeneficiario");

            migrationBuilder.DropColumn(
                name: "Cronograma",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Indicadores",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ValorMeta",
                table: "Projeto");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TerminoExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Objeto",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObjetivosEspecificos",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObjetivoGeral",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExecucao",
                table: "Projeto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicoBeneficiario",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Indicadores",
                table: "CronogramaMeta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "CronogramaMeta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ValorMeta",
                table: "CronogramaMeta",
                type: "real",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_ProjetoId",
                table: "Anexo",
                column: "ProjetoId");
        }
    }
}
