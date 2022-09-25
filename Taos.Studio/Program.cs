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
      
            try
            {
              //  NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
                NativeLibrary.SetDllImportResolver(TaosFactory.Instance.GetType().Assembly, DllImportResolver);
            }
            catch (Exception)
            {

            }
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbProviderFactories.RegisterFactory("TDengine", TaosFactory.Instance);
            Application.Run(new MainForm(args.Length == 0 ? null : new IoTSharp.Data.Taos.TaosConnectionStringBuilder( args[0])));
        }
        private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName == "taos")
            {
                // On systems with AVX2 support, load a different library.
                if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.Is64BitProcess)
                {
                    return NativeLibrary.Load("taos_win_x64.dll");
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32NT && !Environment.Is64BitProcess)
                {
                    return NativeLibrary.Load("taos_win_x86.dll");
                }
                else  
                {
                    throw new PlatformNotSupportedException($"{Environment.OSVersion.Platform} {Environment.OSVersion.VersionString}");
                }
            }
            // Otherwise, fallback to default import resolver.
            return IntPtr.Zero;
        }
    }
}
