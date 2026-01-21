using GeradorTxt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt.Domain
{
    public class ItemDocumento
    {
        public int NumeroItem { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public List<CategoriaItem> Categorias { get; set; } = new List<CategoriaItem>();
    }
}
