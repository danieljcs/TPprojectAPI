using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TPAPIproject.Models
{
    public partial class DbContextTP : DbContext
    {
        public DbContextTP()
        {
        }

        public DbContextTP(DbContextOptions<DbContextTP> options)
            : base(options)
        {
        }

        public virtual DbSet<TpDocumentation> TpDocumentations { get; set; }
        public virtual DbSet<TpIdentificationType> TpIdentificationTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
