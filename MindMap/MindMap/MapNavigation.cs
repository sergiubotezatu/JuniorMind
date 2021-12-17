using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MindMap
{
    class MapNavigation
    {
        private MapSource map;
        private (string, string, int)[] display;
        private MainMenu menu;
        private const ConsoleKey Up = ConsoleKey.UpArrow;
        private const ConsoleKey Down = ConsoleKey.DownArrow;
        private const ConsoleKey Right = ConsoleKey.RightArrow;
        private const ConsoleKey Left = ConsoleKey.LeftArrow;
        private const ConsoleKey Enter = ConsoleKey.Enter;
        private const ConsoleKey Tab = ConsoleKey.Tab;
        private const ConsoleKey Rename = ConsoleKey.R;
        private const ConsoleKey Notes = ConsoleKey.N;
        private const ConsoleKey Delete = ConsoleKey.Delete;
        private const ConsoleKey Escape = ConsoleKey.Escape;

        private int Selected { get; set; } = 0;

        public MapNavigation(MapSource Map, (string, string, int)[] Display, MainMenu Menu)
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
                    case Tab:
                        AddSibling();
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
            ConsoleKey[] action = new ConsoleKey[] { Enter, Rename, Notes, Delete, Escape, Tab };
            ConsoleKey[] arrows = { Up, Down, Right, Left };
            ConsoleKey selection;
            do
            {
                selection = Console.ReadKey(true).Key;
                if (arrows.Contains(selection))
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
            Console.WriteLine("\t\t\tNavigation keys:\n\n\n" +
                "1. \"Enter\" - add new empty idea(placeholder) -> use ↑↓ to navigate map | " +
                "3. \"R\" - (re)name idea (press enter to continue mapping) | \"Tab\" add related idea -> ←→ navigate relatives\n\n\n");
                
            for (int i = 0; i < display.Length; i++)
            {
                Console.Write(display[i].Item1);
                if (Selected == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write((display[i].Item2.Equals(string.Empty) ? " " : display[i].Item2));
                Console.ResetColor();
                if (display[i].Item3 == 0)
                {
                    Console.Write("\n");
                }                
            }
        }

        private void PrintSiblingsBlock((string, string, int)[] display, int index)
        {
            for (int i = index + 1; i <= index + display[index].Item3; i++)
            {
                Console.Write(display[i].Item1 + display[i].Item2);
            }
        }

        private int MoveSelection(ConsoleKey keyPressed)
        {
            int siblingsCount = display[Selected].Item3;
            bool horizontalMove = siblingsCount > 0 || display[Selected].Item1 == "<->";
            int horizontalLimit = Selected + siblingsCount;
            switch (keyPressed)
            {
                case Up:
                    return Selected == 0 ? MoveToEnd(display.Length - 1) : - DecrementSiblings(Selected);
                    break;
                case Down:
                    return Selected == display.Length - siblingsCount - 1 ? -Selected : 1 + siblingsCount;
                    break;
                case Right:
                    return horizontalMove ? MoveRight(horizontalLimit) : 0;
                    break;
                case Left:
                    return horizontalMove ? MoveLeft(horizontalLimit, siblingsCount) : 0;
            }

            return 0;
        }

        private int MoveRight(int limit)
        {
            return Selected == limit ? - DecrementSiblings(Selected) : 1;
        }

        private int MoveLeft(int limit, int siblings)
        {
            return display[Selected].Item1 != "<->" ? siblings : -1; 
        }

        private int MoveToEnd(int currentPos)
        {
            while (display[currentPos].Item1 == "<->")
            {
                currentPos--;
            }

            return currentPos;
        }

        private int DecrementSiblings(int currentPos)
        {
            int prev = currentPos - 1;
            int positions = 1;
            while(display[prev].Item1 == "<->")
            {
                prev--;
                positions++;
            }

            return positions;
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
            Console.Clear();
            PrintMap();
            ConsoleKey renaming = Console.ReadKey(true).Key;
            while (renaming != Enter)
            {
                string toRename = display[Selected].Item2;
                switch (renaming)
                {
                    case ConsoleKey.Spacebar:
                        display[Selected].Item2 += " ";
                        break;
                    case ConsoleKey.Backspace:
                        display[Selected].Item2 = toRename.Equals(string.Empty) ? toRename : toRename[0..^1];
                        break;
                    default:
                        display[Selected].Item2 += renaming.ToString();
                        break;
                }                
                Console.Clear();
                PrintMap();
                renaming = Console.ReadKey(true).Key;
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

        private void AddSibling()
        {
            if (Selected > 0)
            {
                map.AddSibling(display[Selected].Item2);
            }

            display = map.GetMapDisplay();
            Console.Clear();
            PrintMap();
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
