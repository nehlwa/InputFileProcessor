using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace FileProcessor
{
    public class TextFileRecordValidator
    {
        static int RowNo;
        bool result;
        string errorMessage;

        public TextFileRecordValidator()
        {
            RowNo = 0;
            result = true;
        }
     
        public bool AreValidFileRecords(string header, IEnumerable<string> rows, string delimiter, out string errorMessage)
        {
            var fileHeader = header.Split(Convert.ToChar(delimiter));
            errorMessage = "Error processing file:";
            foreach (var row in rows)
            {
                RowNo++;
                var splittedrow = row.Split(Convert.ToChar(delimiter));
                  for (var i= 0; i<fileHeader.Count();i++)
                  {
                    switch(fileHeader[i])
                    {
                        case "Project":
                            IsValidProject(splittedrow[i].ToString());
                            break;
                        case "Description":
                            IsValidDescription(splittedrow[i].ToString());
                            break;
                        case "Start date":
                            IsValidStartDate(splittedrow[i].ToString());
                            break;
                        case "Category":
                            IsValidCategory(splittedrow[i].ToString());
                            break;
                        case "Responsible":
                            IsValidResponsible(splittedrow[i].ToString());
                            break;
                        case "Savings amount":
                            IsValidSavingsAmount(splittedrow[i].ToString());
                            break;
                        case "Currency":
                            IsValidCurrency(splittedrow[i].ToString());
                            break;
                        case "Complexity":
                            IsValidComplexity(splittedrow[i].ToString());
                            break;
                     }
                  }
               }
            return result;
         }

        public bool IsValidProject(string ProjectId)
        {
            if (string.IsNullOrEmpty(ProjectId) || ProjectId.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Project ID can not be null";
                result = false;
                return result;
            }
            if (!int.TryParse(ProjectId, out int projectId))
            {
                errorMessage += "\nRecord: " + RowNo + ": Project ID must be a numeric number.";
                result = false;
            }
            return result;
        }

        public bool IsValidDescription(string Description)
        {
            if (string.IsNullOrEmpty(Description) || Description.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Description can not be null";
                result = false;
            }
            return result;
        }

        public bool IsValidStartDate(string startDate)
        {
            if (string.IsNullOrEmpty(startDate.ToString()) || startDate.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Start Date can not be null";
                result = false;
            }
            else
            {
                var formatAllowed = "yyyy-MM-dd hh:mm:ss.fff";
                if (!DateTime.TryParseExact(startDate, formatAllowed, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datevalue))
                {
                    errorMessage += "\nRecord: " + RowNo + ": Start Date must be in format- " + formatAllowed;
                    result = false;
                }
            }
            return result;
        }

        public bool IsValidCategory(string Category)
        {
            if (string.IsNullOrEmpty(Category) || Category.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Category can not be null";
                result = false;
            }
            return result;
        }

        public bool IsValidResponsible(string Responsible)
        {
            if (string.IsNullOrEmpty(Responsible) || Responsible.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Responsible can not be null";
                result = false;
            }
            return result;
        }

        public bool IsValidSavingsAmount(string SavingsAmount)
        {
            if (!(string.IsNullOrEmpty(SavingsAmount) || SavingsAmount.ToUpper() == "NULL"))
            {
                if (Decimal.TryParse(SavingsAmount, out decimal savingsAmount))
                {
                    if (SavingsAmount.Substring(SavingsAmount.IndexOf('.') + 1).Length == 6)
                    {
                        return result;
                    }
                }
                errorMessage += "\nRecord: " + RowNo + ": Savings Amount must be decimal with 6 digits of precision.";
                result = false;
            }
            return result;
        }

        public static bool IsValidCurrency(string currency)
        {
            return true;
        }

        public bool IsValidComplexity(string complexity)
        {
            if (string.IsNullOrEmpty(complexity) || complexity.ToUpper() == "NULL")
            {
                errorMessage += "\nRecord: " + RowNo + ": Complexity can not be null";
                result = false;
            }
            else
            {
                var complexityRange = ConfigurationManager.AppSettings["Complexity"].ToString().Split(',').Select(s => s.Trim());
                if (!complexityRange.Contains(complexity))
                {
                    errorMessage += "\nRecord: " + RowNo + ": Invalid Complexity value provided- " + complexity;
                    result = false;
                }
            }
            return result;
        }

    }
    }
