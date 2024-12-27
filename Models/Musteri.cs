using System.Collections.Generic;
using System;
namespace WebProgramlama.Models

{
    public class Musteri
    {
        public int Id { get; set; }
        public required string IsimSoyisim { get; set; }
        public required string IletisimNumarasi { get; set; }
        public required string Eposta { get; set; }
        public required string Sifre { get; set; }
        public required string Rol { get; set; }
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
