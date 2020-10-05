using System;
using System.ComponentModel.DataAnnotations;
public class NewSession
{
    public string PlayerId { get; set; }

    [Range(1, int.MaxValue)]
    public int LengthInSeconds { get; set; }
    [Range(0, int.MaxValue)]
    public int Wins { get; set; }
    [Range(0, int.MaxValue)]
    public int Deaths { get; set; }

}