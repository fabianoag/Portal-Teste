using Microsoft.Data.Entity;
using PortalTeste.Models;
using PortalTeste.Models.FiltraDados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PortalTeste.DAO
{
    public class EntidadeContext : DbContext //Nunca se esqueça que ele herda de DbContext.
    {
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Subcategoria> sub_categorias { get; set; }
        public DbSet<Subcategoria2> sub_categorias2 { get; set; }
        public DbSet<Faixa_etaria> faixa_etarias { get; set; }
        public DbSet<Marca_produto> marcas { get; set; }
        public DbSet<viewteste> viewTeste { get; set; }
        public DbSet<Lista_Produtos2> Lista_Produtos2 { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Pessoa> pessoas { get; set; }
        public DbSet<Tipo_pessoa> tipo_pessoa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringConexao = ConfigurationManager.ConnectionStrings["conexaoStr"].ConnectionString;
            optionsBuilder.UseSqlServer(stringConexao);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<viewteste>(entity => {
                entity.HasKey(e => e.id_produto);
                entity.ToTable("viewTeste");
                //entity.Property(e => e.Titulo_produto).HasMaxLength(5);
            });

            modelBuilder.Entity<Lista_Produtos2>(entity => {
                entity.HasKey(e => e.id_produto);
                entity.ToTable("Lista_Produtos2");
                //entity.Property(e => e.Titulo_produto).HasMaxLength(5);
            });
        }


    }
}