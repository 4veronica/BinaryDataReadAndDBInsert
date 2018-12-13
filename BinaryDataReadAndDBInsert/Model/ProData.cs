using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDataReadAndDBInsert
{
    class ProData
    {
        public int idNumber { get; set; }
        public string shcode { get; set; }
        public DateTime time { get; set; }

        public int price { get; set; }
        public int proTVol { get; set; }
        public int proSellTvol { get; set; }
        public int proBuyTvol { get; set; }
    }

}
