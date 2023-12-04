using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Util
{
    public static class Printing
    {
        public static void Print<T>(this IEnumerable<T> enumerable, string name = "list")
        {
            var list = enumerable.ToList();

            if (list.Count == 0)
            {
                Debug.Log($"{name}: {{ }}");
                return;
            }

            StringBuilder s = new($"{name}: {{ ");

            foreach (var element in list) s.Append($"{element}, ");

            // get rid of trailing ", "
            s.Length -= 2;

            s.Append(" }");
            Debug.Log(s);
        }
    }
}