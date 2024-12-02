using SistemaVotacao.Models;
using System;

class Program
{
    static void Main(string[] args)
    {
        Votacao votacao = new Votacao();
        Console.WriteLine("Bem-vindo ao sistema de votação!");

        while (true)
        {
            // Pedir nome do eleitor
            string? nomeEleitor;
            while (true)
            {
                Console.Write("Digite o nome do eleitor: ");
                nomeEleitor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nomeEleitor))
                    break;
                Console.WriteLine("O nome do eleitor não pode ser vazio. Tente novamente.");
            }

            // Criar um ID único para o eleitor
            Eleitor eleitor = new Eleitor(votacao.ResultadoVotacao().Count + 1, nomeEleitor);

            // Pedir nome do candidato
            string? nomeCandidato;
            while (true)
            {
                Console.Write("Digite o nome do candidato: ");
                nomeCandidato = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nomeCandidato))
                    break;
                Console.WriteLine("O nome do candidato não pode ser vazio. Tente novamente.");
            }

            // Registrar o voto
            votacao.Votar(eleitor, nomeCandidato);

            // Mostrar resultado da votação até o momento
            Console.WriteLine("\nResultado parcial da votação:");
            var resultado = votacao.ResultadoVotacao();
            foreach (var candidato in resultado)
            {
                Console.WriteLine($"{candidato.Nome}: {candidato.TotalVotos} votos");
            }

            // Perguntar se deseja continuar votando
            Console.WriteLine("\nDeseja registrar outro voto? (s/n): ");
            string? continuar = Console.ReadLine();
            if (continuar?.ToLower() != "s")
            {
                break;
            }
        }
            Console.WriteLine("Deseja limpar o cache de votos? (s/n): ");
            string? limparCache = Console.ReadLine();
            if (limparCache?.ToLower() == "s")
        {
            votacao.LimparVotacao();
        }

            Console.WriteLine("\nObrigado por usar o sistema de votação!");
    }
}