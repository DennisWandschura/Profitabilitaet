using Microsoft.EntityFrameworkCore;

namespace ProfitabilitaetBackend
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

        public static DateTime ToDateTime(this DateOnly value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }
    }
}
