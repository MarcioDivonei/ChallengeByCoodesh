using System;
using System.IO;

namespace GeradorTxt
{
    /// <summary>
    /// Responsável por interagir com o usuário via console.
    /// </summary>
    public static class MainConsole
    {
        private static string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "base-dados.json");
        private static string _outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out");

        public static void Run()
        {
            Directory.CreateDirectory(_outputDir);
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine("1. Configurar arquivo .json (base de dados)");
                Console.WriteLine("2. Configurar diretório de output");
                Console.WriteLine("3. Gerar arquivo");
                Console.WriteLine("4. Gerar exemplos dos leiautes");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");

                var opt = Console.ReadLine();
                Console.WriteLine();
                Console.Clear();

                switch (opt)
                {
                    case "1":
                        ConfigurarCaminhoJson();
                        break;

                    case "2":
                        ConfigurarSaidaTxt();
                        break;

                    case "3":
                        GerarArquivo();
                        break;
                    case "4":
                        GerarExemplos();
                        break;
                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.WriteLine("\n\nPressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void ConfigurarCaminhoJson()
        {
            Console.Write("Informe o caminho completo do arquivo .json: ");
            var jp = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(jp) && File.Exists(jp))
            {
                _jsonPath = jp;
                Console.WriteLine("OK! JSON configurado: " + _jsonPath);

            }
            else
            {
                Console.WriteLine("Caminho inválido ou arquivo não encontrado.");
            }
        }
        private static void ConfigurarSaidaTxt()
        {
            Console.Write("Informe o diretório de saída para o .txt: ");
            var od = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(od))
            {
                _outputDir = od;
                Directory.CreateDirectory(_outputDir);
                Console.WriteLine("OK! Diretório de saída configurado: " + _outputDir);
            }
            else
            {
                Console.WriteLine("Diretório inválido.");
            }
        }
        private static void GerarArquivo()
        {
            try
            {
                Console.WriteLine("Escolha a versão do leiaute:");
                Console.WriteLine("1 - Leiaute 01");
                Console.WriteLine("2 - Leiaute 02");
                var escolha = Console.ReadLine();

                IGeradorArquivo gerador = null;

                switch (escolha)
                {
                    case "1":
                        gerador = new GeradorArquivoBase();
                        break;

                    case "2":
                        gerador = new GeradorArquivoVersao2();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Digite 1, 2 ou 3.");
                        return;
                }

                var dados = JsonRepository.LoadEmpresas(_jsonPath);
                var fileName = $"saida_leiaute_{escolha}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                var fullPath = Path.Combine(_outputDir, fileName);



                gerador.Gerar(dados, fullPath);
                Console.WriteLine("Arquivo gerado em: " + fullPath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gerar arquivo: " + ex.Message);
            }
        }
        static void GerarExemplos()
        {
            Console.WriteLine("Somente para base-dados e base-dados-v2");
            Console.WriteLine("Exemplo Leiaute 01:");
            Console.WriteLine("00|CNPJEMPRESA|NOMEDAEMPRESA|TELEFONE");
            Console.WriteLine("01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO");
            Console.WriteLine("02|DESCRIÇÃOITEM|VALORITEM");
            Console.WriteLine("99|QUANTIDADELINHASDOTIPO\n");

            Console.WriteLine("Somente para base-dados-v2");
            Console.WriteLine("Exemplo Leiaute 02:");
            Console.WriteLine("00|CNPJEMPRESA|NOMEDAEMPRESA|TELEFONE");
            Console.WriteLine("01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO");
            Console.WriteLine("02|NUMEROITEM|DESCRIÇÃOITEM|VALORITEM");
            Console.WriteLine("03|NUMEROCATEGORIA|DESCRIÇÃOCATEGORIA");            
            Console.WriteLine("99|QUANTIDADELINHASDOTIPO\n");
        }
    }
}
