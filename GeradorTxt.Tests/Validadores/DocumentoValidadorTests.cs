using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorTxt.Domain;
using GeradorTxt.Validadores;
using NUnit.Framework;


namespace GeradorTxt.Tests.Validadores
{
    [TestFixture]
    public class DocumentoValidadorTests
    {
        [Test]
        public void Validar_ValoresCorretos_NaoLancaExcecao()
        {
            var doc = new Documento { Numero = "001", Valor = 50 };
            doc.Itens.Add(new ItemDocumento { Valor = 20 });
            doc.Itens.Add(new ItemDocumento { Valor = 30 });

            var emp = new Empresa { Nome = "Teste" };
            var validador = new DocumentoValidador();

            Assert.DoesNotThrow(() => validador.Validar(doc, emp));
        }

        [Test]
        public void Validar_ValoresIncorretos_LancaExcecao()
        {
            var doc = new Documento { Numero = "001", Valor = 100 };
            doc.Itens.Add(new ItemDocumento { Valor = 20 });
            doc.Itens.Add(new ItemDocumento { Valor = 30 });

            var emp = new Empresa { Nome = "Teste" };
            var validador = new DocumentoValidador();

            Assert.Throws<InvalidOperationException>(() => validador.Validar(doc, emp));
        }
    }

}
