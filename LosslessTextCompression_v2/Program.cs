using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Collections;

namespace LosslessTextCompression_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text";
            string fileNameText = path + "\\inputtest.txt";
            string fileNameDictionary = path + "\\dicry.txt";
            string fileNameEncodeText = path + "\\encodetext.bin";
            string fileNameDecodeText = path + "\\decodetext.txt";
            FrequencyDictionary frequencyDictionary = new FrequencyDictionary(fileNameText);
            frequencyDictionary.WriteDictionary(fileNameDictionary);

            HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
            huffmanTree.Encode(fileNameText, fileNameEncodeText);

            huffmanTree.Decode(fileNameEncodeText, fileNameDecodeText);

        }
    }
}
