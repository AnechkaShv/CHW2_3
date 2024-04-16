using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FileProcessing
{
    public class DataProcessing
    {
        private Notarius[] _notariuses;
        public Notarius[] Notariuses { get { return _notariuses; } }
        public DataProcessing() { }
        private string[][] _values;
        /// <summary>
        /// This constructor initializing Notarius array with values from the table.
        /// </summary>
        /// <param name="values"></param>
        public DataProcessing(string[][] values)
        {
            _values = values;
            _notariuses = new Notarius[_values.Length];
            for (int i = 0; i < _values.Length; i++)
            {
                _notariuses[i] = new Notarius(_values[i][0], _values[i][1], _values[i][4].Trim('\"'), _values[i][3].Trim('\"'), _values[i][2].Trim('\"'));
            }
        }

        /// <summary>
        /// This method sorts an array.
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Notarius[] Sort(int idx)
        {
            Notarius[] sortedTable = _notariuses;

            // Bubble sorting rows according to the column.
            for (int i = 0; i < sortedTable.Length - 1; i++)
            {
                for (int j = 0; j < sortedTable.Length - i - 1; j++)
                {
                    // Index of the first column to sort(user's menu number 4).
                    if (idx == 1)
                    {
                        // Alphabetical comparison.
                        if (_notariuses[j].CompareTo(_notariuses[j + 1]) >= 0 || sortedTable[j] is null)
                        {
                            Notarius temp = sortedTable[j + 1];
                            sortedTable[j + 1] = sortedTable[j];
                            sortedTable[j] = temp;

                        }
                    }
                    // Index of the second column to sort(user's menu number 5).
                    else
                    {
                        // Descending comparison.
                        if (_notariuses[j].CompareTo(_notariuses[j + 1]) <= 0 || sortedTable[j + 1] is null)
                        {
                            Notarius temp = sortedTable[j + 1];
                            sortedTable[j + 1] = sortedTable[j];
                            sortedTable[j] = temp;

                        }
                    }
                }
            }
            return sortedTable;
        }
        /// <summary>
        /// This method rows with suitable values.
        /// </summary>
        /// <param name="indexColumn"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Notarius[]? Select(int indexColumn, string value)
        {
            // Quantity of suitable elements.
            int counter = 0;

            Notarius[]? selectedTable;

            // Comparing every element in the selected column with user's value.
            for (int i = 0; i < _notariuses.Length; i++)
            {
                if ((indexColumn == 1 && _notariuses[i].Name.Contains(value)) || (_notariuses[i].MetroStations.Contains(value) && indexColumn == 4))
                {
                    counter += 1;
                }
            }
            // If there are no suitable elements returns null.
            if (counter == 0)
            {
                selectedTable = null;
                return selectedTable;
            }
            // Size of the result array of arrays is equal suitable elements = counter.
            selectedTable = new Notarius[counter];

            // Index of result table's row to write result there.
            int idxElem = 0;
            for (int i = 0; i < _notariuses.Length; i++)
            {
                // Comparing every element in the selected column with user's value and filling the result table.
                if ((_notariuses[i].Name.Contains(value) && indexColumn == 1) || (_notariuses[i].MetroStations.Contains(value) && indexColumn == 4))
                {
                    selectedTable[idxElem] = _notariuses[i];
                    idxElem++;
                }
            }
            return selectedTable;
        }
    }
}
