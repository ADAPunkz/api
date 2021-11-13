using NftApi.Data.Models;

namespace NftApi.Http.Models;

public class NftList<T> where T : NftBase
{
    public List<T> Results { get; set; }

    public int PageSize { get; set; }

    public int NextPage { get; set; }

    public int ResultsCount { get; set; }
}
