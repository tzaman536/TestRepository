using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Inharitance
{
    class A1
    {
        public A1()
        {
            Console.WriteLine("Constructor A called");
        }
    }
    class B1 : A1
    {
        public B1()
        {
            Console.WriteLine("Constructor B called");
        }
    }


    class ConstructorSequence
    {
        public static void Start()
        {
            B1 b = new B1();
            Console.ReadLine();
        }
    }
}
