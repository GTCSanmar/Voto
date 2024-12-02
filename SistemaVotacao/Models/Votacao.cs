using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaVotacao.Models
{
    public class Votacao
    {
        private List<Candidato> candidatos;
        private List<int> eleitoresQueVotaram; // IDs de eleitores que já votaram
        private string caminhoArquivo = "votos.txt"; // Arquivo para salvar votos

        public Votacao()
        {
            candidatos = new List<Candidato>();
            eleitoresQueVotaram = new List<int>();
            CarregarVotos(); // Carregar votos salvos do arquivo
        }

        public void AdicionarCandidato(Candidato candidato)
        {
            if (candidato == null) throw new ArgumentNullException(nameof(candidato));
            candidatos.Add(candidato);
        }

        public Candidato? BuscarCandidatoPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
            return candidatos.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public void Votar(Eleitor eleitor, string nomeCandidato)
        {
            if (eleitor == null) throw new ArgumentNullException(nameof(eleitor));
            if (string.IsNullOrWhiteSpace(nomeCandidato)) throw new ArgumentException("O nome do candidato não pode ser nulo ou vazio.", nameof(nomeCandidato));

            if (eleitoresQueVotaram.Contains(eleitor.Id))
            {
                Console.WriteLine("Eleitor já votou!");
                return;
            }

            // Verificar se o candidato já existe
            var candidato = BuscarCandidatoPorNome(nomeCandidato);
            if (candidato == null)
            {
                // Se o candidato não existir, ele será adicionado
                candidato = new Candidato(candidatos.Count + 1, nomeCandidato);
                AdicionarCandidato(candidato);
            }

            candidato.ReceberVoto(); // Agora temos certeza que o candidato não é nulo
            eleitoresQueVotaram.Add(eleitor.Id);
            Console.WriteLine($"Voto registrado para {candidato.Nome}");

            // Salvar o total de votos no arquivo
            SalvarVotos();
        }
        public void LimparVotacao()
        {
            candidatos.Clear(); // Limpa a lista de candidatos
            eleitoresQueVotaram.Clear(); // Limpa a lista de eleitores que já votaram
            
            // Se quiser também limpar o arquivo de votos:
            if (File.Exists(caminhoArquivo))
            {
                File.Delete(caminhoArquivo);
            }

            Console.WriteLine("O cache de votos foi limpo com sucesso.");
        }

        public List<Candidato> ResultadoVotacao()
        {
            return candidatos;
        }

        private void SalvarVotos()
        {
            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {
                foreach (var candidato in candidatos)
                {
                    writer.WriteLine($"{candidato.Id},{candidato.Nome},{candidato.TotalVotos}");
                }
            }
        }

        private void CarregarVotos()
        {
            if (File.Exists(caminhoArquivo))
            {
                using (StreamReader reader = new StreamReader(caminhoArquivo))
                {
                    string linha;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        var partes = linha.Split(',');
                        int id = int.Parse(partes[0]);
                        string nome = partes[1];
                        int totalVotos = int.Parse(partes[2]);

                        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do candidato não pode ser nulo ou vazio ao carregar votos.", nameof(nome));

                        var candidato = new Candidato(id, nome);
                        for (int i = 0; i < totalVotos; i++)
                        {
                            candidato.ReceberVoto();
                        }

                        candidatos.Add(candidato);
                    }
                }
            }
        }
    }
}
