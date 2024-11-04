namespace Fortificar.Models
{
    public class Empresa
    {

        public int Id { get; set; }
        public String? Nome { get; set; }
        protected String? Cnpj { get; set; }
        protected String? Endereco { get; set; }
        public float? Desembolso { get; set; }


    }
}
