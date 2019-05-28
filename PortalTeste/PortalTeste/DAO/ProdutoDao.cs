using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTeste.Models;
using PortalTeste.Models.FiltraDados;
using Microsoft.Data.Entity;
using System.Text.RegularExpressions;

namespace PortalTeste.DAO
{
    public class ProdutoDao
    {
        /// <summary>
        /// Trás uma lista de produtos.
        /// </summary>C:\Users\Fabiano2\Documents\Visual Studio 2015\Projects\PortalTeste\PortalTeste\Global.asax
        /// <returns>Retorna lista</returns>
        public IList<Produto> lista()
        {
            using (var dao = new EntidadeContext())
            {
                return dao.produtos
                    .Include(p => p.categoria)
                    .Include(p => p.sub_categoria)
                    .Include(p => p.sub_categoria2)
                    .Include(p => p.faixa_etaria)
                    .Include(p => p.marca)
                    .ToList();
            }

        }

        #region Método (listaNav)
        public void listaNav(
            out IList<Categoria> Icategoria,
            out IList<FiltroOrder> IcategoriaFiltro)
        {

            #region LISTA CATEGORIAS
            //Funciona perfeitamente
            IList<Produto> produtos = lista();

            Icategoria = (from a in produtos
                          select a.categoria).ToList();
            #endregion

            #region FILTRAR CATEGORIAS COM CONTAGEM
            var cFiltro = (from a in produtos
                           group a by a.categoria into gd
                           select new { Categoria = gd.Key, total = gd.Count() }).ToList();


            List<FiltroOrder> Cat = new List<FiltroOrder>();
            foreach (var gd in cFiltro)
            {
                FiltroOrder categoria = new FiltroOrder();
                Categoria c = new Categoria();
                c.id_categoria = gd.Categoria.id_categoria;
                c.nm_categoria = gd.Categoria.nm_categoria;

                categoria.categorias = c;
                categoria.total = Convert.ToInt32(gd.total);

                Cat.Add(categoria);
            }
            IcategoriaFiltro = Cat;
            #endregion
        }
        #endregion

        #region Método (LISTA DE PRODUTOS COM VIEW SQL (viewTeste))
        public IList<viewteste> listaView()
        {
            using (var dao = new EntidadeContext())
            {
                return dao.viewTeste.ToList();
            }
        }
        #endregion

        #region Método (LISTA DE PRODUTOS COM VIEW SQL (Lista_Produtos2))
        public void listaProdutos(out IList<Lista_Produtos2> IlistaProdutos,
                                  out IList<FiltroDesconto> IfiltroDesconto,
                                  out IList<FiltroPrecoProduto> IfiltroPrecoProduto,
                                  out IList<FiltroOrder> IfiltroCategoria)
        {
            using (var dao = new EntidadeContext())
            {
                #region LISTA DE PRODUTOS
                IList<Lista_Produtos2> lista = dao.Lista_Produtos2.ToList();
                IlistaProdutos = lista;
                #endregion

                #region FILTRO DESCONTO
                var FiltroDesconto = (from gd in lista
                                      group gd by new
                                      {
                                          gd.pesq_filtro_desconto,
                                          gd.nm_filtro_desconto,
                                          gd.ordem_filtro_desconto
                                      }

                                     into grd
                                      where (grd.Key.nm_filtro_desconto != null)
                                      orderby grd.Key.ordem_filtro_desconto
                                      select new
                                      {
                                          grd.Key.pesq_filtro_desconto,
                                          grd.Key.nm_filtro_desconto,
                                          total = grd.Count()
                                      });

                List<FiltroDesconto> filtroDesconto = new List<FiltroDesconto>();
                foreach (var gd in FiltroDesconto)
                {
                    FiltroDesconto item = new Models.FiltraDados.FiltroDesconto();
                    item.pesq_filtro_desconto = gd.pesq_filtro_desconto;
                    item.nm_filtro_desconto = gd.nm_filtro_desconto;
                    item.total = gd.total;

                    filtroDesconto.Add(item);
                }
                IfiltroDesconto = filtroDesconto;
                #endregion

                #region FILTRO PREÇO PRODUTO

                var FiltroPrProduto = (from gd in lista
                                       group gd by new
                                       {
                                           gd.pesq_filtro_precoProduto,
                                           gd.nm_filtro_precoProduto,
                                           gd.ordem_filtro_precoProduto
                                       }

                     into grd
                                       where (grd.Key.nm_filtro_precoProduto != null)
                                       orderby grd.Key.ordem_filtro_precoProduto
                                       select new
                                       {
                                           grd.Key.pesq_filtro_precoProduto,
                                           grd.Key.nm_filtro_precoProduto,
                                           total = grd.Count()
                                       });

                List<FiltroPrecoProduto> filtroPrecoProduto = new List<FiltroPrecoProduto>();
                foreach (var gd in FiltroPrProduto)
                {
                    FiltroPrecoProduto item = new Models.FiltraDados.FiltroPrecoProduto();
                    item.pesq_filtro_precoProduto = gd.pesq_filtro_precoProduto;
                    item.nm_filtro_precoProduto = gd.nm_filtro_precoProduto;
                    item.total = gd.total;

                    filtroPrecoProduto.Add(item);
                }
                IfiltroPrecoProduto = filtroPrecoProduto;

                #endregion

                #region FILTRO CATEGÓRIA

                var FiltroCat = (from gd in lista
                                 group gd by new
                                 {
                                     gd.id_categoria,
                                     gd.nm_categoria
                                 }

                                       into grd
                                 where (grd.Key.nm_categoria != null)
                                 select new
                                 {
                                     grd.Key.id_categoria,
                                     grd.Key.nm_categoria,
                                     total = grd.Count()
                                 });

                List<FiltroOrder> filtroCategoria = new List<FiltroOrder>();
                foreach (var gd in FiltroCat)
                {
                    FiltroOrder item = new Models.FiltraDados.FiltroOrder();
                    Categoria c = new Categoria()
                    {
                        id_categoria = gd.id_categoria,
                        nm_categoria = gd.nm_categoria
                    };
                    item.categorias = c;
                    item.total = gd.total;

                    filtroCategoria.Add(item);
                }
                IfiltroCategoria = filtroCategoria;

                #endregion
            }
        }
        #endregion

        #region Método (LISTA DE PRODUTOS COM VIEW SQL GRID (Lista_Produtos2))
        public void ltProdutos(FiltroPesquisa pesq,
                                  out IList<Lista_Produtos2> IlistaProdutos,
                                  out IList<FiltroDesconto> IfiltroDesconto,
                                  out IList<FiltroPrecoProduto> IfiltroPrecoProduto,
                                  out IList<FiltroOrder> IfiltroOrder)
        {

            IList<String> IDesc = null;
            if (!String.IsNullOrEmpty(pesq.desc))
            {
                IDesc = pesq.desc.Split(',');
            }

            IList<String> IPreco = null;
            if (!String.IsNullOrEmpty(pesq.preco))
            {
                IPreco = pesq.preco.Split(',');
            }

            using (var dao = new EntidadeContext())
            {
                #region LISTA DE PRODUTOS

                //Order estabelece tipo de categória que ele é.
                //Ex: categória, Subcategoria ou Subcategoria2.
                IList<Lista_Produtos2> lista = dao.Lista_Produtos2.ToList();
                #endregion

                #region FILTRO CATEGÓRIA

                //if (pesq.busca != null)
                if (!String.IsNullOrEmpty(pesq.busca))
                {
                    List<Lista_Produtos2> linha = new List<Lista_Produtos2>();
                    foreach (var lt in lista)
                    {
                        bool item = Regex.IsMatch(lt.Titulo_produto.ToLower(), pesq.busca.ToLower());
                        if (item)
                        {
                            linha.Add(lt);
                        }
                    }
                    lista = linha;
                }

                var FiltroCat = (from gd in lista
                                 select new
                                 {
                                     gd.id_categoria,
                                     gd.nm_categoria,
                                     gd.id_subcategoria,
                                     gd.nm_subcategoria,
                                     gd.id_subcategoria2,
                                     gd.nm_subcategoria2,
                                 });

                List<FiltroOrder> filtroOrder = new List<FiltroOrder>();
                foreach (var gd in FiltroCat)
                {

                    FiltroOrder item = new Models.FiltraDados.FiltroOrder();
                    Categoria cat = new Categoria()
                    {
                        id_categoria = gd.id_categoria,
                        nm_categoria = gd.nm_categoria
                    };
                    Subcategoria subcat = new Subcategoria()
                    {
                        id_categoria = gd.id_categoria,
                        id_subcategoria = gd.id_subcategoria,
                        nm_subcategoria = gd.nm_subcategoria
                    };
                    Subcategoria2 subcat2 = new Subcategoria2()
                    {
                        id_categoria = gd.id_categoria,
                        id_subcategoria2 = gd.id_subcategoria2,
                        nm_subcategoria2 = gd.nm_subcategoria2
                    };

                    item.categorias = cat;
                    item.Subcategorias = subcat;
                    item.Subcategorias2 = subcat2;

                    filtroOrder.Add(item);
                }

                //OBS: O FINAL DESSE CÓDIGO SE ENCONTRA EM "FILTRO (PESQUISA DA PÁGINA)"
                #endregion

                #region FILTRO (PESQUISA DA PÁGINA)

                //OBS: A "order" representa a horientação entre a Categória, Subcategoria e Subcategoria2
                // 1 = Categória, 2 = Subcategoria, 3 = Subcategoria2.
                List<FiltroOrder> itemOrder = new List<FiltroOrder>();
                switch (pesq.or)
                {
                    //E a todo categória.
                    case 0:
                        //Lista de produtos.
                        IlistaProdutos = listaProdutosCheckList(lista, IDesc, IPreco);

                        //Filtro de desconto.
                        IfiltroDesconto = filtroDescReturno(lista);

                        //Filtro preço produto.
                        IfiltroPrecoProduto = FiltroPrProdutoRetorno(lista, IDesc);

                        //Filtro categória.
                        var cat = (from gd in filtroOrder.Where(p => p.categorias.id_categoria != 1)
                                   group gd by new
                                   {
                                       gd.categorias.id_categoria,
                                       gd.categorias.nm_categoria
                                   }
                                  into grd
                                   where (grd.Key.nm_categoria != null)
                                   select new
                                   {
                                       grd.Key.id_categoria,
                                       grd.Key.nm_categoria,
                                       total = grd.Count()
                                   });
                        foreach (var i in cat)
                        {
                            FiltroOrder ab = new FiltroOrder();
                            Categoria cat1 = new Categoria()
                            {
                                id_categoria = i.id_categoria,
                                nm_categoria = i.nm_categoria
                            };
                            ab.categorias = cat1;
                            ab.total = i.total;

                            //Número da próxima order para ser inserido no botão.
                            ab.or = 1;
                            itemOrder.Add(ab);
                        }
                        IfiltroOrder = itemOrder;
                        break;
                    //E a SubCategória.
                    case 1:

                        //Lista de produtos.
                        IlistaProdutos = listaProdutosCheckList(lista.Where(list => list.id_categoria == pesq.id).ToList(), IDesc, IPreco);

                        //Filtro de desconto.
                        IfiltroDesconto = filtroDescReturno(lista.Where(list => list.id_categoria == pesq.id).ToList());

                        //Filtro preço produto.
                        IfiltroPrecoProduto = FiltroPrProdutoRetorno(lista.Where(list => list.id_categoria == pesq.id).ToList(), IDesc);

                        //Filtro categória.
                        //IfiltroOrder = filtroOrder.Where(list => list.categorias.id_categoria == pesq.id).ToList();
                        var subCat = (from gd in filtroOrder.Where(p => p.Subcategorias.id_subcategoria != 1)
                                      group gd by new
                                      {
                                          gd.categorias.id_categoria,
                                          gd.Subcategorias.id_subcategoria,
                                          gd.Subcategorias.nm_subcategoria,
                                      } into grd
                                      where (grd.Key.nm_subcategoria != null && grd.Key.id_categoria == pesq.id)
                                      select new
                                      {
                                          grd.Key.id_subcategoria,
                                          grd.Key.nm_subcategoria,
                                          total = grd.Count()
                                      });
                        foreach (var i in subCat)
                        {
                            FiltroOrder ab = new FiltroOrder();
                            Subcategoria subCat1 = new Subcategoria()
                            {
                                id_subcategoria = i.id_subcategoria,
                                nm_subcategoria = i.nm_subcategoria
                            };
                            ab.Subcategorias = subCat1;
                            ab.total = i.total;

                            //Número da próxima order para ser inserido no botão.
                            ab.or = pesq.or + 1;
                            itemOrder.Add(ab);
                        }
                        IfiltroOrder = itemOrder;
                        break;
                    //E a Subcategoria2.
                    case 2:
                        //Lista de produtos.
                        IlistaProdutos = listaProdutosCheckList(lista.Where(list => list.id_subcategoria == pesq.id).ToList(), IDesc, IPreco);

                        //Filtro de desconto
                        IfiltroDesconto = filtroDescReturno(lista.Where(list => list.id_subcategoria == pesq.id).ToList()); ;

                        //Filtro preço produto.
                        IfiltroPrecoProduto = FiltroPrProdutoRetorno(lista.Where(list => list.id_subcategoria == pesq.id).ToList(), IDesc);

                        //Filtro categória.
                        //IfiltroOrder = filtroOrder;
                        var subCat2 = (from gd in filtroOrder.Where(p => p.Subcategorias.id_subcategoria != 1 && p.Subcategorias2.id_subcategoria2 != 1)
                                       group gd by new
                                       {
                                           gd.Subcategorias.id_subcategoria,
                                           gd.Subcategorias2.id_subcategoria2,
                                           gd.Subcategorias2.nm_subcategoria2
                                       } into grd
                                       where (grd.Key.nm_subcategoria2 != null && grd.Key.id_subcategoria == pesq.id)
                                       select new
                                       {
                                           grd.Key.id_subcategoria2,
                                           grd.Key.nm_subcategoria2,
                                           total = grd.Count()
                                       });
                        foreach (var i in subCat2)
                        {
                            FiltroOrder ab = new FiltroOrder();
                            Subcategoria2 subCat2a = new Subcategoria2()
                            {
                                id_subcategoria2 = i.id_subcategoria2,
                                nm_subcategoria2 = i.nm_subcategoria2
                            };
                            ab.Subcategorias2 = subCat2a;
                            ab.total = i.total;

                            //Número da próxima order para ser inserido no botão.
                            ab.or = pesq.or + 1;
                            itemOrder.Add(ab);
                        }
                        IfiltroOrder = itemOrder;
                        break;
                    default:
                        //Lista de produtos.
                        IlistaProdutos = null;

                        //Filtro de desconto.
                        IfiltroDesconto = null;

                        //Filtro preço produto.
                        IfiltroPrecoProduto = null;

                        //Filtro Order.
                        IfiltroOrder = null;
                        break;
                }
                #endregion
            }
        }
        #endregion

        #region RETORMO DE LISTA DE PRODUTOS
        /// <summary>
        /// Verifica o checkbBox da página.
        /// </summary>
        /// <param name="IlistaProdutos">Recebe uma lista de produtos.</param>
        /// <param name="a">Recebe Descontos.</param>
        /// <param name="b">Recebe Preços do produtos.</param>
        /// <returns></returns>
        public static IList<Lista_Produtos2> listaProdutosCheckList(IList<Lista_Produtos2> IlistaProdutos, IList<String> a, IList<String> b)
        {

            if (a != null)
            {
                IlistaProdutos = IlistaProdutos.Where(x => a.Contains(x.pesq_filtro_desconto)).ToList();
            }
            if (b != null)
            {
                IlistaProdutos = IlistaProdutos.Where(x => b.Contains(x.pesq_filtro_precoProduto)).ToList();
            }

            return IlistaProdutos;
        }
        #endregion

        #region RETORNO DE LISTA DE DESCONTO
        public static List<FiltroDesconto> filtroDescReturno(IList<Lista_Produtos2> IlistaProdutos)
        {
            #region FILTRO DESCONTO
            var FiltroDesconto = (from gd in IlistaProdutos
                                  group gd by new
                                  {
                                      gd.pesq_filtro_desconto,
                                      gd.nm_filtro_desconto,
                                      gd.ordem_filtro_desconto
                                  }

                                 into grd
                                  where (grd.Key.nm_filtro_desconto != null)
                                  orderby grd.Key.ordem_filtro_desconto
                                  select new
                                  {
                                      grd.Key.pesq_filtro_desconto,
                                      grd.Key.nm_filtro_desconto,
                                      total = grd.Count()
                                  });

            List<FiltroDesconto> filtroDesconto = new List<FiltroDesconto>();
            foreach (var gd in FiltroDesconto)
            {
                FiltroDesconto item = new Models.FiltraDados.FiltroDesconto();
                item.pesq_filtro_desconto = gd.pesq_filtro_desconto;
                item.nm_filtro_desconto = gd.nm_filtro_desconto;
                item.total = gd.total;

                filtroDesconto.Add(item);
            }

            return filtroDesconto;
            //OBS: ESTE MÉTODO SERÁ USADO EM (ltProdutos).         
            #endregion
        }
        #endregion

        #region RETORNO DE LISTA DE PREÇOS DE PRODUTO
        public static IList<FiltroPrecoProduto> FiltroPrProdutoRetorno(IList<Lista_Produtos2> IlistaProdutos, IList<String> a)
        {
            #region FILTRO PREÇO PRODUTO
            if (a != null)
            {
                IlistaProdutos = IlistaProdutos.Where(x => a.Contains(x.pesq_filtro_desconto)).ToList();
            }
            var FiltroPrProduto = (from gd in IlistaProdutos
                                   group gd by new
                                   {
                                       gd.pesq_filtro_precoProduto,
                                       gd.nm_filtro_precoProduto,
                                       gd.ordem_filtro_precoProduto
                                   }

                 into grd
                                   where (grd.Key.nm_filtro_precoProduto != null)
                                   orderby grd.Key.ordem_filtro_precoProduto
                                   select new
                                   {
                                       grd.Key.pesq_filtro_precoProduto,
                                       grd.Key.nm_filtro_precoProduto,
                                       total = grd.Count()
                                   });

            List<FiltroPrecoProduto> filtroPrecoProduto = new List<FiltroPrecoProduto>();
            foreach (var gd in FiltroPrProduto)
            {
                FiltroPrecoProduto item = new Models.FiltraDados.FiltroPrecoProduto();
                item.pesq_filtro_precoProduto = gd.pesq_filtro_precoProduto;
                item.nm_filtro_precoProduto = gd.nm_filtro_precoProduto;
                item.total = gd.total;

                filtroPrecoProduto.Add(item);
            }

            //OBS: O FINAL DESSE CÓDIGO SE ENCONTRA EM "FILTRO (PESQUISA DA PÁGINA)"
            #endregion

            return filtroPrecoProduto;
        }
        #endregion
    }
}
