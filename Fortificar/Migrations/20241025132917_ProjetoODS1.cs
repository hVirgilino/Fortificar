using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class ProjetoODS1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "ODS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProjetoODS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjetoId = table.Column<int>(type: "int", nullable: false),
                    ODSId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoODS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjetoODS_ODS_ODSId",
                        column: x => x.ODSId,
                        principalTable: "ODS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoODS_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsSelected",
                value: false);

            migrationBuilder.UpdateData(
                table: "ODS",
                keyColumn: "Id",
                keyValue: 17,
                column: "IsSelected",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoODS_ODSId",
                table: "ProjetoODS",
                column: "ODSId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoODS_ProjetoId",
                table: "ProjetoODS",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoODS");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "ODS");
        }
    }
}
