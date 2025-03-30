using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Mise à jour de la chaîne de connexion pour Docker Compose : Username=user
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=10000;Username=user;Password=123;Database=postgres");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
