using WebProgramlama.Models;
using System;

namespace WebProgramlama.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime RandevuTarihi { get; set; }

        public TimeSpan BaslangicSaati { get; set; }
        public int MusteriId { get; set; }
        public int CalisanId { get; set; }
        public int HizmetId { get; set; }  
        
        public decimal Ucret { get; set; }
         
        public required Calisan Calisan{ get; set; }
        public required Musteri Musteri{ get; set; }
        public required Hizmet Hizmet { get; set; }
       
    }
}
