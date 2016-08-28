using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using LiDARFileStuff;

namespace LiDARGUID
{
    class Program
    {
        private static readonly HeadingInfo _headingInfo = new HeadingInfo("LiDARGUID", "0.9");
        private sealed class Options
        {
            [Option('i', "inputfilename", Required = true, HelpText = "LAS file to be processed.")]
            public string InputFileName {get; set; }

            [Option('s', "setguid", HelpText = "Set a new GID.")]
            public bool SetGUID { get; set; }

            [ParserState]
            public IParserState LastParserState { get; set; }

            /*
            [HelpOption]

            public string GetUsage()
            {
                return HelpText.AutoBuild(this,
                  (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
            */
            [HelpOption]
            public string GetUsage()
            {
                var help = new HelpText
                {
                    Heading = _headingInfo,
                    Copyright = new CopyrightInfo("Mauro Assis (assismauro@hotmail.com)", 2016),
                    AdditionalNewLineAfterOption = true,
                    AddDashesToOption = true
                };
                help.AddOptions(this);
                return help;
            }
        }

        static Options options = new Options();
        static void Main(string[] args)
        {
            options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                Environment.Exit(1);
            if(!System.IO.File.Exists(options.InputFileName))
            {
                Console.WriteLine(string.Format("ERROR: file {0} doesn´t exists", options.InputFileName));
                Environment.Exit(1);
            }
            try
            {
                LiDARFile lf = new LiDARFile(options.InputFileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Error opening file {0}: {1}", options.InputFileName, ex.Message));
                Environment.Exit(2);
            }
        }
    }
}
