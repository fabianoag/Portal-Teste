using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Tipo_pessoa
    {
        [Key]
        public int id_tipo_pessoa { get; set; }
        public String nm_tipo_pessoa { get; set; }
    }
}