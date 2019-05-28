using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTeste.Models.FiltraDados
{
    /// <summary>
    /// A "order" representa a horientação entre a Categória, Subcategoria e Subcategoria2.
    /// 1 = Categória, 2 = Subcategoria, 3 = Subcategoria2.
    /// </summary>
    public class FiltroOrder
    {
        public Categoria categorias { get; set; }
        public Subcategoria Subcategorias { get; set; }
        public Subcategoria2 Subcategorias2 { get; set; }
        public int? total { get; set; }
        public int? or { get; set; }
    }
}