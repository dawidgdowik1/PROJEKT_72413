using System;

namespace PROJEKT_72413.Models
{
    public class Rezerwacja
    {
        public int IdRezerwacji { get; set; }
        public DateTime DataRezerwacji { get; set; }
        public int IdKlienta { get; set; } // Klucz obcy
        public int IdWycieczki { get; set; } // Klucz obcy
        public string Status { get; set; }

        public Platnosc Platnosc
        {
            get => default;
            set
            {
            }
        }

        public Pracownik Pracownik
        {
            get => default;
            set
            {
            }
        }
    }
}