using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTeste.Models.FiltraDados
{
    public class FiltroDesconto
    {
        //public int id_categoria { get; set; }
        public string pesq_filtro_desconto { get; set; }
        public string nm_filtro_desconto { get; set; }
        public int? total { get; set; }
    }
}