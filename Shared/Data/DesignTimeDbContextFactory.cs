using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shared.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Mise à jour de la chaîne de connexion pour Docker Compose : Username=user
            optionsBuilder.UseNpgsql( "Host=127.0.0.1;Port=10000;Username=ezchess;Password=123;Database=chess;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
