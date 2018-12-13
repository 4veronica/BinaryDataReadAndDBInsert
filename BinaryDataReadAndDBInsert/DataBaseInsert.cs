using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDataReadAndDBInsert
{
    class DataBaseInsert
    {
        private int quotient = 0;
        private int remains = 0;
        private int totalCount = 0;
        private string connectionString = string.Empty;

        private DataReader binaryRead = null;
        private DataWriter binaryWrite = null;

        private DateTime todayDate;
        private TableIndex tableIndex = null;

        private List<HogaData> hogaData = null;
        private List<CheData> cheData = null;
        private List<ProData> proData = null;
        private List<SrcData> srcData = null;

        public int unitCountInsert { get; set; }
        public string connetionString { get; set; }

        public DataBaseInsert(string connectionString)
        {           
            hogaData = new List<HogaData>();
            cheData = new List<CheData>();
            proData = new List<ProData>();
            srcData = new List<SrcData>();

            tableIndex = new TableIndex();
            TableIndex.connectionString = connectionString;
        }

        private void writeDateTimeType (string date)
        {
            date = "20" + date.Substring(date.Length - 10, 6);
            date = date.Insert(6, "/");
            date = date.Insert(4, "/");
            todayDate = Convert.ToDateTime(date);
        }


        public void DataInsert(string fileName)
        {
            writeDateTimeType(fileName);

            TableIndex.date = todayDate;
            TableIndex.startNumber = tableIndex.GetStartNumberTable(todayDate);
            TableIndex.commonTable = tableIndex.GetCommonTableName(todayDate);
            TableIndex.hogaTable = tableIndex.GetHogaTableName(todayDate);

            binaryRead = new DataReader(fileName, todayDate);            

            binaryRead.FileConnect(fileName);
            totalCount = binaryRead.TotalCount();

            if (binaryRead.ValidDataFile() == true)
            {
                quotient = totalCount / unitCountInsert;
                remains = totalCount % unitCountInsert;

                for (int i = 0; i < (quotient + 1); i++)
                {
                    int startCount, endCount;
                    hogaData = new List<HogaData>();
                    cheData = new List<CheData>();
                    proData = new List<ProData>();
                    srcData = new List<SrcData>();

                    startCount = i * unitCountInsert;

                    if (i != quotient)
                    {
                        endCount = (i + 1) * unitCountInsert;
                    }
                    else
                    {
                        endCount = (i * unitCountInsert) + remains;
                    }
                    binaryRead.FileLoad(startCount, endCount, hogaData, cheData, proData, srcData);
                    binaryWrite = new DataWriter(connectionString, hogaData, cheData, proData, srcData);
                    binaryWrite.DataSendToDatabase();

                    //  이 시점에서 폼 메모에 글이 적혀야 함. 
                
                }
            }
        }
    }
}
