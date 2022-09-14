using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Persistence
{
    public class BDContext : DbContext
    {
        public BDContext(DbContextOptions<BDContext> options) : base(options)
        {
        }

        public DbSet<Cliente>? Clientes2 { get; set; }
        public DbSet<Producto>? Productos2 { get; set; }
        public DbSet<Solicitud>? Solicitudes2 { get; set; }
        public DbSet<Estado>? Estados2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
            //Nombrando las tablas
            modelBuilder.Entity<Cliente>().ToTable("Clientes2");

            modelBuilder.Entity<Producto>().ToTable("Productos2");

            modelBuilder.Entity<Solicitud>().ToTable("Solicitudes2");
            modelBuilder.Entity<Estado>().ToTable("Estados2");

            //Configuracion de PK
            modelBuilder.Entity<Cliente>().HasKey(x => x.id);
            modelBuilder.Entity<Producto>().HasKey(x => x.id);
            modelBuilder.Entity<Solicitud>().HasKey(x => x.id);
            
        }

    }
}
