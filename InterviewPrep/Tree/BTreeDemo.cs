using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{
    public class BTreeNode
    {
        public List<int> Keys;
        public List<BTreeNode> Nodes;
        public bool IsLeaf
        {
            get
            {
                if (Nodes == null || Nodes.Count() == 0)
                    return true;
                else
                    return false;
            }
        }


        public BTreeNode()
        {
            Keys = new List<int>();
            Nodes = null;
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
            if (n == null)
            {
                n = new BTreeNode();
                n.Keys.Add(item);
                return;
            }

            if(n.IsLeaf && n.Keys.Count <= _degree)
            {
                n.Keys.Add(item);
                n.Keys.Sort();
            }
            else
            {
             
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
