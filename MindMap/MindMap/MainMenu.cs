using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace MindMap
{
    class MainMenu
    {
        private int selected;
        private string[] options;
        private string title;

        public MainMenu()
        {
            selected = 0;
            title = "\nMain Menu\n";
            options = new string[] { "1.[Info]", "2.[Start Mapping]", "3.[Exit]" };
        }

        public void Run()
        {
            ShowMenu();
            int selection = Select();
            if (selection == 0)
            {
                PrintInfo();
            }
            
            if (selection == 2)
            {
                Exit();
            }

            Clear();
            MapSource map = new MapSource();
            map.DisplayMap();
        }

        private int Select()
        {
            ConsoleKey[] navigations = new ConsoleKey[] { ConsoleKey.UpArrow, ConsoleKey.DownArrow };
            ConsoleKey selection;
            do
            {
                selection = Console.ReadKey().Key;
                if (navigations.Contains(selection))
                {
                    Clear();
                    selected += MoveSelection(selection);
                    ShowMenu();
                }
            } while (selection != ConsoleKey.Enter);

            return selected;
        }

        private void ShowMenu()
        {
            Console.WriteLine("Welcome to MindMap. " +
                "\nUse up and down arrow keys to navigate the menu. " +
                "\nPress Enter to select an option.");
            ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(title);
            ResetColor();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selected)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine(options[i]);
                ResetColor();
            }
        }

        private int MoveSelection(ConsoleKey keyPressed)
        {
            if (keyPressed == ConsoleKey.UpArrow)
            {
                return selected == 0 ? options.Length - 1 : - 1;
            }

            return selected == options.Length - 1 ? -selected : 1; 
        }

        private void Exit()
        {
            Console.WriteLine("Are you sure you want to exit? \n Yes : Press Enter \n No: Press BackSpace");
            ConsoleKey choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.Enter)
            {
               Environment.Exit(0);
            }

            if (choice == ConsoleKey.Backspace)
            {
                Clear();
                Run();
            }            
        }

        private void PrintInfo()
        {
            Console.WriteLine("This is a mind map app. A mind map is a diagram used to visually organize information." +
                     "\nIt is hierarchical and shows relationships among pieces of the whole. " +
                     "\nSelect [Start mapping] to get things started.\nPress any key to return to the main menu.");
            ReadKey();
            Clear();
            Run();
        }
    }
}
