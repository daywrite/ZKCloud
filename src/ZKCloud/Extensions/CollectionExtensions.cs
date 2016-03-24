using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Extensions {
    public static class CollectionExtensions {
        public static void Foreach<T>(this IEnumerable<T> source, Action<T> action) {
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var item in source) {
                action(item);
            }
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> source, IEnumerable<T> data) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (data == null)
                return source;
            foreach (var item in data) {
                source.Add(item);
            }
            return source;
        }
    }
}
