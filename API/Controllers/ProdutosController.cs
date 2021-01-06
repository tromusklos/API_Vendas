using System.Collections.Generic;
using System;
using System.Linq;
using API.Data;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Validation;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]


    public class ProdutosController : ControllerBase
    {

        private readonly ApplicationDbContext database;

        public ProdutosController(ApplicationDbContext database)
        {
            this.database = database;
        }
        ///<summary>
        ///Metodo resposavel por retornar todos os produtos
        ///</summary>
        ///<returns></returns>
        [HttpGet]
        public IActionResult Produto()
        {
            var produto = database.Produtos.ToList();
            List<ProdutoDTO> fornecedores = new List<ProdutoDTO>() { };

            foreach (var produt in produto)
            {
                List<Fornecedor> fornecedor = database.Fornecedores.Where(f => f.Id == produt.FornecedorId).ToList();

                ProdutoDTO produ = new ProdutoDTO();
                produ.Fornecedores = fornecedor;

                fornecedores.Add(produ);

                database.SaveChanges();

            }
            return Ok(produto);
        }
        ///<summary>
        ///Metodo resposavel por retornar apenas um unico produto
        ///</summary>
        ///<returns></returns>
        [HttpGet("{id}")]
        public IActionResult ProdutoGetId(int id)
        {
            try
            {
                Produto produto = database.Produtos.First(c => c.Id == id);
                return Ok(produto);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
        ///<summary>
        ///Metodo resposavel por retornar por registrar produto
        ///</summary>
        ///<returns></returns>
        [HttpPost]
        public IActionResult CadastroCliente([FromBody] Produto produto)
        {

            Produto produt = new Produto();
            produt.Nome = produto.Nome;
            produt.CodigoProduto = produto.CodigoProduto;
            produt.Valor = produto.Valor;
            produt.Promocao = produto.Promocao;
            produt.ValorPromo = produto.ValorPromo;
            produt.Categoria = produto.Categoria;
            produt.Imagem = produto.Imagem;
            produt.Quantidade = produto.Quantidade;
            produt.Fornecedor = database.Fornecedores.First(f => f.Id == produto.Fornecedor.Id);

            var validator = new ProdutoValidacao();
            var result = validator.Validate(produto);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            database.Add(produt);
            database.SaveChanges();

            return Ok("Cadastrado com sucesso!");
        }
        /// <summary>
        /// Método responsável por editar produtos
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditarProduto(int id, [FromBody] Produto Produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("");
            }
            Produto produto = database.Produtos.First(c => c.Id == id);
            produto.Nome = Produto.Nome;
            produto.Valor = Produto.Valor;
            produto.CodigoProduto = Produto.CodigoProduto;
            produto.Promocao = Produto.Promocao;
            produto.ValorPromo = Produto.ValorPromo;
            produto.Categoria = Produto.Categoria;
            produto.Imagem = Produto.Imagem;
            produto.Quantidade = Produto.Quantidade;

            database.SaveChanges();
            return Ok(produto);
        }
        /// <summary>
        /// Método responsável por deletar produtos
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletarProduto(int id)
        {
            try
            {
                Produto produto = database.Produtos.First(f => f.Id == id);
                database.Produtos.Remove(produto);
                database.SaveChanges();
                return Ok("Produto deletado com sucesso!");
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(e);

            }
        }
        /// <summary>
        /// Método responsável por listar em ordem alfabética crescente nome do produto
        /// </summary>
        /// <returns></returns>

        [HttpGet("asc")]
        public IActionResult ProdutoCrescente(Produto Produto)
        {
            var produtos = database.Produtos.OrderBy(c => c.Nome).ToList();

            foreach (var produto in produtos)
            {
                Produto order = new Produto();
                order.Nome = produto.Nome;

                database.SaveChanges();
                database.Add(order);
            }
            return Ok(produtos);
        }
        /// <summary>
        /// Método responsável por listar em ordem alfabética decrescente nome do produto
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        public IActionResult ProdutoDecrescente(Produto Produto)
        {
            var produtos = database.Produtos.OrderBy(c => c.Nome).ToList();

            foreach (var produto in produtos)
            {
                Produto order = new Produto();
                order.Nome = produto.Nome;

                database.SaveChanges();
                database.Add(order);
            }
            return Ok(produtos);
        }
        /// <summary>
        /// Método responsável por buscar produto por nome 
        /// </summary>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        public IActionResult ProdutoNomes(string nome)
        {
            try
            {
                var produto = database.Produtos.Where(c => c.Nome.Contains(nome)).ToList();
                List<ProdutoDTO> produtos = new List<ProdutoDTO>();
                foreach (var client in produto)
                {
                    ProdutoDTO nameproduto = new ProdutoDTO();
                    nameproduto.Nome = client.Nome;
                    nameproduto.CodigoProduto = client.CodigoProduto;
                    nameproduto.Valor = client.Valor;
                    nameproduto.ValorPromo = client.ValorPromo;
                    nameproduto.Categoria = client.Categoria;
                    nameproduto.Imagem = client.Imagem;
                    nameproduto.Quantidade = client.Quantidade;
                    nameproduto.Id = client.Id;

                    produtos.Add(nameproduto);
                }
                if (produto.Count == 0)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = "Nome não encontrado" });
                }
                return Ok(produtos);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
    }
}

