using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.DTO;
using API.Models;
using API.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
   
    public class FornecedoresController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public FornecedoresController(ApplicationDbContext database)
        {
            this.database = database;
        }
        /// <summary>
        /// Método responsável por retornar todos os fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Fornecedor()
        {
            var fornecedor = database.Fornecedores.ToList();
            return Ok(fornecedor);
        }
        /// <summary>
        /// Método responsável por retornar apenas um unico fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult FornecedorGetId(int id)
        {
            try
            {
                Fornecedor fornecedor = database.Fornecedores.First(f => f.Id == id);
                return Ok(fornecedor);
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
        /// <summary>
        /// Método responsável por registrar fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CadastroFornecedores([FromBody] Fornecedor fornecedor)
        {
            Fornecedor fornecedo = new Fornecedor();
            fornecedo.Nome = fornecedor.Nome;
            fornecedo.Cnpj = fornecedor.Cnpj;

            var validator = new FornecedorValidacao();
            var result = validator.Validate(fornecedor);
            if (!result.IsValid)
                return BadRequest(result.Errors);
                
            database.Add(fornecedo);
            database.SaveChanges();

            return Ok("Fornecedor registrado com sucesso!");

        }
        /// <summary>
        /// Método responsável por editar fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditarFonecedor([FromBody] int id)
        {
            Fornecedor fornecedor = database.Fornecedores.First(f => f.Id == id);
            database.SaveChanges();
            return Ok(fornecedor);
        }
        /// <summary>
        /// Método responsável por deletar fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletarFornecedor(int id)
        {
            try
            {
                Fornecedor fornecedor = database.Fornecedores.First(f => f.Id == id);
                database.Fornecedores.Remove(fornecedor);
                database.SaveChanges();
                return Ok("Deletado com sucesso");
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
       /// <summary>
        /// Método responsável por listar em ordem alfabética crescente nome do fornecedor 
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("asc")]
        public IActionResult FornecedorCrescente()
        {
            var fornecedores = database.Fornecedores.OrderBy(c => c.Nome).ToList();

            foreach (var fornecedor in fornecedores)
            {
                Cliente order = new Cliente();
                order.Nome = fornecedor.Nome;

                database.SaveChanges();
                database.Add(order);
            }

            return Ok(fornecedores);

        }
       /// <summary>
        /// Método responsável por listar em ordem alfabética decrescente nome do fornecedor
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        public IActionResult FornecedorDecrescente()
        {
            var fornecedores = database.Fornecedores.OrderByDescending(c => c.Nome).ToList();

            foreach (var fornecedor in fornecedores)
            {
                Cliente order = new Cliente();
                order.Nome = fornecedor.Nome;

                database.SaveChanges();
                database.Add(order);
            }

            return Ok(fornecedores);

        }
        /// <summary>
        /// Método responsável por buscar fornecedor por nome 
        /// </summary>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        public IActionResult FornecedorNomes(string nome)
        {
             try
            {
                var fornecedor = database.Fornecedores.Where(c => c.Nome.Contains(nome)).ToList();
                List<FornecedorDTO> fornecedores = new List<FornecedorDTO>();
                foreach (var fornece in fornecedor)
                {
                    FornecedorDTO namefornecedor = new FornecedorDTO();
                    namefornecedor.Nome = fornece.Nome;
                    namefornecedor.Cnpj = fornece.Cnpj;
                    namefornecedor.Id = fornece.Id;

                    fornecedores.Add(namefornecedor);
                }
                if (fornecedor.Count == 0)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = "Nome não encontrado" });
                }
                return Ok(fornecedores);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
    }
}