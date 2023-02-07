using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDbAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDbAPI.Context
{
    public class BancoDadosContext : DbContext
    {
        public DbSet<Usuarios> usuario { get; set; }

        public BancoDadosContext(DbContextOptions options) : base(options) { }

        public BancoDadosContext() => this.Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase("InMemoriaDb");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().HasKey(x => x.Id);
            modelBuilder.Entity<Usuarios>().HasData(new Usuarios()
            {
                Id = 1,
                Nome = "Messias",
                Email = "Messias@gmail.com"
            });
        }
    }
}