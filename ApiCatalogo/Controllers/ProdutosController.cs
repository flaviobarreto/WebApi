using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")] //Acesso "api/produtos"
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }
        [HttpGet] //Acesso "api/produtos"
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await _context.Produtos.ToListAsync();
        }


        [HttpGet("{Id:int}", Name = "ObterProduto")] //Acesso "api/produtos/id"
        public async Task<ActionResult<Produto>> Get(int Id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == Id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }
        [HttpPost] //Acesso "api/produtos"
        public ActionResult Post([FromBody] Produto produto) 
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { Id = produto.ProdutoId }, produto);
        }
        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] Produto produto)
        {
            if (Id != produto.ProdutoId)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public ActionResult<Produto> Delete(int Id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == Id);
            //var produto = _context.Produtos.Find(Id);
            if(produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
        }
    }
}

