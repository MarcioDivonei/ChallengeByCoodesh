using System;
using GeradorTxt.Interfaces;
using GeradorTxt.Services;
using GeradorTxt.Validadores;

namespace GeradorTxt.GeradorLeiaute
{
    public class GeradorLeiauteFactory : IGeradorLeiaute
    {
        public IGeradorArquivo Criar(string leiaute)
        {
            if (string.IsNullOrEmpty(leiaute))
                throw new ArgumentException("Layout não informado");
            var documentoValidador = new DocumentoValidador();
            var leiauteValidador = new ValidadorLeiaute01();
            var leiauteValidador2 = new ValidadorLeiaute02();
            switch (leiaute)
            {
                case "1":
                    

                    return new GeradorArquivoLeiaute01(
                    leiauteValidador,
                    documentoValidador
                    );
                case "2":
                    

                    return new GeradorArquivoLeiaute02(
                        documentoValidador,
                        leiauteValidador2
                    );

                default:
                    throw new NotSupportedException(
                        $"Layout {leiaute} não é suportado"
                    );
            }
        }
    }
}
