using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuffmanCode;

namespace HuffmanAdaptiveCode
{
    public class HuffmanAdaptiveTree
    {
        private static readonly byte maskForEncode = 0x80;
        private static readonly byte[] maskForDecode = { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };
        public Node Root { get; private set; }

        private Node _nyt; 
        private List<Node> _nodes;
        private int _nextNum;
        public Dictionary<string, Node> dictionaryNodes;

        public HuffmanAdaptiveTree()
        {
            Reset();
            dictionaryNodes = new Dictionary<string, Node>();
        }

        public void Reset()
        {
            Root = new Node { Number = 0 };
            _nyt = Root;
            _nodes = new List<Node>
            {
                Root
            };
            _nextNum = 1;
        }

        public List<bool> GetNYTCode()
        {
            List<bool> code = new List<bool>();
            Node node = _nyt;
            Node prevNode = _nyt;
            while(node!=Root)
            {
                node = node.Parent;
                if(node?.Right==prevNode)
                {
                    code.Add(true);
                }
                if(node?.Left==prevNode)
                {
                    code.Add(false);
                }
                prevNode = node;
            }
            code?.Reverse();
            return code;
        }

        public List<bool> GetCode(Node nodeWord)
        {
            List<bool> code = new List<bool>();
            Node node = nodeWord;
            Node prevNode =nodeWord;
            while (node != Root)
            {
                node = node.Parent;
                if (node?.Right == prevNode)
                {
                    code.Add(true);
                }
                if (node?.Left == prevNode)
                {
                    code.Add(false);
                }
                prevNode = node;
            }
            code?.Reverse();
            return code;
        }

        public string Encode(string[] text)
        {
            var result = new StringBuilder();

            foreach (var c in text)
                result.Append(Encode(c));

            return result.ToString();
        }

        public List<bool> Encode(string word)
        {
            Node node = null;
            if(dictionaryNodes.ContainsKey(word))
                node = dictionaryNodes[word];

            List<bool> code = new List<bool>();

            if (node != null)
            {
                code = GetCode(node);
                node.Weight++;
            }
            else
            {
                code = GetNYTCode();
                if(code==null)
                {
                    code = new List<bool>();
                }
                ////////
                byte lenWord = (byte)word.Length;
                for (int i = 0; i < 8; i++)
                {
                    if ((byte)((lenWord << i) & maskForEncode) == 0x00)
                    {
                        code.Add(false);
                    }
                    else
                    {
                        code.Add(true);
                    }
                }
                byte[] wordByteArray = Encoding.Default.GetBytes(word);
                foreach (var item in wordByteArray)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((byte)((item << i) & maskForEncode) == 0x00)
                        {
                            code.Add(false);
                        }
                        else
                        {
                            code.Add(true);
                        }
                    }
                }

                node = AddToNYT(word);
                dictionaryNodes.Add(word, node);
            }

            UpdateAll(node.Parent);

            return code;
        }

        public void Encode(string fileNameSource, string fileNameEncode)
        {
            BitArray bitArray = EncodeFile(fileNameSource);
            byte[] res = new byte[bitArray.Count / 8];
            bitArray.CopyTo(res, 0);
            File.WriteAllBytes(@fileNameEncode, res); ;
        }

        public List<bool> Encode(List<string> words)
        {
            List<bool> encodedSource = new List<bool>();
            foreach (var word in words)
            {
                encodedSource.AddRange(Encode(word));
            }
            return encodedSource;
        }

        public BitArray EncodeFile(string fileName)
        {
            StreamReader fileRead = new StreamReader(@fileName, Encoding.Default);
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
                         (line[indexEnd] >= 'A' && line[indexEnd] <= 'Z')) ||
                        ((line[indexEnd] >= 'а' && line[indexEnd] <= 'я') ||
                         (line[indexEnd] >= 'А' && line[indexEnd] <= 'Я')))
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

                    List<bool> encodedWord = Encode(word);

                    encodedSource.AddRange(encodedWord);
                }
            }
            fileRead.Close();
            //дополнить до байта или байтом 0х80
            if (encodedSource.Count % 8 != 0)
            {
                encodedSource.Add(true);
                while (encodedSource.Count % 8 != 0)
                {
                    encodedSource.Add(false);
                }
            }
            else
            {
                encodedSource.AddRange(new List<bool> { false, true, true, true, true, true, true, true });
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(string code)
        {
            var result = new StringBuilder();

            int index = 0;
            while (index < code.Length)
            {
                Node node;

                string word = ReadString(index, code, out int count);
                index += count;

                if (word == null)
                {
                    byte num = byte.Parse(code.Substring(0, 8));
                    string word2code = code.Substring(9, num * 8);
                    string[] w2cArray = new string[(int)Math.Round((double)(word2code.Length / 8), 0)];
                    string wordbuf = "";
                    for (int i = 0; i < w2cArray.Length; i++)
                    {
                        w2cArray[i] = word2code.Substring(i * 8, 8 * (i + 1));
                        wordbuf += Convert.ToString(Convert.ToSByte(w2cArray[i]));
                    }

                    code = code.Remove(0, num + 1);

                    word = wordbuf;
                    node = AddToNYT(word);
                }
                else
                {
                    node = Root.FindOrDefault(word);
                    node.Weight++;
                }

                UpdateAll(node.Parent);

                result.Append(word);
            }

            return result.ToString();
        }

        public byte GetLenWordFromBitArray(int index, BitArray bits)
        {
            byte lenWord = 0x00;
            for (int i = 0; i < 8; i++)
            {
                if (bits[i + index])
                {
                    lenWord = (byte)(lenWord | maskForDecode[i]);
                }
            }
            return lenWord;
        }

        public string GetWordFromBitArray(int index, byte len, BitArray bits)
        {
            //здесь будет слово в байтовом виде 
            byte[] wordByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (bits[index + i * 8 + k])
                    {
                        wordByte[i] = (byte)(wordByte[i] | maskForDecode[k]);
                    }
                }
            }
            //Получаем строковое представление слова
            return Encoding.Default.GetString(wordByte);
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

        public StringBuilder DecodeBlock(BitArray bits, int indexStart, int indexEnd)
        {
            StringBuilder textBlock = new StringBuilder();
            Node nodeFirsIter;
            //вначале файла записана длина слова в байтах одним байтом
            //потом идёт само слово в байтовом виде
            int index = 0;
            //сначала определяем длину слова
            byte lenWord = BitOperations.GetLenWordFromBitArray(index, bits);
            index += 8;
            //Получаем строковое представление слова
            string wordDecode = BitOperations.GetStringFromBitArray(index, lenWord, bits);
            //индекс j будет указывать на след бит после первого слова
            index += 8 * lenWord;
            //заносим в дерево и обновляем его
            nodeFirsIter = AddToNYT(wordDecode);
            UpdateAll(nodeFirsIter.Parent);
            //добавляем декодированое слово в результирующую строку
            textBlock.Append(wordDecode);

            dictionaryNodes.Add(wordDecode, nodeFirsIter);

            //пока не дойдём до конца последовательности битов
            while (index < indexEnd)
            {
                Node node;
                string word = ReadString(index, bits, out int count);
                index += count;

                //если мы не получили код слова, значит его нет в дереве
                //и мы прошли до esc символа

                if (word == null)
                {
                    lenWord = BitOperations.GetLenWordFromBitArray(index, bits);
                    index += 8;
                    word = BitOperations.GetStringFromBitArray(index, lenWord, bits);
                    index += 8 * lenWord;
                    node = AddToNYT(word);

                    dictionaryNodes.Add(word, node);
                }
                else
                {
                    node = dictionaryNodes[word];
                    node.Weight++;
                }
                UpdateAll(node.Parent);

                textBlock.Append(word);
            }

            return textBlock;
        }

        public void Decode(BitArray bits, string fileName)
        {
            StreamWriter fileWrite = new StreamWriter(@fileName);
            int endBitArr = bits.Count - 1;
            while (bits[endBitArr] != true)
                endBitArr--;

            Node nodeFirsIter;
            //вначале файла записана длина слова в байтах одним байтом
            //потом идёт само слово в байтовом виде
            int index = 0;
            //сначала определяем длину слова
            byte lenWord = GetLenWordFromBitArray(index, bits);
            
            index += 8;

            //Получаем строковое представление слова
            string wordDecode = GetWordFromBitArray(index, lenWord, bits);
            //индекс j будет указывать на след бит после первого слова
            index += 8 * lenWord;
            //заносим в дерево и обновляем его
            nodeFirsIter = AddToNYT(wordDecode);
            UpdateAll(nodeFirsIter.Parent);
            //пишим декодированое слово в файл
            fileWrite.Write(wordDecode);

            dictionaryNodes.Add(wordDecode, nodeFirsIter);

            //пока не дойдём до конца последовательности битов
            while (index < endBitArr)
            {
                Node node;
                string word = ReadString(index, bits, out int count);
                index += count;

                //если мы не получили код слова, значит его нет в дереве
                //и мы прошли до esc символа

                if (word == null)
                {
                    lenWord = GetLenWordFromBitArray(index, bits);
                    index += 8;
                    word = GetWordFromBitArray(index, lenWord, bits);
                    index += 8 * lenWord;
                    node = AddToNYT(word);

                    dictionaryNodes.Add(word, node);
                }
                else
                {
                    node = dictionaryNodes[word];
                    node.Weight++;
                }
                UpdateAll(node.Parent);

                fileWrite.Write(word);
            }

            fileWrite.Close();
        }

        private string ReadString(int index, string code, out int count)
        {
            Node current = Root;
            count = 0;

            while (true)
            {
                count++;

                if (current == _nyt)
                    return null;

                if (current.IsLeaf())
                {
                    count--;
                    return current.Word;
                }

                char bit = code[index++];

                if (bit == '0')
                    current = current.Left;
                else if (bit == '1')
                    current = current.Right;
            }
        }

        private string ReadString(int index, BitArray code, out int count)
        {
            Node current = Root;
            count = 0;

            while (true)
            {
                count++;

                if (current == _nyt)
                {
                    count--;
                    return null;
                }
                    

                if (current.IsLeaf())
                {
                    count--;
                    return current.Word;
                }

                var bit = code[index++];

                if (!bit)
                    current = current.Left;
                else if (bit)
                    current = current.Right;
            }
        }

        private Node AddToNYT(string word)
        {
            var node = new Node(_nyt, word)
            {
                Number = _nextNum
            };
            _nyt.Right = node;
            _nodes.Add(node);
            _nextNum++;

            var nyt = new Node(_nyt)
            {
                Number = _nextNum,
                IsNYT = true
            };
            _nyt.IsNYT = false;
            _nyt.Left = nyt;
            _nodes.Add(nyt);
            _nextNum++;

            _nyt = nyt;

            return node;
        }

        private void UpdateAll(Node node)
        {
            while (node != null)
            {
                Update(node);
                node = node.Parent;
            }
        }

        private void Update(Node node)
        {
            Node toReplace = NodeToReplace(node.Number, node.Weight);

            if (toReplace != null && node.Parent != toReplace)
                Replace(node, toReplace);

            node.Weight++;
        }

        private Node NodeToReplace(int startIndex, int weight)
        {
            ///////////////////////////////////////
            startIndex--;
            Node found = null;

            for (int i = startIndex; i >= 0; i--)
                if (_nodes[i].Weight == weight)
                    found = _nodes[i];

            return found;
        }

        private void Replace(Node a, Node b)
        {
            ReplaceNumbers(a, b);
            ReplaceSons(a, b);
        }

        private void ReplaceNumbers(Node a, Node b)
        {
            Node temp = _nodes[a.Number];
            _nodes[a.Number] = _nodes[b.Number];
            _nodes[b.Number] = temp;

            int tempNum = a.Number;
            a.Number = b.Number;
            b.Number = tempNum;
        }

        private void ReplaceSons(Node a, Node b)
        {
            bool bIsLeftSon = b.Parent.IsLeftSon(b);

            if (a.Parent.IsLeftSon(a))
                a.Parent.Left = b;
            else
                a.Parent.Right = b;

            Node temp = b.Parent;
            b.Parent = a.Parent;
            a.Parent = temp;

            if (bIsLeftSon)
                temp.Left = a;
            else
                temp.Right = a;
        }
    }
}
