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
            IDataService db = new DatabaseService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- SYSTEM BIURA PODRÓŻY ---");
                Console.WriteLine("1. Lista Wycieczek (Zarządzanie ofertami)");
                Console.WriteLine("2. Dodaj Klienta");
                Console.WriteLine("3. Nowa Rezerwacja (Rezerwacja biletów)");
                Console.WriteLine("4. Rozlicz płatność (Rozliczanie się z biurem)");
                Console.WriteLine("0. Koniec");
                Console.Write("Wybór: ");

                string opcja = Console.ReadLine();
                if (opcja == "0") break;

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
        static void ZarzadzajOfertami(IDataService db)
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
                        
                        string sqlRead = "SELECT * FROM wycieczki";
                        DataTable dt = db.GetTable(sqlRead);
                        foreach (DataRow r in dt.Rows)
                            Console.WriteLine($"ID: {r["id_wycieczki"]} | Cel: {r["cel"]} | Cena: {r["cena"]} PLN");
                        break;

                    case "2":
                      
                        Console.Write("Cel: "); string cel = Console.ReadLine();
                        Console.Write("Cena: "); string cena = Console.ReadLine();
                        Console.Write("ID Hotelu: "); string hotel = Console.ReadLine();
                        if (!string.IsNullOrEmpty(cel))
                        {
                            db.ExecuteCommand($"INSERT INTO wycieczki (cel, cena, id_hotelu) VALUES ('{cel}', {cena}, {hotel})");
                            Console.WriteLine("Dodano ofertę!");
                        }
                        break;

                    case "3":
                        Console.WriteLine("\n--- EDYCJA OFERTY ---");

                        
                        string sqlList = "SELECT * FROM wycieczki";
                        DataTable dtEdit = db.GetTable(sqlList);

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

                        
                        string sqlUpdate = $"UPDATE wycieczki SET cel = '{nowyCel}', cena = {nowaCena} WHERE id_wycieczki = {idE}";

                        try
                        {
                            db.ExecuteCommand(sqlUpdate);
                            Console.WriteLine("\nSukces: Oferta została zaktualizowana!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nBłąd edycji: " + ex.Message);
                        }
                        break;

                    case "4":
                        
                        Console.Write("Podaj ID wycieczki do usunięcia: "); string idD = Console.ReadLine();
                        db.ExecuteCommand($"DELETE FROM wycieczki WHERE id_wycieczki = {idD}");
                        Console.WriteLine("Oferta usunięta!");
                        break;
                }
                Console.WriteLine("\nNaciśnij klawisz...");
                Console.ReadKey();
            }
        }

        static void DodajKlienta(IDataService db)
        {
            Console.WriteLine("\n--- DODAWANIE NOWEGO KLIENTA ---");
            Klient k = new Klient();

            Console.Write("Imię: "); k.Imie = Console.ReadLine();
            Console.Write("Nazwisko: "); k.Nazwisko = Console.ReadLine();
            Console.Write("Email: "); k.Email = Console.ReadLine();

          
            if (string.IsNullOrWhiteSpace(k.Imie) || string.IsNullOrWhiteSpace(k.Nazwisko))
            {
                Console.WriteLine("BŁĄD: Imię i nazwisko są wymagane!");
                return;
            }

            string sql = $"INSERT INTO klienci (imie, nazwisko, email) VALUES ('{k.Imie}', '{k.Nazwisko}', '{k.Email}')";

            try
            {
                db.ExecuteCommand(sql);
                Console.WriteLine("Sukces: Klient został dodany do bazy!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd bazy: " + ex.Message);
            }
        }

        static void ZrobRezerwacje(IDataService db)
        {
            Console.WriteLine("\n--- NOWA REZERWACJA ---");

          
            DataTable dtK = db.GetTable("SELECT id_klienta, imie, nazwisko FROM klienci");
            if (dtK.Rows.Count == 0)
            {
                Console.WriteLine("BŁĄD: Brak klientów w bazie. Dodaj klienta przed rezerwacją!");
                return; 
            }

            Console.WriteLine("LISTA KLIENTÓW:");
            foreach (DataRow r in dtK.Rows)
                Console.WriteLine($"[{r["id_klienta"]}] {r["imie"]} {r["nazwisko"]}");

            DataTable dtW = db.GetTable("SELECT id_wycieczki, cel FROM wycieczki");
            if (dtW.Rows.Count == 0)
            {
                Console.WriteLine("\nBŁĄD: Brak dostępnych wycieczek!");
                return;
            }

            Console.WriteLine("\nLISTA WYCIECZEK:");
            foreach (DataRow r in dtW.Rows)
                Console.WriteLine($"[{r["id_wycieczki"]}] {r["cel"]}");

          
            Console.Write("\nPodaj ID Klienta: "); string idK = Console.ReadLine();
            Console.Write("Podaj ID Wycieczki: "); string idW = Console.ReadLine();

          
            if (!string.IsNullOrWhiteSpace(idK) && !string.IsNullOrWhiteSpace(idW))
            {
                string sql = $"INSERT INTO rezerwacje (id_klienta, id_wycieczki, data_rezerwacji) VALUES ({idK}, {idW}, CURDATE())";
                try
                {
                    db.ExecuteCommand(sql);
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

        static void RozliczPlatnosc(IDataService db)
        {
            Console.WriteLine("\n--- ROZLICZENIE PŁATNOŚCI ---");
            Console.Write("Podaj ID Rezerwacji: ");
            string idR = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(idR)) return;

           
            DataTable check = db.GetTable($"SELECT id_rezerwacji FROM rezerwacje WHERE id_rezerwacji = {idR}");

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
                db.ExecuteCommand(sql);
                Console.WriteLine("Sukces: Płatność zaksięgowana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd zapisu: " + ex.Message);
            }
        }
    }
}