using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace FileProcessor
{
    public class TextFileParser
    {
        public static List<string> ValidateandParse(string columns, List<string> rows)
        {
            var delimiter = GetExpectedDelimiter();
            if (IsValidFileHeader(columns, delimiter, out string errorMessage))
            {
                if (new TextFileRecordValidator().AreValidFileRecords(columns, rows, delimiter, out errorMessage))
                {
                    rows.ForEach(row => row.Replace("null", ""));
                }
                else
                {
                    new ConsoleOutPut().Error(errorMessage);
                }
            }
            else
            {
                new ConsoleOutPut().Error(errorMessage);
            }
           return rows;
        }

        public static bool IsValidFileHeader(string columns, string delimiter, out string errorMessage)
        {
            var headerFound = true;
            var _errorMessage = "Invalid file header: ";
            var headerList = GetExpectedHeaderList();
            
            var headerRow = columns.Split(Convert.ToChar(delimiter)).ToList();
            var missingHeader = headerList.Where(header => !headerRow.Contains(header)).ToList();
            if (missingHeader.Count()>0)
            {
               _errorMessage += "Input file does not contain following header columns- \n";
               missingHeader.ForEach(header => _errorMessage=string.Concat(_errorMessage, header + "\n"));
               headerFound = false;
            }
            else if (headerRow.Except(headerList).Count() > 0)
            {
               _errorMessage += "Input file contains extra header columns.";
               headerFound = false;
            }
            errorMessage = _errorMessage;
            return headerFound;
        }

        public static string FilterandSort(string header, List<string> parsedRows, CommandOptions options)
        {
            var _delimiter = Convert.ToChar(GetExpectedDelimiter());
            var splittedRows = parsedRows.Select(row => row.Split(_delimiter).ToList());
            if (options.sortByStartDate)
            {
                var startDateIndex = header.Split(_delimiter).ToList().FindIndex(x => x.Equals("Start date"));
                splittedRows = from rows in splittedRows
                        orderby rows.ElementAt(startDateIndex)
                        select rows;
            }
            
            if (int.TryParse(options.ProjectId, out int validProjectId) && (!string.IsNullOrEmpty(options.ProjectId)))
            {
                var projectIdIndex = header.Split(_delimiter).ToList().FindIndex(x => x.Equals("Project"));
                splittedRows = from rows in splittedRows
                             where rows[projectIdIndex]==options.ProjectId
                             select rows;
            }
            var outputRows = splittedRows.Select(row=>string.Join(_delimiter.ToString(), row));
            return string.Concat(string.Concat(header, '\n'), string.Join("\n", outputRows));
        }

        private static string GetExpectedDelimiter()
        {
            var delimiter = ConfigurationManager.AppSettings["DelimiterAllowed"];
            if (delimiter.Length == 0)
            {
                new ConsoleOutPut().Error("Error: Recheck configuration settings for delimiter list.");
            }

            return delimiter;
        }

        private static IEnumerable<string> GetExpectedHeaderList()
        {
            var headerList = ConfigurationManager.AppSettings["HeaderList"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                                                       .Where(s => !string.IsNullOrWhiteSpace(s))
                                                                                       .Select(s => s.Trim()).ToList();
            if(headerList.Count()==0)
            {
                new ConsoleOutPut().Error("Error: Recheck configuration settings for header list.");
            }
            return headerList;
        }
    }
}