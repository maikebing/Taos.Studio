using DarkUI.Controls;
using DarkUI.Docking;

namespace Taos.Studio
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnuMain = new DarkUI.Controls.DarkMenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.checkableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkableWithIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkedWithIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMain = new DarkUI.Controls.DarkToolStrip();
            this.btnNewFile = new System.Windows.Forms.ToolStripButton();
            this.stripMain = new DarkUI.Controls.DarkStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DockPanel = new DarkUI.Docking.DarkDockPanel();
            this.darkSeparator1 = new DarkUI.Controls.DarkSeparator();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.mnuMain.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.stripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuTools,
            this.mnuWindow,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(944, 25);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "darkMenuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewFile,
            this.toolStripSeparator1,
            this.mnuClose});
            this.mnuFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(57, 21);
            this.mnuFile.Text = "&Server";
            // 
            // mnuNewFile
            // 
            this.mnuNewFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuNewFile.Image = global::Taos.Studio.Properties.Resources.NewFile_6276;
            this.mnuNewFile.Name = "mnuNewFile";
            this.mnuNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewFile.Size = new System.Drawing.Size(264, 26);
            this.mnuNewFile.Text = "&Create New Connection";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
            // 
            // mnuClose
            // 
            this.mnuClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuClose.Image = global::Taos.Studio.Properties.Resources.Close_16xLG;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuClose.Size = new System.Drawing.Size(264, 26);
            this.mnuClose.Text = "&Close";
            // 
            // mnuView
            // 
            this.mnuView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDialog});
            this.mnuView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(47, 21);
            this.mnuView.Text = "&View";
            // 
            // mnuDialog
            // 
            this.mnuDialog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuDialog.Image = global::Taos.Studio.Properties.Resources.properties_16xLG;
            this.mnuDialog.Name = "mnuDialog";
            this.mnuDialog.Size = new System.Drawing.Size(184, 26);
            this.mnuDialog.Text = "&Dialog test";
            // 
            // mnuTools
            // 
            this.mnuTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkableToolStripMenuItem,
            this.checkableWithIconToolStripMenuItem,
            this.toolStripSeparator2,
            this.checkedToolStripMenuItem,
            this.checkedWithIconToolStripMenuItem});
            this.mnuTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(52, 21);
            this.mnuTools.Text = "&Tools";
            // 
            // checkableToolStripMenuItem
            // 
            this.checkableToolStripMenuItem.CheckOnClick = true;
            this.checkableToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.checkableToolStripMenuItem.Name = "checkableToolStripMenuItem";
            this.checkableToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.checkableToolStripMenuItem.Text = "Checkable";
            // 
            // checkableWithIconToolStripMenuItem
            // 
            this.checkableWithIconToolStripMenuItem.CheckOnClick = true;
            this.checkableWithIconToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.checkableWithIconToolStripMenuItem.Image = global::Taos.Studio.Properties.Resources.properties_16xLG;
            this.checkableWithIconToolStripMenuItem.Name = "checkableWithIconToolStripMenuItem";
            this.checkableWithIconToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.checkableWithIconToolStripMenuItem.Text = "Checkable with icon";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // checkedToolStripMenuItem
            // 
            this.checkedToolStripMenuItem.Checked = true;
            this.checkedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.checkedToolStripMenuItem.Name = "checkedToolStripMenuItem";
            this.checkedToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.checkedToolStripMenuItem.Text = "Checked";
            // 
            // checkedWithIconToolStripMenuItem
            // 
            this.checkedWithIconToolStripMenuItem.Checked = true;
            this.checkedWithIconToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkedWithIconToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.checkedWithIconToolStripMenuItem.Image = global::Taos.Studio.Properties.Resources.properties_16xLG;
            this.checkedWithIconToolStripMenuItem.Name = "checkedWithIconToolStripMenuItem";
            this.checkedWithIconToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.checkedWithIconToolStripMenuItem.Text = "Checked with icon";
            // 
            // mnuWindow
            // 
            this.mnuWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProject,
            this.mnuProperties,
            this.mnuConsole,
            this.mnuLayers,
            this.mnuHistory});
            this.mnuWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(67, 21);
            this.mnuWindow.Text = "&Window";
            // 
            // mnuProject
            // 
            this.mnuProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuProject.Image = global::Taos.Studio.Properties.Resources.application_16x;
            this.mnuProject.Name = "mnuProject";
            this.mnuProject.Size = new System.Drawing.Size(184, 26);
            this.mnuProject.Text = "&Project Explorer";
            // 
            // mnuProperties
            // 
            this.mnuProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuProperties.Image = global::Taos.Studio.Properties.Resources.properties_16xLG;
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.Size = new System.Drawing.Size(184, 26);
            this.mnuProperties.Text = "P&roperties";
            // 
            // mnuConsole
            // 
            this.mnuConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuConsole.Image = global::Taos.Studio.Properties.Resources.Console;
            this.mnuConsole.Name = "mnuConsole";
            this.mnuConsole.Size = new System.Drawing.Size(184, 26);
            this.mnuConsole.Text = "&Console";
            // 
            // mnuLayers
            // 
            this.mnuLayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuLayers.Image = global::Taos.Studio.Properties.Resources.Collection_16xLG;
            this.mnuLayers.Name = "mnuLayers";
            this.mnuLayers.Size = new System.Drawing.Size(184, 26);
            this.mnuLayers.Text = "&Layers";
            // 
            // mnuHistory
            // 
            this.mnuHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnuHistory.Image")));
            this.mnuHistory.Name = "mnuHistory";
            this.mnuHistory.Size = new System.Drawing.Size(184, 26);
            this.mnuHistory.Text = "&History";
            // 
            // mnuHelp
            // 
            this.mnuHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(47, 21);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mnuAbout.Image = global::Taos.Studio.Properties.Resources.StatusAnnotations_Information_16xLG_color;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(184, 26);
            this.mnuAbout.Text = "&About DarkUI";
            // 
            // toolMain
            // 
            this.toolMain.AutoSize = false;
            this.toolMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewFile});
            this.toolMain.Location = new System.Drawing.Point(0, 27);
            this.toolMain.Name = "toolMain";
            this.toolMain.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolMain.Size = new System.Drawing.Size(944, 28);
            this.toolMain.TabIndex = 1;
            this.toolMain.Text = "darkToolStrip1";
            // 
            // btnNewFile
            // 
            this.btnNewFile.AutoSize = false;
            this.btnNewFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.btnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnNewFile.Image = global::Taos.Studio.Properties.Resources.NewFile_6276;
            this.btnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(24, 24);
            this.btnNewFile.Text = "New file";
            // 
            // stripMain
            // 
            this.stripMain.AutoSize = false;
            this.stripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.stripMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.stripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel5,
            this.toolStripProgressBar1});
            this.stripMain.Location = new System.Drawing.Point(0, 618);
            this.stripMain.Name = "stripMain";
            this.stripMain.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.stripMain.Size = new System.Drawing.Size(944, 24);
            this.stripMain.SizingGrip = false;
            this.stripMain.TabIndex = 2;
            this.stripMain.Text = "darkStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(1, 0, 50, 0);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 16);
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Margin = new System.Windows.Forms.Padding(0, 0, 50, 2);
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(648, 14);
            this.toolStripStatusLabel6.Spring = true;
            this.toolStripStatusLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(53, 16);
            this.toolStripStatusLabel5.Text = "120 MB";
            this.toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DockPanel
            // 
            this.DockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel.Location = new System.Drawing.Point(0, 55);
            this.DockPanel.Name = "DockPanel";
            this.DockPanel.Size = new System.Drawing.Size(944, 563);
            this.DockPanel.TabIndex = 3;
            // 
            // darkSeparator1
            // 
            this.darkSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.darkSeparator1.Location = new System.Drawing.Point(0, 25);
            this.darkSeparator1.Name = "darkSeparator1";
            this.darkSeparator1.Size = new System.Drawing.Size(944, 2);
            this.darkSeparator1.TabIndex = 4;
            this.darkSeparator1.Text = "darkSeparator1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 10);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 642);
            this.Controls.Add(this.DockPanel);
            this.Controls.Add(this.stripMain);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.darkSeparator1);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taos.Studio";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.stripMain.ResumeLayout(false);
            this.stripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkMenuStrip mnuMain;
        private DarkToolStrip toolMain;
        private DarkStatusStrip stripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuDialog;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripButton btnNewFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DarkDockPanel DockPanel;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuConsole;
        private System.Windows.Forms.ToolStripMenuItem mnuLayers;
        private System.Windows.Forms.ToolStripMenuItem mnuHistory;
        private DarkSeparator darkSeparator1;
        private System.Windows.Forms.ToolStripMenuItem checkableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkableWithIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem checkedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkedWithIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}

