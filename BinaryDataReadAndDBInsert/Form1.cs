using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace BinaryDataReadAndDBInsert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {         
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath + "../";

            openFileDialog1.Filter = "Dat Files|*.dat";
            openFileDialog1.Title = "Select a data File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string connectionAddress = "Server=entrophy.mooo.com;port=1004;database=stock;uid=kyungwon;pwd=2overcomelimit!;";

                DataBaseInsert Stock = new DataBaseInsert(connectionAddress);
                Stock.unitCountInsert = 100000;
                Stock.DataInsert(fileName);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        //내가 원하는 것은 DB에 write이 완료되면(이벤트가 발생해서) 메모장에 글을 적는것이다. 



    }
}
