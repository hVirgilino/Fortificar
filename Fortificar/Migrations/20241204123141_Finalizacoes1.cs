using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class Finalizacoes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario");

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "PublicoBeneficiario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Anexo",
                columns: new[] { "Id", "AprovadoAtaEleicao", "AprovadoCNPJ", "AprovadoCPFRespLegal", "AprovadoDadosBancarios", "AprovadoEstatuto", "AprovadoRGRespLegal", "AtaEleicao", "CNPJ", "CPFRespLegal", "DadosBancarios", "Estatuto", "Imagem", "Nome", "RGRespLegal", "Tipo" },
                values: new object[] { -1, "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", null, "Teste", "Teste", "Teste" });

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 6,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 7,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 8,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 9,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 10,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 11,
                column: "ProjetoId",
                value: -1);

            migrationBuilder.InsertData(
                table: "ResponsavelLegal",
                columns: new[] { "Id", "CPF", "CargoOSC", "Endereco", "MandatoVigente", "Nome", "OrgaoExpedidor", "RG", "Telefone1", "Telefone2", "Telefone3" },
                values: new object[] { -1, "Teste", "Teste", "Teste", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teste", "Teste", "Teste", "Teste", "Teste", "Teste" });

            migrationBuilder.InsertData(
                table: "ResponsavelTecnico",
                columns: new[] { "Id", "CPF", "CargoOSC", "Endereco", "MandatoVigente", "Nome", "OrgaoExpedidor", "RG", "Telefone1", "Telefone2", "Telefone3" },
                values: new object[] { -1, "Teste", "Teste", "Teste", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teste", "Teste", "Teste", "Teste", "Teste", "Teste" });

            migrationBuilder.InsertData(
                table: "Proponente",
                columns: new[] { "Id", "Agencia", "Bairro", "Banco", "CEP", "CNPJ", "Cidade", "Complemento", "Conta", "EmailEmpresa", "Endereco", "EquipeMultidisciplinar", "Estado", "Historico", "InformacoesRelevantes", "Infraestrutura", "InscricaoEstadual", "InscricaoMunicipal", "NomeFantasia", "Numero", "PrincipaisAcoes", "PublicoAlvo", "RazaoSocial", "RegioesAtendimento", "ResponsavelLegalId", "Site", "Telefone1", "Telefone2", "Telefone3", "TipoConta" },
                values: new object[] { -1, "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", "Teste", -1, "Teste", "Teste", "Teste", "Teste", "Teste" });

            migrationBuilder.InsertData(
                table: "Projeto",
                columns: new[] { "Id", "AnexoId", "Cronograma", "Indicadores", "InicioExecucao", "Justificativa", "ObjetivoGeral", "ObjetivosEspecificos", "Objeto", "Orcamento", "ProponenteId", "ResponsavelLegalId", "ResponsavelTecnicoId", "SituacaoId", "TerminoExecucao", "ValorMeta" },
                values: new object[] { -1, -1, "Teste", "Teste", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teste", "Teste", "Teste", "Teste", 0f, -1, -1, -1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0f });

            migrationBuilder.AddForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario");

            migrationBuilder.DeleteData(
                table: "Projeto",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Anexo",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Proponente",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "ResponsavelTecnico",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "ResponsavelLegal",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "PublicoBeneficiario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 6,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 7,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 8,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 9,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 10,
                column: "ProjetoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublicoBeneficiario",
                keyColumn: "Id",
                keyValue: 11,
                column: "ProjetoId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicoBeneficiario_Projeto_ProjetoId",
                table: "PublicoBeneficiario",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id");
        }
    }
}
