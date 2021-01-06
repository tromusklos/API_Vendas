using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Produto
    {
        public long Id{get;set;}
        public string Nome{get;set;}
        public string CodigoProduto{get;set;}
        public decimal Valor{get;set;}
        public bool Promocao{get;set;}
        public decimal ValorPromo{get;set;}
        public string Categoria{get;set;}
        public string Imagem{get;set;}
        public long Quantidade{get;set;}

        [ForeignKey("Fornecedor")]
        public long FornecedorId{get;set;}
        public Fornecedor Fornecedor{get;set;}
    }
}