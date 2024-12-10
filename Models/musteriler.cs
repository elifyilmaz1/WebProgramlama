namespace WebProgramlama.Models
{
    public class Musteri
    {
        public int Id { get; set; }
        public required string IsimSoyisim { get; set; }
        public decimal IletisimNumarasi { get; set; }
        public required ICollection<Randevu> Randevular { get; set; }
    }
}
