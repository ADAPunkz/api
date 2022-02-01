namespace NftApi.Http.Models;

public class ListResponse<T> where T : class
{
    public List<T> Results { get; set; }

    public int PageSize { get; set; }

    public int NextPage { get; set; }

    public int ResultsCount { get; set; }
}
