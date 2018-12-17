using LosslessTextCompression_v2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HuffmanAdaptiveCode;

namespace LosslessTextCompression_v2
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text";
        //    string fileNameText = path + "\\input.txt";
        //    string fileNameDictionary = path + "\\dicry.txt";
        //    string fileNameEncodeText = path + "\\encodetext.bin";
        //    string fileNameDecodeText = path + "\\decodetext.txt";


        //    var startTime = System.Diagnostics.Stopwatch.StartNew();
        //    var treeHuffmanAdaptive = new HuffmanAdaptiveTree();
        //    treeHuffmanAdaptive.Encode(fileNameText, fileNameEncodeText);
        //    startTime.Stop();
        //    var resultTime = startTime.Elapsed;

        //    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //            resultTime.Hours,
        //            resultTime.Minutes,
        //            resultTime.Seconds,
        //            resultTime.Milliseconds);

        //    Console.WriteLine("Adaptive encode:  " + elapsedTime);

        //    treeHuffmanAdaptive.Reset();
        //    treeHuffmanAdaptive = null;
        //    HuffmanAdaptiveTree treeHuffmanAdaptive2 = new HuffmanAdaptiveTree();
        //    startTime = System.Diagnostics.Stopwatch.StartNew();
        //    treeHuffmanAdaptive2.Decode(fileNameEncodeText, fileNameDecodeText);

        //    startTime.Stop();
        //    resultTime = startTime.Elapsed;

        //    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //            resultTime.Hours,
        //            resultTime.Minutes,
        //            resultTime.Seconds,
        //            resultTime.Milliseconds);

        //    Console.WriteLine("Adaptive decoded:  " + elapsedTime);
        //}

        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text";
            string fileNameText = path + "\\input.txt";
            string fileNameDictionary = path + "\\dicry.txt";
            string fileNameEncodeText = path + "\\encodetext.bin";
            string fileNameDecodeText = path + "\\decodetext.txt";
            FrequencyDictionary frequencyDictionary = new FrequencyDictionary(fileNameText);


            frequencyDictionary.WriteDictionary(fileNameDictionary);

            var startTime = System.Diagnostics.Stopwatch.StartNew();

            HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);

            startTime.Stop();
            var resultTime = startTime.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    resultTime.Hours,
                    resultTime.Minutes,
                    resultTime.Seconds,
                    resultTime.Milliseconds);
            //Console.WriteLine("Tree build:  " + elapsedTime);

            startTime = System.Diagnostics.Stopwatch.StartNew();
            huffmanTree.Encode(fileNameText, fileNameEncodeText);
            startTime.Stop();
            resultTime = startTime.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    resultTime.Hours,
                    resultTime.Minutes,
                    resultTime.Seconds,
                    resultTime.Milliseconds);

            Console.WriteLine("Encode:  " + elapsedTime);



            startTime = System.Diagnostics.Stopwatch.StartNew();
            HuffmanTree huffmanTreeDecode = new HuffmanTree();
            huffmanTreeDecode.Decode(fileNameEncodeText, fileNameDecodeText);

            startTime.Stop();
            resultTime = startTime.Elapsed;

            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    resultTime.Hours,
                    resultTime.Minutes,
                    resultTime.Seconds,
                    resultTime.Milliseconds);

            Console.WriteLine("Decoded:  " + elapsedTime);
        }

        //static void Main(string[] args)
        //{
        //    char[] buf = { 'A', 'B', 'B', 'A', 'C' };
        //    List<AdaptiveHaffmanAlgorithm.Node> tree = new List<AdaptiveHaffmanAlgorithm.Node>();
        //    Dictionary<char, AdaptiveHaffmanAlgorithm.Node> leaves = new Dictionary<char, AdaptiveHaffmanAlgorithm.Node>();
        //    AdaptiveHaffmanAlgorithm.Node root = new AdaptiveHaffmanAlgorithm.Node();
        //    AdaptiveHaffmanAlgorithm.Node esc = root;
        //    tree.Add(root);

        //    for (int i = 0; i < buf.Length; i++)
        //    {
        //        if (leaves.ContainsKey(buf[i]))//leaves[buf[i]] != null)
        //        {
        //            AdaptiveHaffmanAlgorithm.IncWeight(leaves[buf[i]]);
        //            AdaptiveHaffmanAlgorithm.OutCode(AdaptiveHaffmanAlgorithm.GetCode(leaves[buf[i]], new List<bool>()));
        //        }
        //        else
        //        {
        //            leaves[buf[i]] = new AdaptiveHaffmanAlgorithm.Node();
        //            leaves[buf[i]].Simbol = buf[i];
        //            leaves[buf[i]].Parent = esc;

        //            AdaptiveHaffmanAlgorithm.Node tmp = new AdaptiveHaffmanAlgorithm.Node();
        //            tmp.Parent = esc;

        //            esc.LeftChild = tmp;
        //            esc.RightChild = leaves[buf[i]];
        //            tree.Add(leaves[buf[i]]);
        //            tree.Add(tmp);

        //            AdaptiveHaffmanAlgorithm.OutCode(AdaptiveHaffmanAlgorithm.GetCode(esc, new List<bool>()));
        //            Console.WriteLine(buf[i]);

        //            esc = tmp;
        //            AdaptiveHaffmanAlgorithm.IncWeight(leaves[buf[i]]);

        //        }
        //        tree = AdaptiveHaffmanAlgorithm.SortTree(tree);
        //        AdaptiveHaffmanAlgorithm.Recount(esc.Parent);
        //    }
        //}
    }
}
