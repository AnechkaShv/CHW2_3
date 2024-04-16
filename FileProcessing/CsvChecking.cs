using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FileProcessing
{
    public class CsvChecking
    {
        // The initial path.
        readonly string fPath;
        public string[] ColumnNames {  get; set; }

        private string[][] _values;
        public string[][] Values
        {
            get { return _values; }
        }
        public CsvChecking() { }
        public CsvChecking(string file)
        {

            fPath = file;
        }
        /// <summary>
        /// This method checks correctness of data in the file.
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            // Regular expression for splitting.
            Regex regex = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");

            // Initial names of table columns.
            string[] alphabetEng = { "number", "fullname", "phones", "address", "metrostations" };

            string[] tableRows;
            //Reading all file lines into an array.
            try
            {
                tableRows = File.ReadAllLines(fPath);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Your file name is too long. Please try again.");
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Wrong file path. Please try again");
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("This file can be only read. Please try again.");
                return false;
            }
            catch (IOException)
            {
                Console.WriteLine("Error while writing data in the file. Please try again.");
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong file name.Please try again.");
                return false;
            }

            // Checking data in the file.
            // Number of table rows should be >= 1 because a file must include one columns' name row and at least one row with data.
            if (tableRows is null || tableRows.Length < 2 || regex.Split(tableRows[0]).Length != 5)
            {
                return false;
            }

            // Jagged array with table's values. 
            string[][] values = new string[tableRows.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = regex.Split(tableRows[i]);
            }

            // Checking first two rows, every one must consists of initial names of columns.
            for (int j = 0; j < regex.Split(tableRows[0]).Length; j++)
            {
                if (regex.Split(tableRows[0])[j] != alphabetEng[j])
                {
                    return false;
                }
            }
            // Column names are in the first row of jagged array
            ColumnNames = values[0];
            _values = values[1..];
            return true;
        }
    }
}
