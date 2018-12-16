using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanAdaptiveCode
{
    public class Node
    {
        public string Word { get; set; }
        public int Weight { get; set; }
        public int Number { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public bool IsNYT { get; set; }

        public Node()
        { }

        public Node(Node parent)
        {
            Parent = parent;
        }

        public Node(Node parent, string word)
        {
            Parent = parent;
            Word = word;
            Weight = 1;
        }

        public Node FindOrDefault(string word)
        {
            if (Word == word)
                return this;

            Node result = Left?.FindOrDefault(word);
            if (result != null)
                return result;

            return Right?.FindOrDefault(word);
        }

        private string GetCode(Node searched, string code)
        {
            if (Word == searched.Word)
                return code;

            if (Left == null && Right == null)
                return null;

            string result = Left.GetCode(searched, code + "0");
            if (result != null)
                return result;

            return Right.GetCode(searched, code + "1");
        }

        private List<bool> GetCode(Node searched, List<bool> code)
        {
            if (Word == searched.Word)
                return code;

            if (Left == null && Right == null)
                return null;

            List<bool> left = null;
            List<bool> right = null;

            if (Left != null)
            {
                List<bool> leftPath = new List<bool>();
                leftPath.AddRange(code);
                leftPath.Add(false);
                left = Left.GetCode(searched, leftPath);
            }
            if (Right != null)
            {
                List<bool> rightPath = new List<bool>();
                rightPath.AddRange(code);
                rightPath.Add(true);
                right = Right.GetCode(searched, rightPath);
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

        public string GetNYTCode(string code)
        {
            if (IsNYT)
                return code;

            if (Left == null && Right == null)
                return null;

            string result = Left.GetNYTCode(code + "0");
            if (result != null)
                return result;

            return Right.GetNYTCode(code + "1");
        }

        public List<bool> GetNYTCode(List<bool> code)
        {
            if (IsNYT)
                return code;

            if (Left == null && Right == null)
                return null;

            List<bool> left = null;
            List<bool> right = null;
            if(Left != null)
            {
                List<bool> leftPath = new List<bool>();
                leftPath.AddRange(code);
                leftPath.Add(false);

                left = Left.GetNYTCode(leftPath);
            }
            if (Right != null)
            {
                List<bool> rightPath = new List<bool>();
                rightPath.AddRange(code);
                rightPath.Add(true);
                right = Right.GetNYTCode(rightPath);
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

        //public List<bool> GetNYTCode(List<bool> code)
        //{
        //    if (IsNYT)
        //        return code;

        //    if (Left == null && Right == null)
        //        return null;

        //    List<bool> result = Left.GetNYTCode(code.Add(false));
        //    if (result != null)
        //        return result;

        //    return Right.GetNYTCode(code + "1");
        //}


        public List<bool> GetCode(Node searched)
        {
            //List<bool> code = null;
            List<bool> code = new List<bool>();
            return GetCode(searched, code);
        }

        public bool IsLeftSon(Node son)
        {
            return Left == son;
        }

        public bool IsRightSon(Node son)
        {
            return Right == son;
        }

        public bool IsLeaf() 
        {
            return Left == null && Right == null;
        }
    }
}
