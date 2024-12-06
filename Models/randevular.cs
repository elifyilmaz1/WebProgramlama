namespace WebProgramalama.Models
{
    public class randevu
    {
        public int Id { get; set; }
        public int musteriId { get; set; }
        public int calisanId { get; set; }
        public DateTime randevuTarihi { get; set; }
        public string hizmet { get; set; }  
        public decimal ucret { get; set; }

        public calisan calisanlar{ get; set; }
        public musteri musteriler{ get; set; }
    }
}
