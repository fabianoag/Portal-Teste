using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Marca_produto
    {
        [Key]
        public int id_marca { get; set; }
		public string nm_marca { get; set; }
    }
}