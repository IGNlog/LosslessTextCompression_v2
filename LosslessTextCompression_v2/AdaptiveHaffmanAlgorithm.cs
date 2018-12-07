using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LosslessTextCompression_v2
{
    public class AdaptiveHaffmanAlgorithm
    {

        public class Node
        {
            public char Simbol { get; set; }
            public int Frequency { get; set; }

            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node Parent { get; set; }

            public Node()
            {
                Simbol = '\0';
                Frequency = 0;
                LeftChild = null;
                RightChild = null;
                Parent = null;
            }
        }

        public static void Swap(Node a, Node b)
        {
            //Node buf = new Node
            //{
            //    Simbol = a.Simbol,
            //    Frequency = a.Frequency,
            //    Parent = a.Parent,
            //    LeftChild = a.LeftChild,
            //    RightChild = a.RightChild
            //};
            Node buf = a;
            a = b;
            b = buf;
        }

        public static void IncWeight(Node root)
        {
            root.Frequency++;
            if (root.Parent!=null)
            {
                IncWeight(root.Parent);
            }
        }

        public static void CheckChilds(Node parent)
        {
            if (parent.LeftChild.Frequency > parent.RightChild.Frequency)
            {
                Swap(parent.LeftChild, parent.RightChild);
            }
        }

        public static void SwitchNodes(Node n1, Node n2)
        {
            if (n1.Parent.LeftChild == n1)
            {
                n1.Parent.LeftChild = n2;
            }
            else
            {
                n1.Parent.RightChild = n2;
            }

            if (n2.Parent.LeftChild == n2)
            {
                n2.Parent.LeftChild = n1;
            }
            else
            {
                n2.Parent.RightChild = n1;
            }

            Swap(n1.Parent, n2.Parent);
            CheckChilds(n1.Parent);
        }

        public static void Recount(Node escp)
        {
            if (escp != null)
            {
                escp.Frequency = escp.LeftChild.Frequency + escp.RightChild.Frequency;
                Recount(escp.Parent);
            }
        }

        public static List<Node> SortTree(List<Node> tree)
        {
            bool n = false;
            int ind = 0;
            for (int i = tree.Count - 1; i > 0; i--)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    if (tree[i].Frequency > tree[j].Frequency)
                    {
                        n = true;
                        ind = j;
                    }
                }
                if (n)
                {
                    SwitchNodes(tree[i], tree[ind]);
                    Swap(tree[i], tree[ind]);
                    break;
                }
            }
            return tree;
        }

        public static List<bool> GetCode(Node n, List<bool> code)
        {
            if (n.Parent == null)
                return code;
            if (n.Parent.LeftChild == n)
            {
                code.Add(false);
            }
            else
            {
                code.Add(true);
            }
            if (n.Parent.Parent != null)
                code = GetCode(n.Parent, code);
            return code;
        }

        public static void OutCode(List<bool> code)
        {
            for (int i = code.Count - 1; i > -1; i--)
            {
                Console.WriteLine(code[i]);
            }
        }
    }

    
}
