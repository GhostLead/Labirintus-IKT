using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
//using static labirintus.Metodusok;

namespace labirintus 
{
    class Szerkeszto
    {
        const char JEL = '.';


        static void Main(string[] args)
        {
            menu();
        }

        static char[,] palyaKeszites(int sorokSzama, int oszlopokSzama)
        {
            char[,] matrix = new char[sorokSzama, oszlopokSzama];

            for (int sorIndex = 0; sorIndex < matrix.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < matrix.GetLength(1); oszlopIndex++)
                {
                    matrix[sorIndex, oszlopIndex] = JEL;
                }
            }

            Kiirat(matrix);
            return matrix;
        }

        static char[,] szerkesztes(char[,] palya, List<char> lista)
        {

            while (true)
            {
                Console.Clear();

                Kiirat(palya);


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n[M]enu");
                Console.WriteLine("[U]res pálya");
                Console.WriteLine("[T]örlés (csak egy karakter törlése)");
                Console.WriteLine("\nA mentéshez nyomjon 'ENTER'-t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nAdjon meg egy kordinátát kettősponttal elválasztva ahova a falakat, vagy egy szobát szeretné rakni (pl: x:y): ");
                string input = Console.ReadLine();


                if (input == "")
                {

                    int szoba_darab = 0;
                    int kijarat_darab = 0;
                    int ut_darab = 0;

                    for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
                    {
                        for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                        {

                            if (palya[sorIndex, oszlopIndex] == lista[lista.Count - 1])
                            {
                                kijarat_darab++;
                            }
                            else if (palya[sorIndex, oszlopIndex] == lista[lista.Count - 2])
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

                            else if (szoba_darab == 0)
                            {
                                Console.WriteLine("Nincs a pályán szoba!!");
                                Thread.Sleep(3000);
                                mentes(palya);
                            }
                            else if (kijarat_darab == 0)
                            {
                                Console.WriteLine("Nincs a pályán kijárat!!");
                                Thread.Sleep(3000);
                                mentes(palya);
                            }
                            else if (ut_darab == 0)
                            {
                                Console.WriteLine("Nincs a pályán út!!");
                                Thread.Sleep(3000);
                                mentes(palya);
                            }
                        }
                    }

                    mentes(palya);

                    continue;
                }

                else if (input == "M" || input == "m")
                {
                    Console.Clear();
                    menu();
                }

                else if (input == "U" || input == "u")
                {
                    Console.Clear();
                    for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
                    {
                        for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                        {
                            palya[sorIndex, oszlopIndex] = JEL;
                        }
                    }
                    Kiirat(palya);
                }

                else if (input == "T" || input == "t")
                {
                    Console.Write("Adja meg a koordinátákat (x:y): ");
                    string beker = Console.ReadLine();
                    string[] koordinata = beker.Split(':');

                    int x_koord = Convert.ToInt32(koordinata[0]);
                    int y_koord = Convert.ToInt32(koordinata[1]);
                    
                    palya[x_koord - 1, y_koord - 1] = JEL;
                }

                else if (input.Contains(':'))
                {

                    string[] koordinata = input.Split(':');

                    int x_koord = Convert.ToInt32(koordinata[0]);
                    int y_koord = Convert.ToInt32(koordinata[1]);

                    Console.WriteLine("Mennyit szeretne lerakni: ");
                    int darab = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Használható karakterek: (0)'╬', (1)'═', (2)'╦', (3)'╩', (4)'║', (5)'╣', (6)'╠', (7)'╗', (8)'╝', (9)'╚', (10)'╔', (11)'█', (12)'▄'");
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
                                    palya[x_koord - 1, y_koord - 1] = lista[karakter];
                                    x_koord--;
                                }

                            }

                            else if (irany == 'j')
                            {

                                for (int i = 0; i < darab; i++)
                                {
                                    palya[x_koord - 1, y_koord - 1] = lista[karakter];
                                    y_koord++;
                                }

                            }

                            else if (irany == 'b')
                            {

                                for (int i = 0; i < darab; i++)
                                {
                                    palya[x_koord - 1, y_koord - 1] = lista[karakter];
                                    y_koord--;
                                }

                            }

                            else if (irany == 'l')
                            {

                                for (int i = 0; i < darab; i++)
                                {
                                    palya[x_koord - 1, y_koord - 1] = lista[karakter];
                                    x_koord++;
                                }

                            }

                        }



                        else
                        {

                            palya[x_koord - 1, y_koord - 1] = lista[karakter];

                        }
                        Console.Clear();

                    }

                    

                }
                else if (input != "U" || input != "" || input.Contains(':') == false || input.Contains('T') == false || input.Contains('M') == false)
                {
                    Console.WriteLine("Hibás megadás");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
             
        }

        static void mentes(char[,] mentesPalya)
        {
            List<char> falak = new List<char>() { '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔', '█', '▄' };

            Console.WriteLine("Adja mege a fájl mentési nevét: ");
            string nev = Console.ReadLine();

            if (nev == "")
            {
                szerkesztes(mentesPalya, falak);
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
                File.WriteAllLines(nev, sorok);
            }

            

        }

        static char[,] betoltes(string palyaNeve)
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

            Kiirat(palya);


            return palya;
        }

        static void menu()
        {

            List<char> falak = new List<char>() { '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔', '█', '▄' };


            Console.WriteLine("[1] pálya létrehozása");
            Console.WriteLine("[2] pálya betöltése");
            Console.WriteLine("[3] kilépés a programból");

            int input = Convert.ToInt32(Console.ReadLine());



            if (input == 1)
            {
                Console.Clear();
                Console.WriteLine("Adja meg a mátrix méretét (pl: x:y): ");

                string kordinata = Console.ReadLine();
                string[] ok = kordinata.Split(":");


                int szam = Convert.ToInt32(ok[0]);
                int szam2 = Convert.ToInt32(ok[1]);

                szerkesztes(palyaKeszites(szam, szam2), falak);

            }


            else if (input == 2)
            {
                Console.Clear();
                Console.Write("Adja meg a mentett pálya nevét: ");
                string nev = Console.ReadLine();

                szerkesztes(betoltes(nev), falak);
            }

            else if (input == 3)
            {
                Console.Clear();
                Environment.Exit(0);
            }

        }

        static void Kiirat(char[,] palya)
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
