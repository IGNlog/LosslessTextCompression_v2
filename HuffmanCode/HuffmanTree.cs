using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HuffmanCode
{
    public class HuffmanTree
    {
        public List<NodeHT> nodes = new List<NodeHT>();
        public Dictionary<string, NodeHT> nodesDictionary = new Dictionary<string, NodeHT>();
        public NodeHT Root { get; set; }
        public FrequencyDictionary Frequencies = new FrequencyDictionary();
        public Dictionary<string, List<bool>> CodeTable = new Dictionary<string, List<bool>>();
        private static readonly byte[] maskForEncodeTable = { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };
        private static readonly byte maskForEncode = 0x80;

        public HuffmanTree()
        {
            nodes = new List<NodeHT>();
            Root = null;
            Frequencies = new FrequencyDictionary();
        }

        public HuffmanTree(FrequencyDictionary frequencyDictionary)
        {
            Frequencies = new FrequencyDictionary();
            Frequencies = frequencyDictionary;

            //var startTime = System.Diagnostics.Stopwatch.StartNew();

            BuildTree(Frequencies);

            //startTime.Stop();
            //var resultTime = startTime.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            //        resultTime.Hours,
            //        resultTime.Minutes,
            //        resultTime.Seconds,
            //        resultTime.Milliseconds);
            //Console.WriteLine("Build tree:  " + elapsedTime);

            //startTime = System.Diagnostics.Stopwatch.StartNew();

            BuildCodeTable(Frequencies);

            //startTime.Stop();
            //resultTime = startTime.Elapsed;

           //elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            //        resultTime.Hours,
            //        resultTime.Minutes,
            //        resultTime.Seconds,
            //        resultTime.Milliseconds);
            //Console.WriteLine("Build code table:  " + elapsedTime);
            //BuildCodeTable(Frequencies);
        }


        public List<bool> GetCode(NodeHT nodeLeaf)
        {
            List<bool> code = new List<bool>();
            NodeHT node = nodeLeaf;
            NodeHT nodePrev = node;
            while (node != Root)
            {
                node = node.Parent;
                if (node?.Right == nodePrev)
                {
                    code.Add(true);
                }
                if (node?.Left == nodePrev)
                {
                    code.Add(false);
                }
                nodePrev = node;
            }
            code?.Reverse();
            return code;
        }

        public void BuildCodeTable(FrequencyDictionary frequencyDictionary)
        {
            CodeTable = new Dictionary<string, List<bool>>();
            foreach (var wordNode in nodesDictionary)
            {
                List<bool> encodedWord = GetCode(wordNode.Value); //Root.Traverse(word.Key, new List<bool>());
                CodeTable.Add(wordNode.Key, encodedWord);
            }

        }

        public void BuildTree(FrequencyDictionary frequencyDictionary)
        {
            foreach (KeyValuePair<string, int> word in frequencyDictionary.Dictionary)
            {
                nodes.Add(new NodeHT() { Word = word.Key, Frequency = word.Value });
                nodesDictionary.Add(word.Key, nodes.Last());
            }

            while (nodes.Count > 1)
            {
                List<NodeHT> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<NodeHT>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<NodeHT> taken = orderedNodes.Take(2).ToList();

                    // Create a parent node by combining the frequencies
                    NodeHT parent = new NodeHT()
                    {
                        Word = "",
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };
                    taken[0].Parent = parent;
                    taken[1].Parent = parent;

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();
                Root.Parent = null;
            }
        }

        public List<bool> GetBitArrayTable(FrequencyDictionary frequencyDictionary)
        {
            List<bool> res = new List<bool>();

            byte lenBitWord = 0x00;
            foreach (var item in frequencyDictionary.Dictionary)
            {
                lenBitWord = (byte)item.Key.Length;
                for (int i = 0; i < 8; i++)
                {
                    if(((lenBitWord<<i) & maskForEncode) == 0x00)
                    {
                        res.Add(false);
                    }
                    else
                    {
                        res.Add(true);
                    }
                }

                byte[] wordByteArray = Encoding.Default.GetBytes(item.Key);
                foreach (var symbol in wordByteArray)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((byte)((symbol << i) & maskForEncode) == 0x00)
                        {
                            res.Add(false);
                        }
                        else
                        {
                            res.Add(true);
                        }
                    }
                }

                //выполняем реверс
                byte[] buf = BitConverter.GetBytes(item.Value);
                byte[] freq = new byte[4];
                freq = buf;
                //for (int i = 0; i < buf.Length; i++)
                //{
                //    freq[i] = buf[buf.Length - 1 - i];
                //}
                

                foreach (var b in freq)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (((b << i) & maskForEncode) == 0x00)
                        {
                            res.Add(false);
                        }
                        else
                        {
                            res.Add(true);
                        }
                    }
                }
            }
            //if(codeTableBit.Count % 8 == 0)
            return res;
        }

        public string GetWordFromBitArray(int index, byte len, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] wordByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        wordByte[i] = (byte)(wordByte[i] | maskForEncodeTable[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return Encoding.Default.GetString(wordByte);
        }

        public int GetIntFromBitArray(int index, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] intByte = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        intByte[i] = (byte)(intByte[i] | maskForEncodeTable[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return BitConverter.ToInt32(intByte, 0);
        }

        public FrequencyDictionary GetFrequencyDictionaryFromFile(BitArray bitArray, int indexStart, int indexStop)
        {
            Dictionary<string, int> frequenceDictionary = new Dictionary<string, int>();
            int index = indexStart;
            int freq = 0;
            string word = "";
            while (index < indexStop)
            {
                byte lenWord = 0x00;
                for (int i = 0; i < 8; i++)
                {
                    if(bitArray[index + i])
                    {
                        lenWord = (byte)(lenWord | maskForEncodeTable[i]);
                    }
                }
                index += 8;

                word = GetWordFromBitArray(index, lenWord, bitArray);
                index += 8 * lenWord;

                freq = GetIntFromBitArray(index, bitArray);
                index += 8 * 4;

                frequenceDictionary.Add(word, freq);
            }

            return new FrequencyDictionary(frequenceDictionary.Count, frequenceDictionary);
        }

        public void Encode(string fileNameSource, string fileNameEncode)
        {
            List<bool> bitArray = Encode(fileNameSource);
            List<bool> bitArrayCodeTable = GetBitArrayTable(Frequencies);

            int indexEnd = (bitArray.Count + bitArrayCodeTable.Count) % 8;

            List<bool> bitRes = new List<bool>();
            bitRes.AddRange(bitArrayCodeTable);
            bitRes.AddRange(bitArray);
            if (indexEnd == 0)
            {
                bitRes.AddRange(new List<bool> { true, false, false, false, false, false, false, false });
            }
            else
            {
                bitRes.Add(true);
                indexEnd++;
                while (indexEnd < 8)
                {
                    bitRes.Add(false);
                    indexEnd++;
                }
            }
            BitArray resArr = new BitArray(bitRes.ToArray());
            byte[] lenBACT = BitConverter.GetBytes(bitArrayCodeTable.Count);            
            byte[] res = new byte[ resArr.Count / 8 + lenBACT.Length ];
            lenBACT.CopyTo(res, 0);
            resArr.CopyTo(res, lenBACT.Length);
            File.WriteAllBytes(@fileNameEncode, res);
        }

        public List<bool> Encode(string fileName)
        {
            StreamReader fileRead = new StreamReader(@fileName, Encoding.Default);
            List<bool> encodedSource = new List<bool>();
            string line;
            while ((line = fileRead.ReadLine()) != null)
            {
                //так как считываем мы построчно, то теряем символ '\n'
                line = line + "\n";
                int indexBegin = 0;
                int indexEnd = 0;
                while (indexEnd < line.Length)
                {
                    while (indexEnd < line.Length &&
                        ((line[indexEnd] >= 'a' && line[indexEnd] <= 'z') ||
                         (line[indexEnd] >= 'A' && line[indexEnd] <= 'Z')) ||
                        ((line[indexEnd] >= 'а' && line[indexEnd] <= 'я') ||
                         (line[indexEnd] >= 'А' && line[indexEnd] <= 'Я')))
                    {
                        indexEnd++;
                    }
                    //если мы прошлись по слову
                    string word;
                    if (indexBegin != indexEnd)
                    {
                        word = line.Substring(indexBegin, indexEnd - indexBegin);
                    }
                    else //это знак препенания, пробел или знак табуляции
                    {
                        indexEnd++;
                        word = line.Substring(indexBegin, indexEnd - indexBegin);
                    }
                    indexBegin = indexEnd;

                    //List<bool> encodedSymbol = this.Root.Traverse(word, new List<bool>());
                    //encodedSource.AddRange(encodedSymbol);

                    List<bool> encodedSymbol = CodeTable[word];
                    encodedSource.AddRange(encodedSymbol);
                }
            }
            fileRead.Close();

            return encodedSource;
        }

        public void Decode(string fileNameSource, string fileNameDecode)
        {
            BitArray bitArray = null;
            try
            {
                bitArray = new BitArray((File.ReadAllBytes(fileNameSource)));
            }
            catch (Exception exc)
            {
                throw exc;
            }
            //считываем длину битов декодировочной таблицы
            BitArray numBitCodeTable = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                numBitCodeTable[i] = bitArray[i];
            }
            byte[] num = new byte[4];
            numBitCodeTable.CopyTo(num, 0);
            int lenTable = BitConverter.ToInt32(num, 0);
            Frequencies = GetFrequencyDictionaryFromFile(bitArray, 32, lenTable + 32);
            BuildTree(Frequencies);
            Decode(bitArray, lenTable + 32, fileNameDecode);
        }

        public void Decode(BitArray bits, int indexStart, string fileName)
        {
            StreamWriter fileWrite = new StreamWriter(@fileName);
            NodeHT current = this.Root;
            string decoded = "";

            int endBitArr = bits.Count-1;
            while (bits[endBitArr] != true)
                endBitArr--;

            for(int i=indexStart; i<endBitArr; i++)
            {
                if (bits[i])
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded = current.Word;
                    fileWrite.Write(decoded);
                    current = this.Root;
                }
            }
            fileWrite.Close();
        }

        public bool IsLeaf(NodeHT node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
