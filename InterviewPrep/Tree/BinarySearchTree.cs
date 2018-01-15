using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{
    public class Node
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

        public static void PreorderTraversal(Node root)
        {
            if (root != null)
            {
                Console.Write(string.Format("{0} ",root.data));
                PreorderTraversal(root.left);
                PreorderTraversal(root.right);
            }
      
        }


        public static void InorderTraversal(Node root)
        {
            if (root != null)
            {
                InorderTraversal(root.left);
                Console.Write(string.Format("{0} ", root.data));
                PreorderTraversal(root.right);
            }
        }

        public static void PostorderTraversal(Node root)
        {
            if(root != null)
            {
                PostorderTraversal(root.left);
                PostorderTraversal(root.right);
                Console.Write(string.Format("{0} ", root.data));
            }
        }

        public static void LookupValue(Node root, int data, ref bool found)
        {
            if (found)
                return;

            if(root != null)
            {
                if (root.data == data)
                {
                    Console.Write(string.Format("Search Value {0} ", root.data));
                    found =  true;
                }
                else
                {
                    LookupValue(root.left, data, ref found);
                    LookupValue(root.right, data, ref found);
                }
            }
        }


        public static int Compare(int x, int y)
        {
            if (x == y)
                return 0;
            else if (x < y)
                return -1;
            else
                return 1;
        }

        public static bool Remove(int data, ref Node root)
        {
            // first make sure there exist some items in this tree
            if (root == null)
                return false;       // no items to remove

            // Now, try to find data in the tree
            Node current = root, parent = null;
            bool found = current.data == data;

            int result = Compare(current.data,data);

            while (result != 0)
            {
                if (result > 0)
                {
                    // current.Value > data, if data exists it's in the left subtree
                    parent = current;
                    current = current.left;
                }
                else if (result < 0)
                {
                    // current.Value < data, if data exists it's in the right subtree
                    parent = current;
                    current = current.right;
                }

                // If current == null, then we didn't find the item to remove
                if (current == null)
                    return false;
                else
                    result = Compare(current.data, data);
            }

            // At this point, we've found the node to remove
            //count--;

            // We now need to "rethread" the tree
            // CASE 1: If current has no right child, then current's left child becomes
            //         the node pointed to by the parent
            if (current.right == null)
            {
                if (parent == null)
                    root = current.left;
                else
                {
                    result = Compare(parent.data, current.data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        parent.left = current.left;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        parent.right = current.left;
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            //         replaces current in the tree
            else if (current.right.left == null)
            {
                current.right.left = current.left;

                if (parent == null)
                    root = current.right;
                else
                {
                    result = Compare(parent.data, current.data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        parent.left = current.right;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        parent.right = current.right;
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                Node leftmost = current.right.left, lmParent = current.right;
                while (leftmost.left != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.left;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                lmParent.left = leftmost.right;

                // assign leftmost's left and right to current's left and right children
                leftmost.left = current.left;
                leftmost.right = current.right;

                if (parent == null)
                    root = leftmost;
                else
                {
                    result = Compare(parent.data, current.data);
                    if (result > 0)
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        parent.left = leftmost;
                    else if (result < 0)
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        parent.right = leftmost;
                }
            }

            return true;
        }


        public static void Start()
        {
            // View this article to verify your work https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
            // 90 50 150 20 75 95 175 5 25 66 80 92 111 166 200
            string[] c_temp = Console.ReadLine().Split(' ');
            int[] c = Array.ConvertAll(c_temp, Int32.Parse);

            Node root = null;

            foreach (var data in c)
            {
                root = insert(root, data);
            }


            int height = getHeight(root);
            Console.WriteLine(string.Format("Height of the tree is {0}",height));
            Console.Write("Preorder Traversal: ");
            PreorderTraversal(root);
            Console.WriteLine();
            Console.Write("Inorder Traversal: ");
            InorderTraversal(root);
            Console.WriteLine();
            Console.Write("Postorder Traversal: ");
            PostorderTraversal(root);
            Console.WriteLine();
            bool found = false;
            LookupValue(root, 255, ref found);
            if (found)
                Console.WriteLine("Found lookup value");
            else
                Console.WriteLine("Lookup value not found");

            Remove(175, ref root);

            Console.Write("Preorder Traversal After Delete: ");
            PreorderTraversal(root);
            
            Console.ReadLine();
        }
    }
}
