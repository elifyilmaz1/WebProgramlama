namespace WebProgramlama.Models
{
    public class calisan
    {
      
            public int Id { get; set; }
            public string isim { get; set; }
            public string görev { get; set; }
            public decimal saatlikUcret { get; set; }
            public ICollection<randevu> randevular { get; set; }  
        

    }
}
