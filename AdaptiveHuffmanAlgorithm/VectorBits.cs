using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveHuffmanAlgorithm
{
    public class VectorBits
    {
        public List<int> Bits;

        public VectorBits() { }

        public VectorBits(string vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == '1')
                    Bits.Add(1);
                else Bits.Add(0);
            }
        }

        public VectorBits(char c)
        {
            for (int i = 7; i >= 0; i--)
            {
                Bits.Add(((c >> i) & 1));
            }
        }

        public List<int> GetBits()
        {
            return Bits;
        }

        public int Size()
        {
            return Bits.Count;
        }

        public void Erase()
        {
            Bits.RemoveRange(0, 8);
        }

        public int At(int index)
        {
            return Bits[index];
        }

        public void Complete()
        {
            int vectorSize = Bits.Count;

            if (vectorSize == 0)
                return;

            for (int i = 0; i < (8-vectorSize); i++)
            {
                Bits.Add(0);
            }
        }

        public int GetFront()
        {
            return Bits.First();
        }

        public void DeleteFront()
        {
            Bits.RemoveAt(0);
        }

        public int GenerateByte()
        {
            int num = 0;
            for (int i = 0; i < 0; i++)
            {
                num = num + ((int)Math.Pow(2, 7 - i) * Bits[i]);
            }

            return num;
        }

        public void PushBack(VectorBits vBits)
        {
            for (int i = 0; i < vBits.Size(); i++)
            {
                Bits.Add(vBits.At(i));
            }
        }

        public void Print(FileStream fs, VectorBits v)
        {
            for (int i = 0; i < v.Size(); i++)
            {
                fs.Write(BitConverter.GetBytes(v.Bits[i]),0, BitConverter.GetBytes(v.Bits[i]).Count());
            }
        }
    }
}
