namespace NftApi.Extensions;

public static class StringExtensions
{
    public static int ToAda(this string source)
    {
        var isLong = long.TryParse(source, out var lovelace);

        if (!isLong)
        {
            return -1;
        }

        return (int)(lovelace / 1000000);
    }
}
