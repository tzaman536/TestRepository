using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{
    public class Node
    {
        public Node Left, Right;
        public int Data;
        public Node(int data)
        {
            this.Data = data;
            Left = Right = null;
        }
    }

    public class BinarySearchTree
    {

        public BinarySearchTree()
        {
            Root = null;
        }

        public virtual void Clear()
        {
            Root = null;
        }


        public Node Root;
        

        public int GetHeight(Node root)
        {


            if (root == null)
                return 0;

            if (root.Left == null && root.Right == null)
                return 1;


            int leftHeight = 0;
            if (root.Left != null)
            {
                leftHeight++;
                leftHeight = leftHeight + GetHeight(root.Left);
            }


            int rightHeight = 0;
            if (root.Right != null)
            {
                rightHeight++;
                rightHeight = rightHeight + GetHeight(root.Right);
            }




            if (leftHeight > rightHeight)
                return leftHeight;
            else
                return rightHeight;
        }



        Node Insert(ref Node n , int data)
        {
            if (n == null)
            {
                n = new Node(data);
                return n;
            }
            else {
                Node cur;
                if (data <= n.Data)
                {
                    cur = Insert(ref n.Left, data);
                    n.Left = cur;
                }
                else {
                    cur = Insert(ref n.Right, data);
                    n.Right = cur;
                }
                return n;
            }
        }

        public void PreorderTraversal(Node root)
        {
            if (root != null)
            {
                Console.Write(string.Format("{0} ",root.Data));
                PreorderTraversal(root.Left);
                PreorderTraversal(root.Right);
            }
      
        }


        public void InorderTraversal(Node root)
        {
            if (root != null)
            {
                InorderTraversal(root.Left);
                Console.Write(string.Format("{0} ", root.Data));
                InorderTraversal(root.Right);
            }
        }

        public void PostorderTraversal(Node root)
        {
            if(root != null)
            {
                PostorderTraversal(root.Left);
                PostorderTraversal(root.Right);
                Console.Write(string.Format("{0} ", root.Data));
            }
        }

        public void LookupValue(Node root, int data, ref bool found)
        {
            if (found)
                return;

            if(root != null)
            {
                if (root.Data == data)
                {
                    Console.Write(string.Format("Search Value {0} ", root.Data));
                    found =  true;
                }
                else
                {
                    LookupValue(root.Left, data, ref found);
                    LookupValue(root.Right, data, ref found);
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

        public bool Remove(int removeItem)
        {
            // first make sure there exist some items in this tree
            if (Root == null)
                return false;       // no items to remove

            Node current = Root;
            Node parent = null;
            int result = Compare(removeItem, current.Data);
            while(result != 0)
            {
                if(result> 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }

                // If current == null, then we didn't find the item to remove
                if (current == null)
                    return false;


                result = Compare(removeItem, current.Data);
            }

            // At this point we found the node to remove
            if(current.Right == null)
            {
                if(parent == null)
                {
                    Root = current.Left;
                }
                else
                {
                    result = Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        parent.Left = current.Left;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                    Root = current.Right;
                else
                {
                    result = Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        parent.Left = current.Right;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        parent.Right = current.Right;
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                Node leftmost = current.Right.Left, lmParent = current.Right;
                while (leftmost.Left != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                lmParent.Left = leftmost.Right;

                // assign leftmost's left and right to current's left and right children
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                    Root = leftmost;
                else
                {
                    result = Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        parent.Left = leftmost;
                    else if (result < 0)
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        parent.Right = leftmost;
                }
            }


            return false;
        }

        public void Delete(int removeItem)
        {

            // first make sure there exist some items in this tree
            if (Root == null)
                return ;       // no items to remove

            Node current = Root;
            Node parent = null;
            int result = Compare(removeItem, current.Data);
            while (result != 0)
            {
                if (result > 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }

                // If current == null, then we didn't find the item to remove
                if (current == null)
                    return;


                result = Compare(removeItem, current.Data);
            }


       
            // Three cases to consider, leaf, one child, two children

            // If it is a simple leaf then just null what the parent is pointing to
            if ((current.Left == null) && (current.Right == null))
            {
                if (parent == null)
                {
                    Root = null;
                    return;
                }

                // find out whether left or right is associated 
                // with the parent and null as appropriate
                if (parent.Left == current)
                    parent.Left = null;
                else
                    parent.Right = null;
                return;
            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (current.Left == null)
            {
                // Special case if we're at the root
                if (parent == null)
                {
                    Root = current.Right;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.Left == current)
                    parent.Right = current.Right;
                else
                    parent.Left = current.Right;

                current = null; // Clean up the deleted node
                return;
            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (current.Right == null)
            {
                // Special case if we're at the root			
                if (parent == null)
                {
                    Root = current.Left;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.Left == current)
                    parent.Left = current.Left;
                else
                    parent.Right = current.Left;

                current = null; // Clean up the deleted node
                return;
            }

            /*
            // Both children have nodes, therefore find the successor, 
            // replace deleted node with successor and remove successor
            // The parent argument becomes the parent of the successor
            Node1 successor = findSuccessor(nodeToDelete, ref parent);
            // Make a copy of the successor node
            Node1 tmp = new Node1(successor.name, successor.value);
            // Find out which side the successor parent is pointing to the
            // successor and remove the successor
            if (parent.left == successor)
                parent.left = null;
            else
                parent.right = null;

            // Copy over the successor values to the deleted node position
            nodeToDelete.name = tmp.name;
            nodeToDelete.value = tmp.value;
            _count--;
            */
        }


        public static void Start()
        {
            // View this article to verify your work https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
            //Input: 90 50 150 20 75 95 175 5 25 66 80 92 111 166 200

            //Preorder Traversal: 90, 50, 20, 5, 25, 75, 66, 80, 150, 95, 92, 111, 175, 166, 200
            //Inorder Traversal: 5, 20, 25, 50, 66, 75, 80, 90, 92, 95, 111, 150, 166, 175, 200
            //Post Order Traversal:  5, 25, 20, 66, 80, 75, 50, 92, 111, 95, 166, 200, 175, 150, 90
            string[] c_temp = Console.ReadLine().Split(' ');
            int[] c = Array.ConvertAll(c_temp, Int32.Parse);

            BinarySearchTree bst = new BinarySearchTree();

            foreach (var data in c)
            {
                bst.Insert(ref bst.Root, data);
            }


            int height = bst.GetHeight(bst.Root);
            Console.WriteLine(string.Format("Height of the tree is {0}",height));
            Console.Write("Preorder Traversal: ");
            bst.PreorderTraversal(bst.Root);
            Console.WriteLine();
            Console.Write("Inorder Traversal: ");
            bst.InorderTraversal(bst.Root);
            Console.WriteLine();
            Console.Write("Postorder Traversal: ");
            bst.PostorderTraversal(bst.Root);
            Console.WriteLine();
            bool found = false;
            bst.LookupValue(bst.Root, 255, ref found);
            if (found)
                Console.WriteLine("Found lookup value");
            else
                Console.WriteLine("Lookup value not found");

            //bool deleted = bst.Remove(175);
            //if (!deleted)
            //{
            //    Console.WriteLine("Couldn't find the data to delete");
            //}
            bst.Delete(175);

            Console.Write("Preorder Traversal After Delete: ");
            bst.PreorderTraversal(bst.Root);
            
            Console.ReadLine();
        }
    }
}
