using DarkUI.Docking;
using DarkUI.Forms;
using DarkUI.Win32;
using ICSharpCode.TextEditor.Actions;
using Maikebing.Data.Taos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taos.Studio.Forms;
using Taos.Studio.Properties;

namespace Taos.Studio
{
    public partial class MainForm : DarkForm
    {
       

        private List<DarkDockContent> _toolWindows = new List<DarkDockContent>();

        private DockProject _dockProject;
        private DockProperties _dockProperties;
        private DockConsole _dockConsole;
        private DockLayers _dockLayers;
        private DockHistory _dockHistory;
  

 

        public MainForm()
        {
            InitializeComponent();

            // Add the control scroll message filter to re-route all mousewheel events
            // to the control the user is currently hovering over with their cursor.
            Application.AddMessageFilter(new ControlScrollFilter());

            // Add the dock content drag message filter to handle moving dock content around.
            Application.AddMessageFilter(DockPanel.DockContentDragFilter);

            // Add the dock panel message filter to filter through for dock panel splitter
            // input before letting events pass through to the rest of the application.
            Application.AddMessageFilter(DockPanel.DockResizeFilter);

         

            // Build the tool windows and add them to the dock panel
            _dockProject = new DockProject();
            _dockProperties = new DockProperties();
            _dockConsole = new DockConsole();
            _dockLayers = new DockLayers();
            _dockHistory = new DockHistory();

            // Add the tool windows to a list
            _toolWindows.Add(_dockProject);
            _toolWindows.Add(_dockProperties);
            _toolWindows.Add(_dockConsole);
            _toolWindows.Add(_dockLayers);
            _toolWindows.Add(_dockHistory);

            // Deserialize if a previous state is stored
            if (File.Exists("dockpanel.config"))
            {
                DeserializeDockPanel("dockpanel.config");
            }
            else
            {
                // Add the tool window list contents to the dock panel
                foreach (var toolWindow in _toolWindows)
                    DockPanel.AddContent(toolWindow);

                // Add the history panel to the layer panel group
                DockPanel.AddContent(_dockHistory, _dockLayers.DockGroup);
            }

            // Check window menu items which are contained in the dock panel
            BuildWindowMenu();
             
        }

        public MainForm(TaosConnectionStringBuilder taosConnectionStringBuilder) :this()
        {
            Taos_Login(taosConnectionStringBuilder);
        }

        
        private void ToggleToolWindow(DarkToolWindow toolWindow)
        {
            if (toolWindow.DockPanel == null)
                DockPanel.AddContent(toolWindow);
            else
                DockPanel.RemoveContent(toolWindow);
        }

        private void BuildWindowMenu()
        {
            mnuProject.Checked = DockPanel.ContainsContent(_dockProject);
            mnuProperties.Checked = DockPanel.ContainsContent(_dockProperties);
            mnuConsole.Checked = DockPanel.ContainsContent(_dockConsole);
            mnuLayers.Checked = DockPanel.Contains(_dockLayers);
            mnuHistory.Checked = DockPanel.Contains(_dockHistory);
        }

     

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeDockPanel("dockpanel.config");
        }

        private void DockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void DockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            Taos_Login(null);

        }

        private void Taos_Login(TaosConnectionStringBuilder _connectionString)
        {
            var dialog = new ConnectionForm(_connectionString ?? new TaosConnectionStringBuilder());

            dialog.ShowDialog(this);

            if (dialog.DialogResult != DialogResult.OK) return;
            Task.Run(async () => await ConnectAsync(dialog.ConnectionString)
             ).ContinueWith(t =>
             {
                 _dockProject.LoadTree(t.Result);
             });
        }

        private async Task<TaosConnection> AsyncConnect(TaosConnectionStringBuilder connectionString)
        {
            return await Task.Run(() =>
            {
                var tc = new TaosConnection(connectionString.ToString());
                tc.Open();
                return tc;
            });
        }

        public async Task<TaosConnection> ConnectAsync(TaosConnectionStringBuilder connectionString)
        {
            TaosConnection _db=null;
            try
            {
                _db = await this.AsyncConnect(connectionString);
                // force open database
                var uv = _db.ServerVersion;

            }
            catch (Exception ex)
            {
                _db?.Dispose();
                _db = null;
                MessageBox.Show(ex.Message, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
              
            }
            return _db;
 
        }

        private void Disconnect( TaosConnection _db)
        {
          

            try
            {


                _db?.Dispose();
                _db = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources._ERROR, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dialog_Click(object sender, EventArgs e)
        {
            var test = new DialogControls();
            test.ShowDialog();
        }

        private void Project_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProject);
        }

        private void Properties_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProperties);
        }

        private void Console_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockConsole);
        }

        private void Layers_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockLayers);
        }

        private void History_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockHistory);
        }

        private void About_Click(object sender, EventArgs e)
        {
            var about = new DialogAbout();
            about.ShowDialog();
        }
 
        #region Serialization Region

        private void SerializeDockPanel(string path)
        {
            var state = DockPanel.GetDockPanelState();
            SerializerHelper.Serialize(state, path);
        }

        private void DeserializeDockPanel(string path)
        {
            var state = SerializerHelper.Deserialize<DockPanelState>(path);
            DockPanel.RestoreDockPanelState(state, GetContentBySerializationKey);
        }
         
        private DarkDockContent GetContentBySerializationKey(string key)
        {
            foreach (var window in _toolWindows)
            {
                if (window.SerializationKey == key)
                    return window;
            }

            return null;
        }

        #endregion
    }
}
