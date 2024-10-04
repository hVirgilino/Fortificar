namespace Fortificar.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public int Tipo { get; set; }

        public Usuario()
        {
        }

        public Usuario(int id, string nome, string email, string senha, int tipo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Tipo = tipo;
        }

    }
}
