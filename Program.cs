using System;
using PROJEKT_72413.Database;

namespace PROJEKT_72413
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseService db = new DatabaseService();

            if (db.TestConnection())
            {
                Console.WriteLine("Sukces: Polaczono z baza");
            }
            else
            {
                Console.WriteLine("Blad: Brak polaczenia");
            }

            Console.WriteLine("Nacisnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}