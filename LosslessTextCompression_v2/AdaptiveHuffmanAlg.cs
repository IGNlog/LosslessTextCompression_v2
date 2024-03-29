﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LosslessTextCompression_v2
{
    public class AdaptiveHuffmanAlg
    {
        public static event Action ProgressEvent;
        private static BinaryReader SourceFile;

        private static void OnNotEnoughCodeEvent(ref string Code)
        {
            Code += AdaptiveHuffmanTree.ToBinaryString(SourceFile.ReadByte());
            ProgressEvent();
        }

        public static bool DecodeFile(string InputPath, string OutputPath)
        {
            bool OpenSuccessfully = true;
            BinaryWriter OutputFile = null;
            try
            {
                SourceFile = new BinaryReader(File.OpenRead(InputPath));
                OutputFile = new BinaryWriter(File.Create(OutputPath));
            }
            catch
            {
                OpenSuccessfully = false;
            }

            if (OpenSuccessfully)
            {
                AdaptiveHuffmanTree DecodingModel = new AdaptiveHuffmanTree();
                DecodingModel.CreateModel();
                DecodingModel.NotEnoughCodeEvent += new AdaptiveHuffmanTree.NotEnoughCodeEventDelegate(OnNotEnoughCodeEvent);
                bool FinishFlag = false;
                Byte? Symbol = 0;
                string Code = "";

                //убираем расширение
                do
                {
                    ProgressEvent();
                }
                while (SourceFile.ReadChar() != '.');

                ProgressEvent();
                Code = AdaptiveHuffmanTree.ToBinaryString(SourceFile.ReadByte());

                while (!FinishFlag)
                {
                    Symbol = DecodingModel.Decode(ref Code);
                    if (Symbol == null) FinishFlag = true;
                    if (!FinishFlag)
                    {
                        OutputFile.Write((Byte)Symbol);
                    }
                }
            }
            SourceFile.Close();
            OutputFile.Close();
            return OpenSuccessfully;
        }

        public static bool EncodeFile(string InputPath, string OutputPath)
        {
            bool OpenSuccessfully = true;
            BinaryWriter OutputFile = null;
            try
            {
                SourceFile = new BinaryReader(File.OpenRead(InputPath));
                OutputFile = new BinaryWriter(File.Create(OutputPath));
            }
            catch
            {
                OpenSuccessfully = false;
            }

            if (OpenSuccessfully)
            {
                AdaptiveHuffmanTree EncodingModel = new AdaptiveHuffmanTree();
                EncodingModel.CreateModel();
                bool EndOfFile = false;
                Byte Symbol = 0;
                string Code;
                string Buffer = "";

                //запись расширения исходного файла
                char[] CharTypeBuffer;
                FileInfo Finfo = new FileInfo(InputPath);
                CharTypeBuffer = (Finfo.Extension.Substring(1, Finfo.Extension.Length - 1) + ".").ToCharArray();
                OutputFile.Write(CharTypeBuffer);

                while (!EndOfFile)
                {
                    ProgressEvent();

                    try
                    {
                        Symbol = SourceFile.ReadByte();
                    }
                    catch
                    {
                        EndOfFile = true;
                    }

                    if (!EndOfFile) Code = EncodingModel.Encode(Symbol);
                    else Code = EncodingModel.Encode(null);
                    Code = Buffer + Code;
                    Buffer = Code.Substring(Code.Length - (Code.Length % 8), (Code.Length) - (Code.Length - (Code.Length % 8)));
                    Code = Code.Remove(Code.Length - (Code.Length % 8), (Code.Length) - (Code.Length - (Code.Length % 8)));
                    while (Code != "")
                    {
                        OutputFile.Write(AdaptiveHuffmanTree.ToByte(Code.Substring(0, 8)));
                        Code = Code.Remove(0, 8);
                    }

                    if (EndOfFile)
                    {
                        while (Buffer.Length != 8) Buffer += "0";
                        OutputFile.Write(AdaptiveHuffmanTree.ToByte(Buffer));
                    }

                }
                SourceFile.Close();
                OutputFile.Close();
            }
            return OpenSuccessfully;
        }


    }
}
