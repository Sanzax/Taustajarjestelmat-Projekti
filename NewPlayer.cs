using System;
using System.ComponentModel.DataAnnotations;
public class NewPlayer
{
    [EnumDataType(typeof(Nationality))]
    public Nationality Nationality { get; set; }
    
    [RegularExpression("[M|F|O]")]
    public char Gender { get; set; }

    [Range(1, 31)]
    public int Day { get; set; }
    [Range(1, 12)]
    public int Month { get; set; }
    [Range(1900, int.MaxValue)]
    public int Year { get; set; }

}