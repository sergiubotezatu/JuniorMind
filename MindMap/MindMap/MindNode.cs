using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MindMap
{
    class MindNode
    {
        string category;
        IEnumerable<MindNode> hierarchy;

        public MindNode()
        {
            category = "[Add Info]";
            hierarchy = new LinkedList<MindNode>();
        }

        private string Notes { get; set; }

        private int BranchCount { get; set; } = 0;

        public void Create(string newCategory)
        {
            category = newCategory;
        }

        public void AddBranch()
        {
            hierarchy.Prepend(new MindNode());
        }
    }
}
