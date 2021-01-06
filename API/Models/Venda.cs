using System;
namespace API.Models
{
    public class Venda
    {
        public long Id{get;set;}
        public long ClienteId{get;set;}
        public Cliente Cliente {get;set;}
        public long FornecedorId{get;set;}
        public Fornecedor Fornecedor{get;set;}
        public decimal TotalCompra{get;set;}
        public DateTime DataCompra{get;set;}
    }
}