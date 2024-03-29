﻿using HuffmanCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanAdaptiveCode
{
    public class HuffmanAdaptiveAlgoBlock
    {
        public int SizeBlock { get; set; }
        private static readonly byte[] maskForEncodeTable = { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };
        private static readonly byte maskForEncode = 0x80;
        private const int sizeByte = 8;

        public HuffmanAdaptiveAlgoBlock()
        {
            SizeBlock = 0;
        }

        public HuffmanAdaptiveAlgoBlock(int sizeBlock)
        {
            SizeBlock = sizeBlock;
        }

        /// <summary>
        /// Кодирование блока слов
        /// </summary>
        /// <param name="words">Блок слов определённого размера</param>
        /// <returns>Закодированый блок представленный 
        /// списком битов с добавленной длиной блока</returns>
        public List<bool> EncodeBlock(List<string> words)
        {
            List<bool> encodedSource = new List<bool>();

            HuffmanAdaptiveTree huffmanAdaptiveTree = new HuffmanAdaptiveTree();
            //Кодируем блок
            encodedSource = huffmanAdaptiveTree.Encode(words);
            //Добавляем перед закодированной последовательностью её длину
            encodedSource.InsertRange(0, BitOperations.Int32ToListBool(encodedSource.Count));
            //возращаем закодированный блок
            return encodedSource;
        }

        /// <summary>
        /// Кодирование исходного текста блочным способом
        /// </summary>
        /// <param name="fileNameSource">Имя файла исходного текста</param>
        /// <param name="sizeBlock">Размер блока</param>
        /// <returns>Закодированый текст блочным методом, представленный в виде списка битов</returns>
        public List<bool> EncodeBlock(string fileNameSource, int sizeBlock)
        {
            //Открываем поток для считывания с файла
            StreamReader fileRead = new StreamReader(@fileNameSource, Encoding.Default);
            List<bool> encodedSource = new List<bool>();
            //Считываем весь текст с файла
            string text = fileRead.ReadToEnd();
            int indexBegin = 0;
            int indexEnd = 0;
            //пока не конец текста
            while (indexEnd < text.Length)
            {
                List<string> listWords = new List<string>();
                //идём по тексту, пока не наберём слов количеством в размер блока и пока не конец текста
                for (int i = 0; (i < sizeBlock) && (indexEnd < text.Length); i++)
                {
                    //пока это буква увеличиваем индекс конца слова 
                    while (indexEnd < text.Length && char.IsLetter(text[indexEnd]))
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
            if (bitsArr.Count % 8 == 0)
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

        /// <summary>
        /// Кодирование исходного текста блочным способом c использованием метрики 
        /// </summary>
        /// <param name="fileNameSource">Имя файла исходного текста</param>
        /// <param name="sizeBlock">Размкр блока</param>
        /// <returns>Закодированый текст блочным методом с использованием метрики, представленный в виде списка битов</returns>
        public List<bool> EncodeBlockWithMetrics(string fileNameSource, int sizeBlock,
            out List<double> distanсeEuclide, out List<double> distanceChebyshev, out List<double> distanceCityBlock)
        {
            FrequencyDictionary frequencyDictionary = new FrequencyDictionary(fileNameSource);

            Dictionary<string, double> p = ComparisonVector.GetVectorZerosFromFrequencyDictionary(frequencyDictionary);
            Dictionary<string, double> q = ComparisonVector.GetVectorZerosFromFrequencyDictionary(frequencyDictionary);

            List<double> distanceE = new List<double>();
            List<double> distanceC = new List<double>();
            List<double> distanceCB = new List<double>();

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
                for (int i = 0; (i < sizeBlock) && (indexEnd < text.Length); i++)
                {
                    //пока это буква увеличиваем индекс конца слова 
                    while (indexEnd < text.Length && char.IsLetter(text[indexEnd]))
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

                //работа с растояниями
                q = ComparisonVector.GetVectorFrequencyFromBlock(frequencyDictionary, listWords);
                distanceE.Add(ComparisonVector.GetEuclideanDistance(p, q));
                distanceC.Add(ComparisonVector.GetChebyshevDistance(p, q));
                distanceCB.Add(ComparisonVector.GetCityBlockDistance(p, q));
                p = q;
            }

            //for (int i = 0; i < distanceE.Count; i++)
            //{
            //    Console.Write(distanceE[i].ToString() + " " +  distanceC[i].ToString() + 
            //        " " + distanceCB[i].ToString() + "\n");
            //}

            distanсeEuclide = distanceE;
            distanceChebyshev = distanceC;
            distanceCityBlock = distanceCB;

            //возращаем закодированный блок слов в виде списка бит
            return encodedSource;
        }

        /// <summary>
        /// Кодирование исходного текста блочным способом c использованием метрики 
        /// </summary>
        /// <param name="fileNameSource">Имя файла исходного текста</param>
        /// <param name="fileNameEncode">Имя файла закодированного текста</param>
        /// <param name="sizeBlock">Размер блока</param>
        /// <param name="fileNameMetrics">Имя файла для записи растояния между блоками</param>
        public List<List<double>> EncodeBlockWithMetrics(string fileNameSource, string fileNameEncode, int sizeBlock, string fileNameMetrics)
        {
            List<double> distanceEuclide = new List<double>();
            List<double> distanceChebyshev = new List<double>();
            List<double> distanceCityBlock = new List<double>();
            //Кодируем исходный текст блочным методом
            List<bool> bitsArr = EncodeBlockWithMetrics(@fileNameSource, sizeBlock,
               out distanceEuclide, out distanceChebyshev, out distanceCityBlock);
            //Дописываем приписок в зависимости от кратности 8 полученного списка битов
            //Это нужно для того чтобы знать конец нашей последовательности битов 
            //и была возможность записать эту последовательность в файл, т.к. минимальный объём инфорvации,
            //что мы можем записать в файл - это байт
            if (bitsArr.Count % 8 == 0)
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

            StreamWriter streamWriter = new StreamWriter(fileNameMetrics);
            for (int i = 0; i < distanceEuclide.Count; i++)
            {
                streamWriter.WriteLine(distanceEuclide[i].ToString() + " " +
                    distanceChebyshev[i].ToString() + " " + distanceCityBlock[i].ToString());
            }
            streamWriter.Close();
            return new List<List<double>> { distanceEuclide, distanceChebyshev, distanceCityBlock };
        }

        /// <summary>
        /// Декодирование блока
        /// </summary>
        /// <param name="bits">Последовательность бит, в которой содержиться закодированный блок</param>
        /// <param name="indexStart">Индекс начала блока вместе с его длинной</param>
        /// <param name="indexEnd">Индекс конца блока</param>
        /// <returns>Декодированный блок текста</returns>
        public StringBuilder DecodeBlock(BitArray bits, int indexStart, out int indexEnd)
        {
            int index = indexStart;
            StringBuilder textBlock = new StringBuilder();
            //извлекаем длину закодированного блока
            int lenBlock = BitOperations.GetInt32FromBitArray(index, bits);
            index += sizeByte * sizeof(int);
            HuffmanAdaptiveTree huffmanAdaptiveTree = new HuffmanAdaptiveTree();
            //декодируем блок
            textBlock = huffmanAdaptiveTree.DecodeBlock(bits, index, index + lenBlock);
            //изменяем индекс конца блока
            indexEnd = index + lenBlock;

            return textBlock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileNameEncode"></param>
        /// <param name="fileNameDecode"></param>
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
            int indexEnd = bitArray.Count - 1;
            while (!bitArray[indexEnd])
            {
                indexEnd--;
            }
            indexEnd--;

            StreamWriter fileWrite = new StreamWriter(@fileNameDecode);
            int indexStart = 0;
            StringBuilder textBlocks = new StringBuilder();

            while (indexStart < indexEnd)
            {
                textBlocks = DecodeBlock(bitArray, indexStart, out indexStart);
                fileWrite.Write(textBlocks.ToString());
            }
            fileWrite.Close();
            //File.WriteAllText(@fileNameDecode, text);
        }

    }
}
