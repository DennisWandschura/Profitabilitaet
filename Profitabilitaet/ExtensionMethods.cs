﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet
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

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }

        public static T ToEnum<T>(this string value) where T : struct
        {
            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, "Enum Wert konnte nicht erkannt werden.");
        }
    }
}
