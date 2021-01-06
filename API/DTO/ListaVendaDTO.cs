using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class ListaVendaDTO
    {
        public long Id {get;set;}
        public Cliente Cliente {get;set;} 
        public List<Produto> Produtos{get;set;} = new List<Produto>();
        public decimal TotalCompra{get;set;}
        public DateTime DataCompra{get;set;}
        
    }
}