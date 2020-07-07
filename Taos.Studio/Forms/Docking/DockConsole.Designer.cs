using DarkUI.Controls;
using DarkUI.Docking;

namespace Taos.Studio
{
    partial class DockConsole
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
            this.fConsole1 = new WindowsForm.Console.FConsole();
            this.SuspendLayout();
            // 
            // fConsole1
            // 
            this.fConsole1.Arguments = new string[0];
            this.fConsole1.AutoScrollToEndLine = true;
            this.fConsole1.BackColor = System.Drawing.Color.Black;
            this.fConsole1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fConsole1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fConsole1.Font = new System.Drawing.Font("Consolas", 10F);
            this.fConsole1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(216)))), ((int)(((byte)(194)))));
            this.fConsole1.HyperlinkColor = System.Drawing.Color.Empty;
            this.fConsole1.Location = new System.Drawing.Point(0, 25);
            this.fConsole1.MinimumSize = new System.Drawing.Size(470, 200);
            this.fConsole1.Name = "fConsole1";
            this.fConsole1.ReadOnly = true;
            this.fConsole1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.fConsole1.SecureReadLine = true;
            this.fConsole1.Size = new System.Drawing.Size(708, 441);
            this.fConsole1.State = WindowsForm.Console.Enums.ConsoleState.Writing;
            this.fConsole1.TabIndex = 0;
            this.fConsole1.Text = "";
            this.fConsole1.Title = "";
            // 
            // DockConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fConsole1);
            this.DefaultDockArea = DarkUI.Docking.DarkDockArea.Bottom;
            this.DockText = "Console";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Taos.Studio.Properties.Resources.Console;
            this.Name = "DockConsole";
            this.SerializationKey = "DockConsole";
            this.Size = new System.Drawing.Size(708, 466);
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsForm.Console.FConsole fConsole1;
    }
}
