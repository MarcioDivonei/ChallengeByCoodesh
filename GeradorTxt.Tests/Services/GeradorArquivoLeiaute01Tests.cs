using GeradorTxt.Domain;
using GeradorTxt.Services;
using GeradorTxt.Validadores;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Tests.Services
{
    [TestFixture]
    public class GeradorArquivoLeiaute01Tests
    {
        [Test]
        public void Gerar_DeveCriarArquivoComLinhasEsperadas()
        {
            var empresas = new List<Empresa>
        {
            new Empresa {
                CNPJ = "123",
                Nome = "Empresa Teste",
                Telefone = "9999",
                Documentos = new List<Documento> {
                    new Documento {
                        Modelo = "NF",
                        Numero = "001",
                        Valor = 100,
                        Itens = new List<ItemDocumento> {
                            new ItemDocumento { NumeroItem = 1, Descricao = "Produto A", Valor = 100 }
                        }
                    }
                }
            }
        };

            var gerador = new GeradorArquivoLeiaute01(new ValidadorLeiaute01(), new DocumentoValidador());
            var path = Path.GetTempFileName();

            gerador.Gerar(empresas, path);

            var conteudo = File.ReadAllText(path);
            Console.WriteLine(conteudo);
            var conteudoNormalizado = conteudo.Replace(",", ".");
            Assert.IsTrue(conteudo.Contains("00|123|Empresa Teste|9999"));
            Assert.IsTrue(conteudoNormalizado.Contains("01|NF|001|100.00"));
            Assert.IsTrue(conteudoNormalizado.Contains("02|1|Produto A|100.00"));
            Assert.IsTrue(conteudo.Contains("99|3"));
        }
    }

}
