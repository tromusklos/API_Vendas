
namespace API.Models
{
    public class VendaProduto
    {

        
        public long VendaId{get;set;}
        public Venda Venda{get;set;}
        public long produtoId{get;set;}
        public Produto Produto{get;set;}
        public decimal TotalProduto{get;set;}
    }
}