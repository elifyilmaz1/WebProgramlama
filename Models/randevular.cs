using WebProgramlama.Models;

namespace WebProgramlama.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int CalisanId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public required string Hizmet { get; set; }  
        public decimal Ucret { get; set; }

        public required Calisan Calisanlar{ get; set; }
        public required Musteri Musteriler{ get; set; }
    }
}
