using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tree
{

    public class MyNode
    {
        public int Value;
        public BTreeNode Left;
        public BTreeNode Right;
        
    }
    public class BTreeNode
    {
        public List<MyNode> Keys;
        public BTreeNode()
        {
            Keys = new List<MyNode>();
        } 

        public void AddToKeys(int item)
        {
            MyNode mn = new MyNode() { Value = item, Left = null, Right = null };
            Keys.Add(mn);
            Keys = Keys.OrderBy(x => x.Value).ToList();
        }

        public int GetMiddleIndex(int _degree)
        {
            return (_degree + 1) / 2;
        }


    }
    public class BTree
    {
        int _degree = 2;
        int _level = 0;
        public BTreeNode Root;

        public BTree()
        {
            Root = null;
        }


        public void Add(int item, ref BTreeNode n)
        {
            if (n == null)
            {
                n = new BTreeNode();
                BTreeNode node = new BTreeNode();
                n.AddToKeys(item);
                return;
            }

            if(n.Keys.Count < _degree)
            {
                MyNode currentNode = null;
                for (int i = 0; i < n.Keys.Count; i++)
                {
                    if(n.Keys.ElementAt(i).Left == null && n.Keys.ElementAt(i).Right == null)
                    {
                        n.AddToKeys(item);
                        return;
                    }
                    if (item < n.Keys.ElementAt(i).Value)
                    {
                        currentNode = n.Keys.ElementAt(i);
                        Add(item, ref currentNode.Left);
                        break;
                    }
                    if (item > n.Keys.ElementAt(i).Value)
                    {
                        currentNode = n.Keys.ElementAt(i);
                        Add(item, ref currentNode.Right);
                        break;
                    }
                }

                if (currentNode == null)
                {
                    n.AddToKeys(item);
                }

             
                return;
            }


            n.AddToKeys(item);




            if (n.Keys.Count > _degree)
            {
                int middleIndex = n.GetMiddleIndex(_degree);
                BTreeNode leftNode = new BTreeNode();
                BTreeNode rightNode = new BTreeNode();

                for(int i=0; i< middleIndex; i++)
                {
                    leftNode.AddToKeys(n.Keys.ElementAt(i).Value);
                }
                for (int i = middleIndex+1; i < n.Keys.Count; i++)
                {
                    rightNode.AddToKeys(n.Keys.ElementAt(i).Value);
                }

                MyNode middleNode = n.Keys.ElementAt(middleIndex);
                middleNode.Left = leftNode;
                middleNode.Right = rightNode;

                n.Keys.Clear();
                n.Keys.Add(middleNode);
                _level++;
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
             
                t.Add(i, ref t.Root);
            }
            
        }
    }
}
