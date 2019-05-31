using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ViajaNet.TesteRodrigoNunes.Robo.Models
{
    public partial class viajanetContext : DbContext
    {
        public viajanetContext()
        {
        }

        public viajanetContext(DbContextOptions<viajanetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<tbColeta> tbColeta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<tbColeta>(entity =>
            {
                entity.Property(e => e.Browser)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Pagina)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Parametros)
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });
        }
    }
}
