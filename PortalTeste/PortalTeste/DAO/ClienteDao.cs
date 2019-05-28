using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Data.Entity;
using PortalTeste.Models;


namespace PortalTeste.DAO
{
    public class ClienteDao
    {

        /// <summary>
        /// Autentica o acesso ao site.
        /// </summary>
        /// <param name="login">recebe o login de acesso.</param>
        /// <param name="senha">Recebe a senha de acesso.</param>
        /// <returns></returns>
        public Cliente Autentica(String login, String senha)
        {
            using (var dao = new EntidadeContext())
            {
                return dao.clientes.Where(p => p.login_email_cli == login && p.senha_cli == senha).FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para cadastrar cliente.
        /// </summary>
        /// <param name="id_pessao">Recebe o id da pessoa.</param>
        /// <param name="cli">Recebe os dados do cliente.</param>
        public void cadastroCliente(int id_pessao,Cliente cli)
        {
            using (var dao = new EntidadeContext())
            {
                cli.id_pessoa = id_pessao;
                dao.clientes.Add(cli);
                dao.SaveChanges();
            }
        }

    }
}