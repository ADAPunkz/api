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
    
    public static IQueryable<PunkzNft> OrderByEditionIf(this IQueryable<PunkzNft> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderBy(punk => punk.Edition);
        }

        return source.OrderBy(punk => punk.Edition);
    }

    public static IQueryable<PunkzNft> OrderByRankIf(this IQueryable<PunkzNft> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(punk => punk.Rank);
        }

        return source.OrderBy(punk => punk.Rank);
    }

    public static IQueryable<PunkzNft> OrderByPriceIf(this IQueryable<PunkzNft> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(punk => punk.SalePrice);
        }

        return source.OrderBy(punk => punk.SalePrice);
    }

    public static IQueryable<PunkzNft> OrderByListedAtIf(this IQueryable<PunkzNft> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(punk => punk.ListedAt);
        }

        return source.OrderBy(punk => punk.ListedAt);
    }

    public static IQueryable<PunkzNft> OrderByOfferCountIf(this IQueryable<PunkzNft> source, bool condition, ListSortDirection direction)
    {
        if (!condition)
        {
            return source;

        }

        if (direction == ListSortDirection.Descending)
        {
            return source.OrderByDescending(punk => punk.Offers.Count);
        }

        return source.OrderBy(punk => punk.Offers.Count);
    }
}
