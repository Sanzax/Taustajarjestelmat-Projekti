using System;
using System.ComponentModel.DataAnnotations;

public class Player
{

    public Nationality nationality { get; set; }
    public Guid id { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime BirthDate { get; set; } 

    [RegularExpression("[M|F][O]")]
    public char Gender { get; set; }

    public int Age
    {  
        get 
        {
            return (int)((DateTime.Now - BirthDate).TotalDays/365);
        }
    }

}