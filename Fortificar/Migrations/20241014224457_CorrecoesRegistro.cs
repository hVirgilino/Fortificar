using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class CorrecoesRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proponente_AspNetUsers_UsuarioId",
                table: "Proponente");

            migrationBuilder.DropIndex(
                name: "IX_Proponente_UsuarioId",
                table: "Proponente");

            migrationBuilder.DropColumn(
                name: "FortificarUserId",
                table: "Proponente");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Proponente");

            migrationBuilder.RenameColumn(
                name: "RepresentanteLegalId",
                table: "Proponente",
                newName: "ResponsavelLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_Proponente_ResponsavelLegalId",
                table: "Proponente",
                column: "ResponsavelLegalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proponente_ResponsavelLegal_ResponsavelLegalId",
                table: "Proponente",
                column: "ResponsavelLegalId",
                principalTable: "ResponsavelLegal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proponente_ResponsavelLegal_ResponsavelLegalId",
                table: "Proponente");

            migrationBuilder.DropIndex(
                name: "IX_Proponente_ResponsavelLegalId",
                table: "Proponente");

            migrationBuilder.RenameColumn(
                name: "ResponsavelLegalId",
                table: "Proponente",
                newName: "RepresentanteLegalId");

            migrationBuilder.DropColumn(
                name: "FortificarUserId",
                table: "Proponente");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Proponente",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proponente_UsuarioId",
                table: "Proponente",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proponente_AspNetUsers_UsuarioId",
                table: "Proponente",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
