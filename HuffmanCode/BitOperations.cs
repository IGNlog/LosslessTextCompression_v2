using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HuffmanCode
{
    public class BitOperations
    {
        private static readonly byte[] mask = { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };

        /// <summary>
        /// Преобразует int в List<bool>
        /// </summary>
        /// <param name="num">Преобразуемое число</param>
        /// <returns>List<bool> в котором хранится битовое представление числа в прямой последовательности</returns>
        public static List<bool> Int32ToListBool(int num)
        {
            byte[] bits = BitConverter.GetBytes(num);
            List<bool> listBool = new List<bool>();
            foreach (var b in bits)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (((b << i) & mask[0]) == 0x00)
                    {
                        listBool.Add(false);
                    }
                    else
                    {
                        listBool.Add(true);
                    }
                }
            }
            return listBool;
        }

        /// <summary>
        /// Преобразует byte в List<bool>
        /// </summary>
        /// <param name="num">Преобразуемое число</param>
        /// <returns>List<bool> в котором хранится битовое представление байта в прямой последовательности</returns>
        public static List<bool> ByteToListBool(byte num)
        {
            List<bool> listBool = new List<bool>();
            for (int i = 0; i < 8; i++)
            {
                if (((num << i) & mask[0]) == 0x00)
                {
                    listBool.Add(false);
                }
                else
                {
                    listBool.Add(true);
                }
            }
            return listBool;
        }

        /// <summary>
        /// Преобразует строку в List<bool>
        /// </summary>
        /// <param name="word">Преобразуемое слово</param>
        /// <returns>List<bool> в котором хранится битовое представление слова в прямой последовательности</returns>
        public static List<bool> StringToListBool(string word)
        {
            List<bool> listBool = new List<bool>();
            byte[] wordByteArray = Encoding.Default.GetBytes(word);
            foreach (var symbol in wordByteArray)
            {
                for (int i = 0; i < 8; i++)
                {
                    if ((byte)((symbol << i) & mask[0]) == 0x00)
                    {
                        listBool.Add(false);
                    }
                    else
                    {
                        listBool.Add(true);
                    }
                }
            }
            return listBool;
        }

        /// <summary>
        /// Преобрузует последовательность бит в слово
        /// </summary>
        /// <param name="index">Индекс начала бит слова в последовательности битов</param>
        /// <param name="len">Длина слова в байтах</param>
        /// <param name="bits">Последовательность битов</param>
        /// <returns>Слово в виде строки</returns>
        public static string GetWordString(int index, byte len, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] wordByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        wordByte[i] = (byte)(wordByte[i] | mask[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return Encoding.Default.GetString(wordByte);
        }

        /// <summary>
        /// Преобрузует последовательность бит в слово, представленное массивом байтов
        /// </summary>
        /// <param name="index">Индекс начала бит слова в последовательности битов</param>
        /// <param name="len">Длина слова в байтах</param>
        /// <param name="bits">Последовательность битов</param>
        /// <returns>Слово в виде </returns>
        public static byte[] GetWordByteArray(int index, byte len, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] wordByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        wordByte[i] = (byte)(wordByte[i] | mask[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return wordByte;
        }

        public static string GetStringFromBitArray(int index, byte len, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] wordByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        wordByte[i] = (byte)(wordByte[i] | mask[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return Encoding.Default.GetString(wordByte);
        }

        public static StringBuilder GetStringBuilderFromBitArray(int index, byte len, BitArray bits)
        {
            return new StringBuilder(GetStringFromBitArray(index, len, bits)); 
        }

        /// <summary>
        /// Получить длину слова в байтах
        /// </summary>
        /// <param name="index">Индекс начала длины в битовой последовательности</param>
        /// <param name="bits">Битовая последовательность</param>
        /// <returns>Длину слова в виде байта</returns>
        public static byte GetLenWordFromBitArray(int index, BitArray bits)
        {
            byte lenWord = 0x00;
            for (int i = 0; i < 8; i++)
            {
                if (bits[i + index])
                {
                    lenWord = (byte)(lenWord | mask[i]);
                }
            }
            return lenWord;
        }

        public static int GetInt32FromBitArray(int index, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] intByte = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        intByte[i] = (byte)(intByte[i] | mask[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return BitConverter.ToInt32(intByte, 0);
        }
    }
}
