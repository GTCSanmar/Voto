namespace SistemaVotacao.Models
{
    public class Eleitor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Eleitor(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
