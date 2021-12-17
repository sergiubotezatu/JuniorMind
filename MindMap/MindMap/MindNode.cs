using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace MindMap
{
    class MindNode : IEnumerable<MindNode>
    {
        public string category;
        public HashSet<string> siblings;
        public List<MindNode> hierarchy;

        public MindNode(int ideasCount)
        {
            category = GetNumeralPrefix(ideasCount) + " idea";
            hierarchy = new List<MindNode>();
            siblings = new HashSet<string>();
        }

        public MindNode(string category)
        {
            this.category = category;
            hierarchy = new List<MindNode>();
        }

        public int BranchCount { get; set; } = 0;

        private string Notes { get; set; }        

        public void Create(string newCategory)
        {
            category = newCategory;
        }

        public void AddBranch(int place)
        {
            hierarchy.Add(new MindNode(place));
            BranchCount++;
        }

        public void RemoveBranches()
        {
            BranchCount = 0;
            hierarchy.Clear();
        }

        public void RemoveIdea(string idea)
        {
            foreach (MindNode node in this)
            {
                if (node.ContainsIdea(idea))
                {
                    this.hierarchy = this.hierarchy.Where(x => x.category != idea).ToList();
                }
            }
        }

        public bool ContainsIdea(string idea)
        {
            for (int i = 0; i < this.BranchCount; i++)
            {
                if (this.hierarchy[i].category == idea)
                {
                    return true;
                }
            }

            return false;
        }

        public MindNode GetLast()
        {
            if (hierarchy.Count == 0)
            {
                return null;
            }

            return hierarchy[^1];
        }

        public void AddSibling()
        {
            siblings.Add($"related{siblings.Count + 1}");
        }

        public IEnumerator<MindNode> GetEnumerator()
        {
            foreach(MindNode node in hierarchy)
            {
                foreach (MindNode subNode in EnumerateNodes(node))
                {
                    yield return subNode;
                }
            }            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<MindNode> EnumerateNodes(MindNode node)
        {
            yield return node;
            for (int i = 0; i < node.hierarchy.Count(); i++)
            {
                foreach (MindNode subNode in EnumerateNodes(node.hierarchy[i]))
                {
                    yield return subNode;
                }
            }
        }

        private string GetNumeralPrefix(int prefix)
        {
            switch (prefix)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
            }

            return $"{prefix}th";
        }        
    }
}
