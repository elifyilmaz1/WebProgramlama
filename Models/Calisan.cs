using WebProgramlama.Models;
using System;

namespace WebProgramlama.Models
{
    public class Calisan
    {
      
            public int Id { get; set; }
            public required string Isim { get; set; }
            public required string Gorev { get; set; }
            public decimal SaatlikUcret { get; set; }
            public required ICollection<Randevu> Randevu { get; set; }  
    }
}
