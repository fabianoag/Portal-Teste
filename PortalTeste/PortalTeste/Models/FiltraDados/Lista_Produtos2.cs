using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models.FiltraDados
{
    public class Lista_Produtos2
    {
        [Key]
        public int id_produto { get; set; }
        public string Titulo_produto { get; set; }
        public string pesq_filtro_precoProduto { get; set; }
        public int? ordem_filtro_precoProduto { get; set; }
        public string nm_filtro_precoProduto { get; set; }
        public string pesq_filtro_desconto { get; set; }
        public int? ordem_filtro_desconto { get; set; }
        public string nm_filtro_desconto { get; set; }
        public int id_categoria { get; set; }
        public string nm_categoria { get; set; }
        public int id_subcategoria { get; set; }
        public string nm_subcategoria { get; set; }
        public int id_subcategoria2 { get; set; }
        public string nm_subcategoria2 { get; set; }
        public decimal? desconto { get; set; }
        public string nm_marca { get; set; }
        public string nm_faixa_etaria { get; set; }
        public decimal preco { get; set; }
        public decimal precoTotal { get; set; }
        public Int64 cod_de_barra { get; set; }
        public string mini_imagem { get; set; }
        public int? cod_visualizacao_parc { get; set; }
        public bool habilitado { get; set; }
    }
    }