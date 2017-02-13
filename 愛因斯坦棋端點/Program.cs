using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 愛因斯坦棋端點
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (true)
            {
                try
                {
                    string line;
                    using (StreamReader sr = new StreamReader("Language"))
                    {
                        line = sr.ReadLine();
                    }
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(line);
                    //"en-us"
                }
                catch (IOException){}
                catch (Exception ec)
                { Console.WriteLine(ec.Message); }
                try
                {
                    Application.Run(new Form1());
                }
                catch (Exception ec)
                {
                    Console.WriteLine(ec.Message);
                    MessageBox.Show("當掉了= =");
                }
            }
        }
    }
}
