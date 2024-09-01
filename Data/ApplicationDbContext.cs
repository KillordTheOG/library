using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Library.Models.DBObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Idbook)
                    .HasName("PK__Books__2339855F550950CF");

                entity.Property(e => e.Idbook)
                    .ValueGeneratedNever()
                    .HasColumnName("IDBook");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.Idloan);

                entity.Property(e => e.Idloan)
                    .ValueGeneratedNever()
                    .HasColumnName("IDLoan");

                entity.Property(e => e.Idbook).HasColumnName("IDBook");

                entity.Property(e => e.Idmember).HasColumnName("IDMember");

                entity.HasOne(d => d.IdbookNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.Idbook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loans_Books");

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loans_Members");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Idmember)
                    .HasName("PK__Members__7EB75A6330CBD74B");

                entity.Property(e => e.Idmember)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMember");

                entity.Property(e => e.Adress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
