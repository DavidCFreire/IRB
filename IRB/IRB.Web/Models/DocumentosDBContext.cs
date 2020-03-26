using IRB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.Web.Models
{
    public class DocumentosDBContext : DbContext
    {
        public DocumentosDBContext(DbContextOptions<DocumentosDBContext> options) : base(options)
        {
        }

        public DbSet<Documento> DOCUMENTOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Documento>()
                .HasKey(i => i.PK);
        }
    }
}
