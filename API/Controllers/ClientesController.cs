using System.Data;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using API.Util;
using API.Validation;
using API.DTO;

namespace API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ClientesController(ApplicationDbContext database)
        {
            this.database = database;
        }
        ///<summary>
        ///Metodo resposavel por retornar todos os clientes
        ///</summary>
        ///<returns></returns>
        [HttpGet]
        public IActionResult Cliente()
        {
            var cliente = database.Clientes.ToList();
            return Ok(cliente);
        }
        ///<summary>
        ///Metodo resposavel por retornar apenas um unico cliente
        ///</summary>
        ///<returns></returns>
        [HttpGet("{id}")]
        public IActionResult ClienteGetId(int id)
        {
            try
            {
                Cliente criente = database.Clientes.First(c => c.Id == id);
                return Ok(criente);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
        /// <summary>
        /// Metodo responsável por registrar o usuario
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CadastroCliente([FromBody] Cliente cliente)
        {


            Cliente client = new Cliente();
            client.Nome = cliente.Nome;
            client.Email = cliente.Email;
            client.Senha = Criptografia.Md5Hash(cliente.Senha);
            client.Documento = cliente.Documento;
            client.DataCadastro = cliente.DataCadastro;


            var validator = new ClienteValidacao();
            var result = validator.Validate(cliente);

            if (!result.IsValid)
                return BadRequest(result.Errors);
            
            database.Add(client);
            database.SaveChanges();

            return Ok("Registrado com sucesso!");
        }
        /// <summary>
        /// Medoto responsável pelo login e gerar o token de autenticação 
        /// </summary>
        /// <param name="credenciais"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO credenciais)
        {
            Cliente usuario = database.Clientes.First(user => user.Email.Equals(credenciais.Email));
            try
            {
                if (usuario != null)
                {

                    if (usuario.Senha.Equals(Criptografia.Md5Hash(credenciais.Senha)))
                    {

                        string ChaveDeSeguranca = "desafio_api_dotnet";
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ChaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", usuario.ToString()));
                        claims.Add(new Claim("email", usuario.Email));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                        var JWT = new JwtSecurityToken(
                            issuer: "Sistema de Vendas",
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: credenciaisDeAcesso,
                            claims: claims
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));

                    }
                    else
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("");
                    }
                }
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }
            }
            catch (Exception e)
            {

                Response.StatusCode = 401;
                return new ObjectResult("");
            }


        }
        /// <summary>
        /// Método responsável para editar o cliente/usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult EditarCliente(int id, [FromBody] Cliente Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("");
            }
            Cliente cliente = database.Clientes.First(c => c.Id == id);
            cliente.Nome = Cliente.Nome;
            cliente.Email = Cliente.Email;
            cliente.Senha = Cliente.Senha;
            cliente.Documento = Cliente.Documento;
            cliente.DataCadastro = Cliente.DataCadastro;

            database.SaveChanges();
            return Ok(cliente);
        }
        /// <summary>
        /// Método responsável para deletar usuário 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletarCliente(int id)
        {
            try
            {
                Cliente cliente = database.Clientes.First(c => c.Id == id);
                database.Clientes.Remove(cliente);
                database.SaveChanges();
                return Ok("Deletado com sucesso!");
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }
        /// <summary>
        /// Método responsável por listar em ordem alfabética crescente nome do cliente  
        /// </summary>
        /// <returns></returns>

        [HttpGet("asc")]
        public IActionResult ClienteCrescente(Cliente Cliente)
        {
            var clientes = database.Clientes.OrderBy(c => c.Nome).ToList();

            foreach (var cliente in clientes)
            {
                Cliente order = new Cliente();
                order.Nome = cliente.Nome;

                database.SaveChanges();
                database.Add(order);
            }

            return Ok(clientes);

        }
        /// <summary>
        /// Método responsável por listar em ordem alfabética decrescente nome do cliente  
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        public IActionResult ClienteDecrescente(Cliente Cliente)
        {
            var clientes = database.Clientes.OrderByDescending(c => c.Nome).ToList();

            foreach (var cliente in clientes)
            {
                Cliente order = new Cliente();
                order.Nome = cliente.Nome;

                database.SaveChanges();
                database.Add(order);
            }

            return Ok(clientes);

        }
        /// <summary>
        /// Método responsável por buscar cliente por nome 
        /// </summary>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        public IActionResult ClienteNomes(string nome)
        {
            try
            {

                var cliente = database.Clientes.Where(c => c.Nome.Contains(nome)).ToList();
                List<LoginDTO> clientes = new List<LoginDTO>();
                foreach (var client in cliente)
                {
                    LoginDTO namecliente = new LoginDTO();
                    namecliente.Nome = client.Nome;
                    namecliente.Email = client.Email;
                    namecliente.Senha = client.Senha;
                    namecliente.Id = client.Id;

                    clientes.Add(namecliente);
                }
                if (cliente.Count == 0)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = "Nome não encontrado" });
                }
                return Ok(clientes);
            }
            catch (Exception e)
            {

                Response.StatusCode = 404;
                return new ObjectResult(e);
            }
        }

    }
}