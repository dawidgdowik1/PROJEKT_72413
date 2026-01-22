namespace PROJEKT_72413.Models
{
    public class Hotel : ObiektBazy
    {
        public int IdHotelu { get; set; }
        public string Nazwa { get; set; }
        public int Standard { get; set; } 
        public string Kraj { get; set; }
        public string Miasto { get; set; }

        public Wycieczka Wycieczka
        {
            get => default;
            set
            {
            }
        }

        public Biuro Biuro
        {
            get => default;
            set
            {
            }
        }
    }
}