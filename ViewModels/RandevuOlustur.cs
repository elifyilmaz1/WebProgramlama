namespace WebProgramlama.ViewModels
{
    public class RandevuOlustur
    {
        public DateTime RandevuTarihi { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public int CalisanId { get; set; }
        public int HizmetId { get; set; }
    }
}
