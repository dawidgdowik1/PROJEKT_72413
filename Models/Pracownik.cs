namespace PROJEKT_72413.Models
{
    //Tworzymy klasę Pracownik dziedziczą z Osoba
    public class Pracownik : Osoba
    {
        public string Stanowisko { get; set; }
        public int IdBiura { get; set; }

        public Biuro Biuro { get; set; }
    }
}