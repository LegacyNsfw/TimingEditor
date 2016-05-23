using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    static class Program
    {
        public static bool Debug;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "debug")
                {
                    Debug = true;
                }
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            string name = assembly.ManifestModule.Name;

            bool singleTableMode;
            if ((string.Compare("tableeditor.exe", name, StringComparison.OrdinalIgnoreCase) == 0) ||
                (args.Length == 1 && args[0] == "table"))
            {
                singleTableMode = true;
                Util.DoubleFormat = "0.0000";
                Util.RowHeaderWidth = 60;
                Util.ColumnWidth = 60;
            }
            else
            {
                singleTableMode = false;
                Util.DoubleFormat = "0.00";
                Util.RowHeaderWidth = 60;
                Util.ColumnWidth = 40;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TimingForm(singleTableMode));
        }
    }
}
