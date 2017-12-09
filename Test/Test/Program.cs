using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Test
{
    class Program
    {
        static void Merge(BigInteger[] arr, BigInteger[] L, BigInteger[] R)
        {
            int limit = L.Length;
            if (R.Length < L.Length)
                limit = R.Length;


            for(int i=0; i < limit; i++)
            {
                if(L[i] < R[i])
                {
                 
                }

            }

            //7ZN9401
            //TaZa-13:37:9001




        }

        static void MergeSort(BigInteger[] arr, int start, int end)
        {
         
            if (start < end)
            {

                int midPoint = (start + end) / 2;
                BigInteger[] L = new BigInteger[midPoint];
                BigInteger[] R = new BigInteger[end - midPoint];
                for (int i = 0; i < midPoint; i++)
                    L[i] = arr[i];
                for (int i = midPoint; i < end; i++)
                    R[i] = arr[i];

                MergeSort(L, start, midPoint);
                MergeSort(R, midPoint,end);
                Merge(arr, L,R);
            }

        }

        static public void DoMerge(int[] numbers, int left, int mid, int right)
        {
            int[] temp = new int[25];
            int i, left_end, num_elements, tmp_pos;

            left_end = (mid - 1);
            tmp_pos = left;
            num_elements = (right - left + 1);

            while ((left <= left_end) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
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

        static public void MergeSort_Recursive(int[] numbers, int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort_Recursive(numbers, left, mid);
                MergeSort_Recursive(numbers, (mid + 1), right);

                DoMerge(numbers, left, (mid + 1), right);
            }
        }

        static void Main(string[] args)
        {
            string[] unsorted = new string[] { "10"
                                                ,"3"
                                                ,"5"
                                                };


            //BigInteger[] arr = new BigInteger[unsorted.Length];
            //BigInteger temp;

            int[] arr = new int[unsorted.Length];
            int temp;

            for (int i = 0; i < unsorted.Length; i++)
            {
                //BigInteger.TryParse(unsorted[i], out temp);
                int.TryParse(unsorted[i], out temp);
                arr[i] = temp;
            }
            MergeSort_Recursive(arr, 0, arr.Length  -1);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            Console.ReadLine();
        }
    }
}
