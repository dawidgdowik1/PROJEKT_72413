using System;

namespace PROJEKT_72413.Models
{
    public class Klient
    {
        public int IdKlienta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }

        public Rezerwacja Rezerwacja
        {
            get => default;
            set
            {
            }
        }
    }
}
