namespace Fortificar.Models
{
    public class Anexo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public byte[]? Imagem { get; set;}
        public int ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }
    }

}
