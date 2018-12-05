using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LosslessTextCompression_v2
{
    public class FrequencyDictionary
    {
        //количество слов в словаре
        public int CountWord { get; set; }
        //сам словарь непосредственно
        public Dictionary<string, int> Dictionary { get; set; }

        public FrequencyDictionary(int countWord, Dictionary<string, int> dictionary)
        {
            CountWord = countWord;
            Dictionary = dictionary;
        }

        public FrequencyDictionary()
        {
            CountWord = 0;
            Dictionary<string, int> dictionary = new Dictionary<string, int>(CountWord);
        }

        public FrequencyDictionary(string fileNameText)
        {
            Dictionary = new Dictionary<string, int>();
            Dictionary = GetDictionary(fileNameText);
            CountWord = Dictionary.Count;
        }

        public FrequencyDictionary(string fileNameText, string fileNameDictionary)
        {
            Dictionary = new Dictionary<string, int>();
            Dictionary = GetDictionary(fileNameText);
            CountWord = Dictionary.Count;
            WriteDictionary(Dictionary, fileNameDictionary);
        }

        public Dictionary<string, int> GetDictionary(string fileNameText)
        {
            StreamReader fileRead = new StreamReader(@fileNameText);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
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

                    if (dictionary.ContainsKey(word))
                    {
                        dictionary[word]++;
                    }
                    else
                    {
                        dictionary.Add(word, 1);
                    }
                }
            }
            fileRead.Close();
            return dictionary;

        }

        public static void WriteDictionary(Dictionary<string, int> dictionary, string fileNameDictionary)
        {
            StreamWriter fileWrite = new StreamWriter(@fileNameDictionary);
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                switch (pair.Key)
                {
                    case "\t":
                        fileWrite.WriteLine("SYMBOL_TAB" + "\t" + pair.Value.ToString());
                        break;
                    case "\n":
                        fileWrite.WriteLine("SYMBOL_ENTER" + "\t" + pair.Value.ToString());
                        break;
                    case "\r":
                        fileWrite.WriteLine("SYMBOL_CARRIAGE_RETURN" + "\t" + pair.Value.ToString());
                        break;
                    case "\v":
                        fileWrite.WriteLine("SYMBOL_VERT_TAB" + "\t" + pair.Value.ToString());
                        break;
                    case "\0":
                        fileWrite.WriteLine("SYMBOL_NULL" + "\t" + pair.Value.ToString());
                        break;
                    case "\a":
                        fileWrite.WriteLine("SYMBOL_BIP" + "\t" + pair.Value.ToString());
                        break;
                    case " ":
                        fileWrite.WriteLine("SYMBOL_SPACE" + "\t" + pair.Value.ToString());
                        break;
                    default:
                        fileWrite.WriteLine(pair.Key + "\t" + pair.Value.ToString());
                        break;
                }
            }
            fileWrite.Close();
        }

        public static void ReadDictionary(Dictionary<string, int> dictionary, string fileNameDictionary)
        {
            StreamReader fileRead = new StreamReader(@fileNameDictionary);
            dictionary = new Dictionary<string, int>();

            string line;
            while ((line = fileRead.ReadLine()) != null)
            {
                string[] keyValue = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                switch (keyValue[0])
                {
                    case "SYMBOL_TAB":
                        dictionary.Add("\t", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_ENTER":
                        dictionary.Add("\n", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_CARRIAGE_RETURN":
                        dictionary.Add("\r", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_VERT_TAB":
                        dictionary.Add("\v", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_NULL":
                        dictionary.Add("\0", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_BIP":
                        dictionary.Add("\a", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_SPACE":
                        dictionary.Add(" ", int.Parse(keyValue[1]));
                        break;
                    default:
                        dictionary.Add(keyValue[0], int.Parse(keyValue[1]));
                        break;
                }
            }
        }

        public void WriteDictionary(string fileNameDictionary)
        {
            StreamWriter fileWrite = new StreamWriter(@fileNameDictionary);
            foreach (KeyValuePair<string, int> pair in Dictionary)
            {
                switch (pair.Key)
                {
                    case "\t":
                        fileWrite.WriteLine("SYMBOL_TAB" + "\t" + pair.Value.ToString());
                        break;
                    case "\n":
                        fileWrite.WriteLine("SYMBOL_ENTER" + "\t" + pair.Value.ToString());
                        break;
                    case "\r":
                        fileWrite.WriteLine("SYMBOL_CARRIAGE_RETURN" + "\t" + pair.Value.ToString());
                        break;
                    case "\v":
                        fileWrite.WriteLine("SYMBOL_VERT_TAB" + "\t" + pair.Value.ToString());
                        break;
                    case "\0":
                        fileWrite.WriteLine("SYMBOL_NULL" + "\t" + pair.Value.ToString());
                        break;
                    case "\a":
                        fileWrite.WriteLine("SYMBOL_BIP" + "\t" + pair.Value.ToString());
                        break;
                    case " ":
                        fileWrite.WriteLine("SYMBOL_SPACE" + "\t" + pair.Value.ToString());
                        break;
                    default:
                        fileWrite.WriteLine(pair.Key + "\t" + pair.Value.ToString());
                        break;
                }
            }
            fileWrite.Close();
        }

        public void ReadDictionary(string fileNameDictionary)
        {
            StreamReader fileRead = new StreamReader(@fileNameDictionary);
            if(Dictionary!=null)
                Dictionary.Clear();
            Dictionary = new Dictionary<string, int>();

            string line;
            while ((line = fileRead.ReadLine()) != null)
            {
                string[] keyValue = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                switch (keyValue[0])
                {
                    case "SYMBOL_TAB":
                        Dictionary.Add("\t", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_ENTER":
                        Dictionary.Add("\n", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_CARRIAGE_RETURN":
                        Dictionary.Add("\r", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_VERT_TAB":
                        Dictionary.Add("\v", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_NULL":
                        Dictionary.Add("\v", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_BIP":
                        Dictionary.Add("\a", int.Parse(keyValue[1]));
                        break;
                    case "SYMBOL_SPACE":
                        Dictionary.Add(" ", int.Parse(keyValue[1]));
                        break;
                    default:
                        Dictionary.Add(keyValue[0], int.Parse(keyValue[1]));
                        break;
                }
            }
            CountWord = Dictionary.Count;
        }

        public void SortValue()
        {
            Dictionary = Dictionary.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void SortDescValue()
        {
            Dictionary = Dictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

    }
}
