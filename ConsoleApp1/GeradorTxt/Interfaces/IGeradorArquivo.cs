using System.Collections.Generic;
using GeradorTxt.Domain;

namespace GeradorTxt.Interfaces
{
    public interface IGeradorArquivo
    {
        void Gerar(List<Empresa> empresas, string outputPath);
    }
}
