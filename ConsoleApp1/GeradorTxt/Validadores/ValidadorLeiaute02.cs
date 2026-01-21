using GeradorTxt.Domain;
using GeradorTxt.Interfaces;
using System;
using System.Collections.Generic;

namespace GeradorTxt.Validadores
{
    public class ValidadorLeiaute02 : IValidadorLeiaute
    {
        public void Validar(List<Empresa> empresas)
        {
            foreach (var emp in empresas)
            {
                if (emp.Documentos == null || emp.Documentos.Count == 0)
                    throw new InvalidOperationException($"Empresa {emp.Nome} sem documentos.");

                foreach (var doc in emp.Documentos)
                {
                    if (doc.Itens == null || doc.Itens.Count == 0)
                        throw new InvalidOperationException(
                            $"Documento {doc.Numero} da empresa {emp.Nome} sem itens."
                        );

                    foreach (var item in doc.Itens)
                    {
                        if (item.Categorias == null || item.Categorias.Count == 0)
                            throw new InvalidOperationException(
                                $"Item {item.NumeroItem} do documento {doc.Numero} sem categorias."
                            );
                    }
                }
            }
        }
    }
}
