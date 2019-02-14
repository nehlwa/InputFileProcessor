using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace FileProcessor
{
    public class TextFileProcessor
    {
        public static IEnumerable<string> GetRawData(string filePath)
        {
            IEnumerable<string> data = null;
            try
            {
               data = File.ReadLines(filePath)
                              .Where(row => !string.IsNullOrEmpty(row))
                              .Where(row => !row.StartsWith("#"));
            }
            catch (FileNotFoundException ex)
            {
                data = null;
                new ConsoleOutPut().Error("ERROR:Invalid File Path.");
            }
            return data;
        }
    }
}