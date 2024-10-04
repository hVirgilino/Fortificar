namespace Fortificar.Models
{
    public class Anexo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; } // Por exemplo: "image/jpeg", "application/pdf"
        public byte[] Conteudo { get; set; }
    }

}
