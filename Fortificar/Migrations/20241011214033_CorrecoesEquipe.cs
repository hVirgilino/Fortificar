using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class CorrecoesEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Proponente");

            migrationBuilder.DropColumn(
                name: "NomeEmpresa",
                table: "Proponente");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Proponente",
                newName: "Site");

            migrationBuilder.RenameColumn(
                name: "SiteDivulgacao",
                table: "Proponente",
                newName: "RazaoSocial");

            migrationBuilder.AddColumn<int>(
                name: "CPF",
                table: "MembroEquipe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "MembroEquipe");

            migrationBuilder.RenameColumn(
                name: "Site",
                table: "Proponente",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "Proponente",
                newName: "SiteDivulgacao");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Proponente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEmpresa",
                table: "Proponente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
