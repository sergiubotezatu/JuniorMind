using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MindMap
{
    class MapNavigation
    {
        private MapSource map;
        private (string, string)[] display;
        private MainMenu menu;
        private const ConsoleKey Up = ConsoleKey.UpArrow;
        private const ConsoleKey Down = ConsoleKey.DownArrow;
        private const ConsoleKey Enter = ConsoleKey.Enter;
        private const ConsoleKey Rename = ConsoleKey.R;
        private const ConsoleKey Notes = ConsoleKey.N;
        private const ConsoleKey Delete = ConsoleKey.Delete;
        private const ConsoleKey Escape = ConsoleKey.Escape;

        private int Selected { get; set; } = 0;

        public MapNavigation(MapSource Map, (string, string)[] Display, MainMenu Menu)
        {
            this.map = Map;
            this.display = Display;
            this.menu = Menu;
        }

        public void Run()
        {
            PrintMap();
            ConsoleKey command = Navigate();
            while (command != Escape)
            {
                switch (command)
                {
                    case Rename:
                        Replace();
                        break;
                    case Enter:
                        AddPlaceHolder();
                        break;
                    case Delete:
                        Remove();
                        break;
                }

                command = Navigate();
            }

            GoBack();
        }

        public ConsoleKey Navigate()
        {
            ConsoleKey[] action = new ConsoleKey[] { Enter, Rename, Notes, Delete, Escape };
            ConsoleKey selection;
            do
            {
                selection = Console.ReadKey().Key;
                if (selection == Up || selection == Down)
                {
                    Console.Clear();
                    Selected += MoveSelection(selection);
                    PrintMap();
                }                

            } while (!action.Contains(selection));

            return selection;
        }

        private void PrintMap()
        {
            Console.WriteLine("\t\t\tNavigation keys:\n1. \"Up\" and \"Down\" arrows to navigate the map.\n " +
                "(Selected item is highlighted in white)\n\n2. \"Enter\" Add new empty idea(placeholder)\n\n" +
                "3. \"R\" - Press R to add/modify the name of any selected idea/placeholder\nOnce renamed, press enter to continue mapping\n\n" +
                "4. \"Del\" - Delete any selected idea/placeholder.\n\n\n");
            for (int i = 0; i < display.Length; i++)
            {
                Console.Write(display[i].Item1);
                if (Selected == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(display[i].Item2 + '\n');
                Console.ResetColor();
            }
        }

        private int MoveSelection(ConsoleKey keyPressed)
        {
            if (keyPressed == ConsoleKey.UpArrow)
            {
                return Selected == 0 ? display.Length - 1 : -1;
            }

            return Selected == display.Length - 1 ? -Selected : 1;
        }

        private void AddPlaceHolder()
        {
            if (Selected == 0)
            {
                map.AddMasterBranch();
            }
            else
            {
                map.AddChildBranch(display[Selected].Item2);
            }

            display = map.GetMapDisplay();
            Console.Clear();
            PrintMap();
        }

        private void Replace()
        {
            string former = display[Selected].Item2;
            display[Selected].Item2 = "";
            ConsoleKey renaming = Console.ReadKey().Key;
            while (renaming != Enter)
            {
                switch (renaming)
                {
                    case ConsoleKey.Spacebar:
                        display[Selected].Item2 += " ";
                        break;
                    case ConsoleKey.Backspace:
                        display[Selected].Item2 = display[Selected].Item2[0..^1];
                        break;
                    default:
                        display[Selected].Item2 += renaming.ToString();
                        break;
                }                
                Console.Clear();
                PrintMap();
                renaming = Console.ReadKey().Key;
            }

            map.ReplaceIdea(former, display[Selected].Item2);
            display[Selected].Item2 = "[" + display[Selected].Item2 + "]";
            Console.Clear();
            PrintMap();
        }

        private void Remove()
        {
            string temp = display[Selected].Item2;
            display[Selected].Item2 = "Do you want to delete this? Del = yes | Esc = No";
            Console.Clear();
            PrintMap();
            ConsoleKey action = Console.ReadKey().Key;
            while (action != Delete && action != Escape)
            {
                action = Console.ReadKey().Key;
            }

            Console.Clear();

            if (action == Delete)
            {
                map.DeleteIdea(temp);
                display = map.GetMapDisplay();
                PrintMap();
            }
            else
            {
                display[Selected].Item2 = temp;
                PrintMap();
            }
        }

        private void GoBack()
        {
            Console.Clear();
            Console.WriteLine("are you sure you want to go back to Main Menu ? Enter = yes | Esc = No");
            ConsoleKey action = Console.ReadKey().Key;
            while (action != Enter || action != Escape)
            {
                action = Console.ReadKey().Key;
            }
            Console.Clear();

            if (action == Enter)
            {                
                menu.Run();
            }
            else
            {
                PrintMap();
            }
        }
    }
}
