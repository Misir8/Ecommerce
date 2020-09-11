using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Pagination
{
    public static class Pagination
    {
        public static PagedData<T> PagedResult<T>(this IEnumerable<T> list, int pageNumber, int pageSize, int count) where T : class
        {
            var result = new PagedData<T>();
            result.Data = list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / pageSize));
            result.CurrentPage = pageNumber;
            result.Count = count;
            return result;
        }
    }
}
