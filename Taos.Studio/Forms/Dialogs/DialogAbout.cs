using DarkUI.Forms;
using System.Windows.Forms;

namespace Taos.Studio
{
    public partial class DialogAbout : DarkDialog
    {
        #region Constructor Region

        public DialogAbout()
        {
            InitializeComponent();

            lblVersion.Text = $"Version: {Application.ProductVersion.ToString()}";
            btnOk.Text = "Close";
        }

        #endregion
    }
}
