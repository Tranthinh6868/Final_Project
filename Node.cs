using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleCode
{
    class Node
    {
        public string Value { get; set; }  // Giá trị của node (số hoặc toán tử)
        public Node Left { get; set; }     // Node con trái
        public Node Right { get; set; }    // Node con phải

        public Node(string value)
        {
            this.Value = value;
            Left = null;
            Right = null;
        }
    }
}
