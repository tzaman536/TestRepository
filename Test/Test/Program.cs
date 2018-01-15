using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Test
{
    class Node
    { 
        public string Value { get; set; }
        public Node Left;
        public Node Right; 
    }


    class Program
    {

        static void PrintDepth(Node n, int nextLevel)
        {
            if (n == null)
                return;

            if (!string.IsNullOrEmpty(n.Value) && n.Left == null && n.Right == null)
            {
                Console.WriteLine(nextLevel);  
            }
            if (n.Left != null)
                PrintDepth(n.Left, nextLevel + 1);
            if(n.Right != null)
                PrintDepth(n.Right, nextLevel + 1);
        }

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

        static void Insert(ref Node n, string next)
        {


            if(n == null)
            {
                n = new Node() { Value = next };
                return;
            }


            if (Compare(n.Value, next) == 0)
            {
                return;
            }

            if (Compare(n.Value, next) < 0)
            {
                Insert(ref n.Right, next);

            }
            else
            {
                Insert(ref n.Left, next);
            }

           


        }

     
        static void Main(string[] args)
        {
            string[] ar_temp = Console.ReadLine().Split(',');

            Node root = null;

            foreach (var a in ar_temp)
            {
                Insert(ref root, a);
            }


            PrintDepth(root,0);

         
            Console.ReadLine();
        }
    }
}
