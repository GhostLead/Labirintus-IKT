using System;
using static labirintus.Jatek;
using static labirintus.Szerkeszto;

namespace labirintus
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu() {
            Console.WriteLine("1) Játék");
            Console.WriteLine("2) Pályaszerkesztő");
            Console.WriteLine("3) Kilépés");
            string menu = Console.ReadLine();

            Console.Clear();
            switch (menu)
            {
                case "1":
                    JatekKezdes();
                    break;
                case "2":
                    SzerkesztoKezdes();
                    break;
                case "3":
                    System.Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;
            }
        }
    }
}