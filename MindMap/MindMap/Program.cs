using System;

namespace MindMap
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.Run();
            Console.ReadKey();
        }
    }
}
