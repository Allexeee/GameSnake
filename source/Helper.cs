public static class Helper
{
    static Random random = new Random();
    public static T GetRandomItem<T>(this IList<T> list)
    {
        var index = random.Next(0, list.Count);
        return list[index];
    }
}