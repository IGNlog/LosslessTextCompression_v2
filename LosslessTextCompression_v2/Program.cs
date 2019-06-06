using HuffmanAdaptiveCode;
using HuffmanCode;
using LosslessTextCompression_v2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace LosslessTextCompression_v2
{
    class Program
    {

        static void Main(string[] args)
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Text\\Text";

            int[] arrSizeTexts = { 100, 500, 1000, 5000, 10000, 15000, 20000, 30000, 50000, 100000, 150000, 200000, 300000, 400000, 500000 };
            //Dictionary<int, List<List<double>>> resultExperement = new Dictionary<int, List<List<double>>>();
            //string path = "D:\\Text\\BlockTexts";
            //foreach (var sizeTextFragment in arrSizeTexts)
            //{
            //    string fileNameText = path + "\\1.en" + sizeTextFragment.ToString() + "_ru" + sizeTextFragment.ToString() + ".txt";
            //    string fileNameDictionary = path + "\\1.en" + sizeTextFragment.ToString() + "_ru" + sizeTextFragment.ToString() + ".dicry.txt";
            //    string fileNameEncodeText = path + "\\1.en" + sizeTextFragment.ToString() + "_ru" + sizeTextFragment.ToString() + ".encodetext.bin";
            //    //string fileNameDecodeText = path + "\\1.en10000_ru10000.decodetext.txt";
            //    string fileNameMetricsText = path + "\\1.en" + sizeTextFragment.ToString() + "_ru" + sizeTextFragment.ToString() + ".metrics.txt";
            //    HuffmanAdaptiveAlgoBlock huffmanAdaptiveAlgoBlock = new HuffmanAdaptiveAlgoBlock();
            //    resultExperement.Add(sizeTextFragment,
            //        huffmanAdaptiveAlgoBlock.EncodeBlockWithMetrics(@fileNameText, @fileNameEncodeText, sizeTextFragment >> 1, @fileNameMetricsText));
            //    Console.WriteLine(sizeTextFragment.ToString());
            //}

            //WriteResultsInExcel(path + "\\1.xlsx", resultExperement);
            string path = "D:\\Text\\TT";
            string fileNameText = path + "\\1.tt" + ".txt";
            string fileNameEncodeText = path + "\\1.tt.encodetext.bin";
            string fileNameDecodeText = path + "\\1.tt.decodetext.txt";
            string fileNameMetricsText = path + "\\1.tt.metrics.txt";
            HuffmanAdaptiveAlgoBlock huffmanAdaptiveAlgoBlock = new HuffmanAdaptiveAlgoBlock();
            huffmanAdaptiveAlgoBlock.EncodeBlockWithMetrics(@fileNameText, @fileNameEncodeText, 3, @fileNameMetricsText);
            huffmanAdaptiveAlgoBlock.DecodeBlock(fileNameEncodeText, fileNameDecodeText);
        }

        public static void WriteResultsInExcel(string FileName, Dictionary<int, List<List<double>>> resultExperement)
        {
            int[] arrSizeTexts = { 100, 500, 1000, 5000, 10000, 15000, 20000, 30000, 50000, 100000, 150000, 200000, 300000, 400000, 500000 };
            Excel.Application excel_app = new Excel.Application();

            // Сделать Excel видимым (необязательно).
            excel_app.Visible = false;

            // Откройте книгу.
            Excel.Workbook workbook = excel_app.Workbooks.Open(
                "D:\\Text\\BlockTexts\\1.xlsx",
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            // Посмотрим, существует ли рабочий лист.
            string sheet_name = DateTime.Now.ToString("MM-dd-yy");

            Excel.Worksheet sheet = null;
            foreach (Excel.Worksheet s in workbook.Sheets)
            {
                if (s.Name == sheet_name) sheet = s;
            }

            if (sheet == null)
            {
                // Добавить лист в конце.
                sheet = (Excel.Worksheet)workbook.Sheets.Add(
                    Type.Missing, workbook.Sheets[workbook.Sheets.Count],
                    1, Excel.XlSheetType.xlWorksheet);
                sheet.Name = DateTime.Now.ToString("MM-dd-yy");
            }

            // Добавить некоторые данные в отдельные ячейки.
            sheet.Cells[1, 1] = "Расстояния между английскими блоками текста";
            sheet.Cells[2, 2] = "Размер фрагмента русс текста";
            sheet.Cells[2, 3] = "Размер фрагмента англ текста";
            sheet.Cells[2, 4] = "Размер блока";
            sheet.Cells[2, 5] = "Расстояние Евклида";
            sheet.Cells[2, 6] = "Расстояние Чебышева";
            sheet.Cells[2, 7] = "Расстояние городских кварталов";
            for (int i = 1; i <= arrSizeTexts.Length; i++)
            {
                sheet.Cells[2 + i, 1] = i;
                sheet.Cells[2 + i, 2] = arrSizeTexts[i - 1];
                sheet.Cells[2 + i, 3] = arrSizeTexts[i - 1];
                sheet.Cells[2 + i, 4] = arrSizeTexts[i - 1] >> 1;
                //Евклид
                sheet.Cells[2 + i, 5] = resultExperement[arrSizeTexts[i - 1]][0][1];
                //Чебышев
                sheet.Cells[2 + i, 6] = resultExperement[arrSizeTexts[i - 1]][1][1];
                //Городских кварталов
                sheet.Cells[2 + i, 7] = resultExperement[arrSizeTexts[i - 1]][2][1];
            }

            sheet.Cells[1 + 19, 1] = "Расстояния между русскими блоками текста";
            sheet.Cells[2 + 19, 2] = "Размер фрагмента русс текста";
            sheet.Cells[2 + 19, 3] = "Размер фрагмента англ текста";
            sheet.Cells[2 + 19, 4] = "Размер блока";
            sheet.Cells[2 + 19, 5] = "Расстояние Евклида";
            sheet.Cells[2 + 19, 6] = "Расстояние Чебышева";
            sheet.Cells[2 + 19, 7] = "Расстояние городских кварталов";
            for (int i = 1; i <= arrSizeTexts.Length; i++)
            {
                sheet.Cells[2 + 19 + i, 1] = i;
                sheet.Cells[2 + 19 + i, 2] = sheet.Cells[2 + 19 + i, 3] = arrSizeTexts[i - 1];
                sheet.Cells[2 + 19 + i, 4] = arrSizeTexts[i - 1] >> 1;
                //Евклид
                sheet.Cells[2 + 19 + i, 5] = resultExperement[arrSizeTexts[i - 1]][0][3];
                //Чебышев
                sheet.Cells[2 + 19 + i, 6] = resultExperement[arrSizeTexts[i - 1]][1][3];
                //Городских кварталов
                sheet.Cells[2 + 19 + i, 7] = resultExperement[arrSizeTexts[i - 1]][2][3];
            }

            sheet.Cells[1 + 19 * 2, 1] = "Расстояния между английским блоком и русским блоком текста";
            sheet.Cells[2 + 19 * 2, 2] = "Размер фрагмента русс текста";
            sheet.Cells[2 + 19 * 2, 3] = "Размер фрагмента англ текста";
            sheet.Cells[2 + 19 * 2, 4] = "Размер блока";
            sheet.Cells[2 + 19 * 2, 5] = "Расстояние Евклида";
            sheet.Cells[2 + 19 * 2, 6] = "Расстояние Чебышева";
            sheet.Cells[2 + 19 * 2, 7] = "Расстояние городских кварталов";
            for (int i = 1; i <= arrSizeTexts.Length; i++)
            {
                sheet.Cells[2 + 19 * 2 + i, 1] = i;
                sheet.Cells[2 + 19 * 2 + i, 2] = sheet.Cells[2 + 19 * 2 + i, 3] = arrSizeTexts[i - 1];
                sheet.Cells[2 + 19 * 2 + i, 4] = arrSizeTexts[i - 1] >> 1;
                //Евклид
                sheet.Cells[2 + 19 * 2 + i, 5] = resultExperement[arrSizeTexts[i - 1]][0][2];
                //Чебышев
                sheet.Cells[2 + 19 * 2 + i, 6] = resultExperement[arrSizeTexts[i - 1]][1][2];
                //Городских кварталов
                sheet.Cells[2 + 19 * 2 + i, 7] = resultExperement[arrSizeTexts[i - 1]][2][2];
            }


            // Сохраните изменения и закройте книгу.
            workbook.Close(true, Type.Missing, Type.Missing);

            // Закройте сервер Excel.
            excel_app.Quit();
        }
    }
}
