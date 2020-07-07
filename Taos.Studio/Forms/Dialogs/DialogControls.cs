using DarkUI.Controls;
using DarkUI.Forms;

namespace Taos.Studio
{
    public partial class DialogControls : DarkDialog
    {
        public DialogControls()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new DarkListItem($"List item #{i}");
                lstTest.Items.Add(item);
            }

            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 20; i++)
            {
                var node = new DarkTreeNode($"Root node #{i}");
                node.ExpandedIcon = Taos.Studio.Properties.Resources.folder_open;
                node.Icon = Taos.Studio.Properties.Resources.folder_closed;

                for (var x = 0; x < 10; x++)
                {
                    var childNode = new DarkTreeNode($"Child node #{childCount}");
                    childNode.Icon = Taos.Studio.Properties.Resources.files;
                    childCount++;
                    node.Nodes.Add(childNode);
                }

                treeTest.Nodes.Add(node);
            }

            // Hook dialog button events
            btnDialog.Click += delegate
            {
                DarkMessageBox.ShowError("This is an error", "Dark UI - Example");
            };

            btnMessageBox.Click += delegate
            {
                DarkMessageBox.ShowInformation("This is some information, except it is much bigger, so there we go. I wonder how this is going to go. I hope it resizes properly. It probably will.", "Dark UI - Example");
            };
        }
    }
}
