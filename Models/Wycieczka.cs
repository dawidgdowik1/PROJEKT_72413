using System;

namespace PROJEKT_72413.Models
{
    public class Wycieczka
    {
        public int IdWycieczki { get; set; }
        public string Nazwa { get; set; }
        public string Cel { get; set; }
        public decimal Cena { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public int IdHotelu { get; set; } // Klucz obcy

        public Rezerwacja Rezerwacja
        {
            get => default;
            set
            {
            }
        }
    }
}