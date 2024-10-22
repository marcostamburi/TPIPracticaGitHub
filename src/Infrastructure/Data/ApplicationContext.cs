using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
        { }
        public DbSet<Vendedor> Vendedores {  get; set; }
        public DbSet<Comprador> Compradores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(p =>
            {
                //Relacion producto - vendedor, un producto tiene un vendedor pero un vendedor muchos productos
                p.HasOne(v => v.Vendedor)
                .WithMany(v => v.Productos)
                .HasForeignKey(v => v.VendedorId);
            });

            modelBuilder.Entity<Vendedor>(v =>
            {
                v.HasMany(v => v.Productos)
                .WithOne(p => p.Vendedor)
                .HasForeignKey(p => p.VendedorId);
            });
        }
    }
}
