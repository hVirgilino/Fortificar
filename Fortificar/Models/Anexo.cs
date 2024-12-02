namespace Fortificar.Models
{
    public class Anexo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public byte[]? Imagem { get; set;}

        public string? AtaEleicao { get;  set;}
        public string? AprovadoAtaEleicao { get;  set;}

        public string? Estatuto { get; set;}
        public string? AprovadoEstatuto { get; set;}

        public string? CNPJ { get; set;}
        public string? AprovadoCNPJ { get; set;}

        public string? CPFRespLegal { get; set;}
        public string? AprovadoCPFRespLegal { get; set;}

        public string? RGRespLegal { get; set;}
        public string? AprovadoRGRespLegal { get; set;}

        public string? DadosBancarios { get; set;}
        public string? AprovadoDadosBancarios { get; set;}


    }

}
