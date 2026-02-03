using System.Data;

namespace PROJEKT_72413.Database
{
    // Interfejs definiujący
    public interface IUslugaDanych
    {
        // Metoda do wysyłania poleceń INSERT, UPDATE, DELETE (nie zwraca danych)
        void WykonajPolecenie(string sql);

        // Metoda do pobierania danych (SELECT), zwraca tabelę z wynikami
        DataTable PobierzTabele(string sql);
    }
}