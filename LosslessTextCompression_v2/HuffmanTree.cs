using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LosslessTextCompression_v2
{
    class HuffmanTree
    {
        private List<NodeHT> nodes = new List<NodeHT>();
        public NodeHT Root { get; set; }
        public FrequencyDictionary Frequencies = new FrequencyDictionary();
        public Dictionary<string, List<bool>> CodeTable = new Dictionary<string, List<bool>>();

        public HuffmanTree()
        {
            nodes = null;
            Root = null;
            Frequencies = null;
        }

        public HuffmanTree(FrequencyDictionary frequencyDictionary)
        {
            Frequencies = new FrequencyDictionary();
            Frequencies = frequencyDictionary;
            BuildTree(Frequencies);
            var startTime = System.Diagnostics.Stopwatch.StartNew();

            BuildCodeTable(Frequencies);

            startTime.Stop();
            var resultTime = startTime.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    resultTime.Hours,
                    resultTime.Minutes,
                    resultTime.Seconds,
                    resultTime.Milliseconds);
            Console.WriteLine("Build code table:  " + elapsedTime);
            //BuildCodeTable(Frequencies);
        }

        //public void Build(string[] source)
        //{
        //    for (int i = 0; i < source.Length; i++)
        //    {
        //        if (!Frequencies.ContainsKey(source[i]))
        //        {
        //            Frequencies.Add(source[i], 0);
        //        }

        //        Frequencies[source[i]]++;
        //    }

        //    foreach (KeyValuePair<string, int> symbol in Frequencies)
        //    {
        //        nodes.Add(new NodeHT() { Word = symbol.Key, Frequency = symbol.Value });
        //    }

        //    while (nodes.Count > 1)
        //    {
        //        List<NodeHT> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<NodeHT>();

        //        if (orderedNodes.Count >= 2)
        //        {
        //            // Take first two items
        //            List<NodeHT> taken = orderedNodes.Take(2).ToList<NodeHT>();

        //            // Create a parent node by combining the frequencies
        //            NodeHT parent = new NodeHT()
        //            {
        //                Word = "*",
        //                Frequency = taken[0].Frequency + taken[1].Frequency,
        //                Left = taken[0],
        //                Right = taken[1]
        //            };

        //            nodes.Remove(taken[0]);
        //            nodes.Remove(taken[1]);
        //            nodes.Add(parent);
        //        }

        //        this.Root = nodes.FirstOrDefault();

        //    }

        //}

        public void BuildCodeTable(FrequencyDictionary frequencyDictionary)
        {
            CodeTable = new Dictionary<string, List<bool>>();
            foreach (var word in frequencyDictionary.Dictionary)
            {
                List<bool> encodedWord = Root.Traverse(word.Key, new List<bool>());
                CodeTable.Add(word.Key, encodedWord);
            }
        }

        public void BuildTree(FrequencyDictionary frequencyDictionary)
        {
            foreach (KeyValuePair<string, int> word in frequencyDictionary.Dictionary)
            {
                nodes.Add(new NodeHT() { Word = word.Key, Frequency = word.Value });
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

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();
            }
        }

        public void Encode(string fileNameSource, string fileNameEncode)
        {
            BitArray bitArray = Encode(fileNameSource);
            byte[] res = new byte[bitArray.Count / 8];
            bitArray.CopyTo(res, 0);
            File.WriteAllBytes(@fileNameEncode, res);;
        }

        public BitArray Encode(string fileName)
        {
            StreamReader fileRead = new StreamReader(@fileName);
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
                        ((line[indexEnd] >= 'A' && line[indexEnd] <= 'Z'))))
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
            //дополнить до байта или байтом 0х80
            if(encodedSource.Count%8!=0)
            {
                encodedSource.Add(true);
                while (encodedSource.Count % 8 != 0)
                {
                    encodedSource.Add(false);
                }
            }
            else
            {
                encodedSource.AddRange(new List<bool> {false, true, true, true, true, true, true, true });
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
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
            Decode(bitArray, fileNameDecode);
        }

        public void Decode(BitArray bits, string fileName)
        {
            StreamWriter fileWrite = new StreamWriter(@fileName);
            NodeHT current = this.Root;
            string decoded = "";

            int endBitArr = bits.Count-1;
            while (bits[endBitArr] != true)
                endBitArr--;

            for(int i=0; i<endBitArr; i++)
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
