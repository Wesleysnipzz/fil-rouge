using Microsoft.EntityFrameworkCore;
using Shared.forme;
// Vos entités de formes

namespace Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        // DbSets pour toutes vos entités
        public DbSet<Forme> Formes { get; set; }
        public DbSet<Cercle> Cercles { get; set; }
        public DbSet<Rectangle> Rectangles { get; set; }
        public DbSet<Triangle> Triangles { get; set; }
        public DbSet<Carre> Carres { get; set; }

        // Constructeur par défaut pour EF design-time
        public ApplicationDbContext() : base(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql("Host=127.0.0.1;Port=10000;Username=user;Password=123;Database=chess")
                .Options)
        {
        }

        // Constructeur pour l'injection de dépendances
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configuration de la connexion (au cas où elle ne serait pas déjà configurée)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=10000;Username=user;Password=123;Database=chess");
            }
        }

        // Mapping des entités vers leurs tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forme>().ToTable("Formes");
            modelBuilder.Entity<Carre>().ToTable("Carres");
            modelBuilder.Entity<Rectangle>().ToTable("Rectangles");
            modelBuilder.Entity<Triangle>().ToTable("Triangles");
            modelBuilder.Entity<Cercle>().ToTable("Cercles");
        }
    }
}