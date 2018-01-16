using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{
    public class BTreeNode
    {
        public List<int> Nodes;
        public BTreeNode Left;
        public BTreeNode Right;

        public BTreeNode()
        {
            Nodes = new List<int>();
            Left = null;
            Right = null;
        }

    }
    public class BTree
    {
        int _degree = 2;
        public BTreeNode Root;

        public BTree()
        {
            Root = null;
        }
        
        
        public void Add(int item, ref BTreeNode n, ref BTreeNode parent)
        {
            
            if(n == null)
            {
                n = new BTreeNode();
                n.Nodes.Add(item);
                return;
            }
            
            if(n.Nodes.Count == 1 && n.Left != null && n.Right != null ) 
            {
                if(item < n.Nodes.ElementAt(0))
                {
                    Add(item, ref n.Left, ref n);
                }
                else
                {
                    Add(item, ref n.Right, ref n);
                }
                return;
            }
                    
         
            n.Nodes.Add(item);
            n.Nodes.Sort();

            if (n.Nodes.Count > _degree)
            {
                int splitFrom = _degree / 2;
                int middleElement = n.Nodes.ElementAt(splitFrom);
                if (parent.Nodes.Count < _degree)
                {
                    n.Nodes.RemoveAt(splitFrom);
                    Add(middleElement, ref parent, ref  parent);
                }
                else
                {
                    // Time to create a leaf node
                    List<int> leftArray = new List<int>();
                    List<int> righArray = new List<int>();
                    leftArray = n.Nodes.GetRange(0, splitFrom);
                    righArray = n.Nodes.GetRange(splitFrom + 1, splitFrom);
                    n.Nodes.Clear();
                    n.Nodes.Add(middleElement);
                    n.Left = new BTreeNode();
                    n.Left.Nodes = leftArray;
                    n.Right = new BTreeNode();
                    n.Right.Nodes = righArray;
                }
            }
            
        }
         
    }


    public class BTreeDemo
    {
        public static void Start()
        {
            // Nice simulator
            // https://www.cs.usfca.edu/~galles/visualization/BTree.html

            BTree t = new BTree();
            
            for(int i=1; i<= 10; i++)
            {
                if(i == 5)
                {

                }
                t.Add(i, ref t.Root, ref t.Root);
            }
            
        }
    }
}
