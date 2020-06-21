public class GetRandom
{
    public int min;
    public int max;

    //Get values
    public GetRandom(int minValue, int maxValue)
    {
        min = minValue;
        max = maxValue;
    }

    //Generate number between the 2 numbers
    public int Random
    {
        get { return UnityEngine.Random.Range(min, max); }
    }
}
