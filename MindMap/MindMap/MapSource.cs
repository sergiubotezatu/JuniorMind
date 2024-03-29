﻿using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;
using System.Collections;

namespace MindMap
{
    class MapSource
    {
        public readonly MindNode map;
        private readonly HashSet<string> values;
        private (string, string, int)[] toDisplay;
        private const string corner = " └─>";
        private const string intersection = " ├─>";
        private const string vertical = " │ ";
        
        public MapSource()
        {
            map = new MindNode("[Source]");
            toDisplay = new (string, string, int)[]{("", map.category, 0)};            
        }

        public int Count { get; private set; } = 0;

        public void AddMasterBranch()
        {
            Count++;
            map.AddBranch(Count);
        }

        public void AddChildBranch(string idea)
        {
            foreach (MindNode node in map)
            {
                if ($"[{node.category}]".Equals(idea))
                {
                    node.AddBranch(Count + 1);
                    break;
                }
            }

            Count++;
        }

        public void AddSibling(string idea)
        {
            foreach (MindNode node in map)
            {
                if ($"[{node.category}]".Equals(idea) || node.siblings.Any(x => $"[{x}]".Equals(idea)))
                {
                    node.AddSibling();
                    break;
                }
            }
        }

        public void ReplaceIdea(string idea, string newIdea)
        {
            foreach (MindNode node in map)
            {
                if ($"[{node.category}]".Equals(idea))
                {
                    node.Create(newIdea);
                    break;
                }
            }
        }

        public void DeleteIdea(string idea)
        {
            foreach (MindNode node in map)
            {
                if ($"[{node.category}]".Equals(idea))
                {
                    node.RemoveIdea(idea);
                    node.RemoveBranches();
                    break;
                }
            }
        }

        public (string, string, int)[] GetMapDisplay()
        {
            toDisplay = new (string, string, int)[] { ("", map.category, 0) };
            List<MindNode> tree = map.hierarchy;
            for (int i = 0; i < tree.Count; i++)
            {
                TurnToGraphic(tree[i], i == tree.Count - 1, "");
            }

            return toDisplay;
        }

        private void TurnToGraphic(MindNode mindNode, bool isLast, string appendix)
        {
            (string, string, int) branch = (appendix, $"[{mindNode.category}]", mindNode.siblings.Count);
            if (isLast)
            {
                branch.Item1 += corner;
                appendix += "    ";
            }
            else
            {
                branch.Item1 += intersection;
                appendix += vertical;
            }

            Array.Resize(ref toDisplay, toDisplay.Length + 1);
            toDisplay[^1] = branch;
            if (branch.Item3 > 0)
            {
                GetSiblingsBlock(mindNode.siblings);
            }

            for (int i = 0; i < mindNode.BranchCount; i++)
            {
                TurnToGraphic(mindNode.hierarchy[i], i == mindNode.BranchCount - 1, appendix); 
            }
        }

        private void GetSiblingsBlock(HashSet<string> block)
        {
            int lastSibling = block.Count() - 1;       
            foreach(string sibling in block)
            {
                Array.Resize(ref toDisplay, toDisplay.Length + 1);
                toDisplay[^1] = ("<->", $"[{sibling}]", lastSibling--);
            }
        }
    }
}
