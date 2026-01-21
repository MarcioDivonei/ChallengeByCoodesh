using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeradorTxt.Interfaces
{
    public interface IGeradorLeiaute
    {
        IGeradorArquivo Criar(string layout);
    }
}
