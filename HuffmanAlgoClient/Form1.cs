﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuffmanCode;
using HuffmanAdaptiveCode;

namespace HuffmanAlgoClient
{
    public partial class Form1 : Form
    {
        public long SizeFileInput { get; set; }
        public long SizeFileEncodeC { get; set; }
        public long SizeFileEncodeA { get; set; }
        public long SizeDictionaryC { get; set; }

        public Form1()
        {
            InitializeComponent();
            groupBoxAdaptiveHuffmanAlgo.Enabled = false;
            groupBoxClassicHuffmanAlgo.Enabled = false;
            buttonInput.Enabled = true;
            buttonRun.Enabled = true;
            textBoxAllQuantityWords.Enabled = false;
            labelAllQuantityWords.Enabled = false;

            //Пока Так
            //textBoxBlock.Text = "-";
            //textBoxBlock.Enabled = false;
            //checkBoxBlock.Enabled = false;
            //labelBlock.Enabled = false;

        }

        private void groupBoxAdaptiveHuffmanAlgo_Enter(object sender, EventArgs e)
        {

        }

        private void labelSizeCompressС_Click(object sender, EventArgs e)
        {

        }

        private void labelCoefficientCompressС_Click(object sender, EventArgs e)
        {

        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            groupBoxAdaptiveHuffmanAlgo.Enabled = false;
            groupBoxClassicHuffmanAlgo.Enabled = false;
            openFileDialogInput.Filter = "Кодируемый файл (*.*)|*.*|Декодируемый файл (*.haf)|*.haf";
            openFileDialogInput.Multiselect = false;
            openFileDialogInput.ShowDialog();
            if (openFileDialogInput.FileName != "")
            {
                textBoxInput.Text = @openFileDialogInput.FileName;
                textBoxOutC.Text = @Path.GetDirectoryName(@openFileDialogInput.FileName) + @"\" +
                    @"EncodeClassicAlgo" + @Path.GetFileName(@openFileDialogInput.FileName);
                textBoxOutA.Text = @Path.GetDirectoryName(@openFileDialogInput.FileName) + @"\" +
                    @"EncodeAdaptiveAlgo" + @Path.GetFileName(@openFileDialogInput.FileName);
                SizeFileInput = new FileInfo(@openFileDialogInput.FileName).Length;

                textBoxSizeSourceA.Text = textBoxSizeSourceС.Text = SizeFileInput.ToString() + "  байт";

                textBoxDecodeFileC.Text = @Path.GetDirectoryName(@openFileDialogInput.FileName) + @"\" +
                    @"DecodeClassicAlgo" + @Path.GetFileName(@openFileDialogInput.FileName);
                textBoxDecodeFileA.Text = @Path.GetDirectoryName(@openFileDialogInput.FileName) + @"\" +
                    @"DecodeAdaptiveAlgo" + @Path.GetFileName(@openFileDialogInput.FileName);

                textBoxAllQuantityWords.Text = GetSizeTextInWords(@openFileDialogInput.FileName).ToString();

                labelOutС.Enabled = true;
                textBoxOutC.Enabled = true;
                labelDecodeFileC.Enabled = true;
                textBoxDecodeFileC.Enabled = true;
                textBoxAllQuantityWords.Enabled = true;
                labelAllQuantityWords.Enabled = true;
            }

            
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            //Сначала для классического алгритма
            if (textBoxOutC.Text != "")
            {
                labelOutС.Enabled = true;
                textBoxOutC.Enabled = true;
                //получаем частотный словарь
                FrequencyDictionary frequencyDictionary = new FrequencyDictionary(openFileDialogInput.FileName);
                SizeDictionaryC = frequencyDictionary.CountWord;
                textBoxSizeDictionaryС.Text = SizeDictionaryC.ToString() + "  слов";

                //построение дерева
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                HuffmanTree huffmanTree = new HuffmanTree(frequencyDictionary);
                startTime.Stop();
                var resultTime = startTime.Elapsed.TotalSeconds;

                //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                //        resultTime.Hours,
                //        resultTime.Minutes,
                //        resultTime.Seconds,
                //        resultTime.Milliseconds);
                textBoxTimeBuildTree.Text = resultTime.ToString();

                //Кодирование
                startTime = System.Diagnostics.Stopwatch.StartNew();
                huffmanTree.Encode(@openFileDialogInput.FileName, @textBoxOutC.Text);
                startTime.Stop();
                resultTime = resultTime + startTime.Elapsed.TotalSeconds;
                //elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                //        resultTime.Hours,
                //        resultTime.Minutes,
                //        resultTime.Seconds,
                //        resultTime.Milliseconds);
                textBoxSpeedEncodeС.Text = resultTime.ToString();

                //сбрасываем дерево
                huffmanTree = new HuffmanTree();

                //Декодируем
                startTime = System.Diagnostics.Stopwatch.StartNew();
                huffmanTree.Decode(@textBoxOutC.Text, @textBoxDecodeFileC.Text);
                startTime.Stop();
                resultTime = startTime.Elapsed.TotalSeconds;
                //elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                //        resultTime.Hours,
                //        resultTime.Minutes,
                //        resultTime.Seconds,
                //        resultTime.Milliseconds);
                textBoxSpeedDecodeС.Text = resultTime.ToString();

                //Получаем другие статитстики 
                SizeFileEncodeC = new FileInfo(@textBoxOutC.Text).Length;
                textBoxSizeCompressС.Text = SizeFileEncodeC.ToString() + "  байт";

                textBoxCoefficientCompressС.Text = ((SizeFileInput * 1.0) / SizeFileEncodeC).ToString();

                groupBoxClassicHuffmanAlgo.Enabled = true;
            }

            //Потом для адаптивного
            if (textBoxOutA.Text != "")
            {
                labelOutA.Enabled = true;
                textBoxOutA.Enabled = true;

                HuffmanAdaptiveTree huffmanAdaptiveTree = new HuffmanAdaptiveTree();
                //Кодирование
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                huffmanAdaptiveTree.Encode(@openFileDialogInput.FileName, @textBoxOutA.Text);
                startTime.Stop();
                var resultTime = startTime.Elapsed.TotalSeconds;
                //var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                //        resultTime.Hours,
                //        resultTime.Minutes,
                //        resultTime.Seconds,
                //        resultTime.Milliseconds);
                textBoxSpeedEncodeA.Text = resultTime.ToString();

                //сбрасываем дерево
                huffmanAdaptiveTree = new HuffmanAdaptiveTree();
                huffmanAdaptiveTree.Reset();

                //Декодируем
                startTime = System.Diagnostics.Stopwatch.StartNew();
                huffmanAdaptiveTree.Decode(@textBoxOutA.Text, @textBoxDecodeFileA.Text);
                startTime.Stop();
                resultTime = startTime.Elapsed.TotalSeconds;
                //elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                //        resultTime.Hours,
                //        resultTime.Minutes,
                //        resultTime.Seconds,
                //        resultTime.Milliseconds);
                textBoxSpeedDecodeA.Text = resultTime.ToString();

                //Получаем другие статитстики 
                SizeFileEncodeA = new FileInfo(@textBoxOutA.Text).Length;
                textBoxSizeCompressA.Text = SizeFileEncodeA.ToString() + "  байт";

                textBoxCoefficientCompressA.Text = ((SizeFileInput * 1.0) / SizeFileEncodeA).ToString();

                groupBoxAdaptiveHuffmanAlgo.Enabled = true;
            }

        }

        private int GetSizeTextInWords(string fileName)
        {
            int sizeText = 0;
            StreamReader fileRead = new StreamReader(@fileName, Encoding.Default);
            string text = fileRead.ReadToEnd();
            int indexBegin = 0;
            int indexEnd = 0;
            while (indexEnd < text.Length)
            {
                while ((indexEnd < text.Length) &&
                        ((text[indexEnd] >= 'a' && text[indexEnd] <= 'z') ||
                         (text[indexEnd] >= 'A' && text[indexEnd] <= 'Z')) ||
                        ((text[indexEnd] >= 'а' && text[indexEnd] <= 'я') ||
                         (text[indexEnd] >= 'А' && text[indexEnd] <= 'Я')))
                {
                    indexEnd++;
                }
                //это знак препенания, пробел или знак табуляции
                if (indexBegin == indexEnd)
                {
                    indexEnd++;
                }
                indexBegin = indexEnd;
                sizeText++;
            }
            fileRead.Close();
            return sizeText;
        }

        private void buttonOption_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
    }
}
