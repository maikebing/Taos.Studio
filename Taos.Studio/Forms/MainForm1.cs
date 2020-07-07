﻿using ICSharpCode.TextEditor;
using Taos.Studio.Forms;
using Maikebing.Data.Taos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taos.Studio.Properties;

namespace Taos.Studio
{
    public partial class MainForm1 : Form
    {
        private readonly SynchronizationContext _synchronizationContext;

        private Maikebing.Data.Taos.TaosConnection  _db = null;
        private Maikebing.Data.Taos.TaosConnectionStringBuilder _connectionString = null;
        private SqlCodeCompletion _codeCompletion;

        public MainForm1(Maikebing.Data.Taos.TaosConnectionStringBuilder  taosConnection)
        {
            InitializeComponent();

            // For performance https://stackoverflow.com/questions/4255148/how-to-improve-painting-performance-of-datagridview
            grdResult.DoubleBuffered(true);

            _synchronizationContext = SynchronizationContext.Current;

            _codeCompletion = new SqlCodeCompletion(txtSql, imgCodeCompletion);

            if (taosConnection==null)
            {
                this.Disconnect();
            }
            else
            {
                this.Connect(taosConnection);
            }

            txtSql.ActiveTextAreaControl.TextArea.Caret.PositionChanged += (s, e) =>
            {
                if (this.ActiveTask == null) return;

                this.ActiveTask.EditorContent = txtSql.Text;
                this.ActiveTask.SelectedTab = tabResult.SelectedTab.Name;
                this.ActiveTask.Position = new Tuple<int, int>(txtSql.ActiveTextAreaControl.TextArea.Caret.Line, txtSql.ActiveTextAreaControl.TextArea.Caret.Column);

                lblCursor.Text = $"Line: {(txtSql.ActiveTextAreaControl.Caret.Line + 1)} - Column: {(txtSql.ActiveTextAreaControl.Caret.Column + 1)}";
            };

            // stop all threads
            this.FormClosing += (s, e) =>
            {
                if(_db != null)
                {
                    this.Disconnect();
                }
            };

            // set assembly version on window title
            this.Text += $" (v.{typeof(MainForm).Assembly.GetName().Version})";
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

        public async void Connect(TaosConnectionStringBuilder  connectionString)
        {
            lblCursor.Text = Resources.Opening + connectionString.DataSource;
            lblElapsed.Text = Resources.Reading;
            prgRunning.Style = ProgressBarStyle.Marquee;
            btnConnect.Enabled = false;

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

                return;
            }
            finally
            {
                lblCursor.Text = "";
                lblElapsed.Text = "";
                prgRunning.Style = ProgressBarStyle.Blocks;
                btnConnect.Enabled = true;
            }

            btnConnect.Enabled = true;
            lblCursor.Text = "";
            prgRunning.Style = ProgressBarStyle.Blocks;

            _connectionString = connectionString;

            _codeCompletion.UpdateCodeCompletion(_db);

            btnConnect.Text = Resources.Disconnect;

            this.UIState(true);

            tabSql.TabPages.Add("+", "+");
            this.LoadTreeView();
            this.AddNewTab("");

            txtSql.Focus();
        }

        private void Disconnect()
        {
            foreach (var tab in tabSql.TabPages.Cast<TabPage>().Where(x => x.Name != "+").ToArray())
            {
                var task = tab.Tag as TaskData;
                task.ThreadRunning = false;
                task.WaitHandle.Set();
            }

            // clear all tabs and controls
            tabSql.TabPages.Clear();

            txtSql.Clear();
            grdResult.Clear();
            txtResult.Clear();

            tvwDatabase.Nodes.Clear();

            btnConnect.Text = Resources.Connect;

            this.UIState(false);

            tvwDatabase.Focus();

            tlbMain.Enabled = false;
            lblCursor.Text = Resources.Closing;

            try
            {
             

                _db?.Dispose();
                _db = null;

                lblCursor.Text = "";
                tlbMain.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources._ERROR, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void UIState(bool enabled)
        {
            splitRight.Visible = enabled;
            tabSql.Visible = enabled;
            tvwDatabase.Visible = enabled;

            btnRefresh.Enabled = enabled;
            tabSql.Enabled = enabled;
            btnRun.Enabled = enabled;

            BtnShowConnections.Enabled = enabled;
          
        }

        private TaskData ActiveTask => tabSql.SelectedTab?.Tag as TaskData;

        private void AddNewTab(string content)
        {
            // find + tab
            var tab = tabSql.TabPages.Cast<TabPage>().Where(x => x.Text == "+").Single();

            var task = new TaskData();

            task.EditorContent = content;
            task.Thread = new Thread(new ThreadStart(() => CreateThread(task)));
            task.Thread.Start();

            task.Id = task.Thread.ManagedThreadId;

            tab.Text = tab.Name = task.Id.ToString();
            tab.Tag = task;

            if (tabSql.SelectedTab != tab)
            {
                tabSql.SelectTab(tab);
            }

            // adding new + tab at end
            tabSql.TabPages.Add("+", "+");

            tabResult.SelectTab("tabGrid");
        }
        private void CreateThread(TaskData task)
        {
            while (true)
            {
                task.WaitHandle.Wait();

                if (task.ThreadRunning == false) break;

                if (task.Sql.Trim() == "")
                {
                    task.WaitHandle.Reset();
                    continue;
                }

                var sw = new Stopwatch();
                sw.Start();

                try
                {
                    task.Executing = true;
                    task.IsGridLoaded = task.IsTextLoaded = task.IsParametersLoaded = false;

                    _synchronizationContext.Post(new SendOrPostCallback(o =>
                    {
                        this.LoadResult(task);
                    }), task);

                    task.Parameters = null;

                    
 
                        using (var reader = _db.CreateCommand(task.Sql).ExecuteReader())
                        {
                            task.ReadResult(reader);
                        }

                    task.Elapsed = sw.Elapsed;
                    task.Exception = null;
                    task.Executing = false;

                    // update form button selected
                    _synchronizationContext.Post(new SendOrPostCallback(o =>
                    {
                        var t = o as TaskData;

                        if (this.ActiveTask?.Id == t.Id)
                        {
                            this.LoadResult(o as TaskData);
                        }

                    }), task);
                }
                catch (Exception ex)
                {
                    task.Executing = false;
                    task.Result = null;
                    task.Elapsed = sw.Elapsed;
                    task.Exception = ex;

                    _synchronizationContext.Post(new SendOrPostCallback(o =>
                    {
                        var t = o as TaskData;

                        if (this.ActiveTask?.Id == t.Id)
                        {
                            tabResult.SelectedTab = tabText;
                            this.LoadResult(o as TaskData);
                        }

                    }), task);
                }

                // put thread in wait mode
                task.WaitHandle.Reset();
            }

            task.WaitHandle.Dispose();
        }
        private void ExecuteSql(string sql)
        {
            if (this.ActiveTask?.Executing == false)
            {
                this.ActiveTask.Sql = sql;
                this.ActiveTask.WaitHandle.Set();
            }
        }

        private void LoadTreeView()
        {
            tvwDatabase.Nodes.Clear();
            var root = tvwDatabase.Nodes.Add(_connectionString.DataSource);
            root.ImageKey = "folder";
            root.ContextMenuStrip = ctxMenuRoot;
            var table = _db.CreateCommand("SHOW DATABASES").ExecuteReader().ToJson();
            table.ToList().ForEach(a =>
            {
                var name = Encoding.UTF8.GetString(a.Value<byte[]>("name")).TrimEnd('\0');
                var node = root.Nodes.Add(name, name, "database");
                node.ContextMenuStrip = ctxDataBaseMenu;
            });
            root.ExpandAll();
        }

  

        private void LoadResult(TaskData data)
        {
            if (data == null) return;

            btnRun.Enabled = !data.Executing;

            if (data.Executing)
            {
                grdResult.Clear();
                txtResult.Clear();

                lblResultCount.Visible = false;
                lblElapsed.Text = Resources.Running;
                prgRunning.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                lblResultCount.Visible = true;
                lblElapsed.Text = data.Elapsed.ToString();
                prgRunning.Style = ProgressBarStyle.Blocks;
                lblResultCount.Text = 
                    data.Result == null ? "" :
                    data.Result.Count == 0 ? Resources.NoDocuments :
                    data.Result.Count  == 1 ? Resources._1Document : 
                    data.Result.Count + (data.LimitExceeded ? "+" : "") + Resources.Documents;

                if (data.Exception != null)
                {
                    txtResult.BindErrorMessage(data.Sql, data.Exception);
                    grdResult.BindErrorMessage(data.Sql, data.Exception);
                }
                else if(data.Result != null)
                {
                    if (tabResult.SelectedTab == tabGrid && data.IsGridLoaded == false)
                    {
                        grdResult.BindBsonData(data);
                        data.IsGridLoaded = true;
                    }
                    else if(tabResult.SelectedTab == tabText && data.IsTextLoaded == false)
                    {
                        txtResult.BindBsonData(data);
                        data.IsTextLoaded = true;
                    }
                   
                }
            }
        }

        private void AddSqlSnippet(string sql)
        {
            if (txtSql.Text.Trim().Length == 0)
            {
                txtSql.Text = sql.Replace("\\n", "\n");
            }
            else
            {
                AddNewTab(sql.Replace("\\n", "\n"));
            }
        }

        #region Grid Edit

        private void GrdResult_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cell = grdResult.Rows[e.RowIndex].Cells[e.ColumnIndex];

           // cell.Value = JsonSerializer.Serialize(cell.Tag as BsonValue);
        }

        private void GrdResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var field = grdResult.Columns[e.ColumnIndex].HeaderText;
            var id = grdResult.Rows[e.RowIndex].Cells["id"].Tag as long?;
            var cell = grdResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var current = cell.Tag as long?;
            var text = cell.Value.ToString();

            // try run update collection using current/new value
            //var value = this.UpdateCellGrid(id, field, current, text);

            //if (value != current)
            //{
            //    cell.Style.BackColor = Color.LightGreen;
            //}

            //cell.SetBsonValue(value);
        }

        private void GrdResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

      

        #endregion

        #region Toolbar

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadTreeView();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && btnRun.Enabled)
            {
                BtnRun_Click(btnRun, EventArgs.Empty);
            }
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            var sql = txtSql.ActiveTextAreaControl.SelectionManager.SelectedText.Length > 0 ?
                txtSql.ActiveTextAreaControl.SelectionManager.SelectedText :
                txtSql.Text;

            this.ExecuteSql(sql);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (_db == null)
            {
                var dialog = new ConnectionForm(_connectionString ?? new  TaosConnectionStringBuilder());

                dialog.ShowDialog();

                if (dialog.DialogResult != DialogResult.OK) return;

                this.Connect(dialog.ConnectionString);
            }
            else
            {
                this.Disconnect();
            }
        }

       

        #endregion

        #region ContextMenu

        private void CtxMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var colname = tvwDatabase.SelectedNode.Text;

            var sql = string.Format(e.ClickedItem.Tag.ToString(), colname);
            this.AddSqlSnippet(sql);
        }

        private void CtxMenuRoot_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var sql = e.ClickedItem.Tag.ToString();
            this.AddSqlSnippet(sql);
        }

        #endregion

        #region TreeView

        private void TvwCols_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvwDatabase.SelectedNode = tvwDatabase.GetNodeAt(e.X, e.Y);
            }
        }

        private void TvwCols_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.ImageKey)
            {
                case "database":
                    _db.ChangeDatabase(e.Node.Name);
                    var jtable = _db.CreateCommand("SHOW TABLES ").ExecuteReader().ToJson();
                    jtable.ToList().ForEach(a =>
                    {
                        var name = Encoding.UTF8.GetString(a.Value<byte[]>("table_name")).TrimEnd('\0');
                        if (!e.Node.Nodes.ContainsKey(name))
                        {
                           var node=  e.Node.Nodes.Add(name, name, "table");
                            node.Tag = a;
                            node.ContextMenuStrip = ctxTableMenu;
                        }
                    });
                    break;
                case "table":
                    var jrows = _db.CreateCommand($"DESCRIBE  {e.Node.Name}").ExecuteReader().ToJson();
                    List<string> _fields = new List<string>();
                    jrows.ToList().ForEach(a =>
                    {
                        var name = Encoding.UTF8.GetString(a.Value<byte[]>("Field")).TrimEnd('\0');
                        _fields.Add(name);
                        if (!e.Node.Nodes.ContainsKey(name))
                        {
                            var node = e.Node.Nodes.Add(name, name, "field");
                            node.Tag = a;
                        }
                    });
                    AddSqlSnippet($"SELECT  {string.Join(",", _fields)} FROM  {e.Node.Name} LIMIT  100");
                    break;
                default:
                    break;
            }
            e.Node.ExpandAll();


        }

        #endregion

        #region Editor Tabs

        private void TabResult_Selected(object sender, TabControlEventArgs e)
        {
            if (tabSql.TabPages.Count == 0) return;

            this.LoadResult(this.ActiveTask);
            this.ActiveControl =
                tabResult.SelectedTab == tabGrid ? (Control)grdResult : (Control)txtResult ; 
        }

        private void TabSql_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabs = tabControl.TabPages;

            if (tabs.Count <= 1) return;

            if (e.Button == MouseButtons.Middle)
            {
                var tab = tabs.Cast<TabPage>()
                        .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                        .First();

                if (tab.Tag is TaskData task)
                {
                    task.ThreadRunning = false;
                    task.WaitHandle.Set();
                    tabs.Remove(tab);
                }
            }
        }

        private void TabSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this event occurs after tab already selected
            Application.DoEvents();

            txtSql.ActiveTextAreaControl.TextArea.Focus();
        }

        private void TabSql_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null) return;

            txtSql.Clear();
            grdResult.Clear();
            txtResult.Clear();

            lblResultCount.Visible = false;
            lblElapsed.Text = "";
            prgRunning.Style = ProgressBarStyle.Blocks;
            lblResultCount.Text = "";

            Application.DoEvents();

            if (e.TabPage.Name == "+")
            {
                this.AddNewTab("");
            }
            else
            {
                // restore data
                this.ActiveTask.IsGridLoaded = this.ActiveTask.IsTextLoaded = this.ActiveTask.IsParametersLoaded = false;

                txtSql.Text = this.ActiveTask.EditorContent;

                if (this.ActiveTask.Position != null)
                {
                    txtSql.ActiveTextAreaControl.TextArea.Caret.Line = this.ActiveTask.Position.Item1;
                    txtSql.ActiveTextAreaControl.TextArea.Caret.Column = this.ActiveTask.Position.Item2;
                }

                if (tabResult.SelectedTab.Name != this.ActiveTask.SelectedTab && this.ActiveTask.SelectedTab != "")
                {
                    tabResult.SelectTab(this.ActiveTask.SelectedTab); // fire LoadResult from TabResult_IndexChanged
                }
                else
                {
                    this.LoadResult(this.ActiveTask);
                }
            }
        }

        #endregion

        private void BtnShowConnections_Click(object sender, EventArgs e)
        {
            this.ExecuteSql("SHOW CONNECTIONS");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
           new AboutBox().ShowDialog(this);
        }

      
    }
}
