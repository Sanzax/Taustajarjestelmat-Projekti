using System;
using System.ComponentModel.DataAnnotations;

public class Player
{
    [EnumDataType(typeof(Nationality))]
    public Nationality Nationality { get; set; }
    public string Id { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime BirthDate { get; set; }

    [RegularExpression("[M|F|O]")]
    public char Gender { get; set; }

    public int Sessions { get; set; }

    public int Age
    {
        get
        {
            return (int)((DateTime.Now - BirthDate).TotalDays / 365);
        }
    }

}