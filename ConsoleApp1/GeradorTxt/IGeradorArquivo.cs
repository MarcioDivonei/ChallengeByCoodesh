using System.Collections.Generic;

namespace GeradorTxt
{
    public interface IGeradorArquivo
    {
        void Gerar(List<Empresa> empresas, string outputPath);
    }
}