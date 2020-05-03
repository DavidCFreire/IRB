using IRB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class DocumentosTagDBContext : DbContext
    {
        public DocumentosTagDBContext(DbContextOptions<DocumentosTagDBContext> options) : base(options)
        {
        }

        public DbSet<Documentos_Tag> DOCUMENTOS_TAG { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Documentos_Tag>()
                .HasKey(i => i.PK);
        }
    }
}
