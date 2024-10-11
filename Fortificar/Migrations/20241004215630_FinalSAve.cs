using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class FinalSAve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "CronogramaMeta",
                newName: "ValorMeta");

            migrationBuilder.AddColumn<float>(
                name: "ValorEtapa",
                table: "CronogramaMeta",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorEtapa",
                table: "CronogramaMeta");

            migrationBuilder.RenameColumn(
                name: "ValorMeta",
                table: "CronogramaMeta",
                newName: "Valor");
        }
    }
}
