using System;
using System.Data;
using PROJEKT_72413.Database;
using PROJEKT_72413.Models;

namespace PROJEKT_72413
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tworzymy połączenie z naszą bazą danych
            IUslugaDanych db = new ObslugaBazy();

            while (true)
            {
                // Czyszczenie konsoli i wyświetlanie menu głównego
                Console.Clear();
                Console.WriteLine("--- SYSTEM BIURA PODRÓŻY ---");
                Console.WriteLine("1. Lista Wycieczek (Zarządzanie ofertami)");
                Console.WriteLine("2. Dodaj Klienta");
                Console.WriteLine("3. Nowa Rezerwacja (Rezerwacja biletów)");
                Console.WriteLine("4. Rozlicz płatność (Rozliczanie się z biurem)");
                Console.WriteLine("0. Koniec");
                Console.Write("Wybór: ");

                // Pobranie wyboru od użytkownika
                string opcja = Console.ReadLine();
                if (opcja == "0") break;

                // Przełącznik sterujący opcjami menu
                switch (opcja)
                {
                    case "1":
                        ZarzadzajOfertami(db);
                        break;
                    case "2":
                        DodajKlienta(db);
                        break;
                    case "3":
                        ZrobRezerwacje(db);
                        break;
                    case "4":
                        RozliczPlatnosc(db);
                        break;
                }
                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
            }
        }

        // Metoda obsługująca CRUD wycieczek
        static void ZarzadzajOfertami(IUslugaDanych db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- ZARZĄDZANIE OFERTAMI (CRUD) ---");
                Console.WriteLine("1. Wyświetl wszystkie oferty");
                Console.WriteLine("2. Dodaj nową ofertę");
                Console.WriteLine("3. Edytuj ofertę");
                Console.WriteLine("4. Usuń ofertę");
                Console.WriteLine("0. Powrót do menu głównego");
                Console.Write("Wybór: ");

                string wybor = Console.ReadLine();
                if (wybor == "0") break;

                switch (wybor)
                {
                    case "1":
                        // Wyświetlanie listy wycieczek
                        string sqlRead = "SELECT * FROM wycieczki";
                        DataTable dt = db.PobierzTabele(sqlRead);
                        foreach (DataRow r in dt.Rows)
                            Console.WriteLine($"ID: {r["id_wycieczki"]} | Cel: {r["cel"]} | Cena: {r["cena"]} PLN");
                        break;

                    case "2":
                        // Dodawanie nowej wycieczki do bazy
                        Console.Write("Cel: "); string cel = Console.ReadLine();
                        Console.Write("Cena: "); string cena = Console.ReadLine();
                        Console.Write("ID Hotelu: "); string hotel = Console.ReadLine();
                        if (!string.IsNullOrEmpty(cel))
                        {
                            db.WykonajPolecenie($"INSERT INTO wycieczki (cel, cena, id_hotelu) VALUES ('{cel}', {cena}, {hotel})");
                            Console.WriteLine("Dodano ofertę!");
                        }
                        break;

                    case "3":
                        Console.WriteLine("\n--- EDYCJA OFERTY ---");

                        // Pobranie aktualnych danych do edycji
                        string sqlList = "SELECT * FROM wycieczki";
                        DataTable dtEdit = db.PobierzTabele(sqlList);

                        if (dtEdit.Rows.Count == 0)
                        {
                            Console.WriteLine("Brak ofert do edycji.");
                            break;
                        }

                        Console.WriteLine("Dostępne oferty:");
                        foreach (DataRow r in dtEdit.Rows)
                        {
                            Console.WriteLine($"[{r["id_wycieczki"]}] {r["cel"]} - {r["cena"]} PLN");
                        }
                        Console.WriteLine("-----------------------");


                        Console.Write("Podaj ID wycieczki do edycji: ");
                        string idE = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(idE))
                        {
                            Console.WriteLine("BŁĄD: Musisz podać ID!");
                            break;
                        }

                        Console.Write("Podaj nową nazwę (Cel): ");
                        string nowyCel = Console.ReadLine();

                        Console.Write("Podaj nową cenę: ");
                        string nowaCena = Console.ReadLine();

                        // Aktualizacja rekordu w bazie (UPDATE)
                        string sqlUpdate = $"UPDATE wycieczki SET cel = '{nowyCel}', cena = {nowaCena} WHERE id_wycieczki = {idE}";

                        try
                        {
                            db.WykonajPolecenie(sqlUpdate);
                            Console.WriteLine("\nSukces: Oferta została zaktualizowana!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nBłąd edycji: " + ex.Message);
                        }
                        break;

                    case "4":
                        // Usuwanie wycieczki z bazy (DELETE)
                        Console.Write("Podaj ID wycieczki do usunięcia: "); string idD = Console.ReadLine();
                        db.WykonajPolecenie($"DELETE FROM wycieczki WHERE id_wycieczki = {idD}");
                        Console.WriteLine("Oferta usunięta!");
                        break;
                }
                Console.WriteLine("\nNaciśnij klawisz...");
                Console.ReadKey();
            }
        }

        // Metoda dodająca nowego klienta
        static void DodajKlienta(IUslugaDanych db)
        {
            Console.WriteLine("\n--- DODAWANIE NOWEGO KLIENTA ---");
            Klient k = new Klient();

            Console.Write("Imię: "); k.Imie = Console.ReadLine();
            Console.Write("Nazwisko: "); k.Nazwisko = Console.ReadLine();
            Console.Write("Email: "); k.Email = Console.ReadLine();

            // Sprawdzenie czy pola nie są puste
            if (string.IsNullOrWhiteSpace(k.Imie) || string.IsNullOrWhiteSpace(k.Nazwisko))
            {
                Console.WriteLine("BŁĄD: Imię i nazwisko są wymagane!");
                return;
            }

            string sql = $"INSERT INTO klienci (imie, nazwisko, email) VALUES ('{k.Imie}', '{k.Nazwisko}', '{k.Email}')";

            try
            {
                db.WykonajPolecenie(sql);
                Console.WriteLine("Sukces: Klient został dodany do bazy!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd bazy: " + ex.Message);
            }
        }

        // Metoda tworząca nową rezerwację
        static void ZrobRezerwacje(IUslugaDanych db)
        {
            Console.WriteLine("\n--- NOWA REZERWACJA ---");

            // Pobieramy listę klientów
            DataTable dtK = db.PobierzTabele("SELECT id_klienta, imie, nazwisko FROM klienci");
            if (dtK.Rows.Count == 0)
            {
                Console.WriteLine("BŁĄD: Brak klientów w bazie. Dodaj klienta przed rezerwacją!");
                return;
            }

            Console.WriteLine("LISTA KLIENTÓW:");
            foreach (DataRow r in dtK.Rows)
                Console.WriteLine($"[{r["id_klienta"]}] {r["imie"]} {r["nazwisko"]}");

            // Pobieramy listę wycieczek
            DataTable dtW = db.PobierzTabele("SELECT id_wycieczki, cel FROM wycieczki");
            if (dtW.Rows.Count == 0)
            {
                Console.WriteLine("\nBŁĄD: Brak dostępnych wycieczek!");
                return;
            }

            Console.WriteLine("\nLISTA WYCIECZEK:");
            foreach (DataRow r in dtW.Rows)
                Console.WriteLine($"[{r["id_wycieczki"]}] {r["cel"]}");

            // Zbieranie danych do rezerwacji
            Console.Write("\nPodaj ID Klienta: "); string idK = Console.ReadLine();
            Console.Write("Podaj ID Wycieczki: "); string idW = Console.ReadLine();

            // Walidacja i zapis do bazy
            if (!string.IsNullOrWhiteSpace(idK) && !string.IsNullOrWhiteSpace(idW))
            {
                string sql = $"INSERT INTO rezerwacje (id_klienta, id_wycieczki, data_rezerwacji) VALUES ({idK}, {idW}, CURDATE())";
                try
                {
                    db.WykonajPolecenie(sql);
                    Console.WriteLine("Sukces: Rezerwacja zapisana!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd SQL: Sprawdź czy podałeś poprawne ID z listy! " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Błąd: Pola ID nie mogą być puste!");
            }
        }

        // Metoda księgująca wpłaty
        static void RozliczPlatnosc(IUslugaDanych db)
        {
            Console.WriteLine("\n--- ROZLICZENIE PŁATNOŚCI ---");
            Console.Write("Podaj ID Rezerwacji: ");
            string idR = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(idR)) return;

            // Sprawdzamy czy rezerwacja istnieje
            DataTable check = db.PobierzTabele($"SELECT id_rezerwacji FROM rezerwacje WHERE id_rezerwacji = {idR}");

            if (check.Rows.Count == 0)
            {
                Console.WriteLine($"BŁĄD: Nie znaleziono rezerwacji o ID: {idR}!");
                return;
            }

            Console.Write("Podaj kwotę wpłaty: ");
            string kwota = Console.ReadLine();
            string kwotaSql = kwota.Replace(',', '.');

            string sql = $"INSERT INTO platnosci (id_rezerwacji, kwota, data_platnosci) VALUES ({idR}, {kwotaSql}, CURDATE())";

            try
            {
                db.WykonajPolecenie(sql);
                Console.WriteLine("Sukces: Płatność zaksięgowana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd zapisu: " + ex.Message);
            }
        }
    }
}