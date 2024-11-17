namespace Fortificar.Models
{
    public class Anexo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public byte[]? Imagem { get; set;}
        public byte[]? AtaEleicao { get;  set;}
        public byte[]? Estatuto { get; set;}
        public byte[]? CNPJ { get; set;}
        public byte[]? CPFRespLegal { get; set;}
        public byte[]? RGRespLegal { get; set;}
        public byte[]? DadosBancarios { get; set;}

        public int ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }
    }

}
