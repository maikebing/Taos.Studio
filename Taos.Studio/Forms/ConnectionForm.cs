using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taos.Studio.Data;

namespace Taos.Studio.Forms
{
    public partial class ConnectionForm : Form
    {
        private readonly Label _providerLabel;
        private readonly ComboBox _providerComboBox;
        private readonly Label _modelLabel;
        private readonly ComboBox _modelComboBox;
        private readonly Label _timeoutLabel;
        private readonly NumericUpDown _timeoutUpDown;
        private DatabaseProvider _activeProvider;
        private string _taosPassword;
        private bool _loading;

        public DatabaseConnectionOptions Options { get; private set; }

        public ConnectionForm(DatabaseConnectionOptions options)
        {
            InitializeComponent();
            Options = options == null
                ? new DatabaseConnectionOptions()
                : DatabaseConnectionOptions.FromPersistedString(options.ToPersistedString());
            _activeProvider = Options.Provider;
            _taosPassword = Options.Provider == DatabaseProvider.Native
                ? Options.Password
                : new DatabaseConnectionOptions().Password;

            foreach (Control control in Controls)
            {
                control.Top += 104;
            }
            ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height + 104);

            var isChinese = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("zh", StringComparison.OrdinalIgnoreCase);
            _providerLabel = new Label
            {
                AutoSize = false,
                Location = new System.Drawing.Point(17, 25),
                Size = new System.Drawing.Size(141, 20),
                Text = isChinese ? "数据库:" : "Database:"
            };
            _providerComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new System.Drawing.Point(166, 19),
                Size = new System.Drawing.Size(367, 28)
            };
            _providerComboBox.Items.AddRange(new object[] { "Native", "Apache IoTDB" });
            _providerComboBox.SelectedIndexChanged += ProviderComboBox_SelectedIndexChanged;
            _modelLabel = new Label
            {
                AutoSize = false,
                Location = new System.Drawing.Point(17, 61),
                Size = new System.Drawing.Size(141, 20),
                Text = isChinese ? "数据模型:" : "Data model:"
            };
            _modelComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new System.Drawing.Point(166, 55),
                Size = new System.Drawing.Size(367, 28)
            };
            _modelComboBox.Items.AddRange(new object[] { "Tree", "Table" });
            _timeoutLabel = new Label
            {
                AutoSize = false,
                Location = new System.Drawing.Point(17, 95),
                Size = new System.Drawing.Size(141, 20),
                Text = isChinese ? "超时(秒):" : "Timeout (sec):"
            };
            _timeoutUpDown = new NumericUpDown
            {
                Location = new System.Drawing.Point(166, 89),
                Size = new System.Drawing.Size(367, 28),
                Minimum = 1,
                Maximum = 120,
                Value = 30
            };
            Controls.Add(_providerLabel);
            Controls.Add(_providerComboBox);
            Controls.Add(_modelLabel);
            Controls.Add(_modelComboBox);
            Controls.Add(_timeoutLabel);
            Controls.Add(_timeoutUpDown);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Options.Provider = _providerComboBox.SelectedIndex == 1 ? DatabaseProvider.IoTDB : DatabaseProvider.Native;
            Options.Host = txtServerIPAddress.Text.Trim();
            Options.Username = txtUsername.Text;
            Options.Password = txtPassword.Text;
            if (int.TryParse(txtPort.Text, out int port))
            {
                Options.Port = port;
            }
            else
            {
                Options.Port = Options.Provider == DatabaseProvider.IoTDB ? 18080 : 6030;
            }

            if (Options.Provider == DatabaseProvider.Native)
            {
                Options.Charset = (cbxCodePage.SelectedItem as EncodingInfo)?.Name ?? Encoding.Default.EncodingName;
                Options.CreateTaosConnectionString();
            }
            else
            {
                Options.Model = _modelComboBox.SelectedIndex == 1 ? IoTdbModel.Table : IoTdbModel.Tree;
                Options.TimeoutSeconds = (int)_timeoutUpDown.Value;

                try
                {
                    Options.CreateIoTdbBaseUri();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://gitee.com/maikebing/Taos.Studio");
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            _loading = true;
            txtServerIPAddress.Text = Options.Host;
            txtPort.Text = Options.Port.ToString(CultureInfo.InvariantCulture);
            txtUsername.Text = Options.Username;
            txtPassword.Text = Options.Password;
            encodingInfoBindingSource.DataSource = Encoding.GetEncodings();
            encodingInfoBindingSource.Position = encodingInfoBindingSource.IndexOf(
                Encoding.GetEncodings().FirstOrDefault(cp => cp.CodePage == Encoding.Default.CodePage));
            if (string.IsNullOrEmpty(txtServerIPAddress.Text))
            {
                txtServerIPAddress.Text = System.Net.Dns.GetHostName();
            }

            _providerComboBox.SelectedIndex = Options.Provider == DatabaseProvider.IoTDB ? 1 : 0;
            _modelComboBox.SelectedIndex = Options.Model == IoTdbModel.Table ? 1 : 0;
            _timeoutUpDown.Value = Math.Max(_timeoutUpDown.Minimum, Math.Min(_timeoutUpDown.Maximum, Options.TimeoutSeconds));
            _loading = false;
            ApplyProviderSettings();
        }

        private void ProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            var selectedProvider = _providerComboBox.SelectedIndex == 1 ? DatabaseProvider.IoTDB : DatabaseProvider.Native;
            var previousDefaultPort = _activeProvider == DatabaseProvider.IoTDB ? 18080 : 6030;
            var nextDefaultPort = selectedProvider == DatabaseProvider.IoTDB ? 18080 : 6030;
            if (string.IsNullOrWhiteSpace(txtPort.Text) || txtPort.Text == previousDefaultPort.ToString(CultureInfo.InvariantCulture))
            {
                txtPort.Text = nextDefaultPort.ToString(CultureInfo.InvariantCulture);
            }
            if (_activeProvider == DatabaseProvider.Native && selectedProvider == DatabaseProvider.IoTDB)
            {
                _taosPassword = txtPassword.Text;
                txtPassword.Clear();
            }
            else if (_activeProvider == DatabaseProvider.IoTDB && selectedProvider == DatabaseProvider.Native)
            {
                txtPassword.Text = _taosPassword;
            }

            _activeProvider = selectedProvider;
            ApplyProviderSettings();
        }

        private void ApplyProviderSettings()
        {
            var isIoTdb = _providerComboBox.SelectedIndex == 1;
            var isChinese = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("zh", StringComparison.OrdinalIgnoreCase);
            label1.Text = isIoTdb ? (isChinese ? "REST 地址或主机:" : "REST host or URL:") :
                (isChinese ? "服务器 IP 地址:" : "Server IP Address:");
            label5.Visible = cbxCodePage.Visible = !isIoTdb;
            _modelLabel.Visible = _modelComboBox.Visible = isIoTdb;
            _timeoutLabel.Visible = _timeoutUpDown.Visible = isIoTdb;
        }
    }
}
