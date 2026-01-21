namespace PROJEKT_72413.Models
{
    public class Pracownik
    {
        public int IdPracownika { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stanowisko { get; set; }
        public int IdBiura { get; set; } // Klucz obcy
    }
}