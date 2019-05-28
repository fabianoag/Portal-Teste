using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Subcategoria
    {
        [Key]
        public int id_subcategoria { get; set; }
        public int id_categoria { get; set; }
		public string nm_subcategoria { get; set; }
    }
}