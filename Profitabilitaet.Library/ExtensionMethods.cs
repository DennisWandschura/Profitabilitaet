using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Library
{
    public static class ExtensionMethods
    {
        public static async Task<IReadOnlyList<TSource>> ToReadOnlyListAsync<TSource>(
      this IQueryable<TSource> source,
      CancellationToken cancellationToken = default)
        {
            var list = new List<TSource>();
            await foreach (var element in source.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                list.Add(element);
            }

            return list;
        }
    }
}
