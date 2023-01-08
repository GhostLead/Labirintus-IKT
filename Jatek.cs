using System.Timers;
using System.Text.Json;
using static labirintus.Metodusok;

namespace labirintus
{
    class Jatek
    {
        static string[,] SZOVEGEK = {
            {"Térkép fájl elérési útja: ", "Path to the map file: "},
            {"Ilyen nevű fájl nem létezik!", "This file doesn't exist!"},
            {"Pálya neve: {0} , Pálya mérete: {1} sor x {2} oszlop", "Level name: {0}, Level size: {1} rows x {2} columns"},
            {"Lépések száma: {0} , Felfedezett termek: {1} / {2}", "Number of steps: {0} , Discovered rooms: {1} / {2}"},
            {"Továbblépési lehetőségek: ", "Available directions: "},
            {"Biztosan ki szeretnél lépni? (i/n) ", "Are you sure you want to quit? (y/n) "},
            {"Eltelt idő: ", "Elapsed time: "},
            {"Játék vége!", "Game Over!"},
            {"Sikeresen teljesítetted a labirintust!", "You successfully completed the labyrinth!"},
            {"Nem sikerült teljesíteni a labirintust.", "You didn't manage to complete the labyrinth."},
            {"Szeretnél új játékot indítani? (i/n) ", "Do you want to start a new game? (y/n) "},
            {"Kelet", "East"},
            {"Nyugat", "West"},
            {"Észak", "North"},
            {"Dél", "South"},
            {"Hátralévő idő: ", "Time remaining: "},
            {"Beállítások:", "Settings:"},
            {"Ki", "Off"},
            {"Be", "On"},
            {"Felfedező mód: ", "Explorer mode: "},
            {"Időkorlát: ", "Time limit: "},
            {"Idő: ", "Time: "},
            {"Játék indítása", "Start game"},
            {"Mekkora legyen az időkorlát? (mp) ", "How long should the time limit be? (s) "},
            {"Új játék ", "New game "},
            {"Mentés ", "Save "},
            {"Kilépés ", "Quit "},
            {"Szeretnéd betölteni az előző mentést? (i/n) ", "Do you want to load the previous save? (y/n) "},
            {"Nyelvváltás ", "Change language "}
        };

        enum Irany
        {
            Jobbra,
            Balra,
            Fel,
            Le
        }

        static Random vel = new Random();
        static System.Timers.Timer idozito = new System.Timers.Timer(1000);

        static int nyelv = Program.nyelv;
        
        static JatekAllas jatekAllas = new JatekAllas();
        static char[,] terkep = new char[0,0];
        static bool vegeVan = false;

        static string mentesUt = "";
        static string palyaNev = "";

        
        #region Játék indítása
        public static void JatekKezdes()
        {
            Console.Clear();
            TerkepBetoltese();
            if (File.Exists(mentesUt))
            {
                Console.WriteLine(SZOVEGEK[27, nyelv]);
                ConsoleKey be = Console.ReadKey(true).Key;
                if (be == ConsoleKey.I || be == ConsoleKey.Y)
                {
                    Betoltes();
                }
                else
                {
                    UjJatek();
                }
            }
            else
            {
                UjJatek();
            }
            Rajzol();
            idozito.Elapsed += OraFrissit;
            idozito.Start();

            while (!vegeVan)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D:
                            Mozog(Irany.Jobbra);
                            break;
                        case ConsoleKey.A:
                            Mozog(Irany.Balra);
                            break;
                        case ConsoleKey.W:
                            Mozog(Irany.Fel);
                            break;
                        case ConsoleKey.S:
                            Mozog(Irany.Le);
                            break;
                        case ConsoleKey.D1:
                            UrjaKezdes();
                            break;
                        case ConsoleKey.D2:
                            Mentes();
                            break;
                        case ConsoleKey.D3:
                            nyelv = nyelv == 0 ? 1 : 0;
                            Rajzol();
                            break;
                        case ConsoleKey.D4:
                            System.Environment.Exit(0);
                            break;
                    }
                }
            }
            JatekVege();
        }

        static void TerkepBetoltese()
        {
            Console.Write(SZOVEGEK[0, nyelv]);
            string terkepUtja = Console.ReadLine()!;

            if (!File.Exists(terkepUtja))
            {
                Console.WriteLine(SZOVEGEK[1, nyelv]);
                TerkepBetoltese();
            }
            else
            {
                string[] sorok = File.ReadAllLines(terkepUtja);
                int sorHossz = sorok[0].Length;
                terkep = new char[sorok.Length, sorHossz];
                for (int sorIndex = 0; sorIndex < sorok.Length; sorIndex++)
                {
                    for (int oszlopIndex = 0; oszlopIndex < sorHossz; oszlopIndex++)
                    {
                        if (sorok[sorIndex][oszlopIndex] == '.')
                        {
                            terkep[sorIndex, oszlopIndex] = ' ';
                        }
                        else
                        {
                            terkep[sorIndex, oszlopIndex] = sorok[sorIndex][oszlopIndex];
                        }
                    }
                }
                mentesUt = Path.Join(Path.GetDirectoryName(terkepUtja), Path.GetFileNameWithoutExtension(terkepUtja) + ".SAV");
                palyaNev = Path.GetFileName(terkepUtja);
            }
        }

        static void UjJatek()
        {
            BeallitasokKiiratas();
            bool beallitasKesz = false;
            while (!beallitasKesz)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            jatekAllas.felfedezoMod = !jatekAllas.felfedezoMod;
                            BeallitasokKiiratas();
                            break;
                        case ConsoleKey.D2:
                            jatekAllas.idoKorlat = !jatekAllas.idoKorlat;
                            BeallitasokKiiratas();
                            break;
                        case ConsoleKey.D3:
                            Console.Clear();
                            Console.Write(SZOVEGEK[23, nyelv]);
                            int ido = 60;
                            if (int.TryParse(Console.ReadLine(), out ido))
                            {
                                jatekAllas.maxIdo = ido;
                            }
                            BeallitasokKiiratas();
                            break;
                        case ConsoleKey.D4:
                            beallitasKesz = true;
                            break;
                    }
                }
            }

            jatekAllas.jatekosPozicio = KezdoPozicio();
            if (jatekAllas.felfedezoMod)
            {
                jatekAllas.felfedettPoziciok.Add(jatekAllas.jatekosPozicio[0] + ":" + jatekAllas.jatekosPozicio[1]);
            }
        }

        static void BeallitasokKiiratas()
        {
            Console.Clear();
            Console.WriteLine(SZOVEGEK[16, nyelv]);
            Console.WriteLine("1) " + SZOVEGEK[19, nyelv] + (jatekAllas.felfedezoMod ? SZOVEGEK[18, nyelv] : SZOVEGEK[17, nyelv]));
            Console.WriteLine("2) " + SZOVEGEK[20, nyelv] + (jatekAllas.idoKorlat ? SZOVEGEK[18, nyelv] : SZOVEGEK[17, nyelv]));
            Console.WriteLine("3) " + SZOVEGEK[21, nyelv] + IdoFormazas(jatekAllas.maxIdo));
            Console.WriteLine("\n4) " + SZOVEGEK[22, nyelv]);
        }

        static int[] KezdoPozicio()
        {
            int sorIndex, oszlopIndex;
            do
            {
                sorIndex = vel.Next(terkep.GetLength(0));
                oszlopIndex = vel.Next(terkep.GetLength(1));
            } while (terkep[sorIndex, oszlopIndex] == ' ' || terkep[sorIndex, oszlopIndex] == '█');
            return new int[] { sorIndex, oszlopIndex };
        }
        #endregion
        
        #region Kiiratás
        static string IdoFormazas(int masodpercek) => string.Format("{0:00}:{1:00}", masodpercek / 60, masodpercek % 60);
        static void ElteltIdoKiiratas() => Console.WriteLine(SZOVEGEK[6, nyelv] + IdoFormazas(jatekAllas.elteltIdo));
        static void JatekallasKiiratas() => Console.WriteLine(SZOVEGEK[3, nyelv], jatekAllas.lepesek, jatekAllas.felfedezettTermek.Count, GetRoomNumber(terkep));

        static string IranyKiiratas()
        {
            List<string> iranyok = new List<string>();
            List<Irany> merreMozoghat = MerreMozoghat();

            if (merreMozoghat.Contains(Irany.Jobbra))
            {
                iranyok.Add(SZOVEGEK[11, nyelv]);
            }
            if (merreMozoghat.Contains(Irany.Balra))
            {
                iranyok.Add(SZOVEGEK[12, nyelv]);
            }
            if (merreMozoghat.Contains(Irany.Fel))
            {
                iranyok.Add(SZOVEGEK[13, nyelv]);
            }
            if (merreMozoghat.Contains(Irany.Le))
            {
                iranyok.Add(SZOVEGEK[14, nyelv]);
            }

            return string.Join(", ", iranyok);
        }

        static void Rajzol()
        {
            Console.Clear();
            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {
                    if (jatekAllas.jatekosPozicio[0] == sorIndex && jatekAllas.jatekosPozicio[1] == oszlopIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    Console.SetCursorPosition(oszlopIndex, sorIndex);
                    if (!jatekAllas.felfedezoMod || jatekAllas.felfedettPoziciok.Contains(sorIndex + ":" + oszlopIndex))
                    {
                        Console.Write(terkep[sorIndex, oszlopIndex]);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine("\n" + (jatekAllas.megerosites ? SZOVEGEK[5, nyelv] : "") + "\n");

            if (jatekAllas.idoKorlat)
            {
                int hatralevoIdo = jatekAllas.maxIdo - jatekAllas.elteltIdo;
                Console.WriteLine(SZOVEGEK[15, nyelv] + IdoFormazas(hatralevoIdo));
            }
            else
            {
                ElteltIdoKiiratas();
            }
            Console.WriteLine(SZOVEGEK[2, nyelv], palyaNev, terkep.GetLength(0), terkep.GetLength(1));
            JatekallasKiiratas();
            Console.WriteLine(SZOVEGEK[4, nyelv] + IranyKiiratas());
            Console.WriteLine("1) " + SZOVEGEK[24, nyelv] + "2) " + SZOVEGEK[25, nyelv] + "3) " + SZOVEGEK[28, nyelv] + "4) " + SZOVEGEK[26, nyelv]);
        }
        #endregion

        #region Mozgás
        static char[] JOBBRA_NYILAS = { '╬', '═', '╦', '╩', '╠', '╚', '╔', '█' };
        static char[] BALRA_NYILAS = { '╬', '═', '╦', '╩', '╣', '╗', '╝', '█' };
        static char[] FEL_NYILAS = { '╬', '╩', '║', '╣', '╠', '╝', '╚', '█' };
        static char[] LE_NYILAS = { '╬', '╦', '║', '╣', '╠', '╗', '╔', '█' };

        static List<Irany> MerreMozoghat()
        {
            List<Irany> mozoghat = new List<Irany>();

            if (JOBBRA_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1]])
                && (jatekAllas.jatekosPozicio[1] + 1 == terkep.GetLength(1)
                    || BALRA_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1] + 1])))
            {
                mozoghat.Add(Irany.Jobbra);
            }

            if (BALRA_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1]])
                && (jatekAllas.jatekosPozicio[1] == 0
                    || JOBBRA_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1] - 1])))
            {
                mozoghat.Add(Irany.Balra);
            }

            if (FEL_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1]])
                && (jatekAllas.jatekosPozicio[0] == 0
                    || LE_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0] - 1, jatekAllas.jatekosPozicio[1]])))
            {
                mozoghat.Add(Irany.Fel);
            }

            if (LE_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1]])
                && (jatekAllas.jatekosPozicio[0] + 1 == terkep.GetLength(0)
                    || FEL_NYILAS.Contains(terkep[jatekAllas.jatekosPozicio[0] + 1, jatekAllas.jatekosPozicio[1]])))
            {
                mozoghat.Add(Irany.Le);
            }

            return mozoghat;
        }

        static void Mozog(Irany irany)
        {
            List<Irany> mozoghat = MerreMozoghat();
            bool mozgott = false;

            if (irany == Irany.Jobbra && mozoghat.Contains(Irany.Jobbra))
            {
                if (jatekAllas.jatekosPozicio[1] + 1 == terkep.GetLength(1))
                {
                    Kilep();
                }
                else
                {
                    jatekAllas.jatekosPozicio[1]++;
                    mozgott = true;
                }
            }
            else if (irany == Irany.Balra && mozoghat.Contains(Irany.Balra))
            {
                if (jatekAllas.jatekosPozicio[1] == 0)
                {
                    Kilep();
                }
                else
                {
                    jatekAllas.jatekosPozicio[1]--;
                    mozgott = true;
                }
            }
            else if (irany == Irany.Fel && mozoghat.Contains(Irany.Fel))
            {
                if (jatekAllas.jatekosPozicio[0] == 0)
                {
                    Kilep();
                }
                else
                {
                    jatekAllas.jatekosPozicio[0]--;
                    mozgott = true;
                }
            }
            else if (irany == Irany.Le && mozoghat.Contains(Irany.Le))
            {
                if (jatekAllas.jatekosPozicio[0] + 1 == terkep.GetLength(0))
                {
                    Kilep();
                }
                else
                {
                    jatekAllas.jatekosPozicio[0]++;
                    mozgott = true;
                }
            }

            if (mozgott)
            {
                jatekAllas.lepesek++;
                string pozicio = jatekAllas.jatekosPozicio[0] + ":" + jatekAllas.jatekosPozicio[1];
                if (terkep[jatekAllas.jatekosPozicio[0], jatekAllas.jatekosPozicio[1]] == '█' && !jatekAllas.felfedezettTermek.Contains(pozicio))
                {
                    jatekAllas.felfedezettTermek.Add(pozicio);
                    if (jatekAllas.felfedezettTermek.Count == GetRoomNumber(terkep))
                    {
                        jatekAllas.sikeres = true;
                    }
                }
                if (jatekAllas.felfedezoMod)
                {
                    jatekAllas.felfedettPoziciok.Add(pozicio);
                }
                Rajzol();
            }
        }
        #endregion

        #region Mentés és betöltés
        static void Mentes()
        {
            string jatekAllasJson = JsonSerializer.Serialize(jatekAllas, new JsonSerializerOptions { IncludeFields = true });
            File.WriteAllText(mentesUt, jatekAllasJson);
        }

        static void Betoltes()
        {
            string jatekAllasJson = File.ReadAllText(mentesUt);
            jatekAllas = JsonSerializer.Deserialize<JatekAllas>(jatekAllasJson, new JsonSerializerOptions { IncludeFields = true })!;
        }
        #endregion

        #region Játék vége
        static void JatekVege()
        {
            vegeVan = true;
            idozito.Stop();

            Console.Clear();
            Console.ResetColor();

            Console.WriteLine(SZOVEGEK[7, nyelv]);
            Console.WriteLine(SZOVEGEK[jatekAllas.sikeres ? 8 : 9, nyelv]);
            ElteltIdoKiiratas();
            JatekallasKiiratas();

            Console.WriteLine();
            Console.WriteLine(SZOVEGEK[10, nyelv]);
            ConsoleKey be = Console.ReadKey(true).Key;
            if (be == ConsoleKey.I || be == ConsoleKey.Y)
            {
                UrjaKezdes();
            }
        }

        static void Kilep()
        {
            jatekAllas.megerosites = true;
            Rajzol();
            ConsoleKey be = Console.ReadKey(true).Key;
            jatekAllas.megerosites = false;
            if (be == ConsoleKey.I || be == ConsoleKey.Y)
            {
                vegeVan = true;
            }
            else
            {
                Rajzol();
            }
        }

        static void UrjaKezdes()
        {
            Console.Clear();
            jatekAllas = new JatekAllas();
            idozito.Elapsed -= OraFrissit;
            vegeVan = false;

            JatekKezdes();
        }
        #endregion

        static void OraFrissit(Object? source, ElapsedEventArgs e)
        {
            jatekAllas.elteltIdo++;
            if (jatekAllas.idoKorlat && jatekAllas.elteltIdo == jatekAllas.maxIdo)
            {
                vegeVan = true;
            }
            else
            {
                Rajzol();
            }
        }
    }

    class JatekAllas
    {
        public int[] jatekosPozicio = {0, 0};

        public bool sikeres = false;
        public bool megerosites = false;

        public int lepesek = 0;
        public List<string> felfedezettTermek = new List<string>();

        public bool felfedezoMod = false;
        public List<string> felfedettPoziciok = new List<string>();

        public int elteltIdo = 0;
        public bool idoKorlat = false;
        public int maxIdo = 60;
    }
}
