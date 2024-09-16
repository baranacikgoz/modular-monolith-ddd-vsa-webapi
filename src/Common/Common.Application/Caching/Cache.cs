using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Caching;
public static class Cache
{
    public static class Keys
    {
        public static string ById<T>(object id, params string[] includes)
            => includes.Length == 0
            ? $"{typeof(T).Name}:ById:{id}"
            : $"{typeof(T).Name}:ById:{id}:{string.Join(":", includes)}";
        public static string ByOwnerId<T>(object ownerId, params string[] includes)
            => includes.Length == 0
            ? $"{typeof(T).Name}:ByOwnerId:{ownerId}"
            : $"{typeof(T).Name}:ByOwnerId:{ownerId}:{string.Join(":", includes)}";
        public static string Paginated<T>(int page, int pageSize) => $"{typeof(T).Name}:Paginated:{page}:{pageSize}";
    }

    public static class Tags
    {
        public static string ById<T>() => $"{typeof(T).Name}:ById";
        public static string ByOwnerId<T>() => $"{typeof(T).Name}:ByOwnerId";
        public static string Paginated<T>() => $"{typeof(T).Name}:Paginated";
        public static IEnumerable<string> WithIncludes<T>(params string[] includes)
        {
            foreach (var include in includes)
            {
                yield return $"{typeof(T).Name}:WithInclude:{include}";
            }
        }
    }
}
