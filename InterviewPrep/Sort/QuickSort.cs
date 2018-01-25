using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Sort
{
    public class QuickSort
    {
        static void Sort(string[] elements, int left, int right)
        {
            int i = left, j = right;
            string pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (String.StringCompare.Compare(elements[i], pivot) < 0)
                {
                    i++;
                }

                while (String.StringCompare.Compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    string tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }
            // Recursive calls
            if (left < j)
            {
                Sort(elements, left, j);
            }

            if (i < right)
            {
                Sort(elements, i, right);
            }
        }


        public static void Start()
        {
            string[] array = new string[] { "a", "b", "ab", "d", "cd", "x", "y" };
            QuickSort.Sort(array, 0, array.Length -1);
            foreach(var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
