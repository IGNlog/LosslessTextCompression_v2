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
        private const int sizeByte = 8;

        public HuffmanAlgoBlock()
        {
            SizeBlock = 0;
        }

        public HuffmanAlgoBlock(int size)
        {
            SizeBlock = size;
        }

        /// <summary>
        /// Кодирование исходного текста блочным способом
        /// </summary>
        /// <param name="fileNameSource">Имя файла исходного текста</param>
        /// <param name="sizeBlock">Размкр блока</param>
        /// <returns>Закодированый текст блочным методом, представленный в виде списка битов</returns>
        public List<bool> EncodeBlock(string fileNameSource, int sizeBlock)
        {
            //Открываем поток для считывания с файла
            StreamReader fileRead = new StreamReader(@fileNameSource, Encoding.Default);
            List<bool> encodedSource = new List<bool>();
            //Считываем весь текст с файла
            string text = fileRead.ReadToEnd();
            //int indexText = 0;
            int indexBegin = 0;
            int indexEnd = 0;
            //пока не конец текста
            while (indexEnd < text.Length)
            {
                //indexBegin = 0;
                //indexEnd = 0;
                List<string> listWords = new List<string>();
                //идём по тексту, пока не наберём слов количеством в размер блока и пока не конец текста
                for (int i = 0; (i < sizeBlock) && (indexEnd < text.Length) ; i++)
                {
                    //пока это буква увеличиваем индекс конца слова 
                    while (indexEnd < text.Length && char.IsLetter(text[indexEnd])
                           /*((text[indexEnd] >= 'a' && text[indexEnd] <= 'z') ||
                            (text[indexEnd] >= 'A' && text[indexEnd] <= 'Z')) ||
                           ((text[indexEnd] >= 'а' && text[indexEnd] <= 'я') ||
                            (text[indexEnd] >= 'А' && text[indexEnd] <= 'Я'))*/)
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
                    //добавляем слово в блок
                    listWords.Add(word);
                }
                //кодирум блок слов и добавляем получившиеся биты в список битов
                encodedSource.AddRange(EncodeBlock(listWords));
                
            }
            //возращаем закодированный блок слов в виде списка бит
            return encodedSource;
        }

        /// <summary>
        /// Кодирование блока слов
        /// </summary>
        /// <param name="words">Блок слов определённого размера</param>
        /// <returns>Закодированый блок представленный списком битов</returns>
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
            //получаем длину таблицы в виде массиве байтов
            //byte[] lenTable = BitConverter.GetBytes(table.Count);
            List<bool> lenghtTable = BitOperations.Int32ToListBool(table.Count);
            lenghtTable.AddRange(table);
            table = lenghtTable;
            //Добавляем код
            table.AddRange(encodedSource);
            //Получаем длину всего закодированного блока
            List<bool> lenghtAll = BitOperations.Int32ToListBool(table.Count);
            //добавляем эту длину в в начало блока
            lenghtAll.AddRange(table);
 
            encodedSource = lenghtAll;
            //возращаем закодированный блок
            return encodedSource;
        }

        /// <summary>
        /// Кодироване исходного текста и запись закодированного текста в файл
        /// </summary>
        /// <param name="fileNameSource">Имя файла исходного текста</param>
        /// <param name="fileNameEncode">Имя файла куда будет записан результат</param>
        /// <param name="sizeBlock">Размер блока</param>
        public void EncodeBlock(string fileNameSource, string fileNameEncode, int sizeBlock)
        {
            //Кодируем исходный текст блочным методом
            List<bool> bitsArr = EncodeBlock(@fileNameSource, sizeBlock);
            //Дописываем приписок в зависимости от кратности 8 полученного списка битов
            //Это нужно для того чтобы знать конец нашей последовательности битов 
            //и была возможность записать эту последовательность в файл, т.к. минимальный объём инфорбации,
            //что мы можем записать в файл - это байт
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
            //Превращаем наш список битов в массив байтов
            BitArray resArr = new BitArray(bitsArr.ToArray());
            byte[] res = new byte[resArr.Count / 8];
            resArr.CopyTo(res, 0);
            //и записываем в файл
            File.WriteAllBytes(@fileNameEncode, res);
        }

        public void DecodeBlock(string fileNameEncode, string fileNameDecode)
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
            //отсекаем дописок
            int indexEnd = bitArray.Count-1; 
            while (!bitArray[indexEnd])
            {
                indexEnd--;
            }
            indexEnd--;

            StreamWriter fileWrite = new StreamWriter(@fileNameDecode);
            int indexStart = 0; string textBlock = "";

            while (indexStart < indexEnd)
            {
                textBlock = DecodeBlock(bitArray, indexStart, out indexStart);
                fileWrite.Write(textBlock);
            }
            fileWrite.Close();
            //File.WriteAllText(@fileNameDecode, text);
        }

        //public 

        //public string DecodeBlock(string fileNameEncode)
        //{
        //    BitArray bitArray = null;
        //    try
        //    {
        //        bitArray = new BitArray((File.ReadAllBytes(fileNameEncode)));
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    string text = "";
           
        //    int indexStart = 0;

        //    while(indexStart < bitArray.Count)
        //    {
        //        text += DecodeBlock(bitArray, indexStart, out indexStart);
        //    }

        //    return text;
        //}

        public string DecodeBlock(BitArray bits, int indexStart, out int indexStop)
        {
            int index = indexStart;
            string textBlock = "";
            //Извдекаем всю длину блока вместе с диной 
            //таблицы и последовательности кодов
            int lenAllBlock = HuffmanTree.GetIntFromBitArray(index, bits);
            index += sizeByte * sizeof(int);
            //Извлекаем длину таблицы
            int lenTable = HuffmanTree.GetIntFromBitArray(index, bits);
            index += sizeByte * sizeof(int);
            //получае частотный словарь с блока
            FrequencyDictionary frequencyDictionary = HuffmanTree.GetFrequencyDictionaryFromFile(bits, index, index + lenTable);
            index = index + lenTable;
            //получаем дерево и декодировучныю таблицу
            HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
            //Декодируем блок
            textBlock = huffmanTree.DecodeBlock(bits, index, index + ((lenAllBlock) - (lenTable + sizeof(int)*sizeByte)));
            indexStop = index + ((lenAllBlock) - (lenTable + sizeof(int) * sizeByte));
            //indexStop = lenAllBlock + sizeof(int) * sizeByte;

            return textBlock;
        }

    }
}
