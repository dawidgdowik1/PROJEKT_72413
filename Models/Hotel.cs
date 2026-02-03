namespace PROJEKT_72413.Models
{ 
    ///Tworzymy klasę Hotel dziedzicząca z ObiektBazy
    public class Hotel : ObiektBazy
    {
        public int IdHotelu { get; set; }
        public string Nazwa { get; set; }
        public int Standard { get; set; } 
        public string Lokalizacja { get; set; }



        /// Powiązanie:
        public Wycieczka Wycieczka { get; set; }

        public Biuro Biuro { get; set; }
        
    }
}