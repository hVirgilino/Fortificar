using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class VerifAnexo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AprovadoAtaEleicao",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprovadoCNPJ",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprovadoCPFRespLegal",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprovadoDadosBancarios",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprovadoEstatuto",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprovadoRGRespLegal",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprovadoAtaEleicao",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "AprovadoCNPJ",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "AprovadoCPFRespLegal",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "AprovadoDadosBancarios",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "AprovadoEstatuto",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "AprovadoRGRespLegal",
                table: "Anexo");
        }
    }
}
