﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTeste.Models.FiltraDados
{
    
    public class FiltroPesquisa
    {
        public int id { get; set; }
        public String busca { get; set; }  
        public int or { get; set; }
        public String desc { get; set; }
        public String preco { get; set; }
    }
}