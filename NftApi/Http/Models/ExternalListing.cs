namespace NftApi.Http.Models;

public abstract class ExternalListing
{
    public abstract NormalizedListing Normalize(string tokenPrefix);
}
