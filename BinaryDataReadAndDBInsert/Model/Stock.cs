using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDataReadAndDBInsert
{   
    class Stock
    {
        //public int idNumber { get; set; }
        //public string shcode { get; set; }
        //public DateTime time { get; set; }

        public HogaData hoga { get; set; }
        public CheData che { get; set; }
        public SrcData src { get; set; }
        public ProData pro { get; set; }
    }
}
