﻿using ICSharpCode.TextEditor;

namespace Taos.Studio
{
    partial class MainForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm1));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tvwDatabase = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.tabText = new System.Windows.Forms.TabPage();
            this.tabSql = new System.Windows.Forms.TabControl();
            this.stbStatus = new System.Windows.Forms.StatusStrip();
            this.lblCursor = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblResultCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgRunning = new System.Windows.Forms.ToolStripProgressBar();
            this.lblElapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.tlbSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnShowConnections = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.ctxTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuQueryAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQueryCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDropCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.imgCodeCompletion = new System.Windows.Forms.ImageList(this.components);
            this.ctxDataBaseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSql = new  TextEditorControl();
            this.txtResult = new  TextEditorControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.tabText.SuspendLayout();
            this.stbStatus.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.ctxTableMenu.SuspendLayout();
            this.ctxMenuRoot.SuspendLayout();
            this.ctxDataBaseMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            resources.ApplyResources(this.splitMain, "splitMain");
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tvwDatabase);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitRight);
            this.splitMain.Panel2.Controls.Add(this.tabSql);
            this.splitMain.TabStop = false;
            // 
            // tvwDatabase
            // 
            resources.ApplyResources(this.tvwDatabase, "tvwDatabase");
            this.tvwDatabase.ImageList = this.imgList;
            this.tvwDatabase.Name = "tvwDatabase";
            this.tvwDatabase.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvwCols_NodeMouseDoubleClick);
            this.tvwDatabase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TvwCols_MouseUp);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "database");
            this.imgList.Images.SetKeyName(1, "folder");
            this.imgList.Images.SetKeyName(2, "table");
            this.imgList.Images.SetKeyName(3, "table_gear");
            // 
            // splitRight
            // 
            resources.ApplyResources(this.splitRight, "splitRight");
            this.splitRight.Name = "splitRight";
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitRight.Panel1.Controls.Add(this.txtSql);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.tabResult);
            // 
            // tabResult
            // 
            resources.ApplyResources(this.tabResult, "tabResult");
            this.tabResult.Controls.Add(this.tabGrid);
            this.tabResult.Controls.Add(this.tabText);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.TabStop = false;
            this.tabResult.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabResult_Selected);
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.grdResult);
            resources.ApplyResources(this.tabGrid, "tabGrid");
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.grdResult, "grdResult");
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Name = "grdResult";
            this.grdResult.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GrdResult_CellBeginEdit);
            this.grdResult.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdResult_CellEndEdit);
            this.grdResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GrdResult_RowPostPaint);
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.txtResult);
            resources.ApplyResources(this.tabText, "tabText");
            this.tabText.Name = "tabText";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // tabSql
            // 
            resources.ApplyResources(this.tabSql, "tabSql");
            this.tabSql.Name = "tabSql";
            this.tabSql.SelectedIndex = 0;
            this.tabSql.TabStop = false;
            this.tabSql.SelectedIndexChanged += new System.EventHandler(this.TabSql_SelectedIndexChanged);
            this.tabSql.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabSql_Selected);
            this.tabSql.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabSql_MouseClick);
            // 
            // stbStatus
            // 
            this.stbStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCursor,
            this.lblResultCount,
            this.prgRunning,
            this.lblElapsed});
            resources.ApplyResources(this.stbStatus, "stbStatus");
            this.stbStatus.Name = "stbStatus";
            // 
            // lblCursor
            // 
            this.lblCursor.Name = "lblCursor";
            resources.ApplyResources(this.lblCursor, "lblCursor");
            this.lblCursor.Spring = true;
            // 
            // lblResultCount
            // 
            resources.ApplyResources(this.lblResultCount, "lblResultCount");
            this.lblResultCount.Name = "lblResultCount";
            // 
            // prgRunning
            // 
            this.prgRunning.Name = "prgRunning";
            resources.ApplyResources(this.prgRunning, "prgRunning");
            // 
            // lblElapsed
            // 
            resources.ApplyResources(this.lblElapsed, "lblElapsed");
            this.lblElapsed.Name = "lblElapsed";
            // 
            // tlbMain
            // 
            this.tlbMain.GripMargin = new System.Windows.Forms.Padding(3);
            this.tlbMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.tlbSep1,
            this.btnRefresh,
            this.tlbSep2,
            this.btnRun,
            this.toolStripSeparator1,
            this.BtnShowConnections,
            this.btnAbout});
            resources.ApplyResources(this.tlbMain, "tlbMain");
            this.tlbMain.Name = "tlbMain";
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::Taos.Studio.Properties.Resources.database_connect;
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(3);
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // tlbSep1
            // 
            this.tlbSep1.Name = "tlbSep1";
            resources.ApplyResources(this.tlbSep1, "tlbSep1");
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Taos.Studio.Properties.Resources.arrow_refresh;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(3);
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // tlbSep2
            // 
            this.tlbSep2.Name = "tlbSep2";
            resources.ApplyResources(this.tlbSep2, "tlbSep2");
            // 
            // btnRun
            // 
            this.btnRun.Image = global::Taos.Studio.Properties.Resources.resultset_next;
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(3);
            this.btnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // BtnShowConnections
            // 
            this.BtnShowConnections.Image = global::Taos.Studio.Properties.Resources.information;
            resources.ApplyResources(this.BtnShowConnections, "BtnShowConnections");
            this.BtnShowConnections.Name = "BtnShowConnections";
            this.BtnShowConnections.Click += new System.EventHandler(this.BtnShowConnections_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::Taos.Studio.Properties.Resources.logo;
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // ctxTableMenu
            // 
            this.ctxTableMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQueryAll,
            this.mnuInsert,
            this.mnuQueryCount,
            this.mnuSep1,
            this.mnuDropCollection});
            this.ctxTableMenu.Name = "ctxMenu";
            resources.ApplyResources(this.ctxTableMenu, "ctxTableMenu");
            this.ctxTableMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenu_ItemClicked);
            // 
            // mnuQueryAll
            // 
            this.mnuQueryAll.Image = global::Taos.Studio.Properties.Resources.table_lightning;
            this.mnuQueryAll.Name = "mnuQueryAll";
            resources.ApplyResources(this.mnuQueryAll, "mnuQueryAll");
            this.mnuQueryAll.Tag = "SELECT * FROM {0};";
            // 
            // mnuInsert
            // 
            this.mnuInsert.Image = global::Taos.Studio.Properties.Resources.table_save;
            this.mnuInsert.Name = "mnuInsert";
            resources.ApplyResources(this.mnuInsert, "mnuInsert");
            this.mnuInsert.Tag = "INSERT INTO  {0}  VALUES (   )";
            // 
            // mnuQueryCount
            // 
            this.mnuQueryCount.Image = global::Taos.Studio.Properties.Resources.table;
            this.mnuQueryCount.Name = "mnuQueryCount";
            resources.ApplyResources(this.mnuQueryCount, "mnuQueryCount");
            this.mnuQueryCount.Tag = "SELECT COUNT(*) FROM {0};";
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            resources.ApplyResources(this.mnuSep1, "mnuSep1");
            // 
            // mnuDropCollection
            // 
            this.mnuDropCollection.Image = global::Taos.Studio.Properties.Resources.table_delete;
            this.mnuDropCollection.Name = "mnuDropCollection";
            resources.ApplyResources(this.mnuDropCollection, "mnuDropCollection");
            this.mnuDropCollection.Tag = "DROP TABLE {0};";
            // 
            // ctxMenuRoot
            // 
            this.ctxMenuRoot.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInfo,
            this.toolStripSeparator3,
            this.showConnectionsToolStripMenuItem,
            this.toolStripMenuItem2});
            this.ctxMenuRoot.Name = "ctxMenu";
            resources.ApplyResources(this.ctxMenuRoot, "ctxMenuRoot");
            this.ctxMenuRoot.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenuRoot_ItemClicked);
            // 
            // mnuInfo
            // 
            this.mnuInfo.Image = global::Taos.Studio.Properties.Resources.information;
            this.mnuInfo.Name = "mnuInfo";
            resources.ApplyResources(this.mnuInfo, "mnuInfo");
            this.mnuInfo.Tag = "SELECT server_version();";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // showConnectionsToolStripMenuItem
            // 
            this.showConnectionsToolStripMenuItem.Name = "showConnectionsToolStripMenuItem";
            resources.ApplyResources(this.showConnectionsToolStripMenuItem, "showConnectionsToolStripMenuItem");
            this.showConnectionsToolStripMenuItem.Click += new System.EventHandler(this.BtnShowConnections_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Tag = "CREATE DATABASE  IF NOT EXISTS db_name ";
            // 
            // imgCodeCompletion
            // 
            this.imgCodeCompletion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgCodeCompletion.ImageStream")));
            this.imgCodeCompletion.TransparentColor = System.Drawing.Color.Transparent;
            this.imgCodeCompletion.Images.SetKeyName(0, "METHOD");
            this.imgCodeCompletion.Images.SetKeyName(1, "COLLECTION");
            this.imgCodeCompletion.Images.SetKeyName(2, "FIELD");
            this.imgCodeCompletion.Images.SetKeyName(3, "KEYWORD");
            this.imgCodeCompletion.Images.SetKeyName(4, "SYSTEM");
            this.imgCodeCompletion.Images.SetKeyName(5, "SYSTEM_FN");
            // 
            // ctxDataBaseMenu
            // 
            this.ctxDataBaseMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxDataBaseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.toolStripMenuItem4});
            this.ctxDataBaseMenu.Name = "ctxMenu";
            resources.ApplyResources(this.ctxDataBaseMenu, "ctxDataBaseMenu");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Taos.Studio.Properties.Resources.table_lightning;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Tag = "USE {0};";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::Taos.Studio.Properties.Resources.table_delete;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Tag = "DROP DATABASE  IF EXISTS {0};";
            // 
            // txtSql
            // 
            resources.ApplyResources(this.txtSql, "txtSql");
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSql.ConvertTabsToSpaces = true;
            this.txtSql.Highlighting = "SQL";
            this.txtSql.Name = "txtSql";
            this.txtSql.ShowLineNumbers = false;
            this.txtSql.ShowVRuler = false;
            // 
            // txtResult
            // 
            resources.ApplyResources(this.txtResult, "txtResult");
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Highlighting = "JSON";
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ShowLineNumbers = false;
            this.txtResult.ShowVRuler = false;
            // 
            // MainForm1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.splitMain);
            this.KeyPreview = true;
            this.Name = "MainForm1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.tabText.ResumeLayout(false);
            this.stbStatus.ResumeLayout(false);
            this.stbStatus.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ctxTableMenu.ResumeLayout(false);
            this.ctxMenuRoot.ResumeLayout(false);
            this.ctxDataBaseMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView tvwDatabase;
        private System.Windows.Forms.StatusStrip stbStatus;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsed;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.ToolStripStatusLabel lblResultCount;
        private System.Windows.Forms.ToolStrip tlbMain;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripSeparator tlbSep1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator tlbSep2;
        private System.Windows.Forms.ToolStripStatusLabel lblCursor;
        private System.Windows.Forms.ToolStripProgressBar prgRunning;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip ctxTableMenu;
        private TextEditorControl txtSql;
        private System.Windows.Forms.TabControl tabSql;
        private System.Windows.Forms.ToolStripMenuItem mnuDropCollection;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryAll;
        private System.Windows.Forms.ContextMenuStrip ctxMenuRoot;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryCount;
        private TextEditorControl txtResult;
        private System.Windows.Forms.ImageList imgCodeCompletion;
        private System.Windows.Forms.ToolStripButton BtnShowConnections;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripMenuItem showConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuInsert;
        private System.Windows.Forms.ContextMenuStrip ctxDataBaseMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}

