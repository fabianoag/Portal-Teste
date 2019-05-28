using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Faixa_etaria
    {
        [Key]
        public int cod_faixa_etaria { get; set; }
		public String nm_faixa_etaria { get; set; }
		public byte? idade { get; set; }
    }
}