using PortalTeste.DAO;//Referência do DAO
using PortalTeste.Filtros;
using PortalTeste.Models;//Referência das Entidades
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;//

namespace PortalTeste.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autentica(String login, String senha)
        {
            Cliente user;
            using (null)
            {
                var dao = new ClienteDao();
                user = dao.Autentica(login, senha);
            }
            if (user != null)
            {
                Session["usuarioLogado"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sair()
        {
            Session["usuarioLogado"] = null;
            return RedirectToAction("Index", "Home");
        }

        public static Cliente usuario()
        {
            Cliente c = new Cliente();
            RecupereLClienteSession rec = new Filtros.RecupereLClienteSession();
            var a = rec.RecLogin();
            if (a != null)
            {
                c = a;
            }

            return c;
        }

        /// <summary>
        /// Faz o cadatro do cliente.
        /// </summary>
        /// <param name="pessoa">Recebe os dados da pessoa.</param>
        /// <param name="cli">Recebe oss dados do cliente.</param>
        /// <returns></returns>
        public ActionResult cadastrar(Pessoa pessoa, Cliente cli, String tipo_cliente)
        {
            IList<Tipo_pessoa> tp;
            using (var dao = new EntidadeContext())
            {
                tp = dao.tipo_pessoa.ToList();
            }

            if (tipo_cliente != null)
            {
                if (tipo_cliente == "Juridico")
                {
                    cli.fisico_cli = false;
                    cli.juridico_cli = true;
                }
                else
                {
                    cli.fisico_cli = true;
                    cli.juridico_cli = false;
                }
            }


            ViewBag.tipo_Pessoas = tp;
            ViewBag.pessoa = pessoa;
            ViewBag.cliente = cli;
            ViewBag.tipo_cliente = tipo_cliente;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Pessoa pessoa, Cliente cli, String tipo_cliente,String conf_senha)
        {
            if (tipo_cliente != null)
            {
                if (tipo_cliente == "Juridico")
                {
                    cli.fisico_cli = false;
                    cli.juridico_cli = true;
                }
                else
                {
                    cli.fisico_cli = true;
                    cli.juridico_cli = false;
                }
            }

            if (tipo_cliente == null)
            {
                ModelState.AddModelError("select.Tipo_cliente", "O campo tipo é obrigátorio.");
            }
            if (conf_senha == "")
            {
                ModelState.AddModelError("conf_senha", "O campo redigite senha é obrigátorio.");
            }
            if (conf_senha != cli.senha_cli&&cli.senha_cli != null)
            {
                ModelState.AddModelError("conf_senha", "Senhas diferentes.");
            }

            //ModelState.IsValid = verifica se o evento do form e do tipo post ou seja e foi enviado.
            if (ModelState.IsValid)
            {
                Pessoa ps = null;
                ps = null;
                PessoaDao pessoaDao = new PessoaDao();
                ps = pessoaDao.cadastrarPessoa(pessoa);

                ClienteDao clienteDao = new ClienteDao();
                clienteDao.cadastroCliente(ps.id_pessoa, cli);
            }
            else
            {
                IList<Tipo_pessoa> tp;
                using (var dao = new EntidadeContext())
                {
                    tp = dao.tipo_pessoa.ToList();
                }

                ViewBag.tipo_Pessoas = tp;
                ViewBag.pessoa = pessoa;
                ViewBag.cliente = cli;
                ViewBag.tipo_cliente = tipo_cliente;
                return View("cadastrar");
            }

            return View();
        }

    }
}