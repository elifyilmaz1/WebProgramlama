namespace WebProgramlama.Models
{
    using Microsoft.EntityFrameworkCore;

    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
    }
}
