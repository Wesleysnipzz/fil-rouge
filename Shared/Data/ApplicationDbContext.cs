using Microsoft.EntityFrameworkCore;
using Shared.forme;

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
        public DbSet<Board> Boards { get; set; }

        // Constructeur pour l'injection de dépendances
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configuration de la connexion dans OnConfiguring (si nécessaire)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si jamais la chaîne de connexion n'est pas déjà configurée dans DI
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
            modelBuilder.Entity<Board>().ToTable("Boards");
            
            // Configuration correcte de la relation Board-Forme
            modelBuilder.Entity<Forme>()
                .HasOne<Board>()
                .WithMany()
                .HasForeignKey(f => f.BoardId);
        }
    }
}