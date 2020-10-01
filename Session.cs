using System;

public class Session
{
    public Guid id { get; set; }
    public Guid playerId{ get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    //lasketaan startin ja endin perusteella kun sessio tallennetaan tietokantaan
    public int LengthInSeconds 
    { 
        get 
        {
            return (int)EndTime.Subtract(StartTime).TotalSeconds;
        }
    }

    //kuinka monta kertaa pelaaja aloitti uuden pelin session aikana
    public int Starts 
    { 
        get 
        {
            return Wins + Deaths;
        }
     }
    //kuinka monta kertaa pelaaja tappoi bossin. 
    public int Wins { get; set; }
    public int Deaths { get; set; }
}