using System;
using System.Collections.Generic;

namespace labirintus
{
class Metodusok
{
/// <summary>
/// Megadja, hogy hány termet tartamaz a térkép
/// </summary>
/// <param name="map">Labirintus mátrixa</param>
/// <returns>Termek száma</returns>
public static int GetRoomNumber(char[,] map)
{
    int szobakSzama = 0;
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[i,y]=='█')
            {
                szobakSzama++;
            }
        }
    }
    return szobakSzama;
}
/// <summary>
/// A kapott térkép széleit végignézve megállapítja, hogy hány kijárat van.
/// </summary>
/// <param name="map">Labirintus mátrixa</param>
/// <returns>Az alkalmas kijáratok száma</returns>
public static int GetSuitableEntrance(char[,] map)
{
    char[] Felul = { '╬',  '╩', '║', '╣', '╠', '╝', '╚'};
    char[] Alul = { '╬', '╦',  '║', '╣', '╠', '╗',  '╔', };
    char[] Jobbra = { '╬', '═', '╦', '╩',  '╠',  '╚', '╔' };
    char[] Balra = { '╬', '═', '╦', '╩', '╣', '╗', '╝' };
    int kijaratokSzama = 0;
    for (int sorIndex = 0; sorIndex < map.GetLength(0); sorIndex++)
    {
        for (int oszlopIndex = 0; oszlopIndex < map.GetLength(1); oszlopIndex++)
        {
            if (sorIndex==0&& Felul.Contains(map[sorIndex,oszlopIndex]))
            {
                kijaratokSzama++;
            }
            if (oszlopIndex==0 && Balra.Contains(map[sorIndex,oszlopIndex]))
            {
                kijaratokSzama++;

            }
            if (sorIndex == map.GetLength(0)-1 && Alul.Contains(map[sorIndex,oszlopIndex]))
            {
                kijaratokSzama++;

            }
            if (oszlopIndex == map.GetLength(1)-1 && Jobbra.Contains(map[sorIndex,oszlopIndex]))
            {
                kijaratokSzama++;

            }

        }
    }
    return kijaratokSzama;
}
/// <summary>
/// Megnézi, hogy van-e a térképen meg nem engedett karakter?
/// </summary>
/// <param name="map">Labirintus mátrixa</param>
/// <returns>true - A térkép tartalmaz szabálytalan karaktert, false - nincs benne ilyen</returns>
public static bool IsInvalidElement(char[,] map)
{
    bool Tartalom = false;
    char[] Karakterek = { '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔', '█', '.' };
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {

            {
                if (!Karakterek.Contains(map[i, y]))
                {
                    Tartalom = true;
                }
            }

        }
    }
    return Tartalom;
}
/// <summary>
/// Visszaadja azoknak a járatkaraktereknek a pozícióját, amelyekhez egyetlen szomszéd pozícióból sem lehet eljutni.
/// </summary>
/// <param name="map">Labirintus mátrixa</param>
/// <returns>A pozíciók "sor_index:oszlop_index" formátumban szerepelnek a lista elemeiként
public static List<string> GetUnavailableElements(char[,] map)
{
    List<string> unavailables = new List<string>();
    // ?
    // pld: string poz = "4:12"; 
    return unavailables;
}
/// <summary>
/// Labiritust generál a kapott pozíciókat tartalmazó lista alapján. A lista elemei egymáshoz kapcsolódó járatok pozíciói.
/// </summary>
/// <param name="positionsList">"sor_index:oszlop_index" formátumban az egymáshoz kapcsolódó járatok pozícióit tartalmazó lista </param>
/// <returns>A létrehozott labirintus térképe</returns>
public static char[,] GenerateLabyrinth(List<string> positionsList)
{
    return null;

}
}
}