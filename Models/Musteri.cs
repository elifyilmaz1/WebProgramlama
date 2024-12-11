using System.Collections.Generic;
using System;
namespace WebProgramlama.Models

{
    public class Musteri
    {
        public int Id { get; set; }
        public required string IsimSoyisim { get; set; }
        public decimal IletisimNumarasi { get; set; }
        public required ICollection<Randevu> Randevu { get; set; }
    }
}
