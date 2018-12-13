using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDataReadAndDBInsert
{
    class CheData
    {
        public int idNumber { get; set; }
        public string shcode { get; set; }
        public DateTime time { get; set; }

        public int price { get; set; }               //현재가
        public double drate { get; set; }            //등락률
        public string cGubun { get; set; }           //체결구분
        public int cVolume { get; set; }             //체결량
        public int volume { get; set; }              //누적 거래량
        public int value { get; set; }               //누적 거래대금 (백만원)
        public int mdVolume { get; set; }            //매도 누적체결량
        public int msVolume { get; set; }            //매수 누적체결량
    }

}
