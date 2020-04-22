using IRB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class UsuarioDBContext : DbContext
    {
        public UsuarioDBContext(DbContextOptions<UsuarioDBContext> options) : base(options)
        {
        }

        public DbSet<Usuario> USUARIOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasKey(i => i.PK);
        }
    }
}
