using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ApiCatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base (options) { }
        public System.Data.Entity.DbSet<Categoria> Categorias { get; set; }
        public System.Data.Entity.DbSet<Produto> Produtos { get; set; }
    }
}
