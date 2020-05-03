using IRB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class TagsDBContext : DbContext
    {
        public TagsDBContext(DbContextOptions<TagsDBContext> options) : base(options)
        {
        }

        public DbSet<Tags> TAGS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tags>()
                .HasKey(i => i.PK);
        }
    }
}
