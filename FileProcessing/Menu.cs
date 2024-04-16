using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing
{
    public class Menu
    {
        private string[] _menuItems;
        private string _menuName;
        public Menu(string menuName, string[] values)
        {
            _menuItems = values;
            _menuName = menuName;
        }
        /// <summary>
        /// This method prints the menu items of interactive menu.
        /// </summary>
        private void PrintMenu(string[] elems, int row, int column, int idx)
        {
            Console.SetCursorPosition(column, row);

            for (int i = 0; i < elems.Length; i++)
            {
                // Setting color of current menu item.
                if (i + 1 == idx)
                {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(elems[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        /// <summary>
        /// This menu implements all menu actions.
        /// </summary>
        /// <param name="tableValues"></param>
        /// <param name="columnNames"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int ActMenu()
        {
            int idx;

            // Cycle of repeating to process errors.
            while (true)
            {
                Console.WriteLine(_menuName);

                // Cursor of current place.
                int row = Console.CursorTop;
                int column = Console.CursorLeft;
                // Default index of menu.
                idx = 1;

                // Cycle of repeating to show interactive menu.
                while (true)
                {
                    // The variable to break the cycle after choosing menu item.
                    bool isExit = true;

                    PrintMenu(_menuItems, row, column, idx);

                    switch (Console.ReadKey(true).Key)
                    {
                        // When user enters down key.
                        case ConsoleKey.DownArrow:
                            // Checking if it is the last element.
                            if (idx < _menuItems.Length)
                                // Moving to the next item.
                                idx++;
                            break;
                        // When user enters up key.
                        case ConsoleKey.UpArrow:
                            // Checking if it is the first and minimal element.
                            if (idx > 1)
                                // Moving to the previous item.
                                idx--;
                            break;
                        // When user chooses the item.
                        case ConsoleKey.Enter:
                            // Initializing the result table according to user's choice.
                            idx = idx switch
                            {
                                1 => 1,
                                2 => 2,
                                3 => 3,
                                4 => 4,
                                5 => 5
                            };
                            // Exiting interactive menu.
                            isExit = false;
                            break;

                    }
                    if (!isExit)
                        break;
                }
                return idx;
            }
        }
    }
}
