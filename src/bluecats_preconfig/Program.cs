using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace winform_sample
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    static class Extension
    {
        public static Dictionary<string, string> ToDictionary(this System.Collections.IDictionary iDic)
        {
            var dic = new Dictionary<string, string>();
            var enumerator = iDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                dic[enumerator.Key.ToString()] = enumerator.Value.ToString();
            }
            return dic;
        }
    }

    class EnviromentValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Explain { get; set; }
    }


}
