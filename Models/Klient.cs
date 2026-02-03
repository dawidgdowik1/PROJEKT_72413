namespace PROJEKT_72413.Models
{   ///Tworzymy klasę klient dziedziczącą z Osoba
    public class Klient : Osoba
    {
        
        public int Id { get; set; }
        public string Email { get; set; }
    }
}