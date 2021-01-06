using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class VendaDTO
    {

        public List<Produto> Produtos{get;set;} = new List<Produto>();
        public decimal TotalCompra{get;set;}
        public DateTime DataCompra{get;set;}
        public Cliente Cliente {get;set;}
        public Fornecedor Fornecedor{get;set;}

    }

}