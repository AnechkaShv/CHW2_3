using DataProcessing;
using FileProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileProcessing
{
    public class Interface
    {
        private PathProcessing _fpath;
        private CsvChecking _checker;
        private string[][] _resTable;
        private Notarius[] _notariuses;
        private Notarius[] _notTable;
        private string[] _columnNames;
        enum ChooseOption
        {
            Top,
            Bottom
        }
        /// <summary>
        /// This constructor checks path and data in it.
        /// </summary>
        public Interface()
        {
            while (true)
            {
                Console.WriteLine("Please enter path of your *.csv file");
                try
                {
                    // Checking path.
                    _fpath = new PathProcessing(Console.ReadLine());

                    _checker = new CsvChecking(_fpath.FPath);
                }
                catch (PathTooLongException)
                {
                    Console.WriteLine("Your file name is too long. Please try again.");
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Wrong file path. Please try again");
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("This file can be only read. Please try again.");
                    continue;
                }
                catch (IOException)
                {
                    Console.WriteLine("Error while writing data in the file. Please try again.");
                    continue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong file name.Please try again.");
                    continue;
                }
                // Checking data.
                if (!_checker.Read())
                {
                    Console.WriteLine("Wrong file's data.Please try again");
                    continue;
                }
                Console.WriteLine("You enter correct file name.Congratulations!");
                break;
            }
            // Initializing array with table's data.
            _resTable = _checker.Values;
            _columnNames = _checker.ColumnNames;
        }
        /// <summary>
        /// This method acts menu and user's choice.
        /// </summary>
        /// <param name="num"></param>
        public void ShowMenu(out int num)
        {
            // Initializing Notarius fields.
            DataProcessing notariuses = new DataProcessing(_resTable);

            // Show interactive menu.
            Menu menu = new Menu("Use up/down keys to choose menu item.", new string[] { "1. Sort table by value address alphabetically", "2. Sort table by value address descending", "3. Make a selection by value fullnames", "4. Make a selection by value metrostations", "5. Exit the program" });
            // Repeating cycle to show menu again if the corresponding option has been chosen.
            while (true)
            {
                // Index of user's choice.
                num = menu.ActMenu();
                bool flag = true;
                // If user chooses sorting alphabetically or descending.
                if (num == 1 || num == 2)
                {
                    _notariuses = notariuses.Sort(num);
                    break;
                }
                // If user chooses selecting fullnames
                if (num == 3)
                {
                    _notariuses = SelectInterface(notariuses, "fullname", ref flag);
                    if (!flag)
                        continue;
                    break;
                }
                // If user chooses selecting metro atations.
                if (num == 4)
                {
                    _notariuses = SelectInterface(notariuses, "metrostations", ref flag);
                    if (!flag)
                        continue;
                    break;
                }
                // If user chooses exit.
                if (num == 5)
                    return;
            }
        }
        /// <summary>
        /// This method implements selection interface and return selected values from the table.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public Notarius[] SelectInterface(DataProcessing table, string name, ref bool flag)
        {
            int num;
            // This cycle doesn't allow to do next step until user enters right data.
            while (true)
            {
                Console.WriteLine($"Enter values to make a selection in column {name}");
                Console.Write("Value: ");

                // Getting selection value from user.
                string? value = Console.ReadLine();

                // Checking value.
                if (value == null || value.Length <= 0)
                {
                    Console.WriteLine("Wrong value. Please try again.");
                    continue;
                }
                // Selecting table.
                _notTable = table.Select(Array.IndexOf(_columnNames, name), value);

                // If there are no user's value in the column(s) than notTable = null  and printing menu of actions.
                if (_notTable == null)
                {
                    Menu emptyOutput = new Menu("This value doesn't exist in the table. Do you want to try again?", new string[] { "1. Yes", "2. No. Return to the menu" });

                    int numEmpty = emptyOutput.ActMenu();
                    // If user wants to enter value(s) again.
                    if (numEmpty == 1)
                    {
                        continue;
                    }
                    // If user wants to return to the main menu.
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                break;
            }
            return _notTable;
        }
        /// <summary>
        /// This method prints formatted table.
        /// </summary>
        public void PrintInterface()
        {
            // Implement interactive menu.
            Menu print = new Menu("How do you want to print table?", new string[] { $"{ChooseOption.Top}", $"{ChooseOption.Bottom}" });

            // Choose the way of printing using enum.
            // If it is needed to print first n rows.
            if (print.ActMenu() == (int)ChooseOption.Top + 1)
            {
                Console.WriteLine($"Enter 1 <= N <= {_notariuses.Length}");

                // Number of printable strings.
                int N;

                // Processing wrong numbers.
                while (!int.TryParse(Console.ReadLine(), out N) || N < 1 || N > _notariuses.Length)
                    Console.WriteLine("Wrong number.Please try again.");

                // Jagged array for Notarius data.
                string[][] printTable = new string[N][];

                // Initializing array with Notarius data.
                for (int i = 0; i < N; i++)
                {
                    printTable[i] = new string[] { $"{_notariuses[i].Number}", $"{_notariuses[i].Name}", $"{_notariuses[i].Contacts.Phones}", $"{_notariuses[i].Contacts.Address}", $"{_notariuses[i].MetroStations}" };
                }
                // Array of the maximal length of element in every row.
                int[] maxElems = new int[_columnNames.Length];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < printTable[i].Length; j++)
                    {
                        //Maximal length of element between two column names rows in each column.
                        maxElems[j] = Math.Max(Math.Max(maxElems[j], printTable[i][j].Length), _columnNames[j].Length);
                    }
                }
                // Printing columns' names.
                for (int i = 0; i < _columnNames.Length; i++)
                {
                    // Printing formatted values except the last one.
                    if (i != _columnNames[i].Length - 1)
                        Console.Write(_columnNames[i] + new string(' ', maxElems[i] - _columnNames[i].Length) + '|' + ' ');
                    else
                        Console.Write(_columnNames[i] + new string(' ', maxElems[i] - _columnNames[i].Length));
                }
                Console.WriteLine();

                // Checking empty values.
                for (int i = 0; i < printTable.Length; i++, Console.WriteLine())
                {
                    // Number of empty elements in each row.
                    int counter = 0;
                    for (int j = 0; j < printTable[i].Length; j++)
                    {
                        if (printTable[i][j] == " " || printTable[i][j] is null)
                        {
                            counter += 1;
                        }
                    }
                    // Printing elements of row only if the row isn't empty.
                    if (counter != printTable[i].Length)
                    {
                        for (int j = 0; j < printTable[i].Length; j++)
                        {
                            // Checking elements.
                            if (printTable[i][j] != null && printTable[i][j] != " " && printTable[i][j].Length > 0)
                            {
                                // Printing elements of the table with right number of spaces.
                                if (j != printTable[i].Length - 1)
                                {
                                    Console.Write(printTable[i][j] + new string(' ', maxElems[j] - printTable[i][j].Length) + '|' + ' ');
                                }
                                // Printing last element without spaces.
                                else
                                {
                                    Console.Write(printTable[i][j]);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Enter 1 <= N <= {_notariuses.Length}");

                // Number of printable strings.
                int N;

                // Processing wrong numbers.
                while (!int.TryParse(Console.ReadLine(), out N) || N < 1 || N > _notariuses.Length)
                    Console.WriteLine("Wrong number.Please try again.");

                // Jagged array for Notarius data.
                string[][] printTable = new string[N][];

                // Initializing array with Notarius data.
                for (int i = _notariuses.Length - N; i < _notariuses.Length; i++)
                {
                    printTable[i -_notariuses.Length + N] = new string[] { $"{_notariuses[i].Number}", $"{_notariuses[i].Name}", $"{_notariuses[i].Contacts.Phones}", $"{_notariuses[i].Contacts.Address}", $"{_notariuses[i].MetroStations}" };
                }
                // Array of the maximal length of element in every row.
                int[] maxElems = new int[_columnNames.Length];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < printTable[i].Length; j++)
                    {
                        //Maximal length of element between two column names rows in each column.
                        maxElems[j] = Math.Max(Math.Max(maxElems[j], printTable[i][j].Length), _columnNames[j].Length);
                    }
                }
                // Printing columns' names.
                for (int i = 0; i < _columnNames.Length; i++)
                {
                    // Printing formatted values except the last one.
                    if (i != _columnNames[i].Length - 1)
                        Console.Write(_columnNames[i] + new string(' ', maxElems[i] - _columnNames[i].Length) + '|' + ' ');
                    else
                        Console.Write(_columnNames[i] + new string(' ', maxElems[i] - _columnNames[i].Length));
                }
                Console.WriteLine();

                // Checking empty values.
                for (int i = 0; i < printTable.Length; i++, Console.WriteLine())
                {
                    // Number of empty elements in each row.
                    int counter = 0;
                    for (int j = 0; j < printTable[i].Length; j++)
                    {
                        if (printTable[i][j] == " " || printTable[i][j] is null)
                        {
                            counter += 1;
                        }
                    }
                    // Printing elements of row only if the row isn't empty.
                    if (counter != printTable[i].Length)
                    {
                        for (int j = 0; j < printTable[i].Length; j++)
                        {
                            // Checking elements.
                            if (printTable[i][j] != null && printTable[i][j] != " " && printTable[i][j].Length > 0)
                            {
                                // Printing elements of the table with right number of spaces.
                                if (j != printTable[i].Length - 1)
                                {
                                    Console.Write(printTable[i][j] + new string(' ', maxElems[j] - printTable[i][j].Length) + '|' + ' ');
                                }
                                // Printing last element without spaces.
                                else
                                {
                                    Console.Write(printTable[i][j]);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This method implements saving interface and saves data.
        /// </summary>
        public void SaveInterface()
        {
            Menu save = new Menu("The data is going to be saved. Please try the way of saving", new string[] { "1. Save instead of the exsisted file", "2. Append to the existed file", "3. Save in a new file." });

            // repeating checking cycle.
            while (true)
            {
                try
                {
                    // Chosen index.
                    int num = save.ActMenu();

                    FileSaving fileSaving;
                    // If user chooses saving in the initial file.
                    if (num == 1)
                    {
                        fileSaving = new FileSaving(_fpath, _notariuses);
                        fileSaving.WriteToThis(_columnNames);
                    }
                    // If user chooses appending to initial file.
                    if (num == 2)
                    {
                        fileSaving = new FileSaving(_fpath, _notariuses);
                        fileSaving.Append(_columnNames);
                    }
                    // If user chooses saving in a new file.
                    if (num == 3)
                    {
                        Console.WriteLine("Enter new file's name");

                        PathProcessing nPath = new PathProcessing();
                        // Checking file.
                        nPath.NPath = Console.ReadLine();

                        fileSaving = new FileSaving(_fpath, nPath, _notariuses);
                        fileSaving.WriteToNew(_columnNames);
                    }
                    Console.WriteLine("Data has been successfully saved.");
                    break;
                }
                catch (PathTooLongException)
                {
                    Console.WriteLine("Your file name is too long. Please try again.");
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Wrong file path. Please try again");
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("This file can be only read. Please try again.");
                    continue;
                }
                catch (IOException)
                {
                    Console.WriteLine("Error while writing data in the file. Please try again.");
                    continue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong file name.Please try again.");
                    continue;
                }
            }
        }
    }
}


