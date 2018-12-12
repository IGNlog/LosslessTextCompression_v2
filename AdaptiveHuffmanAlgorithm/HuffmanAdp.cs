using AdaptiveHuffmanCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveHuffmanAlgorithm
{
    public class HuffmanAdp
    {
        public VectorBits Words { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Node> CopyNodes { get; set; }
        public Dictionary<string, int> Frequencies { get; set; }

        public string File { get; set; }

        public Node Root { get; set; }
        public double Entropy { get; set; }
        public int CodeCounter { get; set; }
        public int WordCounter { get; set; }
        public int CurrentSize { get; set; }
        public int TotalFrequency { get; set; }

        public Dictionary<string, Node> HuffmanAdap { get; set; }

        public HuffmanAdp()
        {
            Entropy = CodeCounter = TotalFrequency = 0;
        }

        public int GetTotalFrequency()
        {
            return TotalFrequency;
        }

        public Node GetRoot()
        {
            return Root;
        }

        public int GetWordCounter()
        {
            return WordCounter;
        }

        public void SetWordCounter(int wordCounter)
        {
            WordCounter = wordCounter;
        }

        public void SetCurrentSize(int currentSize)
        {
            CurrentSize = currentSize;
        }

        public Dictionary<string, int> GetFrequencies()
        {
            return Frequencies;
        }

        public string GetFile()
        {
            return File;
        }

        public bool CompareNodes(Node i, Node j)
        {
            return i.GetFrequency() < j.GetFrequency();
        }

        public void BuildTree()
        {
            if(CopyNodes.Count==1)
            {
                Root = CopyNodes.First();
                return;
            }

            Node newNode = new Node("", CopyNodes.First().GetFrequency() + CopyNodes[1].GetFrequency(), CopyNodes.First(), CopyNodes[1], null);

            CopyNodes.First().SetParent(newNode);
            CopyNodes[1].SetParent(newNode);

            CopyNodes.RemoveRange(0, 2);
            CopyNodes.Add(newNode);
            CopyNodes.Sort((x, y) => x.Frequency.CompareTo(y.Frequency));
            BuildTree();
        }

        //Сохраняет листья для следующего дерева Хаффмана
        public void SavingLeafs(Node curRoot)
        {
            if (curRoot.GetLeft() == null && curRoot.GetRight() == null)
                return;

            if (curRoot.GetLeft() != null)
                SavingLeafs(curRoot.GetLeft());

            if (curRoot.GetRight() != null)
                SavingLeafs(curRoot.GetRight());

            if (curRoot.GetLeft() != null && curRoot.GetLeft().GetId() < 256)
                curRoot.SetLeft(null);

            if (curRoot.GetRight() != null && curRoot.GetRight().GetId() < 256)
                curRoot.SetRight(null);
        }

        // Уничтожаем построенное дерево, сохраняя память
        public void DestroyTree(Node curRoot)
        {
            if (curRoot == null || curRoot == null)
                return;

            DestroyTree(curRoot.GetLeft());
            DestroyTree(curRoot.GetRight());

            
            curRoot = null;
        }

        // Перемещает дерево Хаффмана для листа в корень, строит реферальный код пути
        public string GenerateCode(Node pt)
        {
            Node it;
            string code = "";

            while (pt.GetParent() != null)
            {
                it = pt;
                pt = pt.GetParent();
                if (pt.GetLeft().GetId() == it.GetId())
                {
                    code = code + "0";
                }
                else
                {
                    code = code + "1";
                }
            }
            code.Reverse();
            CodeCounter = CodeCounter + code.Length;
            return code;
        }

        public string DiscoverNode(Node pt, VectorBits words)
        {
            // Проверяем, является ли это листовой узел и прекращаем получать его 'carac'.
            // Если нет, выполняет итерацию влево или вправо в зависимости от переднего бита символов

            Node it;
            it = pt;

            while (true)
            {
                if (it.GetLeft() == null && it.GetRight() == null)
                    return it.GetWord();

                if (words.GetFront() == 0)
                {
                    words.DeleteFront();
                    it = it.GetLeft();
                }
                else if (words.GetFront() == 1)
                {
                    words.DeleteFront();
                    it = it.GetRight();
                }
            }
        }

        public void Compress(string filePath)
        {
            double l;   //средняя длина


        }

    }
}
