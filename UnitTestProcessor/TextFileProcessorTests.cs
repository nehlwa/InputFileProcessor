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
    public class TextFileProcessorTests
    {

        internal static string checkEnv = "Test";

        [TestMethod()]
        public void GetRawDataTest()
        {
            //Arrange
            var expected = File.ReadLines("InputFile.txt")
                           .Where(row => !string.IsNullOrEmpty(row))
                           .Where(row => !row.StartsWith("#"));
            //Act
            var actual = TextFileProcessor.GetRawData("InputFile.txt");
            //Assert
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }

        [TestMethod()]
        public void GetRawDataFilenotExistTest()
        {
            var actual = TextFileProcessor.GetRawData("NotAFile.txt");
            Assert.AreEqual(null, actual);
        }
    }
}