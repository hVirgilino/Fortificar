using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class AnexoCamposNovos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Caminho",
                table: "Anexo",
                newName: "Tipo");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Anexo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Anexo");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Anexo",
                newName: "Caminho");
        }
    }
}
