using System;

[Serializable]
public class InvalidIDException : System.Exception
{
    public InvalidIDException() : base()
    {

        Console.WriteLine("NotFoundException thrown");
    }
    /* public NotFoundException(string name) : base(String.Format(name))
     {

     }
     public NotFoundException(string message, System.Exception inner) : base(message, inner) { }

     protected NotFoundException(System.Runtime.Serialization.SerializationInfo info,
         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }*/
}