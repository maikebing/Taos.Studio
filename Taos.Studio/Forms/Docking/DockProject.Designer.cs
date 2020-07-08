using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Docking;

namespace Taos.Studio
{
    partial class DockProject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeProject = new DarkUI.Controls.DarkTreeView();
            this.menuDataBase = new DarkUI.Controls.DarkContextMenu();
            this.svr_DisconstServer = new System.Windows.Forms.ToolStripMenuItem();
            this.svr_NewConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.svr_ShowConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.db_New = new System.Windows.Forms.ToolStripMenuItem();
            this.db_RemoveDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.db_ShowDataBaseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_ShowData = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_ShowData100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_ShowData1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.g_NewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.g_RemovGruop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.g_RenameGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_New = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_del = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_modify = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_query = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_Script = new System.Windows.Forms.ToolStripMenuItem();
            this.sELECTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uPDATEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNSERTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDataBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeProject
            // 
            this.treeProject.AllowMoveNodes = true;
            this.treeProject.ContextMenuStrip = this.menuDataBase;
            this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProject.Location = new System.Drawing.Point(0, 25);
            this.treeProject.MaxDragChange = 20;
            this.treeProject.Name = "treeProject";
            this.treeProject.ShowIcons = true;
            this.treeProject.Size = new System.Drawing.Size(280, 425);
            this.treeProject.TabIndex = 0;
            this.treeProject.Text = "darkTreeView1";
            this.treeProject.SelectedNodesChanged += new System.EventHandler(this.treeProject_SelectedNodesChanged);
            this.treeProject.DoubleClick += new System.EventHandler(this.treeProject_DoubleClick);
            // 
            // menuDataBase
            // 
            this.menuDataBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.menuDataBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.menuDataBase.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuDataBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.svr_NewConnect,
            this.svr_ShowConnect,
            this.svr_DisconstServer,
            this.toolStripSeparator1,
            this.db_New,
            this.db_RemoveDatabase,
            this.db_ShowDataBaseInfo,
            this.toolStripSeparator2,
            this.tb_query,
            this.tb_ShowData,
            this.tb_Script,
            this.tb_New,
            this.tb_modify,
            this.tb_del,
            this.toolStripSeparator3,
            this.g_NewGroup,
            this.g_RemovGruop,
            this.g_RenameGroup});
            this.menuDataBase.Name = "menuDataBase";
            this.menuDataBase.Size = new System.Drawing.Size(199, 385);
            this.menuDataBase.Opening += new System.ComponentModel.CancelEventHandler(this.menuDataBase_Opening);
            // 
            // svr_DisconstServer
            // 
            this.svr_DisconstServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.svr_DisconstServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.svr_DisconstServer.Name = "svr_DisconstServer";
            this.svr_DisconstServer.Size = new System.Drawing.Size(198, 24);
            this.svr_DisconstServer.Text = "断开数据库";
            // 
            // svr_NewConnect
            // 
            this.svr_NewConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.svr_NewConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.svr_NewConnect.Name = "svr_NewConnect";
            this.svr_NewConnect.Size = new System.Drawing.Size(198, 24);
            this.svr_NewConnect.Text = "新建连接";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // svr_ShowConnect
            // 
            this.svr_ShowConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.svr_ShowConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.svr_ShowConnect.Name = "svr_ShowConnect";
            this.svr_ShowConnect.Size = new System.Drawing.Size(198, 24);
            this.svr_ShowConnect.Text = "查看连接";
            // 
            // db_New
            // 
            this.db_New.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.db_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.db_New.Name = "db_New";
            this.db_New.Size = new System.Drawing.Size(198, 24);
            this.db_New.Text = "新建数据库";
            // 
            // db_RemoveDatabase
            // 
            this.db_RemoveDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.db_RemoveDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.db_RemoveDatabase.Name = "db_RemoveDatabase";
            this.db_RemoveDatabase.Size = new System.Drawing.Size(198, 24);
            this.db_RemoveDatabase.Text = "删除数据库";
            // 
            // db_ShowDataBaseInfo
            // 
            this.db_ShowDataBaseInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.db_ShowDataBaseInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.db_ShowDataBaseInfo.Name = "db_ShowDataBaseInfo";
            this.db_ShowDataBaseInfo.Size = new System.Drawing.Size(198, 24);
            this.db_ShowDataBaseInfo.Text = "数据库信息";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // tb_ShowData
            // 
            this.tb_ShowData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_ShowData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tb_ShowData100,
            this.tb_ShowData1000});
            this.tb_ShowData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_ShowData.Name = "tb_ShowData";
            this.tb_ShowData.Size = new System.Drawing.Size(198, 24);
            this.tb_ShowData.Text = "查看表";
            // 
            // tb_ShowData100
            // 
            this.tb_ShowData100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_ShowData100.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_ShowData100.Name = "tb_ShowData100";
            this.tb_ShowData100.Size = new System.Drawing.Size(224, 26);
            this.tb_ShowData100.Text = "最近100条";
            // 
            // tb_ShowData1000
            // 
            this.tb_ShowData1000.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_ShowData1000.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_ShowData1000.Name = "tb_ShowData1000";
            this.tb_ShowData1000.Size = new System.Drawing.Size(224, 26);
            this.tb_ShowData1000.Text = "最近1000条";
            // 
            // g_NewGroup
            // 
            this.g_NewGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.g_NewGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.g_NewGroup.Name = "g_NewGroup";
            this.g_NewGroup.Size = new System.Drawing.Size(198, 24);
            this.g_NewGroup.Text = "新建服务器分组";
            // 
            // g_RemovGruop
            // 
            this.g_RemovGruop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.g_RemovGruop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.g_RemovGruop.Name = "g_RemovGruop";
            this.g_RemovGruop.Size = new System.Drawing.Size(198, 24);
            this.g_RemovGruop.Text = "删除服务器分组";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
            // 
            // g_RenameGroup
            // 
            this.g_RenameGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.g_RenameGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.g_RenameGroup.Name = "g_RenameGroup";
            this.g_RenameGroup.Size = new System.Drawing.Size(198, 24);
            this.g_RenameGroup.Text = "重命名服务器分组";
            // 
            // tb_New
            // 
            this.tb_New.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_New.Name = "tb_New";
            this.tb_New.Size = new System.Drawing.Size(198, 24);
            this.tb_New.Text = "新建表";
            // 
            // tb_del
            // 
            this.tb_del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_del.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_del.Name = "tb_del";
            this.tb_del.Size = new System.Drawing.Size(198, 24);
            this.tb_del.Text = "删除表";
            // 
            // tb_modify
            // 
            this.tb_modify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_modify.Name = "tb_modify";
            this.tb_modify.Size = new System.Drawing.Size(198, 24);
            this.tb_modify.Text = "修改表";
            // 
            // tb_query
            // 
            this.tb_query.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_query.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_query.Name = "tb_query";
            this.tb_query.Size = new System.Drawing.Size(198, 24);
            this.tb_query.Text = "查询表";
            // 
            // tb_Script
            // 
            this.tb_Script.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tb_Script.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sELECTToolStripMenuItem,
            this.uPDATEToolStripMenuItem,
            this.dELETEToolStripMenuItem,
            this.iNSERTToolStripMenuItem});
            this.tb_Script.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_Script.Name = "tb_Script";
            this.tb_Script.Size = new System.Drawing.Size(210, 24);
            this.tb_Script.Text = "脚本";
            // 
            // sELECTToolStripMenuItem
            // 
            this.sELECTToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.sELECTToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sELECTToolStripMenuItem.Name = "sELECTToolStripMenuItem";
            this.sELECTToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sELECTToolStripMenuItem.Text = "SELECT";
            this.sELECTToolStripMenuItem.Click += new System.EventHandler(this.sELECTToolStripMenuItem_Click);
            // 
            // uPDATEToolStripMenuItem
            // 
            this.uPDATEToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.uPDATEToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.uPDATEToolStripMenuItem.Name = "uPDATEToolStripMenuItem";
            this.uPDATEToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.uPDATEToolStripMenuItem.Text = "UPDATE";
            // 
            // dELETEToolStripMenuItem
            // 
            this.dELETEToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.dELETEToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dELETEToolStripMenuItem.Name = "dELETEToolStripMenuItem";
            this.dELETEToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dELETEToolStripMenuItem.Text = "DELETE";
            // 
            // iNSERTToolStripMenuItem
            // 
            this.iNSERTToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.iNSERTToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.iNSERTToolStripMenuItem.Name = "iNSERTToolStripMenuItem";
            this.iNSERTToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.iNSERTToolStripMenuItem.Text = "INSERT";
            // 
            // DockProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeProject);
            this.DefaultDockArea = DarkUI.Docking.DarkDockArea.Left;
            this.DockText = "Database Server Explorer";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Taos.Studio.Properties.Resources.application_16x;
            this.Name = "DockProject";
            this.SerializationKey = "DockProject";
            this.Size = new System.Drawing.Size(280, 450);
            this.menuDataBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkTreeView treeProject;
        private DarkContextMenu menuDataBase;
        private System.Windows.Forms.ToolStripMenuItem svr_DisconstServer;
        private System.Windows.Forms.ToolStripMenuItem svr_NewConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem svr_ShowConnect;
        private System.Windows.Forms.ToolStripMenuItem db_New;
        private System.Windows.Forms.ToolStripMenuItem db_RemoveDatabase;
        private System.Windows.Forms.ToolStripMenuItem db_ShowDataBaseInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tb_ShowData;
        private System.Windows.Forms.ToolStripMenuItem tb_ShowData100;
        private System.Windows.Forms.ToolStripMenuItem tb_ShowData1000;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem g_NewGroup;
        private System.Windows.Forms.ToolStripMenuItem g_RemovGruop;
        private System.Windows.Forms.ToolStripMenuItem g_RenameGroup;
        private System.Windows.Forms.ToolStripMenuItem tb_New;
        private System.Windows.Forms.ToolStripMenuItem tb_modify;
        private System.Windows.Forms.ToolStripMenuItem tb_del;
        private System.Windows.Forms.ToolStripMenuItem tb_query;
        private System.Windows.Forms.ToolStripMenuItem tb_Script;
        private System.Windows.Forms.ToolStripMenuItem sELECTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uPDATEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNSERTToolStripMenuItem;
    }
}
