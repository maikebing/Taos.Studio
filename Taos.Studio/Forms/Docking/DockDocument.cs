using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Docking;
using DarkUI.Forms;
using Maikebing.Data.Taos;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taos.Studio.Classes;
using Taos.Studio.Properties;
using WindowsForm.Console.Extensions;

namespace Taos.Studio
{
    public partial class DockDocument : DarkDocument
    {
        private readonly NodeTag _nodeTag;
        private readonly TaosConnection _db;
        private readonly MainForm _owner;
        private readonly TaskData _data;

        public DockDocument()
        {
            InitializeComponent();
        }

        public DockDocument(MainForm owner, string title , string sql,NodeTag nt, Image icon)
            : this()
        {
            DockText = title;
            Icon = icon;
            _nodeTag = nt;
            txtSql.Text = sql;
            _db = _nodeTag.Taos;
            _owner = owner;
            _data = new TaskData();
           
        }

        

        public override void Close()
        {
            var result = DarkMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;

            base.Close();
        }

        public void Run()
        {

            var sql = txtSql.ActiveTextAreaControl.SelectionManager.SelectedText.Length > 0 ?
            txtSql.ActiveTextAreaControl.SelectionManager.SelectedText :
            txtSql.Text;
            grdResult.Clear();
            txtResult.Clear();
            _owner.lblResultCount.Visible = false;
            _owner.lblElapsed.Text = Resources.Running;
            _owner.prgRunning.Style = ProgressBarStyle.Marquee;
            DateTime dt = DateTime.Now;
            _owner.WriteLine($"Run {sql}", Color.Green, true);
            Task.Run(() =>
           {
               try
               {
                   using (var reader = _db.CreateCommand(sql).ExecuteReader())
                   {
                       _data.ReadResult(reader);
                       _data.Elapsed = DateTime.Now.Subtract(dt);
                   }
               }
               catch (Exception ex)
               {
                   this.Invoke((MethodInvoker)delegate
                   {
                       _owner.WriteLine($"{ex.Message}", Color.Red, true);
                   });
               }

           }).ContinueWith(t =>
           {
           this.Invoke((MethodInvoker)  delegate   
           {

               _owner.lblResultCount.Visible = true;
               _owner.lblElapsed.Text = _data.Elapsed.ToString();
               _owner.prgRunning.Style = ProgressBarStyle.Blocks;
               _owner.lblResultCount.Text =
                   _data.Result == null ? "" :
                   _data.Result.Count == 0 ? Resources.NoDocuments :
                   _data.Result.Count == 1 ? Resources._1Document :
                   _data.Result.Count + (_data.LimitExceeded ? "+" : "") + Resources.Documents;

               if (_data.Exception != null)
               {
                   txtResult.BindErrorMessage(_data.Sql, _data.Exception);
                   grdResult.BindErrorMessage(_data.Sql, _data.Exception);
               }
               else if (_data.Result != null)
               {

                   grdResult.BindBsonData(_data);
                   _data.IsGridLoaded = true;

               }

           });
           });
        }
        private void LoadResult(TaskData data)
        {
            if (data == null) return;

            _owner. btnRun.Enabled = !data.Executing;

            if (data.Executing)
            {
             
            }
            else
            {
             
            }
        }

    }
}
