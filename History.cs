using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class History
    {
        String Intput { get; set; }
        String Output { get; set; }
        DateTime timeEvent { get; set; }
        public History() { }
        public History(String Intput, String Output)
        {
            this.Intput = Intput;
            this.Output = Output;
            timeEvent = DateTime.Now;
        }
        
    }
}
