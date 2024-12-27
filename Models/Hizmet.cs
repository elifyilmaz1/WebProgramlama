namespace WebProgramlama.Models
{
    public class Hizmet
    {
        public int Id { get; set; }
        public required string Isim { get; set; }
        public decimal Ucret { get; set; }
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
