namespace Fortificar.Models
{
    public class PublicoBeneficiario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool IsSelected { get; set; } 
    }

    public class ProjetoPublicoBeneficiario
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }

        public int PublicoBeneficiarioId { get; set; }
        public PublicoBeneficiario? PublicoBeneficiario { get; set; }
    }
}
