using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PortalTeste.Models
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }
        public String nm_categoria { get; set; }
    }
}