using FileProcessing;

namespace KHW2_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // For right encoding only on devices with english orientation.
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.InputEncoding = System.Text.Encoding.GetEncoding("utf-16");
            do
            {
                try
                {
                    Interface program = new Interface();
                    int num;
                    program.ShowMenu(out num);
                    if (num != 5)
                    {
                        program.PrintInterface();
                        program.SaveInterface();
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Wrong file. Please try again.");
                    continue;
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
                    Console.WriteLine("Error while opening file. Please ry again");
                    continue;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error while working with arrays. Please try again");
                    continue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Error while working with file. Please try again.");
                    continue;
                }
                Console.WriteLine("Enter key to restart. To exit enter Escape.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}