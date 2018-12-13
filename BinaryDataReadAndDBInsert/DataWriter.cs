using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDataReadAndDBInsert
{
    class DataWriter
    {       
        private List<HogaData> hogaData = null;
        private List<CheData> cheData = null;
        private List<ProData> proData = null;
        private List<SrcData> srcData = null;
        private string connectionString = string.Empty;

        public DataWriter(string connectionString, List<HogaData> hogaData, List<CheData> cheData, List<ProData> proData, List<SrcData> srcData)
        {           
            this.hogaData = hogaData;
            this.cheData = cheData;
            this.proData = proData;
            this.srcData = srcData;
            this.connectionString = connectionString;
        }

        public void DataSendToDatabase()
        {
            using (MySqlConnection mConnection = new MySqlConnection(connectionString))
            {
                WriteHogaData(mConnection);
                WriteCheData(mConnection);
                WriteProData(mConnection);
                WriteSrcData(mConnection);
            }
        }


        private void WriteHogaData(MySqlConnection mConnection)
        {
            string tableName = TableIndex.hogaTable;
            StringBuilder sCommand = new StringBuilder("INSERT INTO ");

            sCommand.Append(tableName);
            sCommand.Append(" (idNumber, shcode, time, bidho1, bidrem1, offerho1, offerrem1, bidho2, bidrem2, offerho2, offerrem2, bidho3, bidrem3, offerho3, offerrem3," +
                " bidho4, bidrem4, offerho4, offerrem4, bidho5, bidrem5, offerho5, offerrem5, bidho6, bidrem6, offerho6, offerrem6, bidho7, bidrem7, offerho7, offerrem7," +
                " bidho8, bidrem8, offerho8, offerrem8, bidho9, bidrem9, offerho9, offerrem9, bidho10, bidrem10, offerho10, offerrem10, totofferrem, totbidrem) VALUES ");

            List<string> Rows = new List<string>();
            for (int i = 0; i < hogaData.Count(); i++)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}'" +
                    ",'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}')",
                     hogaData[i].idNumber, hogaData[i].shcode, hogaData[i].time,
                     hogaData[i].bidHo1, hogaData[i].bidRem1, hogaData[i].offerHo1, hogaData[i].offerRem1, hogaData[i].bidHo2, hogaData[i].bidRem2, hogaData[i].offerHo2, hogaData[i].offerRem2,
                     hogaData[i].bidHo3, hogaData[i].bidRem3, hogaData[i].offerHo3, hogaData[i].offerRem3, hogaData[i].bidHo4, hogaData[i].bidRem4, hogaData[i].offerHo4, hogaData[i].offerRem4,
                     hogaData[i].bidHo5, hogaData[i].bidRem5, hogaData[i].offerHo5, hogaData[i].offerRem5, hogaData[i].bidHo6, hogaData[i].bidRem6, hogaData[i].offerHo6, hogaData[i].offerRem6,
                     hogaData[i].bidHo7, hogaData[i].bidRem7, hogaData[i].offerHo7, hogaData[i].offerRem7, hogaData[i].bidHo8, hogaData[i].bidRem8, hogaData[i].offerHo8, hogaData[i].offerRem8,
                     hogaData[i].bidHo9, hogaData[i].bidRem9, hogaData[i].offerHo9, hogaData[i].offerRem9, hogaData[i].bidHo10, hogaData[i].bidRem10, hogaData[i].offerHo10, hogaData[i].offerRem10,
                     hogaData[i].totOfferRem, hogaData[i].totbidrem));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            if (hogaData.Count != 0)
            {
                try
                {
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    mConnection.Close();
                }
            }
        }



        private void WriteCheData(MySqlConnection mConnection)
        {
            string tableName = "c_" + TableIndex.commonTable;
            StringBuilder sCommand = new StringBuilder("INSERT INTO ");

            sCommand.Append(tableName);
            sCommand.Append(" (idNumber, shcode, time, price, drate, cgubun, cvolume, volume, value, mdvolume, msvolume) VALUES ");

            List<string> Rows = new List<string>();
            for (int i = 0; i < cheData.Count(); i++)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                     cheData[i].idNumber, cheData[i].shcode, cheData[i].time, cheData[i].price, cheData[i].drate, cheData[i].cGubun,
                     cheData[i].cVolume, cheData[i].volume, cheData[i].value, cheData[i].mdVolume, cheData[i].msVolume));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            if (cheData.Count() != 0)
            {
                try
                {
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    mConnection.Close();
                }
            }
        }



        private void WriteProData(MySqlConnection mConnection)
        {

            string tableName = "p_" + TableIndex.commonTable;
            StringBuilder sCommand = new StringBuilder("INSERT INTO ");

            sCommand.Append(tableName);
            sCommand.Append(" (idNumber, shcode, time, price, protvol, proselltvol, probuytvol) VALUES ");

            List<string> Rows = new List<string>();
            for (int i = 0; i < proData.Count(); i++)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                     proData[i].idNumber, proData[i].shcode, proData[i].time, proData[i].price, proData[i].proTVol, proData[i].proSellTvol, proData[i].proBuyTvol));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            if (proData.Count() != 0)
            {
                try
                {
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    mConnection.Close();
                }
            }
        }



        private void WriteSrcData(MySqlConnection mConnection)
        {
            string tableName = "s_" + TableIndex.commonTable;
            StringBuilder sCommand = new StringBuilder("INSERT INTO ");

            sCommand.Append(tableName);
            sCommand.Append(" (idNumber, shcode, time, ftradmdvol, ftradmsvol, ftradmdcha, ftradmscha," +
                "offerno1, tradmdvol1, tradmdcha1, bidno1, tradmsvol1, tradmscha1, offerno2, tradmdvol2, tradmdcha2, bidno2, tradmsvol2, tradmscha2," +
                "offerno3, tradmdvol3, tradmdcha3, bidno3, tradmsvol3, tradmscha3, offerno4, tradmdvol4, tradmdcha4, bidno4, tradmsvol4, tradmscha4," +
                "offerno5, tradmdvol5, tradmdcha5, bidno5, tradmsvol5, tradmscha5 ) VALUES ");

            List<string> Rows = new List<string>();
            for (int i = 0; i < srcData.Count(); i++)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}'" +
                    ",'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}')",
                     srcData[i].idNumber, srcData[i].shcode, srcData[i].time, srcData[i].fTradMdVol, srcData[i].fTradMsVol, srcData[i].fTradMdCha, srcData[i].fTradMsCha,
                     srcData[i].offerNo1, srcData[i].tradMdVol1, srcData[i].tradMdCha1, srcData[i].bidNo1, srcData[i].tradMsVol1, srcData[i].tradMsCha1,
                     srcData[i].offerNo2, srcData[i].tradMdVol2, srcData[i].tradMdCha2, srcData[i].bidNo2, srcData[i].tradMsVol2, srcData[i].tradMsCha2,
                     srcData[i].offerNo3, srcData[i].tradMdVol3, srcData[i].tradMdCha3, srcData[i].bidNo3, srcData[i].tradMsVol3, srcData[i].tradMsCha3,
                     srcData[i].offerNo4, srcData[i].tradMdVol4, srcData[i].tradMdCha4, srcData[i].bidNo4, srcData[i].tradMsVol4, srcData[i].tradMsCha4,
                     srcData[i].offerNo5, srcData[i].tradMdVol5, srcData[i].tradMdCha5, srcData[i].bidNo5, srcData[i].tradMsVol5, srcData[i].tradMsCha5));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            if (srcData.Count() != 0)
            {
                try
                {
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    mConnection.Close();
                }
            }
        }



    }
}
