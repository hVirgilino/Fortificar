using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class DocPendentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ValorMin",
                table: "Parametro",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ValorMax",
                table: "Parametro",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AtaEleicao",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CNPJ",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CPFRespLegal",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DadosBancarios",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Estatuto",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RGRespLegal",
                table: "Anexo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Situacao",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "O projeto foi enviado à FORTES.", "Enviado" });

            migrationBuilder.UpdateData(
                table: "Situacao",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "O projeto foi aprovado e está em andamento.", "Em andamento" });

            migrationBuilder.InsertData(
                table: "Situacao",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[] { 7, "O projeto está em análise.", "Em análise" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Situacao",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "AtaEleicao",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "CPFRespLegal",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "DadosBancarios",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "Estatuto",
                table: "Anexo");

            migrationBuilder.DropColumn(
                name: "RGRespLegal",
                table: "Anexo");

            migrationBuilder.AlterColumn<string>(
                name: "ValorMin",
                table: "Parametro",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValorMax",
                table: "Parametro",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Situacao",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "O projeto foi enviado à FORTES e está em análise.", "Em análise" });

            migrationBuilder.UpdateData(
                table: "Situacao",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "O projeto foi aprovado.", "Aprovado" });
        }
    }
}
