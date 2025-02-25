using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq03
{
    internal class CustomComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (x == null && y == null) return false;
            return new string(x?.Trim().OrderBy(c => c).ToArray())== new string(y?.Trim().OrderBy(c => c).ToArray());
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return new string(obj.Trim().OrderBy(c => c).ToArray()).GetHashCode();
        }
    }
}
