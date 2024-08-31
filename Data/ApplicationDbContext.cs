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

        // public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        // public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        // public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        // public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        // public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        // public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
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
            // modelBuilder.Entity<AspNetRole>(entity =>
            // {
            //     entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //         .IsUnique()
            //         .HasFilter("([NormalizedName] IS NOT NULL)");
            //
            //     entity.Property(e => e.Name).HasMaxLength(256);
            //
            //     entity.Property(e => e.NormalizedName).HasMaxLength(256);
            // });
            //
            // modelBuilder.Entity<AspNetRoleClaim>(entity =>
            // {
            //     entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");
            //
            //     entity.HasOne(d => d.Role)
            //         .WithMany(p => p.AspNetRoleClaims)
            //         .HasForeignKey(d => d.RoleId);
            // });
            //
            // modelBuilder.Entity<AspNetUser>(entity =>
            // {
            //     entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");
            //
            //     entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //         .IsUnique()
            //         .HasFilter("([NormalizedUserName] IS NOT NULL)");
            //
            //     entity.Property(e => e.Email).HasMaxLength(256);
            //
            //     entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            //
            //     entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            //
            //     entity.Property(e => e.UserName).HasMaxLength(256);
            //
            //     entity.HasMany(d => d.Roles)
            //         .WithMany(p => p.Users)
            //         .UsingEntity<Dictionary<string, object>>(
            //             "AspNetUserRole",
            //             l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //             r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //             j =>
            //             {
            //                 j.HasKey("UserId", "RoleId");
            //
            //                 j.ToTable("AspNetUserRoles");
            //
            //                 j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //             });
            // });
            //
            // modelBuilder.Entity<AspNetUserClaim>(entity =>
            // {
            //     entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");
            //
            //     entity.HasOne(d => d.User)
            //         .WithMany(p => p.AspNetUserClaims)
            //         .HasForeignKey(d => d.UserId);
            // });
            //
            // modelBuilder.Entity<AspNetUserLogin>(entity =>
            // {
            //     entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            //
            //     entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");
            //
            //     entity.Property(e => e.LoginProvider).HasMaxLength(128);
            //
            //     entity.Property(e => e.ProviderKey).HasMaxLength(128);
            //
            //     entity.HasOne(d => d.User)
            //         .WithMany(p => p.AspNetUserLogins)
            //         .HasForeignKey(d => d.UserId);
            // });
            //
            // modelBuilder.Entity<AspNetUserToken>(entity =>
            // {
            //     entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            //
            //     entity.Property(e => e.LoginProvider).HasMaxLength(128);
            //
            //     entity.Property(e => e.Name).HasMaxLength(128);
            //
            //     entity.HasOne(d => d.User)
            //         .WithMany(p => p.AspNetUserTokens)
            //         .HasForeignKey(d => d.UserId);
            // });

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
