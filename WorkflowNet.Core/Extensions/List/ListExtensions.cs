using System;
using System.Collections.Generic;

namespace WorkflowNet.Core.Extensions.List
{
    public static class ListExtensions
    {
        public static void Set<T>(this List<T> list, Predicate<T> predicate, T value)
        {
            var index = list.FindIndex(predicate);
            if (index != -1)
                list[index] = value;
        }
    }
}
