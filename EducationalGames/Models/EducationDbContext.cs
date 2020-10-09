using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EducationalGames.Models
{
    public partial class EducationDbContext : DbContext
    {
        public EducationDbContext()
        {
        }

        public EducationDbContext(DbContextOptions<EducationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Math> Math { get; set; }
        public virtual DbSet<Parent> Parent { get; set; }
        public virtual DbSet<Science> Science { get; set; }
        public virtual DbSet<StudentParent> StudentParent { get; set; }
        public virtual DbSet<StudentTeacher> StudentTeacher { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=EducationDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Math>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PK__Math__2AB897FD34EC740D");

                entity.Property(e => e.Type).HasMaxLength(30);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.Math)
                    .HasForeignKey(d => d.StudId)
                    .HasConstraintName("FK__Math__StudId__693CA210");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Math)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Math__UserId__01142BA1");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Parent)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Parent__RoleId__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Parent)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Parent__UserId__656C112C");
            });

            modelBuilder.Entity<Science>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(40);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Science)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Science__UserId__160F4887");
            });

            modelBuilder.Entity<StudentParent>(entity =>
            {
                entity.HasKey(e => e.StudParentId)
                    .HasName("PK__StudentP__498D132D944F57E3");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.StudentParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__StudentPa__Paren__2DE6D218");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentParent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__StudentPa__Stude__2CF2ADDF");
            });

            modelBuilder.Entity<StudentTeacher>(entity =>
            {
                entity.HasKey(e => e.StudTeachId)
                    .HasName("PK__StudentT__07C70A5EE87184B7");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTeacher)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__StudentTe__Stude__29221CFB");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.StudentTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__StudentTe__Teach__2A164134");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__Students__32C52B9911590C3E");

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Students__RoleId__5DCAEF64");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Students__UserId__5CD6CB2B");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Teacher__RoleId__619B8048");

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.StudId)
                    .HasConstraintName("FK__Teacher__StudId__628FA481");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Teacher__UserId__60A75C0F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
