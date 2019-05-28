using PortalTeste.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTeste.App_Start
{
    public class FilterConfig
    {
        /// <summary>
        /// Regitro do filtro para ser inserido no Global.asax, que e a configurações globais da aplicação.
        /// </summary>
        /// <param name="filters">Tipo de filtro que e passado para o registro.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AutorizacaoFilterAttribute());
        }
    }
}