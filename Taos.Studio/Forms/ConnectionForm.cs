using Maikebing.Data.Taos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taos.Studio.Forms
{
    public partial class ConnectionForm : Form
    {
        private const long MB = 1024 * 1024;

        public TaosConnectionStringBuilder ConnectionString = new TaosConnectionStringBuilder();

        public ConnectionForm(TaosConnectionStringBuilder cs)
        {
            InitializeComponent();

            txtServerIPAddress.Text = cs.DataSource;
            txtPort.Text = cs.Port.ToString();
            txtUsername.Text = cs.Username;
            txtPassword.Text = cs.Password;
            encodingInfoBindingSource.DataSource = System.Text.Encoding.GetEncodings();
            encodingInfoBindingSource.Position= encodingInfoBindingSource.IndexOf( System.Text.Encoding.GetEncodings().ToList().FirstOrDefault(e => e.CodePage == System.Text.Encoding.Default.CodePage));
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
           

            this.ConnectionString.DataSource = txtServerIPAddress.Text;
            this.ConnectionString.Username = txtUsername.Text;
            this.ConnectionString.Password = txtPassword.Text;
            ConnectionString["Charset"] = (cbxCodePage.SelectedItem as EncodingInfo).Name ?? System.Text.Encoding.Default.EncodingName;
           if (int.TryParse(txtPort.Text,out int port ))
            {
                this.ConnectionString.Port =  port;
            }
           else
            {
                this.ConnectionString.Port = 6030;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.maikebing.com/Taos.PrivacyPolicy");
        }

       
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://taos.studio/");
        }
    }
}
