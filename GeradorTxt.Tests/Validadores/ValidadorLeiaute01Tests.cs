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
    public class ValidadorLeiaute01Tests
    {
        [Test]
        public void Validar_EmpresaSemDocumentos_LancaExcecao()
        {
            var empresas = new List<Empresa> { new Empresa { Nome = "Teste" } };
            var validador = new ValidadorLeiaute01();

            Assert.Throws<InvalidOperationException>(() => validador.Validar(empresas));
        }

        [Test]
        public void Validar_DocumentoSemItens_LancaExcecao()
        {
            var empresas = new List<Empresa>
        {
            new Empresa {
                Nome = "Teste",
                Documentos = new List<Documento> { new Documento { Numero = "001" } }
            }
        };
            var validador = new ValidadorLeiaute01();

            Assert.Throws<InvalidOperationException>(() => validador.Validar(empresas));
        }
    }

}
