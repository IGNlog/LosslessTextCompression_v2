using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosslessTextCompression_v2
{
    internal class AdaptiveHuffmanTree
    {
        private AdaptiveHuffmanTree Left { get; set; }
        private AdaptiveHuffmanTree Right { get; set; }
        private int Number { get; set; } //индекс узла
        private Byte? Symbol { get; set; } //кодируемый символ
        private string SpecialSymbol { get; set; } //символы Esc и EoF
        private int Weight { get; set; } //вес узла

        public event NotEnoughCodeEventDelegate NotEnoughCodeEvent;
        public delegate void NotEnoughCodeEventDelegate(ref string Code);

        public void CreateModel()
        {
            Number = 3;
            Weight = 2;
            Left = new AdaptiveHuffmanTree();
            Left.SpecialSymbol = "Esc";
            Left.Number = 1;
            Left.Weight = 1;
            Right = new AdaptiveHuffmanTree();
            Right.SpecialSymbol = "EoF";
            Right.Number = 2;
            Right.Weight = 1;
        }

        #region Find
        private AdaptiveHuffmanTree Find(Byte? SymbolToFind)
        {
            if (Symbol == SymbolToFind) return this;
            AdaptiveHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(SymbolToFind);
            if (Result == null && Left != null) Result = Left.Find(SymbolToFind);
            return Result;
        }

        private AdaptiveHuffmanTree Find(string SpecialSymbolToFind)
        {
            if (SpecialSymbol == SpecialSymbolToFind) return this;
            AdaptiveHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(SpecialSymbolToFind);
            if (Result == null && Left != null) Result = Left.Find(SpecialSymbolToFind);
            return Result;
        }

        private AdaptiveHuffmanTree Find(int NumberToFind)
        {
            if (Number == NumberToFind) return this;
            AdaptiveHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(NumberToFind);
            if (Result == null && Left != null) Result = Left.Find(NumberToFind);
            return Result;
        }

        private AdaptiveHuffmanTree FindParent(AdaptiveHuffmanTree Child)
        {
            if (Left == Child || Right == Child || this == Child) return this;
            AdaptiveHuffmanTree Result = null;
            if (Right != null) Result = Right.FindParent(Child);
            if (Result == null && Left != null) Result = Left.FindParent(Child);
            return Result;
        }
        #endregion

        //перестроение дерева
        private void Rebuild(Byte? SymbolToEncode)
        {
            AdaptiveHuffmanTree CurrentVertex = Find(SymbolToEncode);

            //создание нового листа
            if (CurrentVertex == null)
            {
                AdaptiveHuffmanTree NewVertex = new AdaptiveHuffmanTree();
                AdaptiveHuffmanTree LastVertex = Find(1);
                AdaptiveHuffmanTree LastVertexParent = FindParent(LastVertex);
                AdaptiveHuffmanTree VertexWithSymbol = new AdaptiveHuffmanTree();
                VertexWithSymbol.Symbol = SymbolToEncode;
                VertexWithSymbol.Weight = 1;
                NewVertex.Weight = LastVertex.Weight + VertexWithSymbol.Weight;
                LastVertexParent.Left = NewVertex;
                NewVertex.Left = VertexWithSymbol;
                NewVertex.Right = LastVertex;
                CurrentVertex = NewVertex;
                ReNumber();
            }
            else CurrentVertex.Weight++;

            while (CurrentVertex != this)
            {

                //проверяем нужна ли перестановка
                int Number = CurrentVertex.Number;
                while (CurrentVertex.Weight == Find(Number + 1).Weight + 1) Number++;

                //если нужна:
                if (Number != CurrentVertex.Number)
                {
                    AdaptiveHuffmanTree VertexForChange;
                    VertexForChange = Find(Number);

                    //перестановка узлов-родителей
                    if (FindParent(VertexForChange) != FindParent(CurrentVertex))
                    {
                        AdaptiveHuffmanTree Parent1 = FindParent(VertexForChange);
                        AdaptiveHuffmanTree Parent2 = FindParent(CurrentVertex);
                        if (Parent1.Left == VertexForChange) Parent1.Left = CurrentVertex;
                        if (Parent1.Right == VertexForChange) Parent1.Right = CurrentVertex;
                        if (Parent2.Left == CurrentVertex) Parent2.Left = VertexForChange;
                        if (Parent2.Right == CurrentVertex) Parent2.Right = VertexForChange;
                    }
                    else
                    {
                        AdaptiveHuffmanTree Parent = FindParent(VertexForChange);
                        if (Parent.Left == VertexForChange)
                        {
                            Parent.Left = CurrentVertex;
                            Parent.Right = VertexForChange;
                        }
                        if (Parent.Left == CurrentVertex)
                        {
                            Parent.Left = VertexForChange;
                            Parent.Right = CurrentVertex;
                        }
                    }
                }
                CurrentVertex = FindParent(CurrentVertex);
                CurrentVertex.Weight++;
                ReNumber();
            }
        }

        //подсчет количества узлов
        private int Count()
        {
            int Quantity = 1;
            if (Right != null) Quantity += Right.Count();
            if (Left != null) Quantity += Left.Count();
            return Quantity;
        }

        //пересчет номера каждого узла
        private void ReNumber()
        {
            int index = this.Count();
            AdaptiveHuffmanTree CurrentVertex;
            Queue<AdaptiveHuffmanTree> Queue = new Queue<AdaptiveHuffmanTree>();
            Queue.Enqueue(this);
            do
            {
                CurrentVertex = Queue.Dequeue();
                CurrentVertex.Number = index;
                index--;
                if (CurrentVertex.Right != null) Queue.Enqueue(CurrentVertex.Right);
                if (CurrentVertex.Left != null) Queue.Enqueue(CurrentVertex.Left);
            }
            while (Queue.Count != 0);
        }

        //декодирование одного символа
        public Byte? Decode(ref string Code)
        {
            AdaptiveHuffmanTree TargetLeaf = this;
            Byte? DecodedSymbol = null;
            string Destination = "";

            do
            {
                if (Code.Length == 0) NotEnoughCodeEvent(ref Code);
                Destination = Code.Substring(0, 1);
                Code = Code.Substring(1, Code.Length - 1);
                if (Destination == "0") TargetLeaf = TargetLeaf.Left;
                if (Destination == "1") TargetLeaf = TargetLeaf.Right;
            }
            while (TargetLeaf.Symbol == null && TargetLeaf.SpecialSymbol == null);

            if (TargetLeaf.Symbol != null) DecodedSymbol = TargetLeaf.Symbol;
            if (TargetLeaf.SpecialSymbol == "Esc")
            {
                if (Code.Length < 8) NotEnoughCodeEvent(ref Code);
                DecodedSymbol = ToByte(Code.Substring(0, 8));
                if (Code.Length != 8) Code = Code.Substring(8, Code.Length - 8);
                else Code = "";
            }
            if (TargetLeaf.SpecialSymbol == "Eof") return null;

            Rebuild(DecodedSymbol);
            char a = Convert.ToChar(DecodedSymbol);
            return DecodedSymbol;
        }

        //кодирование символа
        public string Encode(Byte? SymbolToEncode)
        {
            string Code = "";
            char a = Convert.ToChar(SymbolToEncode);
            AdaptiveHuffmanTree KeyLeave;
            if (SymbolToEncode == null) KeyLeave = Find("EoF");
            else
            {
                KeyLeave = Find(SymbolToEncode);
                if (KeyLeave == null)
                {
                    Code = AdaptiveHuffmanTree.ToBinaryString(SymbolToEncode);
                    KeyLeave = Find("Esc");
                }
            }
            AdaptiveHuffmanTree Parent = this.FindParent(KeyLeave);
            do
            {
                if (Parent.Left == KeyLeave) Code = "0" + Code;
                if (Parent.Right == KeyLeave) Code = "1" + Code;
                KeyLeave = Parent;
                Parent = this.FindParent(KeyLeave);
            }
            while (Parent != KeyLeave);

            Rebuild(SymbolToEncode);

            return Code;
        }

        //перевод двоичной строки в байт
        public static Byte ToByte(string str)
        {
            Byte value = 0;
            for (int i = 0; i < 8; i++)
            {
                value += (byte)(byte.Parse(str.Substring(7 - i, 1)) * Math.Pow(2, i));
            }
            return value;
        }

        //перевод байта в двоичную строку
        public static string ToBinaryString(Byte? SymbolToConvert)
        {
            string Result = "";
            while (SymbolToConvert != 0)
            {
                Result = (SymbolToConvert % 2).ToString() + Result;
                SymbolToConvert /= 2;
            }
            while (Result.Length != 8) Result = "0" + Result;
            return Result;
        }
    }


}
