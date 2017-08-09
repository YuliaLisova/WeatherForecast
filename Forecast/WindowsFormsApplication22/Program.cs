using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;


namespace WindowsFormsApplication22
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Dictionary<string, string> city_code = new Dictionary<string, string>();
            //string directory = Application. StartupPath + @"\codes.txt";
            
            string first, second;

            using (StreamReader sr = new StreamReader(Path.GetFileName("codes.txt"), System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    first = sr.ReadLine();

                    second = sr.ReadLine();

                    city_code.Add(first, second);
                }

            }

            


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(city_code));
        }
    }
}
