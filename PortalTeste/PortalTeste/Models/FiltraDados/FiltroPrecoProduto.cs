using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTeste.Models.FiltraDados
{
    public class FiltroPrecoProduto
    {
        public int id_categoria { get; set; }
        public string pesq_filtro_precoProduto { get; set; }
        public string nm_filtro_precoProduto { get; set; }
        public int? total { get; set; }
    }
}