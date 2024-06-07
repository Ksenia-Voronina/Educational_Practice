using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace testLab
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }
        private static testContext _context;

        public static testContext GetContext()
        {
            if (_context == null)
                _context = new testContext();
            return _context;
        }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Testversion> Testversions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;persist security info=False;user=root;password=Ksu12345!;database=test", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Idquestions)
                    .HasName("PRIMARY");

                entity.ToTable("questions");

                entity.HasIndex(e => e.Idtestversion, "fk_testversion_questions_idx");

                entity.Property(e => e.Idquestions)
                    .ValueGeneratedNever()
                    .HasColumnName("idquestions");

                entity.Property(e => e.Idtestversion).HasColumnName("idtestversion");

                entity.Property(e => e.Textquestions)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("textquestions");

                entity.Property(e => e.TrueAnswer)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("trueAnswer");

                entity.HasOne(d => d.IdtestversionNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.Idtestversion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_testversion_questions");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.Idquestions, e.Idusers })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("result");

                entity.HasIndex(e => e.Idusers, "fk_users_res_idx");

                entity.Property(e => e.Idquestions).HasColumnName("idquestions");

                entity.Property(e => e.Idusers).HasColumnName("idusers");

                entity.Property(e => e.Answeruser)
                    .HasColumnType("text")
                    .HasColumnName("answeruser");

                entity.Property(e => e.Idtestversion)
                    .HasMaxLength(45)
                    .HasColumnName("idtestversion");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.HasOne(d => d.IdquestionsNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.Idquestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_questions_res");

                entity.HasOne(d => d.IdusersNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.Idusers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_res");
            });

            modelBuilder.Entity<Testversion>(entity =>
            {
                entity.HasKey(e => e.Idtestversion)
                    .HasName("PRIMARY");

                entity.ToTable("testversion");

                entity.Property(e => e.Idtestversion)
                    .ValueGeneratedNever()
                    .HasColumnName("idtestversion");

                entity.Property(e => e.Titleversion)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("titleversion");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Idusers)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Idusers)
                    .ValueGeneratedNever()
                    .HasColumnName("idusers");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
