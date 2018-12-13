using System;
using MySql.Data.MySqlClient;


namespace BinaryDataReadAndDBInsert
{
    class TableIndex
    {
        public static string connectionString { get; set; }
        public static DateTime date { get; set; }
        public static int startNumber { get; set; }
        public static int endNumber { get; set; }
        public static string startTable { get; set; }
        public static string endTable { get; set; }
        public static string hogaTable { get; set; }
        public static string commonTable { get; set; }
     
        public int GetStartNumberTable(DateTime todayDate)
        {
            using (MySqlConnection mConnection = new MySqlConnection(connectionString))
            {              
                int startNumber = 0;
                string commonTableName = string.Empty;

                string beginDate = todayDate.AddDays(-10).ToString("yyyyMMdd");
                string endDate = todayDate.ToString("yyyyMMdd");

                mConnection.Open();
                string sql = "SELECT * FROM indextable WHERE date = (SELECT MAX(date) FROM indextable where date < '" +
                    endDate + "' AND date > " + "'" + beginDate + "');";
              
                MySqlCommand commmandSql = new MySqlCommand(sql, mConnection);
                MySqlDataReader dataRead = commmandSql.ExecuteReader();
                dataRead.Read();
                commonTableName = (string)dataRead["commonTable"];

                if (CompareWeeks(todayDate, commonTableName))
                    startNumber = (int)dataRead["endNo"] + 1;
                else startNumber = 1;

                dataRead.Close();
                return startNumber;
            }
        }
        
        public string GetHogaTableName(DateTime todayDate)
        {
            return "h_" + todayDate.ToString("yyyyMMdd");
        }


        public string GetCommonTableName(DateTime todayDate)
        {
            int numberOfWeek = GetWeeksOfYear(todayDate);
            return todayDate.ToString("yyyyMM") + "W" + numberOfWeek.ToString();         
        }

        private int GetWeeksOfYear(DateTime date)
        {
            System.Globalization.CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("ko");
            System.Globalization.Calendar cal = cult_info.Calendar;
            int weekNo = cal.GetWeekOfYear(date, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            int week1day = cal.GetWeekOfYear(date.AddDays(-(date.Day + 1)), cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            return weekNo - week1day + 1;
        }

        private bool CompareWeeks(DateTime todayDate, string commonTableName)
        {
            int numberOfWeek = GetWeeksOfYear(todayDate);
            int previousLastWichWeek = int.Parse(commonTableName.Substring(7, 1));

            return numberOfWeek == previousLastWichWeek;
        }
        



    }





}
