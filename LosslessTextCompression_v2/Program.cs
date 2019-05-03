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
using HuffmanCode;

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


        //static void Main(string[] args)
        //{
        //    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text";
        //    string fileNameText = path + "\\input.txt";
        //    string fileNameDictionary = path + "\\dicry.txt";
        //    string fileNameEncodeText = path + "\\encodetext.bin";
        //    string fileNameDecodeText = path + "\\decodetext.txt";
        //    FrequencyDictionary frequencyDictionary = new FrequencyDictionary(fileNameText);


        //    frequencyDictionary.WriteDictionary(fileNameDictionary);

        //    var startTime = System.Diagnostics.Stopwatch.StartNew();

        //    HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);

        //    startTime.Stop();
        //    var resultTime = startTime.Elapsed;

        //    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //            resultTime.Hours,
        //            resultTime.Minutes,
        //            resultTime.Seconds,
        //            resultTime.Milliseconds);
        //    Console.WriteLine("Tree build:  " + elapsedTime);

        //    startTime = System.Diagnostics.Stopwatch.StartNew();
        //    huffmanTree.Encode(fileNameText, fileNameEncodeText);
        //    startTime.Stop();
        //    resultTime = startTime.Elapsed;

        //    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //            resultTime.Hours,
        //            resultTime.Minutes,
        //            resultTime.Seconds,
        //            resultTime.Milliseconds);

        //    Console.WriteLine("Encode:  " + elapsedTime);



        //    startTime = System.Diagnostics.Stopwatch.StartNew();
        //    HuffmanTree huffmanTreeDecode = new HuffmanTree();
        //    huffmanTreeDecode.Decode(fileNameEncodeText, fileNameDecodeText);

        //    startTime.Stop();
        //    resultTime = startTime.Elapsed;

        //    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //            resultTime.Hours,
        //            resultTime.Minutes,
        //            resultTime.Seconds,
        //            resultTime.Milliseconds);

        //    Console.WriteLine("Decoded:  " + elapsedTime);
        //}


        static void Main(string[] args)
        {
            ////Тест работы блочного класического алгоритма Хаффмана
            //List<string> words = new List<string>();
            //words.Add("A");
            //words.Add("B");
            //words.Add("B");
            //words.Add("A");
            //words.Add("A");
            //words.Add("B");
            //words.Add("A");
            //words.Add("A");

            //HuffmanAlgoBlock huffmanAlgoBlock = new HuffmanAlgoBlock();
            //List<bool> listEncode = huffmanAlgoBlock.EncodeBlock(words);

            //BitArray bitArrayEncode = new BitArray(listEncode.ToArray());

            //HuffmanAlgoBlock huffmanAlgoBlock1 = new HuffmanAlgoBlock();
            //int stop = bitArrayEncode.Count -1;
            //string res = huffmanAlgoBlock1.DecodeBlock(bitArrayEncode, 0, out stop);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text\\Text";
            string fileNameText = path + "\\text.txt";
            string fileNameDictionary = path + "\\dicry.txt";
            string fileNameEncodeText = path + "\\encodetext.bin";
            string fileNameDecodeText = path + "\\decodetext.txt";

            HuffmanAlgoBlock huffmanAlgoBlock = new HuffmanAlgoBlock();
            huffmanAlgoBlock.EncodeBlockWithMetrics(@fileNameText, @fileNameEncodeText, 300);

            huffmanAlgoBlock.DecodeBlock(@fileNameEncodeText, @fileNameDecodeText);
        }
    }
}
