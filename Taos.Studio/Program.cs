using IoTSharp.Data.Taos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taos.Studio.Data;

namespace Taos.Studio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      
        
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbProviderFactories.RegisterFactory("TDengine", TaosFactory.Instance);
            var connectionOptions = args.Length == 0 ? null : DatabaseConnectionOptions.FromPersistedString(args[0]);
            Application.Run(new MainForm(connectionOptions));
        }
    
    }
}
