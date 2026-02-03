using System.Data;

namespace PROJEKT_72413.Models
{   //Klasa Wycieczka dziedzicząca z ObiektBazy
    public class Wycieczka : ObiektBazy
    {
        public int IdWycieczki { get; set; }
        public string Cel { get; set; }
        public decimal Cena { get; set; }
        public int IdHotelu { get; set; }

        public Hotel Hotel { get; set; }
    }
}
