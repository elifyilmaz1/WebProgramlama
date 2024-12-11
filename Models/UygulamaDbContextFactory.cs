using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebProgramlama.Models
{
    public class UygulamaDbContextFactory : IDesignTimeDbContextFactory<UygulamaDbContext>
    {
        public UygulamaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UygulamaDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Database=KuaforDB;Username=postgres;Password=12345");

            return new UygulamaDbContext(optionsBuilder.Options);
        }
    }
}
