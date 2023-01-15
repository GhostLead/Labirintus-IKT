using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics.Tracing;

namespace labirintus 
{
    class Szerkeszto
    {
        const char JEL = '.';
        public static int nyelv = 3;
        public static void SzerkesztoKezdes()
        {
            nyelv = 0;
            while (true)
            {
                Console.WriteLine("Magyar vagy angol nyelvű legyen a pályaszerkesztő?");
                Console.WriteLine("Should the map editor be in Hungarian or English?");
                Console.WriteLine("0 = HUN/Magyar   1 = ENG/Angol");
                nyelv = Convert.ToInt32(Console.ReadLine());

                if (nyelv == 0 || nyelv == 1)
                {
                    break;

                }
                else
                {
                    Console.WriteLine("Hibás bevitel!");
                    Console.WriteLine("Wrong input!");

                }
            }
            Menu();
        }

        static char[,] PalyaKeszites(int sorokSzama, int oszlopokSzama)
        {
            char[,] matrix = new char[sorokSzama, oszlopokSzama];

            for (int sorIndex = 0; sorIndex < matrix.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < matrix.GetLength(1); oszlopIndex++)
                {
                    matrix[sorIndex, oszlopIndex] = JEL;
                }
            }

            Kiiratas(matrix);
            return matrix;
        }

        static char[,] Szerkesztes(char[,] palya, List<char> lista)
        {
            List<char> balra_kijarat = new List<char>() { '╬', '═', '╦', '╩', '╣', '╗', '╝',};
            List<char> jobbra_kijarat = new List<char>() { '╬', '═', '╦', '╩',  '╠', '╚', '╔'};
            List<char> felfele_kijarat = new List<char>() { '╬', '╩', '║', '╣', '╠', '╝', '╚',};
            List<char> lefele_kijarat = new List<char>() { '╬', '╦', '║', '╣', '╠', '╗', '╔'};

            while (true)
            {
                Console.Clear();

                Kiiratas(palya);

                if (nyelv == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[M]enü");
                    Console.WriteLine("[U]res pálya");
                    Console.WriteLine("[T]örlés (csak egy karakter törlése)");
                    Console.WriteLine("\nA mentéshez nyomjon 'ENTER'-t");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nAdjon meg egy kordinátát kettősponttal elválasztva ahova a falakat, vagy egy szobát szeretné rakni (pl: x:y): ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[M]enu");
                    Console.WriteLine("[E]mpty map");
                    Console.WriteLine("[D]elete (only one character)");
                    Console.WriteLine("\nTo save press 'ENTER'");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nEnter a coordinate, separated by a colon, where you want to place the walls or a room (e.g. x:y): ");
                }
                
                string input = Console.ReadLine();


                if (input == "")
                {

                    int szoba_darab = 0;
                    int kijarat_darab = 0;
                    int ut_darab = 0;

                    List<char> bal_szel = new List<char>();
                    List<char> jobb_szel = new List<char>();
                    List<char> felso_szel = new List<char>();
                    List<char> also_szel = new List<char>();

                    for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
                    {
                        for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                        {

                            if(oszlopIndex == 0)
                            {
                                bal_szel.Add(palya[sorIndex, oszlopIndex]);
                            }
                            if (sorIndex == 0)
                            {
                                felso_szel.Add(palya[sorIndex, oszlopIndex]);
                            }
                            if (oszlopIndex == palya.GetLength(1))
                            {
                                jobb_szel.Add(palya[sorIndex, oszlopIndex]);
                            }
                            if (oszlopIndex == palya.GetLength(0))
                            {
                                also_szel.Add(palya[sorIndex, oszlopIndex]);
                            }

                            foreach (char item in balra_kijarat)
                            {
                                if (balra_kijarat.Contains(item))
                                {
                                    kijarat_darab++;
                                }
                            }
                            foreach (char item in jobbra_kijarat)
                            {
                                if (jobbra_kijarat.Contains(item))
                                {
                                    kijarat_darab++;
                                }
                            }
                            foreach (char item in felfele_kijarat)
                            {
                                if (felfele_kijarat.Contains(item))
                                {
                                    kijarat_darab++;
                                }
                            }
                            foreach (char item in lefele_kijarat)
                            {
                                if (lefele_kijarat.Contains(item))
                                {
                                    kijarat_darab++;
                                }
                            }
                            if (palya[sorIndex, oszlopIndex] == lista[lista.Count - 1])
                            {
                                szoba_darab++;
                            }
                            else if (palya[sorIndex, oszlopIndex] == lista[0] ||
                                palya[sorIndex, oszlopIndex] == lista[1] ||
                                palya[sorIndex, oszlopIndex] == lista[2] ||
                                palya[sorIndex, oszlopIndex] == lista[3] ||
                                palya[sorIndex, oszlopIndex] == lista[4] ||
                                palya[sorIndex, oszlopIndex] == lista[5] ||
                                palya[sorIndex, oszlopIndex] == lista[6] ||
                                palya[sorIndex, oszlopIndex] == lista[7] ||
                                palya[sorIndex, oszlopIndex] == lista[8] ||
                                palya[sorIndex, oszlopIndex] == lista[9] ||
                                palya[sorIndex, oszlopIndex] == lista[10])
                            {
                                ut_darab++;
                            }
                        }
                    }

                    if (nyelv == 0)
                    {
                        if (kijarat_darab == 0)
                        {
                            Console.WriteLine("Nincs a pályán kijárat!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                        else if (szoba_darab == 0)
                        {
                            Console.WriteLine("Nincs a pályán szoba!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                        else if (ut_darab == 0)
                        {
                            Console.WriteLine("Nincs a pályán út!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                    }
                    else
                    {
                        if (kijarat_darab == 0)
                        {
                            Console.WriteLine("There is no escape from here!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                        else if (szoba_darab == 0)
                        {
                            Console.WriteLine("There are no rooms!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                        else if (ut_darab == 0)
                        {
                            Console.WriteLine("You can't walk here!!");
                            Thread.Sleep(3000);
                            Mentes(palya);
                        }
                    }

                    

                    Mentes(palya);
                }

                else if (input == "M" || input == "m")
                {
                    Console.Clear();
                    Menu();
                }

                if (Szerkeszto.nyelv == 0)
                {
                    if (input == "U" || input == "u")
                    {
                        Console.Clear();
                        for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
                        {
                            for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                            {
                                palya[sorIndex, oszlopIndex] = JEL;
                            }
                        }
                        Kiiratas(palya);
                    }
                }
                else
                {
                    if (input == "E" || input == "e")
                    {
                        Console.Clear();
                        for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
                        {
                            for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                            {
                                palya[sorIndex, oszlopIndex] = JEL;
                            }
                        }
                        Kiiratas(palya);
                    }
                }


                if (nyelv == 0)
                {
                    if (input == "T" || input == "t")
                    {
                        Console.Write("Adja meg a koordinátákat (x:y): ");
                        string beker = Console.ReadLine();
                        string[] koordinata = beker.Split(':');

                        int x_koord = Convert.ToInt32(koordinata[0]);
                        int y_koord = Convert.ToInt32(koordinata[1]);

                        palya[x_koord - 1, y_koord - 1] = JEL;
                    }
                }
                else
                {
                    if (input == "D" || input == "d")
                    {
                        if (nyelv == 0)
                        {
                            Console.Write("Adja meg a koordinátákat (x:y): ");
                        }
                        else
                        {
                            Console.Write("Enter the coordinates (x:y): ");
                        }
                        
                        string beker = Console.ReadLine();
                        string[] koordinata = beker.Split(':');

                        int x_koord = Convert.ToInt32(koordinata[0]);
                        int y_koord = Convert.ToInt32(koordinata[1]);

                        palya[x_koord - 1, y_koord - 1] = JEL;
                    }
                }

                

                if (input.Contains(':'))
                {

                    string[] koordinata = input.Split(':');

                    int x_koord = Convert.ToInt32(koordinata[0]);
                    int y_koord = Convert.ToInt32(koordinata[1]);

                    if (nyelv == 0)
                    {
                        Console.WriteLine("Mennyit szeretne lerakni: ");
                        int darab = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Használható karakterek: [0] = '╬' , [1] = '═', [2] = '╦', [3] = '╩', [4] = '║', [5] = '╣', [6] = '╠', [7] = '╗', [8] = '╝', [9] = '╚', [10] = '╔', [11] = '█')");
                        Console.Write("A lehelyezendő karakter száma:");
                        int karakter = Convert.ToInt32(Console.ReadLine());

                        if (karakter < 0 || karakter > lista.Count)
                        {
                            Console.WriteLine("Nincs ilyen számú fal");
                        }

                        else if (karakter >= 0 && karakter <= lista.Count)
                        {
                            if (darab > 1)
                            {

                                Console.Write("Milyen irányba? ([f]el, [l]e, [j]obb, [b]al) : ");
                                char irany = Convert.ToChar(Console.ReadLine());

                                if (irany == 'f')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        x_koord--;
                                    }

                                }
                                else if (irany == 'j')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        y_koord++;
                                    }
                                }
                                else if (irany == 'b')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        y_koord--;
                                    }

                                }
                                else if (irany == 'l')
                                {
                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        x_koord++;
                                    }
                                }
                            }
                            else
                            {
                                palya[x_koord, y_koord] = lista[karakter];
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("How much do you want to place down? : ");
                        int darab = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Usable characters: [0] = '╬' , [1] = '═', [2] = '╦', [3] = '╩', [4] = '║', [5] = '╣', [6] = '╠', [7] = '╗', [8] = '╝', [9] = '╚', [10] = '╔', [11] = '█')");
                        Console.Write("The number of the chosen character:");
                        int karakter = Convert.ToInt32(Console.ReadLine());

                        if (karakter < 0 || karakter > lista.Count)
                        {
                            Console.WriteLine("There is no character with this number!");
                        }

                        else if (karakter >= 0 && karakter <= lista.Count)
                        {
                            if (darab > 1)
                            {

                                Console.Write("What direction? ([u]p, [d]own, [r]ight, [l]elft) : ");
                                char irany = Convert.ToChar(Console.ReadLine());

                                if (irany == 'u' || irany == 'U')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        x_koord--;
                                    }

                                }
                                else if (irany == 'r' || irany == 'R')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        y_koord++;
                                    }
                                }
                                else if (irany == 'l' || irany == 'L')
                                {

                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        y_koord--;
                                    }

                                }
                                else if (irany == 'd' || irany == 'D')
                                {
                                    for (int i = 0; i < darab; i++)
                                    {
                                        palya[x_koord, y_koord] = lista[karakter];
                                        x_koord++;
                                    }
                                }
                            }
                            else
                            {
                                palya[x_koord, y_koord] = lista[karakter];
                            }
                        }
                    }

                    
                }
               

                
            }
             
        }

        static void Mentes(char[,] mentesPalya)
        {
            List<char> falak = new List<char>() { '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔', '█'};

            if (Szerkeszto.nyelv == 0)
            {
                Console.WriteLine("Adja meg a fájl mentési nevét (ha mégsem szeretné menteni nyomjon 'ENTER'-t): ");
            }
            else
            {
                Console.WriteLine("Enter the name of the file to save (press 'ENTER' if you do not want to save it): ");
            }

            
            string nev = Console.ReadLine();

            if (nev == "")
            {
                Szerkesztes(mentesPalya, falak);
            }
            else
            {
                string[] sorok = new string[mentesPalya.GetLength(0)];

                for (int sorIndex = 0; sorIndex < mentesPalya.GetLength(0); sorIndex++)
                {
                    for (int oszlopIndex = 0; oszlopIndex < mentesPalya.GetLength(1); oszlopIndex++)
                    {
                        sorok[sorIndex] += mentesPalya[sorIndex, oszlopIndex];
                    }
                }
                File.WriteAllLines(nev+".txt", sorok);
            }
            Console.Clear();
            if (nyelv == 0)
            {
                Console.WriteLine("Készen van a pálya!");
            }
            else
            {
                Console.WriteLine("The map is ready!");
            }
            
            Environment.Exit(0);

        }

        static char[,] Betoltes(string palyaNeve)
        {
            string[] sorok = File.ReadAllLines(palyaNeve);
            char[,] palya = new char[sorok.Length, sorok[0].Length];
            for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
            {
                for (int oszlopIndexe = 0; oszlopIndexe < palya.GetLength(1); oszlopIndexe++)
                {
                    palya[sorIndex, oszlopIndexe] = sorok[sorIndex][oszlopIndexe];
                }
            }

            Kiiratas(palya);
            return palya;
        }

        static void Menu()
        {
            

            Console.Clear();
            List<char> falak = new List<char>() { '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔', '█'};

            if (nyelv == 0)
            {
                Console.WriteLine("[1] Pálya létrehozása");
                Console.WriteLine("[2] Pálya betöltése");
                Console.WriteLine("[3] Nyelv váltás");
                Console.WriteLine("[4] Kilépés a programból");
            }
            else
            {
                Console.WriteLine("[1] Create map");
                Console.WriteLine("[2] Load map");
                Console.WriteLine("[3] Change language");
                Console.WriteLine("[4] Exit program");
            }

            

            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                Console.Clear();
                if(nyelv == 0)
                {
                    Console.WriteLine("Adja meg a mátrix méretét (pl: 100x100): ");
                }
                else
                {
                    Console.WriteLine("Enter the size of the matrix (e.g: 100x100): ");
                }

                string kordinata = Console.ReadLine();
                string[] ok = kordinata.Split("x");


                int szam = Convert.ToInt32(ok[0]);
                int szam2 = Convert.ToInt32(ok[1]);

                Szerkesztes(PalyaKeszites(szam, szam2), falak);

            }

            else if (input == 2)
            {
                Console.Clear();
                if (nyelv == 0)
                {
                    Console.Write("Adja meg a betöltendő pálya nevét: ");
                }
                else
                {
                    Console.Write("Enter the name of the map to be loaded: ");
                }
                
                string nev = Console.ReadLine();

                Szerkesztes(Betoltes(nev), falak);
            }

            else if (input == 3)
            {
                if (nyelv == 0)
                {
                    nyelv++;
                    Menu();
                }
                else
                {
                    nyelv--;
                    Menu();
                }
            }

            else if (input == 4)
            {
                Console.Clear();
                if (nyelv == 0)
                {
                    Console.WriteLine("Viszlát!");
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                }
                Thread.Sleep(3000);
                Console.Clear();
                Environment.Exit(0);
            }
        }

        static void Kiiratas(char[,] palya)
        {
            for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                {
                    Console.Write(palya[sorIndex, oszlopIndex] + "");
                }
                Console.WriteLine();
            }
        }
    }
}
