using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BinaryDataReadAndDBInsert
{
    class DataReader
    {

        public event EventHandler SomethingHappened;

        private BinaryReader br = null;
        private DateTime todayDate;
        private string today;

        private string fileName = string.Empty;      

        public DataReader(string fileName, DateTime todayDate)
        {
            this.fileName = fileName;
            this.todayDate = todayDate;
            this.today = todayDate.ToString("yyyyMMdd");            
        }

        public void FileConnect(string fileName)
        {
            br = new BinaryReader(new FileStream(fileName, FileMode.Open), Encoding.Default);
        }

        public void FileDisConnect()
        {
            br.Close();           
        }

        public int TotalCount()
        {
            br.BaseStream.Seek(-4, SeekOrigin.End);
            int totalCount = br.ReadInt32();
            
            //offSet 가장 앞으로 이동
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            return totalCount;
        }

        public Boolean ValidDataFile()
        {
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            return br.ReadString() == "#RMSJ2";             
        }



        public void FileLoad(int startCount, int endCount, List<HogaData> hogaData, List<CheData> cheData, List<ProData> proData, List<SrcData> srcData)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string packetType = string.Empty;

            for (int i = startCount; i < endCount; i++)
            {
                int idNumber = i;
                string shcode = br.ReadString();
                packetType = br.ReadString();
                            
                DateTime time = DateTime.ParseExact(today + br.ReadString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                if (packetType == "#호가")
                {
                    ReadHogaData(hogaData, idNumber, shcode, time, br);
                }
                else if (packetType == "#체결")
                {
                    ReadCheData(cheData, idNumber, shcode, time, br);
                }
                else if (packetType == "#프로")
                {
                    ReadProData(proData, idNumber, shcode, time, br);
                }
                else if (packetType == "#거래")
                {
                    ReadSrcData(srcData, idNumber, shcode, time, br);
                }
            }

                
            //txt_memo.AppendText(string.Format("진행 사항 : {0}/{1}" + Environment.NewLine, i, quotient));
        }


        private bool IsValidFile(string idString)    
        {
            return idString == "#RMSJ2";
        }


        private void ReadHogaData(List<HogaData> hogaData, int idNumber, string shcode, DateTime time, BinaryReader hoga)
        {
            hogaData.Add(new HogaData()
            {
                idNumber = TableIndex.startNumber + idNumber,
                shcode = shcode,
                time = time,

                bidHo1 = hoga.ReadInt32(),
                bidRem1 = hoga.ReadInt32(),
                offerHo1 = hoga.ReadInt32(),
                offerRem1 = hoga.ReadInt32(),

                bidHo2 = hoga.ReadInt32(),
                bidRem2 = hoga.ReadInt32(),
                offerHo2 = hoga.ReadInt32(),
                offerRem2 = hoga.ReadInt32(),

                bidHo3 = hoga.ReadInt32(),
                bidRem3 = hoga.ReadInt32(),
                offerHo3 = hoga.ReadInt32(),
                offerRem3 = hoga.ReadInt32(),

                bidHo4 = hoga.ReadInt32(),
                bidRem4 = hoga.ReadInt32(),
                offerHo4 = hoga.ReadInt32(),
                offerRem4 = hoga.ReadInt32(),

                bidHo5 = hoga.ReadInt32(),
                bidRem5 = hoga.ReadInt32(),
                offerHo5 = hoga.ReadInt32(),
                offerRem5 = hoga.ReadInt32(),

                bidHo6 = hoga.ReadInt32(),
                bidRem6 = hoga.ReadInt32(),
                offerHo6 = hoga.ReadInt32(),
                offerRem6 = hoga.ReadInt32(),

                bidHo7 = hoga.ReadInt32(),
                bidRem7 = hoga.ReadInt32(),
                offerHo7 = hoga.ReadInt32(),
                offerRem7 = hoga.ReadInt32(),

                bidHo8 = hoga.ReadInt32(),
                bidRem8 = hoga.ReadInt32(),
                offerHo8 = hoga.ReadInt32(),
                offerRem8 = hoga.ReadInt32(),

                bidHo9 = hoga.ReadInt32(),
                bidRem9 = hoga.ReadInt32(),
                offerHo9 = hoga.ReadInt32(),
                offerRem9 = hoga.ReadInt32(),

                bidHo10 = hoga.ReadInt32(),
                bidRem10 = hoga.ReadInt32(),
                offerHo10 = hoga.ReadInt32(),
                offerRem10 = hoga.ReadInt32(),

                totOfferRem = hoga.ReadInt32(),
                totbidrem = hoga.ReadInt32(),
            });
        }


        private void ReadCheData(List<CheData> cheData, int idNumber, string shcode, DateTime time, BinaryReader che)
        {
            cheData.Add(new CheData()
            {
                idNumber = TableIndex.startNumber + idNumber,
                shcode = shcode,
                time = time,

                cVolume = che.ReadInt32(),
                price = che.ReadInt32(),
                drate = Convert.ToDouble(che.ReadString()),
                volume = che.ReadInt32(),
                mdVolume = che.ReadInt32(),
                msVolume = che.ReadInt32(),
                value = che.ReadInt32(),
                cGubun = che.ReadString(),
            });
        }


        private void ReadProData(List<ProData> proData, int idNumber, string shcode, DateTime time, BinaryReader pro)
        {
            proData.Add(new ProData()
            {
                idNumber = TableIndex.startNumber + idNumber,
                shcode = shcode,
                time = time,

                price = pro.ReadInt32(),
                proTVol = pro.ReadInt32(),
                proSellTvol = pro.ReadInt32(),
                proBuyTvol = pro.ReadInt32(),
            });
        }


        private void ReadSrcData(List<SrcData> srcData, int idNumber, string shcode, DateTime time, BinaryReader src)
        {
            srcData.Add(new SrcData()
            {
                idNumber = TableIndex.startNumber + idNumber,
                shcode = shcode,
                time = time,

                fTradMdVol = src.ReadInt32(),
                fTradMsVol = src.ReadInt32(),
                fTradMdCha = src.ReadInt32(),
                fTradMsCha = src.ReadInt32(),

                offerNo1 = src.ReadString(),
                tradMdVol1 = src.ReadInt32(),
                tradMdCha1 = src.ReadInt32(),
                bidNo1 = src.ReadString(),
                tradMsVol1 = src.ReadInt32(),
                tradMsCha1 = src.ReadInt32(),

                offerNo2 = src.ReadString(),
                tradMdVol2 = src.ReadInt32(),
                tradMdCha2 = src.ReadInt32(),
                bidNo2 = src.ReadString(),
                tradMsVol2 = src.ReadInt32(),
                tradMsCha2 = src.ReadInt32(),

                offerNo3 = src.ReadString(),
                tradMdVol3 = src.ReadInt32(),
                tradMdCha3 = src.ReadInt32(),
                bidNo3 = src.ReadString(),
                tradMsVol3 = src.ReadInt32(),
                tradMsCha3 = src.ReadInt32(),

                offerNo4 = src.ReadString(),
                tradMdVol4 = src.ReadInt32(),
                tradMdCha4 = src.ReadInt32(),
                bidNo4 = src.ReadString(),
                tradMsVol4 = src.ReadInt32(),
                tradMsCha4 = src.ReadInt32(),

                offerNo5 = src.ReadString(),
                tradMdVol5 = src.ReadInt32(),
                tradMdCha5 = src.ReadInt32(),
                bidNo5 = src.ReadString(),
                tradMsVol5 = src.ReadInt32(),
                tradMsCha5 = src.ReadInt32(),
            });
        }
    }

}

