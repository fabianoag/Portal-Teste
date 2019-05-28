using PortalTeste.DAO;
using PortalTeste.Models;
using PortalTeste.Models.FiltraDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTeste.Filtros;
using PagedList;

namespace PortalTeste.Controllers
{
    [AutorizacaoFilter]
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            ProdutoDao dao = new ProdutoDao();
            IList<Produto> produtos = dao.lista();
            return View(produtos);
        }

        public ActionResult ProdutoToLinq()
        {
            ProdutoDao dao = new ProdutoDao();
            IList<Produto> produtos = dao.lista();
            ViewBag.produtos = produtos;

            IList<Categoria> categoriaList;
            IList<FiltroOrder> CatF;
            dao.listaNav(out categoriaList ,out CatF);
            ViewBag.categorias = categoriaList;
            ViewBag.CatF = CatF;

            IList<viewteste> listaView = dao.listaView();
            ViewBag.listeView = listaView;


            IList<Lista_Produtos2> listaProdutos;
            IList<FiltroDesconto> filtroDesconto;
            IList<FiltroPrecoProduto> filtroPrecoProduto;
            IList<FiltroOrder> filtroCategoria;
            dao.listaProdutos(out listaProdutos, out filtroDesconto, out filtroPrecoProduto, out filtroCategoria);
            ViewBag.listaProdutos = listaProdutos;
            ViewBag.filtroDesconto = filtroDesconto;
            ViewBag.filtroPrecoProduto = filtroPrecoProduto;
            ViewBag.filtroCategoria = filtroCategoria;

            return View();
        }

        public ActionResult grid(FiltroPesquisa pesq, int page = 1, int pageSize = 4)
        {
            PagedList<Lista_Produtos2> pageList;
            using (null)
            {
                ProdutoDao dao = new ProdutoDao();          
                IList<Lista_Produtos2> listaProdutos;
                IList<FiltroDesconto> filtroDesconto;
                IList<FiltroPrecoProduto> filtroPrecoProduto;
                IList<FiltroOrder> filtroOrder;
                dao.ltProdutos(pesq, out listaProdutos, out filtroDesconto, out filtroPrecoProduto, out filtroOrder);

                //Paginação da webSite.
                PagedList<Lista_Produtos2> pl = new PagedList<Lista_Produtos2>(listaProdutos, page, pageSize);
                //ViewBag.listaProdutos = pageList;

                pageList = pl; 

                ViewBag.filtroDesconto = filtroDesconto;
                ViewBag.filtroPrecoProduto = filtroPrecoProduto;
                ViewBag.foCount = filtroOrder.Count();
                ViewBag.filtroOrder = filtroOrder;

                ViewBag.pesq = pesq;
                ViewBag.VerificaFDesc = pesq.desc;
                ViewBag.VerificaFPProduto = pesq.preco;
                ViewBag.page = page;
                //ViewBag.order = Order;
            }
            return View(pageList);
        }
    }
}