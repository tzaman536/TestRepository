using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Sort
{
    public class BubbleSort
    {
        public static void Sort(string[] arr, int n)
        {
            bool flag = true;
            string temp;
            for (int i = 1; (i <= n) && flag; i++)
            {
                flag = false;
                for (int j = 0; j < (n - 1); j++)
                {
                    if (String.StringCompare.Compare(arr[j + 1], arr[j]) < 0)
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        flag = true;
                    }
                }
            }
        }

    }
}
