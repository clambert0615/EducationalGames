using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace EducationAPI.Models
{
    public partial class EducationAPIDbContext : DbContext
    {
        public EducationAPIDbContext()
        {
        }

        public EducationAPIDbContext(DbContextOptions<EducationAPIDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        public virtual DbSet<Planets> Planets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planets>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Planets__0DC06FACD6C01502");

                entity.Property(e => e.Answer).HasMaxLength(100);

                entity.Property(e => e.Question).HasMaxLength(500);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
