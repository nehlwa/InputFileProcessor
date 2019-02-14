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
    public class TextFileRecordValidatorTests
    {
        [TestMethod()]
        public void AreValidFileRecordsTest_ForValidRecords()
        {
            //Arrange
            var rawData = File.ReadLines("InputFile.txt")
                         .Where(row => !string.IsNullOrEmpty(row))
                         .Where(row =>!row.StartsWith("#"));
            var columns = rawData.First();
            var rows = rawData.Skip(1);

            //Act
            var actual = new TextFileRecordValidator().AreValidFileRecords(columns, rows, "\t", out string errorMessage);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void AreValidFileRecordsTest_ForInValidRecords()
        {
            //Arrange
            var rawData = File.ReadLines("InputFileWithInvalidRecords.txt")
                         .Where(row => !string.IsNullOrEmpty(row))
                         .Where(row => !row.StartsWith("#"));
            var columns = rawData.First();
            var rows = rawData.Skip(1);

            //Act
            var actual = new TextFileRecordValidator().AreValidFileRecords(columns, rows, "\t", out string errorMessage);

            //Assert
            Assert.IsFalse(actual);
        }


        [TestMethod()]
        public void IsValidProjectTest_ForValidProjectID()
        {
            //Arrange
            string ProjectId = "0";
            
            //Act
            var actual = new TextFileRecordValidator().IsValidProject(ProjectId.ToString());

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidProjectTest_ForInValidProjectID()
        {
            //Arrange
            string ProjectId = "";

            //Act
            var actual = new TextFileRecordValidator().IsValidProject(ProjectId.ToString());

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void IsValidDescriptionTest_ForValidDescription()
        {
            //Arrange
            var description = "Project Description";

            //Act
            var actual = new TextFileRecordValidator().IsValidDescription(description);

            //Assert
            Assert.IsTrue(actual);

        }

        [TestMethod()]
        public void IsValidDescriptionTest_ForInvalidDescription()
        {
            //Arrange
            var description = "";

            //Act
            var actual = new TextFileRecordValidator().IsValidDescription(description);

            //Assert
            Assert.IsFalse(actual);

        }

        [TestMethod()]
        public void IsValidStartDateTest_ForValidStartDate()
        {
            //Arrange
            var startDate = "2019-01-13 00:00:00.000";
            
            //Act
            var actual = new TextFileRecordValidator().IsValidStartDate(startDate);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidStartDateTest_ForInvalidStartDate()
        {
            //Arrange
            var startDate = "2019-01-13 00:00:00";

            //Act
            var actual = new TextFileRecordValidator().IsValidStartDate(startDate);

            //Assert
            Assert.IsFalse(actual);
        }


        [TestMethod()]
        public void IsValidCategoryTest_ForValidCategory()
        {
            //Arrange
            var category = "Project Category";

            //Act
            var actual = new TextFileRecordValidator().IsValidCategory(category);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidCategoryTest_ForInvalidCategory()
        {
            //Arrange
            var category = "";

            //Act
            var actual = new TextFileRecordValidator().IsValidCategory(category);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void IsValidResponsibleTest_ForValidResponsible()
        {
            //Arrange
            var responsible = "Project Responsible";

            //Act
            var actual = new TextFileRecordValidator().IsValidResponsible(responsible);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidResponsibleTest_ForInvalidResponsible()
        {
            //Arrange
            var responsible = "";

            //Act
            var actual = new TextFileRecordValidator().IsValidResponsible(responsible);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void IsValidSavingsAmountTest_ForValidValue()
        {
            //Arrange
            var savingsAmount = "123.670000";

            //Act
            var actual = new TextFileRecordValidator().IsValidSavingsAmount(savingsAmount);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidSavingsAmountTest_ForInvalidValue()
        {
            //Arrange
            var savingsAmount = "123.670000K";

            //Act
            var actual = new TextFileRecordValidator().IsValidSavingsAmount(savingsAmount);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void IsValidComplexityTest_ForValidValue()
        {
            //Arrange
            var complexity = "Simple";

            //Act
            var actual = new TextFileRecordValidator().IsValidComplexity(complexity);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void IsValidComplexityTest_ForInvalidValue()
        {
            //Arrange
            var complexity = "Simple1";

            //Act
            var actual = new TextFileRecordValidator().IsValidComplexity(complexity);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void IsValidCurrencyTest_ForValidValue()
        {
            //Arrange
            var currency = "EUR";

            //Act
            var actual = TextFileRecordValidator.IsValidCurrency(currency);

            //Assert
            Assert.IsTrue(actual);
        }

    }
}