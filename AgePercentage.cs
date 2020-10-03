public class AgePercentage
{
    public int MinAge{get;set;}

    public int MaxAge{get;set;}
    
    public int Count{get;set;}
    public float Percentage{get;set;}
    public AgePercentage(int minAge, int maxAge, int count, float percentage){
        MinAge = minAge;
        MaxAge = maxAge;
        Count = count;
        Percentage = percentage;
    }
    public AgePercentage(int minAge, int maxAge, int count){
        MinAge = minAge;
        MaxAge = maxAge;
        Count = count;
    }
}