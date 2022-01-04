using System.ComponentModel;
using System.Linq.Expressions;
using NftApi.Data.Models;

namespace NftApi.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IQueryable<NftBase> OrderByEditionIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderBy(nft => nft.Edition)
            : source.OrderBy(nft => nft.Edition);
    }

    public static IQueryable<NftBase> OrderByRankIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderByDescending(nft => nft.Rank)
            : source.OrderBy(nft => nft.Rank);
    }

    public static IQueryable<NftBase> OrderByPriceIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderByDescending(nft => nft.SalePrice)
            : source.OrderBy(nft => nft.SalePrice);
    }

    public static IQueryable<NftBase> OrderByListedAtIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderByDescending(nft => nft.ListedAt)
            : source.OrderBy(nft => nft.ListedAt);
    }

    public static IQueryable<NftBase> OrderByOfferCountIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderByDescending(nft => nft.Offers.Count)
            : source.OrderBy(nft => nft.Offers.Count);
    }

    public static IQueryable<NftBase> OrderByMintedAtIf(
        this IQueryable<NftBase> source,
        bool condition,
        ListSortDirection direction)
    {
        if (!condition)
        {
            return source;
        }

        return direction == ListSortDirection.Descending
            ? source.OrderByDescending(nft => nft.MintedAt)
            : source.OrderBy(nft => nft.MintedAt);
    }
}
