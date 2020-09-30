using System;
using System.ComponentModel.DataAnnotations;

public class Player
{


    public Guid id { get; set; }

    public DateTime CreationDate { get; set; }

    [RegularExpression("[M|F]")]
    public char gender { get; set; }

    public int age { get; set; }

}