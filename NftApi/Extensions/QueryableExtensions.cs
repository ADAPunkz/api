using System.ComponentModel;
using System.Linq.Expressions;
using NftApi.Data.Models;

namespace NftApi.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
        {
            return source.Where(predicate);
        }

        return source;
    }
    
    public static IQueryable<NftBase> OrderByEditionIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderBy(nft => nft.Edition);
        }

        return source.OrderBy(nft => nft.Edition);
    }

    public static IQueryable<NftBase> OrderByRankIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(nft => nft.Rank);
        }

        return source.OrderBy(nft => nft.Rank);
    }

    public static IQueryable<NftBase> OrderByPriceIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(nft => nft.SalePrice);
        }

        return source.OrderBy(nft => nft.SalePrice);
    }

    public static IQueryable<NftBase> OrderByListedAtIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(nft => nft.ListedAt);
        }

        return source.OrderBy(nft => nft.ListedAt);
    }

    public static IQueryable<NftBase> OrderByOfferCountIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(nft => nft.Offers.Count);
        }

        return source.OrderBy(nft => nft.Offers.Count);
    }

    public static IQueryable<NftBase> OrderByMintedAtIf(this IQueryable<NftBase> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(nft => nft.MintedAt);
        }

        return source.OrderBy(nft => nft.MintedAt);
    }
}
