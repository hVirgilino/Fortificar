using Microsoft.EntityFrameworkCore;

namespace Fortificar.Models
{
    public class ODS
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public bool IsSelected { get; internal set; }

    }

    public class ProjetoODS
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public int ODSId { get; set; }
        public ODS ODS { get; set; }
    }
}
