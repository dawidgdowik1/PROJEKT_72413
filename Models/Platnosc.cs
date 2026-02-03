using System;

namespace PROJEKT_72413.Models
{   //Klasa Platnosc dziedzicząca z ObiektBazy
    public class Platnosc : ObiektBazy
    {
        public int IdPlatnosci { get; set; }
        public decimal Kwota { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public int IdRezerwacji { get; set; }

        public Rezerwacja Rezerwacja { get; set; }
    }
}