using GeradorTxt.Domain;
using GeradorTxt.Validadores;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Tests.Validadores
{
    [TestFixture]
    public class ValidadorLeiaute02Tests
    {
        [Test]
        public void Validar_ItemSemCategorias_LancaExcecao()
        {
            var empresas = new List<Empresa>
        {
            new Empresa {
                Nome = "Teste",
                Documentos = new List<Documento> {
                    new Documento {
                        Numero = "001",
                        Itens = new List<ItemDocumento> {
                            new ItemDocumento { NumeroItem = 1 }
                        }
                    }
                }
            }
        };
            var validador = new ValidadorLeiaute02();

            Assert.Throws<InvalidOperationException>(() => validador.Validar(empresas));
        }
    }

}
