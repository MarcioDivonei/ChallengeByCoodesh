using GeradorTxt.Interfaces;
using GeradorTxt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Validadores
{
    public class DocumentoValidador : IDocumentoValidador
    {
        public void Validar(Documento doc, Empresa emp)
        {
            var soma = doc.Itens.Sum(i => i.Valor);

            if (soma != doc.Valor)
            {
                throw new InvalidOperationException(
                    $"Documento {doc.Numero} da empresa {emp.Nome} possui valor inválido."
                );
            }
        }
    }
}
