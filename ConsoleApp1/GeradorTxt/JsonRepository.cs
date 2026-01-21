using GeradorTxt.Interfaces;
using GeradorTxt.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace GeradorTxt
{
    public class JsonEmpresaRepository : IEmpresaRepository
    {
        public List<Empresa> Carregar(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Empresa>>(json);
        }

    }
}
