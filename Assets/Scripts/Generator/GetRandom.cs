public class GetRandom
{
    public int min;
    public int max;

    public GetRandom(int minValue, int maxValue)
    {
        min = minValue;
        max = maxValue;
    }
    public int Random
    {
        get { return UnityEngine.Random.Range(min, max); }
    }
}
