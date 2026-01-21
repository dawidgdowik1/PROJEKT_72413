using System;

namespace PROJEKT_72413.Models
{
    public class Platnosc
    {
        public int IdPlatnosci { get; set; }
        public decimal Kwota { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string MetodaPlatnosci { get; set; }
        public int IdRezerwacji { get; set; } // Klucz obcy
    }
}