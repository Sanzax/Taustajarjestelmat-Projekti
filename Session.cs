using System;

public class Session
{
    public Guid id { get; set; }
    Player player { get; set; }
    DateTime StartTime { get; set; }
    DateTime EndTime { get; set; }
    //lasketaan startin ja endin perusteella kun sessio tallennetaan tietokantaan
    int LenghtInSeconds { get; set; }

    //kuinka monta kertaa pelaaja aloitti uuden pelin session aikana
    int Starts { get; set; }
    //kuinka monta kertaa pelaaja tappoi bossin. 
    int Wins { get; set; }
}