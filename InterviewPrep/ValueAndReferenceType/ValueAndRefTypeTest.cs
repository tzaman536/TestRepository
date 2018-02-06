using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.ValueAndReferenceType
{
    class ValueAndRefTypeTest
    {
        public static void Start()
        {
            int x = new int();
            x = 20;
            int y = new int();
            y = x;
            Console.WriteLine("x is before y=30 assignment {0}", x);
            Console.WriteLine("y is before y=30 assignment {0}", y);

            y = 30;
            Console.WriteLine("x is {0}", x);
            Console.WriteLine("y is {0}", y);
            Console.ReadLine();

        }
    }
}
