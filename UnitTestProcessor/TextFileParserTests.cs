using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFileProcessor
{
    [TestClass()]
    public class TextFileParserTests
    {
    
        [TestMethod()]
        public void IsValidFileHeaderTest_ForValidHeader()
        {
            //Arrange
            var columns = "Project\tDescription\tStart date\tCategory\tResponsible\tSavings amount\tCurrency\tComplexity";
            var delimiter = "\t";

            //Act
            var actual = TextFileParser.IsValidFileHeader(columns, delimiter, out string errorMessage);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidFileHeaderTest_ForInvalidHeader()
        {
            //Arrange
            var columns = "Project\tDescription\tStart date\tCategory\tResponsible\tSavings amount\tCurrency\tComplexity\tExtra Column";
            var delimiter = "\t";

            //Act
            var actual = TextFileParser.IsValidFileHeader(columns, delimiter, out string errorMessage);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ValidateandParseTest_ForValidData()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var rows = rawData.Skip(1).ToList();
            var expected = File.ReadAllLines("expectedOutputFile.txt").Skip(1)
                           .Where(row => !string.IsNullOrEmpty(row))
                           .Where(row => !row.StartsWith("#")).ToList();

            //Act
            var actualRows = TextFileParser.ValidateandParse(fileHeader, rows);
    
            //Assert
            CollectionAssert.AreEqual(expected, actualRows);
        }

        [TestMethod()]
        public void ValidateandParseTest_ForInvalidData()
        {
            //Arrange
            var rawData = File.ReadLines("InputFileWithInvalidRecords.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var rows = rawData.Skip(1).ToList();
            var expected = File.ReadAllLines("expectedOutputFile.txt").Skip(1)
                           .Where(row => !string.IsNullOrEmpty(row))
                           .Where(row => !row.StartsWith("#")).ToList();

            //Act
            var actualRows = TextFileParser.ValidateandParse(fileHeader, rows);

            //Assert
            CollectionAssert.AreNotEqual(expected, actualRows);
        }

        [TestMethod()]
        public void FilterandSortTest_WithNoFilter()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var parsedRows = TextFileParser.ValidateandParse(fileHeader, rawData.Skip(1).ToList());
            var options = new CommandOptions()
            {
                filePath = "InputFile.txt",
                sortByStartDate = false,
                ProjectId = ""
            };
            var expectedOutput = String.Join("\n", File.ReadAllLines("expectedOutputFile.txt")
                                 .Where(row => !string.IsNullOrEmpty(row))
                                 .Where(row => !row.StartsWith("#")).ToList());

            //Act
            var actualOutput = TextFileParser.FilterandSort(fileHeader, parsedRows, options);
      
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void FilterandSortTest_WithOnlySortByStartDateFilter()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var parsedRows = TextFileParser.ValidateandParse(fileHeader, rawData.Skip(1).ToList());
            var options = new CommandOptions()
            {
                filePath = "InputFile.txt",
                sortByStartDate = true,
                ProjectId = ""
            };
            var expectedOutput = String.Join("\n", File.ReadAllLines("expectedOutputFile_FilterbyStartDate.txt")
                               .Where(row => !string.IsNullOrEmpty(row))
                               .Where(row => !row.StartsWith("#")).ToList());

            //Act
            var actualOutput = TextFileParser.FilterandSort(fileHeader, parsedRows, options);

            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void FilterandSortTest_WithOnlyProjectIdFilter()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var parsedRows = TextFileParser.ValidateandParse(fileHeader, rawData.Skip(1).ToList());
            var options = new CommandOptions()
            {
                filePath = "InputFile.txt",
                sortByStartDate = false,
                ProjectId = "5"
            };
            var expectedOutput = String.Join("\n", File.ReadAllLines("expectedOutputFile_FilterbyProjectId.txt")
                        .Where(row => !string.IsNullOrEmpty(row))
                        .Where(row => !row.StartsWith("#")).ToList());

            //Act
            var actualOutput = TextFileParser.FilterandSort(fileHeader, parsedRows, options);
   
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void FilterandSortTest_WithAllFilters()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                       .Where(row => !string.IsNullOrEmpty(row))
                       .Where(row => !row.StartsWith("#"));
            var fileHeader = rawData.First();
            var parsedRows = TextFileParser.ValidateandParse(fileHeader, rawData.Skip(1).ToList());
            var options = new CommandOptions()
            {
                filePath = "InputFile.txt",
                sortByStartDate = true,
                ProjectId = "5"
            };
             var expectedOutput = String.Join("\n", File.ReadAllLines("expectedOutputFile_FilterByProjectIdStartDate.txt")
                        .Where(row => !string.IsNullOrEmpty(row)).ToList());
            
            //Act
            var actualOutput = TextFileParser.FilterandSort(fileHeader, parsedRows, options);

            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}