using GeradorTxt.Domain;
using GeradorTxt.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeradorTxt.Services
{
    public class GeradorArquivoLeiaute02 : IGeradorArquivo
    {
        private readonly IDocumentoValidador _validadorDocumentos;
        private readonly IValidadorLeiaute _validadorLeiaute;


        public GeradorArquivoLeiaute02(IDocumentoValidador validador,
            IValidadorLeiaute validadorLeiaute)
        {   

            _validadorDocumentos = validador;
            _validadorLeiaute = validadorLeiaute;
        }

        public void Gerar(List<Empresa> empresas, string outputPath)
        {
            _validadorLeiaute.Validar(empresas);
            var sb = new StringBuilder();
            int linhas00 = 0, linhas01 = 0, linhas02 = 0, linhas03 = 0;

            foreach (var emp in empresas)
            {
                sb.AppendLine($"00|{emp.CNPJ}|{emp.Nome}|{emp.Telefone}");
                linhas00++;

                foreach (var doc in emp.Documentos)
                {
                    _validadorDocumentos.Validar(doc, emp);

                    sb.AppendLine($"01|{doc.Modelo}|{doc.Numero}|{doc.Valor:0.00}");
                    linhas01++;

                    foreach (var item in doc.Itens)
                    {
                        sb.AppendLine($"02|{item.NumeroItem}|{item.Descricao}|{item.Valor:0.00}");
                        linhas02++;

                        foreach (var cat in item.Categorias)
                        {
                            sb.AppendLine($"03|{cat.NumeroCategoria}|{cat.DescricaoCategoria}");
                            linhas03++;
                        }
                    }
                }
            }

            sb.AppendLine($"09|00|{linhas00}");
            sb.AppendLine($"09|01|{linhas01}");
            sb.AppendLine($"09|02|{linhas02}");
            sb.AppendLine($"09|03|{linhas03}");
            sb.AppendLine($"99|{linhas00 + linhas01 + linhas02 + linhas03}");

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }
       

    }
}
