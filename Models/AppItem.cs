using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatboobGGStream
{
    public class AppItem
    {
        public String AppPath { get; set; }
        public String AppTitle { get; set; }
        public String AppArgs { get; set; } 

        public AppItem()
        {
            this.AppArgs = "";
            this.AppTitle = "";
            this.AppArgs = "";
        }
    }
}
