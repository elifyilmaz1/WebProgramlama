namespace WebProgramalama.Models
{
    public class hizmet
    {
        public int Id { get; set; }
        public string isim { get; set; }
        public decimal Ucret { get; set; }
        public ICollection<randevu> randevular { get; set; }
    }
}
