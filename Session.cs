using System;

public class Session
{
    public string id { get; set; }
    public string playerId { get; set; }
    public DateTime StartTime { get; set; }
    public DayOfWeek Day { get; set; }
    public int Hour { get; set; }
    public DateTime EndTime { get; set; }
    //lasketaan startin ja endin perusteella kun sessio tallennetaan tietokantaan
    public int LengthInSeconds { get; set; }

    //kuinka monta kertaa pelaaja aloitti uuden pelin session aikana
    public int Starts { get; set; }
    //kuinka monta kertaa pelaaja tappoi bossin. 
    public int Wins { get; set; }
    public int Deaths { get; set; }
}