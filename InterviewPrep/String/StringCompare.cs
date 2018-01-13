using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.String
{
    public class StringCompare
    {
        public static int Compare(string x, string y)
        {
            // If the length is not the same, we return the difference.
            // A negative # means, x Length is shorter, 0 means the same (this doesn't occur) and a postive # means Y is bigger
            if (x.Length != y.Length) return x.Length - y.Length;

            // Now the length is the same.
            // Compare the number from the first digit.
            for (int i = 0; i < x.Length; i++)
            {
                char left = x[i];
                char right = y[i];
                if (left != right)
                    return left - right;
            }

            // Default: "0" means both numbers are the same.
            return 0;
        }

    }
}
