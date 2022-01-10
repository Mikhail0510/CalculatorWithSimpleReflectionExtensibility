using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_.BusinessLogic
{
    internal class ExtensionsInnerDescription
    {
        public List<ExtensionState> Items = new List<ExtensionState>();
    }

    public class ExtensionState
    {
        public int ExtensionId { get; set; }
        public string FullPath { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

    }
}
