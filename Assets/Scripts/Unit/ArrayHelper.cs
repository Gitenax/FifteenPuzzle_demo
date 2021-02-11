public static class ArrayHelper
{
    public static bool InBound<T>(this T[,] array, Point index)
    {
        return (index.X >= 0 && index.Y >= 0) && (index.X < array.GetLength(1) && index.Y < array.GetLength(0));
    }
}
