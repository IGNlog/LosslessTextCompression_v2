using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCode
{
    public class NodeHT
    {
        public string Word { get; set; }
        public int Frequency { get; set; }
        public NodeHT Right { get; set; }
        public NodeHT Left { get; set; }
        public NodeHT Parent { get; set; }

        public List<bool> Traverse(string word, List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (word.Equals(this.Word))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(word, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(word, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
