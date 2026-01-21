using GeradorTxt.GeradorLeiaute;
using GeradorTxt.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Tests.Factory
{
    [TestFixture]
    public class GeradorLeiauteFactoryTests
    {
        [Test]
        public void Criar_Leiaute1_DeveRetornarGeradorArquivoLeiaute01()
        {
            var factory = new GeradorLeiauteFactory();
            var gerador = factory.Criar("1");
            Assert.IsInstanceOf<IGeradorArquivo>(gerador);
        }

        [Test]
        public void Criar_LeiauteInvalido_DeveLancarExcecao()
        {
            var factory = new GeradorLeiauteFactory();
            Assert.Throws<NotSupportedException>(() => factory.Criar("99"));
        }
    }

}
