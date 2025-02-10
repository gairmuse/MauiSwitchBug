namespace BugTest
{
    public static class ArrayExtensions
    {
        public static T? FirstOrDefaultNoLinq<T>(this T[]? items) => items is null || items.Length == 0 ? default : items[0];
    }
}