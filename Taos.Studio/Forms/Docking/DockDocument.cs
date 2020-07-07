using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Docking;
using DarkUI.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Taos.Studio
{
    public partial class DockDocument : DarkDocument
    {
       

        public DockDocument()
        {
            InitializeComponent();
        }

        public DockDocument(string text, Image icon)
            : this()
        {
            DockText = text;
            Icon = icon;
        }

        

        public override void Close()
        {
            var result = DarkMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;

            base.Close();
        }

    }
}
