namespace Fortificar.Models
{
    public class ODS
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }

        public ODS(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
