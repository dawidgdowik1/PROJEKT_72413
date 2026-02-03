using System;

namespace PROJEKT_72413.Models
{   //Tworzymy klasę Rezerwacja dziedzicząca z ObiektBazy
    public class Rezerwacja : ObiektBazy
    {
        public int IdRezerwacji { get; set; }
        public DateTime DataRezerwacji { get; set; }
        public int IdKlienta { get; set; } 
        public int IdWycieczki { get; set; }
        public int IdPracownika { get; set; }

        public Klient Klient { get; set; }
        public Wycieczka Wycieczka { get; set; }
        public Pracownik Pracownik { get; set; }

    }
}