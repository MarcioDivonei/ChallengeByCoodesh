using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Tests.Infra
{
    [TestFixture]
    public class JsonEmpresaRepositoryTests
    {
        [Test]
        public void Carregar_DeveDeserializarJson()
        {
            var json = "[{\"CNPJ\":\"123\",\"Nome\":\"Empresa Teste\",\"Telefone\":\"9999\",\"Documentos\":[]}]";
            var path = Path.GetTempFileName();
            File.WriteAllText(path, json);

            var repo = new JsonEmpresaRepository();
            var empresas = repo.Carregar(path);

            Assert.AreEqual(1, empresas.Count);
            Assert.AreEqual("123", empresas[0].CNPJ);
            Assert.AreEqual("Empresa Teste", empresas[0].Nome);
        }
    }

}
