using LosslessTextCompression_v2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LosslessTextCompression_v2
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text";
        //    string fileNameText = path + "\\inputtest.txt";
        //    string fileNameDictionary = path + "\\dicry.txt";
        //    string fileNameEncodeText = path + "\\encodetext.bin";
        //    string fileNameDecodeText = path + "\\decodetext.txt";
        //    FrequencyDictionary frequencyDictionary = new FrequencyDictionary(fileNameText);
        //    frequencyDictionary.WriteDictionary(fileNameDictionary);

        //    HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
        //    huffmanTree.Encode(fileNameText, fileNameEncodeText);

        //    huffmanTree.Decode(fileNameEncodeText, fileNameDecodeText);

        //}

        static void Main(string[] args)
        {
            char[] buf = { 'A', 'B', 'B', 'A', 'C' };
            List<AdaptiveHaffmanAlgorithm.Node> tree = new List<AdaptiveHaffmanAlgorithm.Node>();
            Dictionary<char, AdaptiveHaffmanAlgorithm.Node> leaves = new Dictionary<char, AdaptiveHaffmanAlgorithm.Node>();
            AdaptiveHaffmanAlgorithm.Node root = new AdaptiveHaffmanAlgorithm.Node();
            AdaptiveHaffmanAlgorithm.Node esc = root;
            tree.Add(root);

            for (int i = 0; i < buf.Length; i++)
            {
                if (leaves.ContainsKey(buf[i]))//leaves[buf[i]] != null)
                {
                    AdaptiveHaffmanAlgorithm.IncWeight(leaves[buf[i]]);
                    AdaptiveHaffmanAlgorithm.OutCode(AdaptiveHaffmanAlgorithm.GetCode(leaves[buf[i]], new List<bool>()));
                }
                else
                {
                    leaves[buf[i]] = new AdaptiveHaffmanAlgorithm.Node();
                    leaves[buf[i]].Simbol = buf[i];
                    leaves[buf[i]].Parent = esc;

                    AdaptiveHaffmanAlgorithm.Node tmp = new AdaptiveHaffmanAlgorithm.Node();
                    tmp.Parent = esc;

                    esc.LeftChild = tmp;
                    esc.RightChild = leaves[buf[i]];
                    tree.Add(leaves[buf[i]]);
                    tree.Add(tmp);

                    AdaptiveHaffmanAlgorithm.OutCode(AdaptiveHaffmanAlgorithm.GetCode(esc, new List<bool>()));
                    Console.WriteLine(buf[i]);

                    esc = tmp;
                    AdaptiveHaffmanAlgorithm.IncWeight(leaves[buf[i]]);

                }
                tree = AdaptiveHaffmanAlgorithm.SortTree(tree);
                AdaptiveHaffmanAlgorithm.Recount(esc.Parent);
            }
        }
    }
}
