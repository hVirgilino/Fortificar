using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class Orcamento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Parametro",
                newName: "ValorMin");

            migrationBuilder.AddColumn<string>(
                name: "ValorMax",
                table: "Parametro",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorMax",
                table: "Parametro");

            migrationBuilder.RenameColumn(
                name: "ValorMin",
                table: "Parametro",
                newName: "Valor");
        }
    }
}
