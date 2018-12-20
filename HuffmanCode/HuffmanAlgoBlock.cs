using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCode
{
    public class HuffmanAlgoBlock
    {
        public int SizeBlock { get; set; }
        private static readonly byte[] maskForEncodeTable = { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };
        private static readonly byte maskForEncode = 0x80;

        public HuffmanAlgoBlock()
        {
            SizeBlock = 0;
        }

        public HuffmanAlgoBlock(int size)
        {
            SizeBlock = size;
        }

        public List<bool> EncodeBlock(string fileNameSource, int sizeBlock)
        {
            StreamReader fileRead = new StreamReader(@fileNameSource, Encoding.Default);
            List<bool> encodedSource = new List<bool>();

            string text = fileRead.ReadToEnd();
            int indexText = 0;
            while(indexText < text.Length)
            {
                int indexBegin = 0;
                int indexEnd = 0;
                List<string> listWords = new List<string>();
                for (int i = 0; (i < sizeBlock) && (indexEnd < text.Length) ; i++)
                {
                    while (indexEnd < text.Length &&
                           ((text[indexEnd] >= 'a' && text[indexEnd] <= 'z') ||
                            (text[indexEnd] >= 'A' && text[indexEnd] <= 'Z')) ||
                           ((text[indexEnd] >= 'а' && text[indexEnd] <= 'я') ||
                            (text[indexEnd] >= 'А' && text[indexEnd] <= 'Я')))
                    {
                        indexEnd++;
                    }
                    //если мы прошлись по слову
                    string word;
                    if (indexBegin != indexEnd)
                    {
                        word = text.Substring(indexBegin, indexEnd - indexBegin);
                    }
                    else //это знак препенания, пробел или знак табуляции
                    {
                        indexEnd++;
                        word = text.Substring(indexBegin, indexEnd - indexBegin);
                    }
                    indexBegin = indexEnd;
                    listWords.Add(word);
                }
                encodedSource.AddRange(EncodeBlock(listWords));
            }

            return encodedSource;

        }

        public List<bool> EncodeBlock(List<string> words)
        {
            List<bool> encodedSource = new List<bool>();
            //Составляем частотный словарь из блока слов
            FrequencyDictionary frequencyDictionary = new FrequencyDictionary(words);
            //Строим дерево по полученому словарю
            HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
            //Кодируем блок 
            encodedSource = huffmanTree.EncodeBlock(words);
            //добавляем таблицу для декодировки
            List<bool> table = huffmanTree.GetBitArrayTable(frequencyDictionary);
            byte[] lenTable = BitConverter.GetBytes(table.Count);
            List<bool> lenghtTable = new List<bool>();
            foreach (var b in lenTable)
            {
                for (int i = 0; i < 8; i++)
                {
                    if(((b<<i) & maskForEncode) == 0x00)
                    {
                        lenghtTable.Add(false);
                    }
                    else
                    {
                        lenghtTable.Add(true);
                    }
                }
            }
            lenghtTable.AddRange(table);
            table = lenghtTable;
            //Добавляем код
            table.AddRange(encodedSource);

            byte[] lenAllBit = BitConverter.GetBytes(table.Count);
            List<bool> lenghtAll = new List<bool>();
            foreach (var b in lenAllBit)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (((b << i) & maskForEncode) == 0x00)
                    {
                        lenghtAll.Add(false);
                    }
                    else
                    {
                        lenghtAll.Add(true);
                    }
                }
            }
            lenghtAll.AddRange(table);
 
            encodedSource = lenghtAll;

            return encodedSource;
        }

        public void EncodeBlock(string fileNameSource, string fileNameEncode, int sizeBlock)
        {
            List<bool> bitsArr = EncodeBlock(fileNameSource, sizeBlock);
            if(bitsArr.Count % 8 == 0)
            {
                bitsArr.AddRange(new List<bool> { true, false, false, false, false, false, false, false });
            }
            else
            {
                bitsArr.Add(true);
                while (bitsArr.Count % 8 != 0)
                {
                    bitsArr.Add(false);
                }
            }
            BitArray resArr = new BitArray(bitsArr.ToArray());
            byte[] res = new byte[resArr.Count / 8];
            resArr.CopyTo(res, 0);
            File.WriteAllBytes(@fileNameEncode, res);
        }

        public void DecodeBlock(string fileNameEncode, string fileNameDecode)
        {
            string text = DecodeBlock(fileNameEncode);
            File.WriteAllText(@fileNameDecode, text);
        }

        public string DecodeBlock(string fileNameEncode)
        {
            BitArray bitArray = null;
            try
            {
                bitArray = new BitArray((File.ReadAllBytes(fileNameEncode)));
            }
            catch (Exception exc)
            {
                throw exc;
            }
            string text = "";
           
            int indexStart = 0;

            while(indexStart < bitArray.Count)
            {
                text += DecodeBlock(bitArray, indexStart, out indexStart);
            }

            return text;
        }

        public string DecodeBlock(BitArray bits, int indexStart, out int indexStop)
        {
            int index = indexStart;
            string textBlock = "";
            //Извдекаем всю длину блока вместе с диной 
            //таблицы и последовательности кодов
            int lenAllBlock = HuffmanTree.GetIntFromBitArray(index, bits);
            index += 8 * 4;
            //Извлекаем длину таблицы
            int lenTable = HuffmanTree.GetIntFromBitArray(index, bits);
            index += 8 * 4;
            //получае частотный словарь с блока
            FrequencyDictionary frequencyDictionary = HuffmanTree.GetFrequencyDictionaryFromFile(bits, index, index + lenTable);
            index += index + lenTable;
            //получаем дерево и декодировучныю таблицу
            HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
            //Декодируем блок
            textBlock = huffmanTree.DecodeBlock(bits, index, lenAllBlock + 4 * 8);
            indexStop = lenAllBlock + 4 * 8;

            return textBlock;
        }

    }
}
