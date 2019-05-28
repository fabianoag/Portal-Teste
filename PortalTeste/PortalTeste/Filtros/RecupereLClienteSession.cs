using PortalTeste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTeste.Filtros
{
    public class RecupereLClienteSession
    {

        public Cliente RecLogin()
        {
            object result = HttpContext.Current.Session["usuarioLogado"];
            if (result == null)
            {
                return null;
            }
            else
            {
                return (Cliente)result;
            }
        }
    }
}