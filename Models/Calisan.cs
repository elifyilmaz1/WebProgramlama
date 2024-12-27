using WebProgramlama.Models;
using System;

namespace WebProgramlama.Models
{
    public class Calisan
    {

        public int Id { get; set; }
        public required string UzmanlikAlani { get; set; }
        public required string YapabildigiIslemler { get; set; }
        public required string Isim { get; set; }
        public decimal SaatlikUcret { get; set; }
        public string CalismaSaatleri { get; set; } = "10:00-17:00";
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
