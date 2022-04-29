using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriasController(AppDbContext contexto, IConfiguration config, ILogger <CategoriasController> logger)
        { 
            _context = contexto;
            _configuration = config;
            _logger = logger;

        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IServiceWeb serviceweb, string nome)
        {
            return serviceweb.Saudacao(nome);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.ToList();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do banco de dados");
            }
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("/categorias")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {
            _logger.LogInformation("================= GET/ Api/ Categorias pro");
            return _context.Categorias.ToList();
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("{Id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int Id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == Id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {Id} não foi encontrada");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do banco de dados");
            }
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult Post([System.Web.Http.FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", new { Id = categoria.CategoriaId }, categoria);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar uma nova categoria");
            }
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPut("{Id:int:min(1)}")]
        public ActionResult Put(int Id, [System.Web.Http.FromBody] Categoria categoria)
        {
            try
            {
                if (Id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possivel atualizar a categoria com id = {Id}");
                }
                _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok($"A categoria com id = {Id} foi atualizado com sucesso");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar a categoria com id = {Id}");
            }
        }
        
        [Microsoft.AspNetCore.Mvc.HttpDelete("{Id:int:min(1)}")]
        public ActionResult<Categoria> Delete(int Id)
        {
            try
            {
                var categoria = _context.Produtos.FirstOrDefault(p => p.CategoriaId == Id);
                //var produto = _context.Produtos.Find(Id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {Id} não foi encontrada");
                }
                _context.Produtos.Remove(categoria);
                _context.SaveChanges();
                return categoria.Categoria;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar a categoria com id = {Id}");
            }
        }
    }
}
