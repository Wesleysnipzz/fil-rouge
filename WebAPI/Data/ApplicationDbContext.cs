using Microsoft.EntityFrameworkCore;
using EzChess.forme;  // Tes entités de formes
using WebAPI.Models.Entities;  // Tes entités de placement et autres
namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Ajout du constructeur par défaut pour EF design-time
        public ApplicationDbContext() : base(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                // Mise à jour de la chaîne de connexion : Username=user
                .UseNpgsql("Host=127.0.0.1;Port=10000;Username=user;Password=123;Database=chess")
                .Options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // DbSets pour toutes tes entités
        public DbSet<Forme> Formes { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Cercle> Cercles { get; set; }
        public DbSet<Rectangle> Rectangles { get; set; }
        public DbSet<Triangle> Triangles { get; set; }
        public DbSet<Carre> Carres { get; set; }

        // Configuration de la connexion avec PostgreSQL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si aucune chaîne de connexion n'est spécifiée
            if (!optionsBuilder.IsConfigured)
            {
                // Mise à jour de la chaîne de connexion pour Docker Compose
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=10000;Username=user;Password=123;Database=chess");
            }
        }

        // Configuration des modèles et relations entre entités
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping des entités avec leurs tables
            modelBuilder.Entity<Forme>().ToTable("Formes");
            modelBuilder.Entity<Placement>().ToTable("Placements");
            modelBuilder.Entity<Carre>().ToTable("Carres");
            modelBuilder.Entity<Rectangle>().ToTable("Rectangles");
            modelBuilder.Entity<Triangle>().ToTable("Triangles");
            modelBuilder.Entity<Cercle>().ToTable("Cercles");

            // Configuration de la relation Placement -> Forme
            modelBuilder.Entity<Placement>()
                .HasKey(p => p.Position);  // Définition de la clé primaire

            modelBuilder.Entity<Placement>()
                .HasOne(p => p.Forme)  // Relation avec Forme
                .WithMany()  // Relation un-à-plusieurs
                .HasForeignKey(p => p.FormeId)  // Clé étrangère
                .OnDelete(DeleteBehavior.SetNull);  // Comportement de suppression (ici, SetNull)
        }
    }
}
