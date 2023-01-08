using static labirintus.Jatek;
using static labirintus.Szerkeszto;

namespace labirintus
{
    class Program
    {
        public static int nyelv = 0;

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1) " + (nyelv == 0 ? "Játék" : "Game"));
            Console.WriteLine("2) " + (nyelv == 0 ? "Pályaszerkesztő" : "Level editor"));
            Console.WriteLine("3) " + (nyelv == 0 ? "Nyelvváltás" : "Change language"));
            Console.WriteLine("4) " + (nyelv == 0 ? "Kilépés" : "Quit"));

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    JatekKezdes();
                    break;
                case ConsoleKey.D2:
                    SzerkesztoKezdes();
                    break;
                case ConsoleKey.D3:
                    nyelv = nyelv == 0 ? 1 : 0;
                    Menu();
                    break;
                case ConsoleKey.D4:
                    System.Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;
            }
        }
    }
}