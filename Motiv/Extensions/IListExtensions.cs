using Motiv.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Motiv.Extensions
{
    public static class IListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (list is List<T> asList)
            {
                asList.AddRange(items);
            }
            else
            {
                foreach (T item in items)
                {
                    list.Add(item);
                }
            }
        }

        public static void AddRange(this IList<string> list, IEnumerable<Transaction> transactions)
        {
            var dates = transactions.Select(x => x.Date.ToLocalTime().ToString("yyyy-MM-dd"));
            list.AddRange(dates);
        }
    }
}
