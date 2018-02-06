using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Inharitance
{
    public class A
    {
        public virtual void Print()
        {
            Console.WriteLine("A");
        }
    }
    public class B : A
    {
        public override void Print()
        {
            Console.WriteLine("B");
        }
    }

    public class C : B
    {
        public new void Print()
        {
            Console.WriteLine("C");
        }
    }


    public class TestNewKeyWord
    {
        public static void Start()
        {
            A a = new A();
            B b = new B();
            C c = new C();
            a.Print();
            b.Print();
            c.Print();
            Console.ReadLine();
        }
    }
}
