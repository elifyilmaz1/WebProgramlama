namespace WebProgramalama.Models
{
    public class musteri
    {
        public int Id { get; set; }
        public string isimSoyisim { get; set; }
        public decimal iletisimNumarasi { get; set; }
        public ICollection<randevu> randevular { get; set; }
    }
}
