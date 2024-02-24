namespace GraphGenerator.Shared.Extensions;

public static class RandomExtensions
{
    public static double NextDouble(this Random random, double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }
    
    public static T Pick<T>(this Random random, T[] array)
    {
        return array[random.Next(0, array.Length)];
    }
    
    public static T Pick<T>(this Random random, List<T> list)
    {
        return list[random.Next(0, list.Count)];
    }
}
