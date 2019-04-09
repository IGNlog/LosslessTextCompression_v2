using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuffmanAlgoClient
{
    public partial class Options : Form
    {
        public int CountWordsInText { get; set; }

        public Options()
        {
            InitializeComponent();
            CountWordsInText = 0;
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            openFileDialogTextSize.Multiselect = false;
            openFileDialogTextSize.ShowDialog();

            if (openFileDialogTextSize.FileName != "")
            {
                textBoxInput.Text = @openFileDialogTextSize.FileName;
                CountWordsInText = GetSizeTextInWords(@openFileDialogTextSize.FileName);
                textBoxCountWordsInInput.Text = CountWordsInText.ToString();
                buttonTextSize.Enabled = true;
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
                while (indexEnd < text.Length &&
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

        private void WriteTextInFile(string text, string fileNameOutput)
        {
            File.WriteAllText(@fileNameOutput, text, Encoding.Default);
        }

        private string GetTextFixSize(string fileNameInput, int countWords)
        {
            string textAll = "";
            string textFixSize = "";
            StreamReader fileRead = new StreamReader(@fileNameInput, Encoding.Default);
            textAll = fileRead.ReadToEnd();
            int indexBegin = 0;
            int indexEnd = 0;
            for (int i = 0; i < countWords; i++)
            {
                while (indexEnd < textAll.Length &&
                        ((textAll[indexEnd] >= 'a' && textAll[indexEnd] <= 'z') ||
                         (textAll[indexEnd] >= 'A' && textAll[indexEnd] <= 'Z')) ||
                        ((textAll[indexEnd] >= 'а' && textAll[indexEnd] <= 'я') ||
                         (textAll[indexEnd] >= 'А' && textAll[indexEnd] <= 'Я')))
                {
                    indexEnd++;
                }
                if (indexBegin == indexEnd)
                {
                    indexEnd++;
                }
                indexBegin = indexEnd;
            }
            textFixSize = textAll.Substring(0, indexEnd);
            fileRead.Close();
            return textFixSize;
        }

        private void buttonTextSize_Click(object sender, EventArgs e)
        {
            if (comboBoxTextSize.SelectedItem.ToString() != "")
            {
                int countWords = int.Parse(comboBoxTextSize.SelectedItem.ToString());
                string textFixSize = GetTextFixSize(textBoxInput.Text, countWords);
                WriteTextInFile(textFixSize, @textBoxOutC.Text);
                buttonTextSize.Enabled = false;
            }
        }

        private void comboBoxTextSize_SelectedValueChanged(object sender, EventArgs e)
        {
            if (openFileDialogTextSize.FileName != "")
            {
                //textBoxOutC.Text = @Path.GetDirectoryName(openFileDialogTextSize.FileName) +
                //    @"\" + "CountWord_" + int.Parse(comboBoxTextSize.SelectedItem.ToString()).ToString()
                //    + "_" + @Path.GetFileName(openFileDialogTextSize.FileName);
                textBoxOutC.Text = @Path.GetDirectoryName(openFileDialogTextSize.FileName) +
                    @"\" + @Path.GetFileNameWithoutExtension(openFileDialogTextSize.FileName)+ "_" + 
                    "CountWord_" + int.Parse(comboBoxTextSize.SelectedItem.ToString()).ToString()
                     + @Path.GetExtension(openFileDialogTextSize.FileName);
                buttonTextSize.Enabled = true;
            }
        }
    }
}
