using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class PubBenefProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicoBeneficiario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSelected = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicoBeneficiario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjetoPublicoBeneficiario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjetoId = table.Column<int>(type: "int", nullable: false),
                    PublicoBeneficiarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoPublicoBeneficiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjetoPublicoBeneficiario_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoPublicoBeneficiario_PublicoBeneficiario_PublicoBeneficiarioId",
                        column: x => x.PublicoBeneficiarioId,
                        principalTable: "PublicoBeneficiario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PublicoBeneficiario",
                columns: new[] { "Id", "IsSelected", "Nome" },
                values: new object[,]
                {
                    { 1, false, "Criança" },
                    { 2, false, "Adolescente" },
                    { 3, false, "Jovens" },
                    { 4, false, "Adulto" },
                    { 5, false, "Idosos" },
                    { 6, false, "Criança e Adolescente" },
                    { 7, false, "Criança, Adolescentes, Jovens, Adultos e Idosos" },
                    { 8, false, "Adultos e Idosos" },
                    { 9, false, "Mulheres" },
                    { 10, false, "PCD" },
                    { 11, false, "Dependentes de substâncias psicoativas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoPublicoBeneficiario_ProjetoId",
                table: "ProjetoPublicoBeneficiario",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoPublicoBeneficiario_PublicoBeneficiarioId",
                table: "ProjetoPublicoBeneficiario",
                column: "PublicoBeneficiarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoPublicoBeneficiario");

            migrationBuilder.DropTable(
                name: "PublicoBeneficiario");
        }
    }
}
