using GeradorTxt.GeradorLeiaute;
using GeradorTxt;
using GeradorTxt.Interfaces;
using System;
using System.IO;

namespace GeradorTxt
{
    public static class MainConsole
    {
        private static string _jsonPath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "base-dados-1.json");
        private static string _jsonPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "base-dados-2.json");
        private static string _outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out");

        public static void Run()
        {
            Directory.CreateDirectory(_outputDir);

            while (true)
            {
                Console.WriteLine("\nMenu");
                Console.WriteLine("1 - Configurar arquivos JSON (entrada)");
                Console.WriteLine("2 - Configurar diretório de saída");
                Console.WriteLine("3 - Gerar arquivo TXT");
                Console.WriteLine("4 - Exibir exemplos dos leiautes");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                var opcao = Console.ReadLine();
                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        ConfigurarJsonEntrada();
                        break;
                    case "2":
                        ConfigurarSaida();
                        break;
                    case "3":
                        GerarArquivo();
                        break;
                    case "4":
                        MostrarExemplos();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private static void ConfigurarJsonEntrada()
        {
            Console.Write("Informe o caminho do JSON 1: ");
            var p1 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(p1) && File.Exists(p1))
                _jsonPath1 = p1;

            Console.Write("Informe o caminho do JSON 2: ");
            var p2 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(p2) && File.Exists(p2))
                _jsonPath2 = p2;

            Console.WriteLine("Arquivos JSON configurados com sucesso.");
        }


        private static void ConfigurarSaida()
        {
            Console.Write("Informe o diretório de saída: ");
            var dir = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(dir))
            {
                _outputDir = dir;
                Directory.CreateDirectory(_outputDir);
            }

            Console.WriteLine("Diretório de saída configurado.");
        }
        private static void GerarArquivo()
        {
            Console.Write("Escolha o leiaute (1 ou 2): ");
            var leiaute = Console.ReadLine();

            IGeradorLeiaute factory = new GeradorLeiauteFactory();

            IGeradorArquivo gerador;
            try
            {
                gerador = factory.Criar(leiaute);
            }
            catch
            {
                Console.WriteLine("Leiaute inválido");
                return;
            }
                

            Console.Write("Qual JSON deseja usar? (1 ou 2): ");
            var jsonEscolhido = Console.ReadLine();
            string jsonPath;
        
            switch (jsonEscolhido)
            {
                case "1":
                    jsonPath = _jsonPath1;
                    break;

                case "2":
                    jsonPath = _jsonPath2;
                    break;

                default:
                    Console.WriteLine("Escreva uma opção válida");
                    return;
            }

            var repo = new JsonEmpresaRepository();
            var empresas = repo.Carregar(jsonPath);

            var fileName = $"saida_leiaute_{leiaute}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var fullPath = Path.Combine(_outputDir, fileName);

            gerador.Gerar(empresas, fullPath);

            Console.WriteLine("Arquivo gerado com sucesso:");
            Console.WriteLine(fullPath);
        }

          private static void MostrarExemplos()
        {
            Console.WriteLine("=== Exemplo Leiaute 01 ===");
            Console.WriteLine("00|CNPJEMPRESA|NOMEDAEMPRESA|TELEFONE");
            Console.WriteLine("01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO");
            Console.WriteLine("02|NUMEROITEM|DESCRICAOITEM|VALORITEM");
            Console.WriteLine("09|TIPO|QTD");
            Console.WriteLine("99|TOTAL_LINHAS\n");

            Console.WriteLine("=== Exemplo Leiaute 02 ===");
            Console.WriteLine("00|CNPJEMPRESA|NOMEDAEMPRESA|TELEFONE");
            Console.WriteLine("01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO");
            Console.WriteLine("02|NUMEROITEM|DESCRICAOITEM|VALORITEM");
            Console.WriteLine("03|NUMEROCATEGORIA|DESCRICAOCATEGORIA");
            Console.WriteLine("09|TIPO|QTD");
            Console.WriteLine("99|TOTAL_LINHAS\n");
        }
    }
}
