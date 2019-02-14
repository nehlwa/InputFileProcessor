using CommandLine;
using CommandLine.Text;

namespace FileProcessor
{
    public class CommandOptions
    {
        [Option('f', "file", Required = true, HelpText = "Input File to be processed.")]
        public string filePath { get; set; }

        [Option('s', "sortByStartDate", Default = false, HelpText = "Display File Data sort by start date")]
        public bool sortByStartDate { get; set; }

        [Option('p', "project", Required = false, HelpText = "Filters project data by Project Id")]
        public string ProjectId { get; set; }
    }
}