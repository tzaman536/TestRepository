using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{
    class Node
    {
        public Node left, right;
        public int data;
        public Node(int data)
        {
            this.data = data;
            left = right = null;
        }
    }

    public class BinarySearchTree
    {
        static int getHeight(Node root)
        {


            if (root == null)
                return 0;

            if (root.left == null && root.right == null)
                return 1;


            int leftHeight = 0;
            if (root.left != null)
            {
                leftHeight++;
                leftHeight = leftHeight + getHeight(root.left);
            }


            int rightHeight = 0;
            if (root.right != null)
            {
                rightHeight++;
                rightHeight = rightHeight + getHeight(root.right);
            }




            if (leftHeight > rightHeight)
                return leftHeight;
            else
                return rightHeight;
        }



        static Node insert(Node root, int data)
        {
            if (root == null)
            {
                return new Node(data);
            }
            else {
                Node cur;
                if (data <= root.data)
                {
                    cur = insert(root.left, data);
                    root.left = cur;
                }
                else {
                    cur = insert(root.right, data);
                    root.right = cur;
                }
                return root;
            }
        }

        public static void Start()
        {

            string[] c_temp = Console.ReadLine().Split(' ');
            int[] c = Array.ConvertAll(c_temp, Int32.Parse);

            Node root = null;

            foreach (var data in c)
            {
                root = insert(root, data);
            }


            int height = getHeight(root);
            Console.WriteLine(height);
            Console.ReadLine();
        }
    }
}
