using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryDataReadAndDBInsert
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Form1 view = new Form1();
            //IModel mdl = new IncModel();
            //IController cnt = new IncController(view, mdl);
            //Application.Run(view);

            Application.Run(new Form1());
        }
    }
}
