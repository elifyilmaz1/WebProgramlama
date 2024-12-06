namespace WebProgramalama.Models
{
    using Microsoft.EntityFrameworkCore;

    public class UygulamaDbContext : DbContext
    {
        public UygulamanDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {
        }
        public DbSet<calisan> calisanlar{ get; set; }
        public DbSet<musteri> musteriler{ get; set; }
        public DbSet<randevu> randevular{ get; set; }
        public DbSet<hizmet> hizmetler{ get; set; }
    }
}
