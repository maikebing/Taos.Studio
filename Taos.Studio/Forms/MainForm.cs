using ICSharpCode.TextEditor;
using Taos.Studio.Forms;
using IoTSharp.Data.Taos;
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
using System.Runtime.CompilerServices;
using Taos.Studio.Data;

namespace Taos.Studio
{
    public partial class MainForm : Form
    {
        private readonly SynchronizationContext _synchronizationContext;

        private IoTSharp.Data.Taos.TaosConnection _db = null;
        private IoTdbRestClient _iotdb = null;
        private DatabaseConnectionOptions _connectionOptions = null;
        private string _currentIoTdbDatabase = "";
        private SqlCodeCompletion _codeCompletion;

        public MainForm(DatabaseConnectionOptions connectionOptions)
        {
            InitializeComponent();

            // For performance https://stackoverflow.com/questions/4255148/how-to-improve-painting-performance-of-datagridview
            grdResult.DoubleBuffered(true);
            _synchronizationContext = SynchronizationContext.Current;

            _codeCompletion = new SqlCodeCompletion(txtSql, imgCodeCompletion);

            if (connectionOptions == null)
            {
                this.Disconnect();
            }
            else
            {
                this.Connect(connectionOptions);
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
                if (_db != null || _iotdb != null)
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
                connectionString.UseWebSocket();
                var tc = new TaosConnection(connectionString.ToString());
                tc.Open();
                return tc;
            });
        }

        public async void Connect(DatabaseConnectionOptions options)
        {
            lblCursor.Text = Resources.Opening + options.Host;
            lblElapsed.Text = Resources.Reading;
            prgRunning.Style = ProgressBarStyle.Marquee;
            btnConnect.Enabled = false;

            try
            {
                if (options.Provider == DatabaseProvider.IoTDB)
                {
                    _iotdb = await Task.Run(() =>
                    {
                        var client = new IoTdbRestClient(
                            options.CreateIoTdbBaseUri(),
                            options.Username,
                            options.Password,
                            TimeSpan.FromSeconds(options.TimeoutSeconds),
                            model: options.Model);
                        try
                        {
                            client.Execute("SHOW DATABASES");
                            return client;
                        }
                        catch
                        {
                            client.Dispose();
                            throw;
                        }
                    });
                }
                else
                {
                    _db = await this.AsyncConnect(options.CreateTaosConnectionString());

                    // force open database
                    var uv = _db.ServerVersion;
                }
            }
            catch (Exception ex)
            {
                DisposeConnections();

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

            _connectionOptions = options;

            _codeCompletion.UpdateCodeCompletion(_db);

            btnConnect.Text = Resources.Disconnect;

            this.UIState(true);

            tabSql.TabPages.Add("+", "+");
            this.LoadTreeView();
            this.AddNewTab("(none)", "");

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
            _currentIoTdbDatabase = "";

            btnConnect.Text = Resources.Connect;

            this.UIState(false);

            tvwDatabase.Focus();

            tlbMain.Enabled = false;
            lblCursor.Text = Resources.Closing;

            var exception = DisposeConnections();
            lblCursor.Text = "";
            tlbMain.Enabled = true;
            if (exception != null)
            {
                MessageBox.Show(exception.Message, Resources._ERROR, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private Exception DisposeConnections()
        {
            Exception exception = null;
            try
            {
                _db?.Dispose();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                _db = null;
            }

            try
            {
                _iotdb?.Dispose();
            }
            catch (Exception ex)
            {
                exception = exception ?? ex;
            }
            finally
            {
                _iotdb = null;
                _currentIoTdbDatabase = "";
            }

            return exception;
        }

        private void UIState(bool enabled)
        {
            splitRight.Visible = enabled;
            tabSql.Visible = enabled;
            tvwDatabase.Visible = enabled;

            btnRefresh.Enabled = enabled;
            tabSql.Enabled = enabled;
            btnRun.Enabled = enabled;

            BtnShowConnections.Enabled = enabled && _iotdb == null;

        }

        private TaskData ActiveTask => tabSql.SelectedTab?.Tag as TaskData;

        private void AddNewTab(string title, string content)
        {
            // find + tab
            var tab = tabSql.TabPages.Cast<TabPage>().Where(x => x.Name == "+").Single();

            var task = new TaskData();

            task.EditorContent = content;
            task.Thread = new Thread(new ThreadStart(() => CreateThread(task)));
            task.Thread.Start();

            task.Id = task.Thread.ManagedThreadId;

            tab.Name = task.Id.ToString();
            tab.Tag = task;

            if (tabSql.SelectedTab != tab)
            {
                tabSql.SelectTab(tab);
            }

            // adding new + tab at end
            tabSql.TabPages.Add("+", "+");
            tab.Text = title;
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


                    task.StartDateTime = DateTime.Now;
                    task.Result = ExecuteQuery(task.Sql);
                    task.EndDateTime = DateTime.Now;
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
            var root = tvwDatabase.Nodes.Add(_connectionOptions?.Host ?? "Database");
            root.ImageKey = "folder";
            root.ContextMenuStrip = _iotdb == null ? ctxMenuRoot : null;

            if (_iotdb != null)
            {
                var databases = _iotdb.Execute("SHOW DATABASES");
                foreach (DataRow row in databases.Rows)
                {
                    var name = GetRowValue(row, "database", "database_name", "name");
                    if (string.IsNullOrWhiteSpace(name)) continue;
                    var node = root.Nodes.Add(name, name, "database");
                    node.Tag = "iotdb";
                }
                root.ExpandAll();
                return;
            }

            var table = _db.CreateCommand("SHOW DATABASES").ExecuteReader().ToJson();
            table.ToList().ForEach(a =>
            {
                var name = a.Value<string>("name")?.RemoveNull();
                var node = root.Nodes.Add(name, name, "database");
                node.ContextMenuStrip = ctxDataBaseMenu;
            });
            root.ExpandAll();
        }

        private DataTable ExecuteQuery(string sql)
        {
            if (_iotdb != null)
            {
                var database = _connectionOptions?.Model == IoTdbModel.Table ? _currentIoTdbDatabase : null;
                return _iotdb.Execute(sql, database);
            }

            using (var reader = _db.CreateCommand(sql).ExecuteReader())
            {
                return reader.ToDataTable();
            }
        }

        private static string GetRowValue(DataRow row, params string[] candidates)
        {
            foreach (var candidate in candidates)
            {
                var column = row.Table.Columns.Cast<DataColumn>()
                    .FirstOrDefault(item => item.ColumnName.Equals(candidate, StringComparison.OrdinalIgnoreCase));
                if (column != null && row[column] != DBNull.Value)
                {
                    return Convert.ToString(row[column]);
                }
            }

            return row.Table.Columns.Count == 0 || row[0] == DBNull.Value ? "" : Convert.ToString(row[0]);
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
                    data.Result.Rows.Count == 0 ? Resources.NoDocuments :
                    data.Result.Rows.Count == 1 ? Resources._1Document :
                    data.Result.Rows.Count + (data.LimitExceeded ? "+" : "") + Resources.Documents;

                if (data.Exception != null)
                {
                    txtResult.BindErrorMessage(data.Sql, data.Exception);
                    grdResult.BindErrorMessage(data.Sql, data.Exception);
                }
                else if (data.Result != null)
                {
                    if (tabResult.SelectedTab == tabGrid && data.IsGridLoaded == false)
                    {
                        grdResult.BindBsonData(chartMain, data, txtResult);
                        grdResult.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        data.IsGridLoaded = true;
                    }
                    else if (tabResult.SelectedTab == tabText && data.IsTextLoaded == false)
                    {
                        txtResult.AppendLine($"{DateTime.Now}-开始时间:{data.StartDateTime}\r\b\t\t耗时:{data.EndDateTime.Subtract(data.StartDateTime)}\r\b\t\t共计:{data.Result.Columns.Count}列{data.Result.Rows.Count}调数据 ");
                        data.IsTextLoaded = true;
                    }

                }
            }
        }

        private void AddSqlSnippet(string title, string sql)
        {
            if (txtSql.Text.Trim().Length == 0)
            {
                txtSql.Text = sql.Replace("\\n", "\n");
            }
            else
            {
                AddNewTab(title, sql.Replace("\\n", "\n"));
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
            RunActive();
        }

        private void RunActive()
        {
            var sql = txtSql.ActiveTextAreaControl.SelectionManager.SelectedText.Length > 0 ?
                txtSql.ActiveTextAreaControl.SelectionManager.SelectedText :
                txtSql.Text;
            this.ExecuteSql(sql);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (_db == null && _iotdb == null)
            {
                var options = _connectionOptions;
                try
                {
                    options = options ?? DatabaseConnectionOptions.FromPersistedString(Properties.Settings.Default.ServeInfo);
                }
                catch (Exception)
                {
                }

                using (var dialog = new ConnectionForm(options ?? new DatabaseConnectionOptions()))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    _connectionOptions = dialog.Options;
                    Properties.Settings.Default.ServeInfo = dialog.Options.ToPersistedString();
                    Properties.Settings.Default.Save();
                    this.Connect(dialog.Options);
                }
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
            if (tvwDatabase.SelectedNode != null)
            {
                var colname = tvwDatabase.SelectedNode.Text;
                var sql = string.Format(e.ClickedItem.Tag.ToString(), colname);
                this.AddSqlSnippet($"{e.ClickedItem.Text} {colname}", sql);
                RunSQL(sql);
            }
        }

        private void RunSQL(string sql)
        {
            if (sql?.Contains("drop", StringComparison.OrdinalIgnoreCase) == true ||
                sql?.Contains("delete", StringComparison.OrdinalIgnoreCase) == true)
            {
                Debug.WriteLine("不自动执行:" + sql);
            }
            else if (menuRun.Checked)
            {
                RunActive();
            }
        }

        private void CtxMenuRoot_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var sql = e.ClickedItem.Tag?.ToString();
            if (tvwDatabase.SelectedNode != null)
            {
                var colname = tvwDatabase.SelectedNode.Text;
                var sql1 = string.Format(sql, colname);
                sql = sql1;
            }
            this.AddSqlSnippet(e.ClickedItem.Text, sql);

            RunSQL(sql);
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
            try
            {
                if (_iotdb != null)
                {
                    LoadIoTdbNode(e.Node);
                    e.Node.ExpandAll();
                    return;
                }

                switch (e.Node.ImageKey)
                {
                    case "database":
                        _db.ChangeDatabase(e.Node.Name);
                        e.FillTableToTree(_db, ctxTableMenu, "dbtable", "TABLES", "table", "SHOW TABLES", "table_name");
                        e.FillTableToTree(_db, ctxTableMenu, "dbstable", "STABLES", "stable", "SHOW STABLES ", "name");
                        var stable = e.Node.Nodes["dbstable"];
                        var table = e.Node.Nodes["dbtable"];
                        foreach (TreeNode item in stable.Nodes)
                        {
                            var jrows = _db.CreateCommand($"SELECT TBNAME FROM   {item.Name}").ExecuteReader().ToJson();
                            jrows.ToList().ForEach(a =>
                            {
                                var name = a.Value<string>("tbname").RemoveNull();
                                TreeNode tn = table.Nodes[name];
                                table.Nodes.RemoveByKey(name);
                                item.Nodes.Add(tn);
                            });
                        }
                        break;
                    case "stable":
                    case "table":
                        if (e.Node.Tag != null)
                        {
                            var jrows = _db.CreateCommand($"DESCRIBE  {e.Node.Name}").ExecuteReader().ToJson();
                            List<string> _fields = new List<string>();
                            //表最多显示1024个
                            jrows.Take(1024).ToList().ForEach(a =>
                            {
                                var name = a.Value<string>("Field").RemoveNull();
                                _fields.Add(name);
                                if (!e.Node.Nodes.ContainsKey(name))
                                {
                                    var node = e.Node.Nodes.Add(name, name, "field");
                                    node.Tag = a;
                                }
                            });
                            AddSqlSnippet(e.Node.Text, $"SELECT  {string.Join(",", _fields)} FROM  {e.Node.Name} LIMIT  100");
                        }
                        break;
                    default:
                        break;
                }
                e.Node.ExpandAll();

            }
            catch (Exception ex)
            {
                if (_iotdb != null)
                {
                    MessageBox.Show(ex.Message, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void LoadIoTdbNode(TreeNode node)
        {
            switch (node.ImageKey)
            {
                case "database":
                    _currentIoTdbDatabase = node.Name;
                    if (node.Nodes.Count == 0)
                    {
                        if (_connectionOptions?.Model == IoTdbModel.Table)
                        {
                            node.Nodes.Add("tables", "Tables", "iotdb-tables-group");
                        }
                        else
                        {
                            node.Nodes.Add("timeseries", "Time Series", "iotdb-timeseries-group");
                        }
                    }
                    break;
                case "iotdb-timeseries-group":
                    RefreshIoTdbChildren(node, _iotdb.Execute("SHOW TIMESERIES " + GetTreePathPattern(node.Parent.Name)),
                        new[] { "timeseries", "path", "name" }, "iotdb-timeseries");
                    break;
                case "iotdb-tables-group":
                    _currentIoTdbDatabase = node.Parent.Name;
                    RefreshIoTdbChildren(node, _iotdb.Execute("SHOW TABLES", _currentIoTdbDatabase),
                        new[] { "tableName", "table_name", "name" }, "iotdb-table");
                    break;
                case "iotdb-timeseries":
                    AddSqlSnippet(node.Text, "SELECT * FROM " + node.Name + " LIMIT 100");
                    break;
                case "iotdb-table":
                    _currentIoTdbDatabase = node.Parent.Parent.Name;
                    var tableName = QuoteIdentifier(node.Text);
                    var columns = _iotdb.Execute("DESCRIBE " + tableName, _currentIoTdbDatabase);
                    foreach (DataRow row in columns.Rows.Cast<DataRow>().Take(1024))
                    {
                        var fieldName = GetRowValue(row, "columnName", "column_name", "column", "name", "measurement");
                        if (string.IsNullOrWhiteSpace(fieldName) || node.Nodes.ContainsKey(fieldName)) continue;
                        node.Nodes.Add(fieldName, fieldName, "field");
                    }
                    AddSqlSnippet(node.Text, "SELECT * FROM " + tableName + " LIMIT 100");
                    break;
            }
        }

        private static void RefreshIoTdbChildren(TreeNode parent, DataTable rows, string[] columnNames, string imageKey)
        {
            parent.Nodes.Clear();
            foreach (DataRow row in rows.Rows)
            {
                var name = GetRowValue(row, columnNames);
                if (string.IsNullOrWhiteSpace(name) || parent.Nodes.ContainsKey(name)) continue;
                parent.Nodes.Add(name, name, imageKey);
            }
        }

        private static string GetTreePathPattern(string database)
        {
            var path = (database ?? "").TrimEnd('.');
            return path.EndsWith(".**", StringComparison.Ordinal) ? path : path + ".**";
        }

        private static string QuoteIdentifier(string identifier)
        {
            return "`" + (identifier ?? "").Replace("`", "``") + "`";
        }

        #endregion

        #region Editor Tabs

        private void TabResult_Selected(object sender, TabControlEventArgs e)
        {
            if (tabSql.TabPages.Count == 0) return;

            this.LoadResult(this.ActiveTask);
            this.ActiveControl =
                tabResult.SelectedTab == tabGrid ? (Control)grdResult : (Control)txtResult;
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
                this.AddNewTab("(none)", "");
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
            AddNewTab("显示链接", "SHOW CONNECTIONS");
            RunActive();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void menuExcueing_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }



        private void grdResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdResult.Columns[e.ColumnIndex].ValueType == typeof(DateTime))
            {
                var dt = e.Value as DateTime?;
                e.Value = dt.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss.fff zz");
            }
        }

        private void dropDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTab("显示链接", "SHOW CONNECTIONS");
        }

        private void tvwDatabase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            dropDatabaseToolStripMenuItem.Visible = _iotdb == null && e.Node.ImageKey == "database";
            if (_iotdb != null && _connectionOptions?.Model == IoTdbModel.Table)
            {
                var databaseNode = FindIoTdbDatabaseNode(e.Node);
                if (databaseNode != null) _currentIoTdbDatabase = databaseNode.Name;
            }
        }

        private static TreeNode FindIoTdbDatabaseNode(TreeNode node)
        {
            while (node != null && node.ImageKey != "database") node = node.Parent;
            return node;
        }
    }
}
