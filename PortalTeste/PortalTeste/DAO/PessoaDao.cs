using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTeste.DAO;
using PortalTeste.Models;

namespace PortalTeste.DAO
{
    public class PessoaDao
    {

        public Pessoa cadastrarPessoa(Pessoa ps)
        {
            using (var dao = new EntidadeContext())
            {
                ps.data_cadastro_pessoa = DateTime.Now;
                dao.pessoas.Add(ps);
                dao.SaveChanges();                
            }
            return ps;
        }
    }
}