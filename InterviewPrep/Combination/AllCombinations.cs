using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Combination
{
    public class AllCombinations
    {
        public static void Start()
        {
            /*
            input
            69 29
            25 27 40 38 17 2 28 23 9 43 18 49 15 24 19 11 1 39 32 16 35 30 48 34 20 3 6 13 44

            output
            101768



            input
            166 23
            5 37 8 39 33 17 22 32 13 7 10 35 40 2 43 49 46 19 41 1 12 11 28
            output
            96190959


            
        */
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            string[] c_temp = Console.ReadLine().Split(' ');
            long[] c = Array.ConvertAll(c_temp, Int64.Parse);


            long[] numways = new long[n + 1]; // numways[x] means # ways to get sum x
            numways[0] = 1; // init base case n=0

            // go thru coins 1-by-1 to build up numways[] dynamically
            // just need to consider cases where sum j>=c[i]    
            for (int i = 0; i < m; i++)
            {
                for (long j = c[i]; j <= n; j++)
                {
                    // find numways to get sum j given value c[i]
                    // it consists of those found earlier plus
                    // new ones.
                    // E.g. if c[]=1,2,3... and c[i]=3,j=5,
                    //      new ones will now include '3' with
                    //      numways[2] = 2, that is:
                    //      '3' with '2', '3' with '1'+'1'
                    numways[j] += numways[j - c[i]];
                }
            }

            Console.WriteLine(numways[n]);


            Console.ReadLine();
        }
    }
}
