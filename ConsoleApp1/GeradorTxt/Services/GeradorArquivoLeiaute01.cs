using GeradorTxt.Interfaces;
using GeradorTxt.Domain;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GeradorTxt.Services
{
    public class GeradorArquivoLeiaute01 : IGeradorArquivo
    {
        private readonly IValidadorLeiaute _validadorLeiaute;
        private readonly IDocumentoValidador _validadorDocumento;

        public GeradorArquivoLeiaute01 (IValidadorLeiaute validadorLeiaute, IDocumentoValidador documentoValidador)
        {
            _validadorLeiaute = validadorLeiaute;
            _validadorDocumento = documentoValidador;
        }
        public void Gerar(List<Empresa> empresas, string outputPath)
        {
            _validadorLeiaute.Validar(empresas);
            var sb = new StringBuilder();
            int linhas00 = 0, linhas01 = 0, linhas02 = 0;

            foreach (var emp in empresas)
            {
                sb.AppendLine($"00|{emp.CNPJ}|{emp.Nome}|{emp.Telefone}");
                linhas00++;

                foreach (var doc in emp.Documentos)
                {
                    _validadorDocumento.Validar(doc,emp);

                    sb.AppendLine($"01|{doc.Modelo}|{doc.Numero}|{doc.Valor:0.00}");
                    linhas01++;

                    foreach (var item in doc.Itens)
                    {
                        sb.AppendLine($"02|{item.NumeroItem}|{item.Descricao}|{item.Valor:0.00}");
                        linhas02++;
                    }
                }
            }

            sb.AppendLine($"09|00|{linhas00}");
            sb.AppendLine($"09|01|{linhas01}");
            sb.AppendLine($"09|02|{linhas02}");
            sb.AppendLine($"99|{linhas00 + linhas01 + linhas02}");

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }
    }
}
