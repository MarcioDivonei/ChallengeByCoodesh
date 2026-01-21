using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorTxt.Domain;

namespace GeradorTxt.Interfaces
{
    public interface IDocumentoValidador
    {
        void Validar(Documento documento, Empresa empresa);
    }
}
