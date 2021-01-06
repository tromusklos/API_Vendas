using System;
using API.Models;
using API.Util;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes {get;set;}
        public DbSet<Produto> Produtos {get;set;}
        public DbSet<Fornecedor> Fornecedores {get;set;}
        public DbSet<Venda> Vendas {get;set;}
        public DbSet<VendaProduto> VendaProdutos{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaProduto>().HasKey(sc =>
            new { sc.produtoId, sc.VendaId });

            
             modelBuilder.Entity<Cliente>().HasData(new Cliente()
            {
                Id = 1,
                Nome = "João Paulo",
                Email = "joao@joao.com",
                Senha = Criptografia.Md5Hash("12124545"),
                Documento = "88.888.888-44",
                DataCadastro = new DateTime(2020,12,11)
            });

            modelBuilder.Entity<Cliente>().HasData(new Cliente()
            {
                Id = 2,
                Nome = "Camila Santos",
                Email = "camila@camila.com",
                Senha = Criptografia.Md5Hash("65988452"),
                Documento = "77.858.458-96",
                DataCadastro = new DateTime(2020,12,11)
            });

            modelBuilder.Entity<Fornecedor>().HasData(new Fornecedor()
            {
                Id = 1,
                Nome = "João do Gás",
                Cnpj = "81.191.124/0001-01"

            });

            modelBuilder.Entity<Fornecedor>().HasData(new Fornecedor()
            {
                Id = 2,
                Nome = "Amazon",
                Cnpj = "88.857.444/0001-01"
            });

            modelBuilder.Entity<Produto>().HasData(new Produto()
            {
                Id = 1,
                Nome = "Echo dot",
                CodigoProduto = "548",
                Valor = 199,
                Promocao = false,
                ValorPromo = 199,
                Categoria = "Eletronicos",
                Imagem = "echodot.jpg",
                Quantidade = 100,
                FornecedorId = 2,
            });
            modelBuilder.Entity<Produto>().HasData(new Produto()
            {
                Id = 2,
                Nome = "Gás",
                CodigoProduto = "4487",
                Valor = 70,
                Promocao = false,
                ValorPromo = 70,
                Categoria = "Gás",
                Imagem = "gas.jpg",
                Quantidade = 150,
                FornecedorId = 1,
            });
            modelBuilder.Entity<Produto>().HasData(new Produto()
            {
                Id = 3,
                Nome = "Smartphone",
                CodigoProduto = "874",
                Valor = 1699,
                Promocao = false,
                ValorPromo = 1699,
                Categoria = "Celulares",
                Imagem = "sansung.jpg",
                Quantidade = 30,
                FornecedorId = 2,
            });
            modelBuilder.Entity<Produto>().HasData(new Produto()
            {
                Id = 4,
                Nome = "GásP45",
                CodigoProduto = "045",
                Valor = 150,
                Promocao = false,
                ValorPromo = 150,
                Categoria = "Gás",
                Imagem = "gasp45.jpg",
                Quantidade = 50,
                FornecedorId = 1,
            });
            modelBuilder.Entity<Venda>().HasData(new Venda()
            {
                Id = 1,
                FornecedorId = 1,
                ClienteId = 1,
                TotalCompra = 16,
                DataCompra = new DateTime(2020,12,11)
            });
            modelBuilder.Entity<VendaProduto>().HasData(new VendaProduto()
            {
                VendaId = 1,
                produtoId = 1,
                TotalProduto = 12
                
            });
            modelBuilder.Entity<VendaProduto>().HasData(new VendaProduto()
            {
                VendaId = 1,
                produtoId = 3,
                TotalProduto = 4
            });
        }
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}