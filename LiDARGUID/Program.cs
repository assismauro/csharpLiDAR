// Decompiled with JetBrains decompiler
// Type: UpdateLASReaderFiles.Program
// Assembly: UpdateLASHeaderValues, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B71B2CA7-7A3F-4C08-BD14-0476F7D4B26C
// Assembly location: E:\temp\UpdateLASHeaderValues\UpdateLASHeaderValues.exe

using CommandLine;
using CommandLine.Text;
using LiDARFileStuff;
using System;
using System.IO;

namespace UpdateLASHeaderFiles
{
    internal class Program
    {
        private static readonly HeadingInfo _headingInfo = new HeadingInfo("LiDARGUID", "0.9");
        private static Program.Options options = new Program.Options();

        private static void Main(string[] args)
        {
            Program.options = new Program.Options();
            try
            {
                if (!Parser.Default.ParseArguments(args, (object)Program.options))
                    Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            if (!File.Exists(Program.options.InputFileName))
            {
                Console.WriteLine(string.Format("ERROR: file {0} doesn´t exists", (object)Program.options.InputFileName));
                Environment.Exit(1);
            }
            LiDARFile liDarFile = (LiDARFile)null;
            try
            {
                liDarFile = new LiDARFile(Program.options.InputFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error opening file {0}: {1}", (object)Program.options.InputFileName, (object)ex.Message));
                Environment.Exit(2);
            }
            if (Program.options.GUID != null)
                liDarFile.GUID = !(Program.options.GUID.ToLower() == "generate") ? Program.options.GUID : Guid.NewGuid().ToString();
            if ((uint)Program.options.FileSourceID > 0U)
                liDarFile.FileSourceID = (ushort)Program.options.FileSourceID;
            liDarFile.Modified = true;
            liDarFile.Close();
        }

        private sealed class Options
        {
            [Option('i', "inputfilename", HelpText = "LAS file to be processed.", Required = true)]
            public string InputFileName { get; set; }

            [Option('g', "guid", DefaultValue = "", HelpText = "Set a new GID.")]
            public string GUID { get; set; }

            [Option('d', "forcediscrete", DefaultValue = false, HelpText = "Force FWF file as discrete.")]
            public bool ForceDiscrete { get; set; }

            [Option('s', "filesourceid", DefaultValue = 0, HelpText = "Set a new source id.")]
            public int FileSourceID { get; set; }

            [ParserState]
            public IParserState LastParserState { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                HelpText helpText1 = new HelpText();
                helpText1.Heading = (string)Program._headingInfo;
                helpText1.Copyright = (string)new CopyrightInfo("Projeto EBA (assismauro@hotmail.com)", 2016);
                int num1 = 1;
                helpText1.AdditionalNewLineAfterOption = num1 != 0;
                int num2 = 1;
                helpText1.AddDashesToOption = num2 != 0;
                HelpText helpText2 = helpText1;
                helpText2.AddOptions((object)this);
                return (string)helpText2;
            }
        }
    }
}
