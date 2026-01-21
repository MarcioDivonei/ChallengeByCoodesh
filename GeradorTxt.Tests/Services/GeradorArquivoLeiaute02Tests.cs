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
    public class GeradorArquivoLeiaute02Tests
    {
        [Test]
        public void Gerar_DeveGerarLinhaDeCategoria()
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
                            new ItemDocumento {
                                NumeroItem = 1,
                                Descricao = "Produto A",
                                Valor = 100,
                                Categorias = new List<CategoriaItem> {
                                    new CategoriaItem { NumeroCategoria = 10, DescricaoCategoria = "Categoria X" }
                                }
                            }
                        }
                    }
                }
            }
        };

            var gerador = new GeradorArquivoLeiaute02(new DocumentoValidador(), new ValidadorLeiaute02());
            var path = Path.GetTempFileName();

            gerador.Gerar(empresas, path);

            var conteudo = File.ReadAllText(path);
            Assert.IsTrue(conteudo.Contains("03|10|Categoria X"));
        }
    }

}
