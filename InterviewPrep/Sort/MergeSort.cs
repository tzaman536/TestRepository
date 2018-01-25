using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Sort
{
    public class MergeSort
    {
        static void DoMerge(string[] numbers, int left, int mid, int right)
        {
            string[] temp = new string[numbers.Length];
            int i, left_end, num_elements, tmp_pos;

            left_end = (mid - 1);
            tmp_pos = left;
            num_elements = (right - left + 1);

            while ((left <= left_end) && (mid <= right))
            {

                if (String.StringCompare.Compare(numbers[left], numbers[mid]) <= 0)
                    temp[tmp_pos++] = numbers[left++];
                else
                    temp[tmp_pos++] = numbers[mid++];
            }

            while (left <= left_end)
                temp[tmp_pos++] = numbers[left++];

            while (mid <= right)
                temp[tmp_pos++] = numbers[mid++];

            for (i = 0; i < num_elements; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }


        static public void Sort(string[] numbers, int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                Sort(numbers, left, mid);
                Sort(numbers, (mid + 1), right);

                DoMerge(numbers, left, (mid + 1), right);
            }
        }


        public static void Start()
        {
            string[] array = new string[] { "a", "ab", "d", "cd",  "b", "y", "x", "c" };
            MergeSort.Sort(array, 0, array.Length - 1);
            foreach (var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine();
            Console.ReadLine();
        }


    }
}
