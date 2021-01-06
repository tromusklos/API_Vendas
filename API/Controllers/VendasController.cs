using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]


    public class VendasController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public VendasController(ApplicationDbContext database)
        {
            this.database = database;
        }
        /// <summary>
        /// Método responsável por lisas as vendas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var vendas = database.Vendas.ToList();

            var produtoVendas = database.VendaProdutos.ToList();

            var fornecedor = database.Fornecedores.ToList();

            List<ListaVendaDTO> vendalist = new List<ListaVendaDTO>();

            foreach (var venda in vendas)
            {
                var vendaid = new ListaVendaDTO();

                vendaid.Id = venda.Id;

                foreach (var produto in produtoVendas)
                {
                    List<Produto> produtoslist = database.VendaProdutos.Where(p => p.VendaId == venda.Id).
                                                                        Select(p => p.Produto).
                                                                        ToList();
                    vendaid.Id = venda.Id;
                    vendaid.Produtos = produtoslist;
                }

                vendaid.TotalCompra = venda.TotalCompra;

                vendaid.Cliente = database.Clientes.First(c => c.Id == venda.ClienteId);

                vendalist.Add(vendaid);
            }
            return Ok(vendalist);
        }
        /// <summary>
        /// Método resposável por buscar venda por Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult VendaGetId(int id)
        {
            try
            {
                Venda venda = database.Vendas.First(c => c.Id == id);
                return Ok(venda);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
        /// <summary>
        /// Método responsável por venda do produto
        /// </summary>
        /// <param name="venda"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Venda([FromBody] VendaDTO venda)
        {
           
            Venda vendaprodu = new Venda();

            VendaProduto vendaproduto = new VendaProduto();


            bool verificar = true;
            try
            {
                foreach (var vendas in venda.Produtos)
                {

                if(verificar == true){
                    vendaprodu.Cliente = database.Clientes.First(c => c.Id == venda.Cliente.Id);
                    vendaprodu.Fornecedor = database.Fornecedores.First(f => f.Id == venda.Fornecedor.Id);
                    database.Vendas.Add(vendaprodu);
                    database.SaveChanges();

                    verificar = false;
                        
                }
                        
                    vendaproduto.TotalProduto = database.Produtos.Where(v => v.Id == vendas.Id).Select(v => v.Valor).SingleOrDefault();
                    vendaproduto.VendaId = database.Vendas.Max(v => v.Id);
                    vendaproduto.produtoId = vendas.Id;

                    venda.TotalCompra += vendaproduto.TotalProduto;

                    database.VendaProdutos.Add(vendaproduto);
                    database.SaveChanges();

                    vendaprodu.TotalCompra += vendaproduto.TotalProduto;
                    vendaprodu.DataCompra = DateTime.Today;
                }
                
                database.SaveChanges();
                return Ok("Venda cadastrada com sucesso");
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(e);
            }

            }
        }
    }


