using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class TesteSave2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProponenteId",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsavelLegalId",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsavelTecnicoId",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResponsavelLegal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgaoExpedidor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoOSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MandatoVigente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsavelLegal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsavelTecnico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgaoExpedidor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoOSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MandatoVigente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsavelTecnico", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ProponenteId",
                table: "Projeto",
                column: "ProponenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto",
                column: "ResponsavelLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto",
                column: "ResponsavelTecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Proponente_ProponenteId",
                table: "Projeto",
                column: "ProponenteId",
                principalTable: "Proponente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Proponente_ProponenteId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelLegal_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_ResponsavelTecnico_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.DropTable(
                name: "ResponsavelLegal");

            migrationBuilder.DropTable(
                name: "ResponsavelTecnico");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ProponenteId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ResponsavelTecnicoId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ProponenteId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ResponsavelLegalId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ResponsavelTecnicoId",
                table: "Projeto");
        }
    }
}
