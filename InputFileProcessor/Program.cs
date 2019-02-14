using System;
using log4net;
using CommandLine;
using CommandLine.Text;
using static System.Console;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FileProcessor
{
    public class Program
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal static string checkEnv = "";

        public static void Main(string[] args)
        {
            checkEnv = "Dev";
            var parserResult = new Parser(settings =>
            {
                settings.CaseSensitive = false;
                settings.IgnoreUnknownArguments = true;
            }).ParseArguments<CommandOptions>(args);
            parserResult.WithNotParsed(errs =>
            {
                var helpText = HelpText.AutoBuild(parserResult, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    return HelpText.DefaultParsingErrorsHandler(parserResult, h);
                }, e =>
                {
                    return e;
                });
                log.Error("Invalid command line arguments. Terminating application.");
                WriteLine(helpText);
                ReadLine();
                Environment.Exit(-1);
            });
            parserResult.WithParsed(Execute);
            ReadLine();
        }

        private static void Execute(CommandOptions options)
        {
            var rawData = TextFileProcessor.GetRawData(options.filePath);
            var columns = rawData.First();
            var rows = rawData.Skip(1).ToList();
            var parsedRows = TextFileParser.ValidateandParse(columns, rows);
            var outPutRows = TextFileParser.FilterandSort(columns, parsedRows, options);
            new ConsoleOutPut().output(outPutRows);
        }

    }
}

