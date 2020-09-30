using System;
using System.ComponentModel.DataAnnotations;

public class Player
{

    public Nationality nationality { get; set; }
    public Guid id { get; set; }

    public DateTime CreationDate { get; set; }

    [RegularExpression("[M|F]")]
    public char gender { get; set; }

    public int age { get; set; }

}