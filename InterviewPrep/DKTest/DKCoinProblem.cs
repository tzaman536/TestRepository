using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DKTest
{
    public class DKCoinProblem
    {
        List<string> combo = new List<string>();

        public int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            try
            {

                if (A == null)
                    return -1;

                if (A.Length == 0)
                    return 0;

                var coinWithFaceZero = A.Where(x => x == 1);
                Console.WriteLine(coinWithFaceZero.Count());
                var coinWithFaceOne = A.Where(x => x == 0);
                Console.WriteLine(coinWithFaceOne.Count());
                var coinNeitherZeroNorOne = A.Where(x => x != 0 && x != 1);
                Console.WriteLine(coinNeitherZeroNorOne.Count());
                if(coinNeitherZeroNorOne.Any())


                if (coinWithFaceZero.Count() > coinWithFaceOne.Count())
                    return coinWithFaceZero.Count();
                else
                    return coinWithFaceOne.Count();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }


        private void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public void GetPer(char[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private void GetPer(char[] list, int k, int m)
        {
            if (k == m)
            {
                combo.Add(new string(list));
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }


        public string solution(int A, int B, int C, int D)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            
            char[] arr = string.Format("{0}{1}{2}{3}", A, B, C, D).ToCharArray();
            GetPer(arr);

            List<string> validDates = new List<string>();
            foreach (var item in combo)
            {
                try {
                    //if (item.Equals("2318"))
                    //{

                    //}
                    string time = string.Format("{0}{1}:{2}{3}:00", item.Substring(0, 1), item.Substring(1, 1), item.Substring(2, 1), item.Substring(3, 1));
                    DateTime dt;

                    try {
                        dt = DateTime.ParseExact(time, "HH:mm:ss",
                                        CultureInfo.InvariantCulture);

                        validDates.Add(time.Substring(0,5));
                    }
                    catch
                    {

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


            if (validDates.Any())
            {
                return validDates.Max();
            }
            else
            {
                return "NOT POSSIBLE";
            }
        }

        public static void Start()
        {
            DKCoinProblem c = new DKCoinProblem();
            /*
            int[] arr = new int[] { 1, 0, 0, 1, 0, 0, 1 };
            c.solution(arr);
            */
            Console.WriteLine(c.solution(1, 8, 3, 2));

            Console.ReadLine();
        }
    }
}
