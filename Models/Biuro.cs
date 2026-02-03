using System;

namespace PROJEKT_72413.Models
{
    public class Biuro : ObiektBazy
    {
        public int IdBiura { get; set; }
        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string Adres { get; set; }

        public Pracownik Pracownik { get; set; }
      
    }
}
