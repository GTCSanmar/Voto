namespace SistemaVotacao.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TotalVotos { get; private set; }

        public Candidato(int id, string nome)
        {
            Id = id;
            Nome = nome;
            TotalVotos = 0;
        }

        public void ReceberVoto()
        {
            TotalVotos++;
        }
    }
}
