using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace MindMap
{
    class MapSource
    {
        private List<MindNode> map;

        public MapSource()
        {
            map = new List<MindNode>();
        }

        public int Count { get; private set; } = 0;

        public void DisplayMap()
        {
            BackgroundColor = ConsoleColor.DarkCyan;
            if (map.Count == 0)
            {
                Console.WriteLine("[Source]");
                ResetColor();
                Console.Write("  |\n  └─>");                
            }
        }

        public void AddBranch(MindNode node)
        {
            map.Add(node);
            Count++;
        }
    }
}
