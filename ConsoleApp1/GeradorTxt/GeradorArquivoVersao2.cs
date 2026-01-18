using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt
{
    public class GeradorArquivoVersao2 : IGeradorArquivo
    {
        public void Gerar(List<Empresa> empresas, string outputPath)
        {
            var sb = new StringBuilder();
            int linhas00 = 0, linhas01 = 0, linhas02 = 0, linhas03 = 0;
            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp); //Empresa
                linhas00++;
                foreach (var doc in emp.Documentos)
                {
                    if (doc.Itens.Sum(i => i.Valor) != doc.Valor)
                    {
                        throw new Exception($"Documento {doc.Numero} da empresa {emp.Nome} possui soma de itens divergente.");
                    }
                    EscreverTipo01(sb, doc);//Documento
                    linhas01++;
                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);//ItemDocumento
                        linhas02++;
                        foreach (var cat in item.Categorias)
                        {
                            EscreverTipo03(sb, cat);
                            linhas03++;
                        }
                    }
                }
            }
            sb.AppendLine($"09|00|{linhas00}");
            sb.AppendLine($"09|01|{linhas01}");
            sb.AppendLine($"09|02|{linhas02}");
            sb.AppendLine($"09|03|{linhas03}");
            int totalLinhas = linhas00 + linhas01 + linhas02 + linhas03;
            sb.AppendLine($"99|{totalLinhas}");
            
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
        }

        protected void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|NUMEROITEM|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.NumeroItem).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }
        protected void EscreverTipo03(StringBuilder sb, CategoriaItem cat) 
        {
            // 03|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            sb.Append("03").Append("|")
              .Append(cat.numeroCategoria).Append("|")
              .Append(cat.descricaoCategoria).AppendLine();
        }
    }
}

