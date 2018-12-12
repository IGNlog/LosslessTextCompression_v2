using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveHuffmanCode
{
    public class Node
    {
        public static int Counter = 256;

        public int Id { get; set; }
        public string Word { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public Node(string word, int frequency, Node left, Node right, Node parent)
        {
            Id = Counter++;
            Word = word;
            Frequency = frequency;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public Node(int id, string word, int frequency, Node left, Node right, Node parent)
        {
            Id = id;
            Word = word;
            Frequency = frequency;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetWord()
        {
            return Word;
        }

        public int GetFrequency()
        {
            return Frequency;
        }

        public Node GetLeft()
        {
            return Left;
        }

        public Node GetRight()
        {
            return Right;
        }

        public Node GetParent()
        {
            return Parent;
        }

        public void SetLeft(Node left)
        {
            Left = left;
        }

        public void SetRight(Node right)
        {
            Right = right;
        }

        public void SetParent(Node parent)
        {
            Parent = parent;
        }

        public void ReduceFrequency()
        {
            Frequency = Frequency - 1;
        }

        public void ResetCounter()
        {
            Counter = 256;
        }
    
    }
}
