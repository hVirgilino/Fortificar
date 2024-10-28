using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fortificar.Migrations
{
    /// <inheritdoc />
    public partial class ODS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ODS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ODS", x => x.Id);
                    
                });

            migrationBuilder.InsertData(
                table: "ODS",
                columns: new[] { "Id", "Descricao", "Nome"},
                values: new object[,]
                {
                    { 1, "Acabar com a pobreza em todas as suas formas, em todos os lugares.", "Erradicação da pobreza"},
                    { 2, "Acabar com a fome e a inanição, alcançar a segurança alimentar, melhorar a nutrição e promover a agricultura sustentável.", "Erradicação da fome"},
                    { 3, "Assegurar uma vida saudável e promover o bem-estar para todos, em todas as idades.", "Saúde e Bem-Estar"},
                    { 4, "Assegurar a educação inclusiva e equitativa de qualidade, e promover oportunidades de aprendizagem ao longo da vida para todos.", "Educação de qualidade"},
                    { 5, "Alcançar a igualdade de gênero e empoderar todas as mulheres e meninas.", "Igualdade de gênero"},
                    { 6, "Assegurar a disponibilidade e a gestão sustentável da água e saneamento para todos.", "Água Potável e Saneamento"},
                    { 7, "Assegurar o acesso confiável, sustentável, moderno e a preço acessível à energia para todos.", "Energia acessível e limpa"},
                    { 8, "Promover o crescimento econômico sustentado, inclusivo e sustentável, o emprego pleno e produtivo e o trabalho decente para todos.", "Trabalho decente e crescimento econômico"},
                    { 9, "Construir infraestruturas resilientes, promover a industrialização inclusiva e sustentável e fomentar a inovação.", "Inovação e infraestrutura"},
                    { 10, "Reduzir a desigualdade dentro dos países e entre eles.", "Redução das desigualdades"},
                    { 11, "Tornar as cidades e os assentamentos humanos inclusivos, seguros, resilientes e sustentáveis.", "Cidades e comunidades sustentáveis"},
                    { 12, "Assegurar padrões de produção e de consumo sustentáveis.", "Consumo e produção responsáveis"},
                    { 13, "Tomar medidas urgentes para combater a mudança do clima e seus impactos.", "Ação contra a Mudança Global do Clima"},
                    { 14, "Conservar e promover o uso sustentável dos oceanos, dos mares e dos recursos marinhos para o desenvolvimento sustentável.", "Vida na Água"},
                    { 15, "Proteger, recuperar e promover o uso sustentável dos ecossistemas terrestres, gerir de forma sustentável as florestas, combater a desertificação, deter e reverter a degradação da terra e deter a perda.", "Vida Terrestre"},
                    { 16, "Promover sociedades pacíficas e inclusivas para o desenvolvimento sustentável, proporcionar o acesso à justiça para todos e construir instituições eficazes, responsáveis e inclusivas em todos os níveis.", "Paz, Justiça e Instituições Eficazes"},
                    { 17, "Fortalecer os meios de implementação e revitalizar a parceria global para o desenvolvimento sustentável.", "Parcerias e Meios de Implementação"}
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ODS");
        }
    }
}
