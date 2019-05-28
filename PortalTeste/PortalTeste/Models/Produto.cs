using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Produto
    {
        [Key]
        public int id_produto { get; set; }
        public String Titulo_produto { get; set; }
        public virtual Subcategoria2 sub_categoria2 { get; set; }
        public int id_subcategoria2 { get; set; }
        public virtual Categoria categoria { get; set; }
        public int id_categoria { get; set; }
        public virtual Subcategoria sub_categoria { get; set; }
        public int id_subcategoria { get; set; }
        public decimal? desconto { get; set; }
        public virtual Marca_produto marca { get; set; }
        public int id_marca { get; set; }
        public virtual Faixa_etaria faixa_etaria { get; set; }
        public int? cod_faixa_etaria { get; set; }
        public decimal preco { get; set; }
        public Int64 cod_de_barra { get; set; }
        public int? cod_visualizacao_parc { get; set; }
        public bool habilitado { get; set; }
        public DateTime data_insercao { get; set; }
    }
}