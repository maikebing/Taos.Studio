using ICSharpCode.TextEditor;

namespace Taos.Studio
{
    partial class DockDocument
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
            this.cmbOptions = new DarkUI.Controls.DarkDropdownList();
            this.txtSql = new TextEditorControl();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.tabText = new System.Windows.Forms.TabPage();
            this.txtResult = new  TextEditorControl();
            this.darkSeparator1 = new DarkUI.Controls.DarkSeparator();
            this.tabResult.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.tabText.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbOptions
            // 
            this.cmbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOptions.DropdownDirection = System.Windows.Forms.ToolStripDropDownDirection.AboveRight;
            this.cmbOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptions.Location = new System.Drawing.Point(0, 485);
            this.cmbOptions.MaxHeight = 300;
            this.cmbOptions.Name = "cmbOptions";
            this.cmbOptions.ShowBorder = false;
            this.cmbOptions.Size = new System.Drawing.Size(65, 15);
            this.cmbOptions.TabIndex = 2;
            this.cmbOptions.Text = "darkComboBox1";
            // 
            // txtSql
            // 
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSql.ConvertTabsToSpaces = true;
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSql.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSql.Highlighting = "SQL";
            this.txtSql.Location = new System.Drawing.Point(0, 0);
            this.txtSql.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSql.Name = "txtSql";
            this.txtSql.ShowLineNumbers = false;
            this.txtSql.ShowVRuler = false;
            this.txtSql.Size = new System.Drawing.Size(869, 235);
            this.txtSql.TabIndex = 3;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.tabGrid);
            this.tabResult.Controls.Add(this.tabText);
            this.tabResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResult.Location = new System.Drawing.Point(0, 235);
            this.tabResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(869, 265);
            this.tabResult.TabIndex = 4;
            this.tabResult.TabStop = false;
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.grdResult);
            this.tabGrid.Location = new System.Drawing.Point(4, 29);
            this.tabGrid.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabGrid.Size = new System.Drawing.Size(861, 232);
            this.tabGrid.TabIndex = 0;
            this.tabGrid.Text = "Grid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.Location = new System.Drawing.Point(3, 5);
            this.grdResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdResult.Name = "grdResult";
            this.grdResult.RowHeadersWidth = 51;
            this.grdResult.Size = new System.Drawing.Size(855, 222);
            this.grdResult.TabIndex = 0;
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.txtResult);
            this.tabText.Location = new System.Drawing.Point(4, 29);
            this.tabText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabText.Size = new System.Drawing.Size(861, 232);
            this.tabText.TabIndex = 3;
            this.tabText.Text = "Text";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResult.Highlighting = "JSON";
            this.txtResult.Location = new System.Drawing.Point(6, 5);
            this.txtResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ShowLineNumbers = false;
            this.txtResult.ShowVRuler = false;
            this.txtResult.Size = new System.Drawing.Size(928, 463);
            this.txtResult.TabIndex = 1;
            // 
            // darkSeparator1
            // 
            this.darkSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.darkSeparator1.Location = new System.Drawing.Point(0, 235);
            this.darkSeparator1.Name = "darkSeparator1";
            this.darkSeparator1.Size = new System.Drawing.Size(869, 2);
            this.darkSeparator1.TabIndex = 5;
            this.darkSeparator1.Text = "darkSeparator1";
            // 
            // DockDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.darkSeparator1);
            this.Controls.Add(this.tabResult);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.cmbOptions);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockDocument";
            this.Size = new System.Drawing.Size(869, 500);
            this.tabResult.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.tabText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DarkUI.Controls.DarkDropdownList cmbOptions;
        private  TextEditorControl txtSql;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.TabPage tabText;
        private TextEditorControl txtResult;
        private DarkUI.Controls.DarkSeparator darkSeparator1;
    }
}
