using WebProgramlama.Models;
using System;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual Calisan? Calisan { get; set; }
        public virtual Musteri? Musteri { get; set; }
        public virtual Hizmet? Hizmet { get; set; }
    }
}
