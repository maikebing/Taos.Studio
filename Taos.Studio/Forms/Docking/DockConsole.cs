using DarkUI.Controls;
using DarkUI.Docking;
using System.Drawing;

namespace Taos.Studio
{
    public partial class DockConsole : DarkToolWindow
    {

        public DockConsole()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
          
        }
        public void ScrollToCaret() => fConsole1.ScrollToCaret();
        public void SelectLastLine() => fConsole1.SelectLastLine();
        public void SetText(string message, Color? color = null) => fConsole1.SetText(message, color);
        public void Write(string message, Color? color = null, bool showTimeTag = false) => fConsole1.Write(message, color, showTimeTag);
        public void WriteLine(string message, Color? color = null, bool showTimeTag = false) => fConsole1.WriteLine(message, color, showTimeTag);
    }
}
