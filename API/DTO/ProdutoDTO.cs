using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class ProdutoDTO
    {
        public List<Fornecedor> Fornecedores{get;set;}
        public long Id{get;set;}
        public string Nome{get;set;}
        public string CodigoProduto{get;set;}
        public decimal Valor{get;set;}
        public decimal ValorPromo{get;set;}
        public string Categoria{get;set;}
        public string Imagem{get;set;}
        public long Quantidade{get;set;}
    }
}