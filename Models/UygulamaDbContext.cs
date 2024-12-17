using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;


namespace WebProgramlama.Models
{
     public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Calisan> Calisan { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Calisan)
                .WithMany(c => c.Randevu)
                .HasForeignKey(r => r.CalisanId);

            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Musteri)
                .WithMany(m => m.Randevu)
                .HasForeignKey(r => r.MusteriId);
        
             modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Hizmet)
                .WithMany(h => h.Randevu)
                .HasForeignKey(r => r.HizmetId);
   }

}
    }

