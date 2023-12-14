using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.Froms.Connection
{
    public partial class FrmCauHinhCSDL : XtraFormKira
    {
        private Chuoi.EncryptDecrypt ed = new Chuoi.EncryptDecrypt();

        public FrmCauHinhCSDL()
        {
            InitializeComponent();
        }

        private void FrmChSQL_Load(object sender, EventArgs e)
        {
            txtHost.Text = ClsConnection.Server.Equals("Bad Data.\r\n") ? string.Empty : ClsConnection.Server.Trim();
            txtDataBase.Text = ClsConnection.Database.Equals("Bad Data.\r\n") ? string.Empty : ClsConnection.Database.Trim();
            txtUserName.Text = ClsConnection.Username.Equals("Bad Data.\r\n") ? string.Empty : ClsConnection.Username.Trim();
        }

        private void FrmChSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string host = txtHost.Text.Trim();
                string csdl = txtDataBase.Text.Trim();
                string username = txtUserName.Text.Trim();
                string password = txtPassWord.Text.Trim();

                if (host == "")
                {
                    ClsBaoLoi.ThongTin("Tên server không thể rỗng.\nBạn vui lòng nhập Tên server.");
                    txtHost.Focus();
                    return;
                }

                if (csdl == "")
                {
                    ClsBaoLoi.ThongTin("Tên CSDL không thể rỗng.\nBạn vui lòng nhập Tên CSDL.");
                    txtDataBase.Focus();
                    return;
                }

                if (username == "")
                {
                    ClsBaoLoi.ThongTin("Tài khoản không thể rỗng.\nBạn vui lòng nhập Tài khoản.");
                    txtUserName.Focus();
                    return;
                }

                if (password == "")
                {
                    ClsBaoLoi.ThongTin("Mật khẩu không thể rỗng.\nBạn vui lòng nhập Mật khẩu.");
                    txtPassWord.Focus();
                    return;
                }

                SqlConnection connTemp = new SqlConnection();
               // Server=BINHLAO-PC\SQLEXPRESS;Database=NCS;uid=sa;pwd=sa
                connTemp.ConnectionString = string.Format("Server = {0}; Database = {1}; Uid = {2}; Pwd = {3};;MultipleActiveResultSets=true;Persist Security Info=true;", host, csdl, username, password);
                connTemp.Open();
                DialogResult = DialogResult.OK;
                SaveModel(host, csdl, username, password);
                ClsConnection.MySqlConn = null;
            }
            catch(Exception ex)
            {
               // ClsMsgBox.LoiChung(ex, false);
                ClsMsgBox.Loi("Không thể kết nối đến server!");
                return;
            }

            global::PhoHa7.Library.Properties.Settings.Default.m_server = ed.EncryptText(txtHost.Text.Trim());
            global::PhoHa7.Library.Properties.Settings.Default.m_username = ed.EncryptText(txtUserName.Text.Trim());
            global::PhoHa7.Library.Properties.Settings.Default.m_database = ed.EncryptText(txtDataBase.Text.Trim());
            global::PhoHa7.Library.Properties.Settings.Default.m_password = ed.EncryptText(txtPassWord.Text.Trim());
            global::PhoHa7.Library.Properties.Settings.Default.Save();

            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        ConfigurationSaveMode saveMode = ConfigurationSaveMode.Modified;
        void SaveModel(string pServer,string pDatabase,string pUsername,string pPassword)
        {
            try
            {
                string server = pServer;
                string database = pDatabase;
                string username = pUsername;
                string password = pPassword;

                // Get the application configuration file.
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection selection = config.ConnectionStrings;
                if (selection != null)
                    if (selection.SectionInformation.IsProtected)
                    {
                        selection.SectionInformation.UnprotectSection();
                        saveMode = ConfigurationSaveMode.Modified;
                    }

                //config.AppSettings.Settings["Server"].Value = server;
                //config.AppSettings.Settings["DatabaseName"].Value = database;
                //config.AppSettings.Settings["User"].Value = username;
                //config.AppSettings.Settings["Password"].Value = password;
                //config.Save(ConfigurationSaveMode.Modified);
                //ConfigurationManager.RefreshSection("appSettings");
                List<String> entityList = new List<string>();
                List<String> newEntityList = new List<string>();
                for (int i = 0; i < selection.ConnectionStrings.Count; i++)
                {
                    if (selection.ConnectionStrings[i].ProviderName == "System.Data.EntityClient")
                    {
                        System.Data.EntityClient.EntityConnection c = new System.Data.EntityClient.EntityConnection(selection.ConnectionStrings[i].ConnectionString);
                        //server=localhost;user id=root;database=sumen;persistsecurityinfo=False;password=123456
                        c.StoreConnection.ConnectionString =
                            string.Format(@"server={0};database={1};uid={2};password={3};Persist Security Info=True;MultipleActiveResultSets=true", server, database, username, password);
                        selection.ConnectionStrings[i].ConnectionString = c.ConnectionString;
                        entityList.Add(selection.ConnectionStrings[i].Name);
                    }
                }

                
                config.Save(saveMode, true);

                //  Define the Rsa provider name.
                string provider = "RsaProtectedConfigurationProvider";
                //string provider = "DpapiProtectedConfigurationProvider";
                
                ConfigurationSection appSettingStrings =
                config.GetSection("appSettings");
                if (appSettingStrings != null)
                {
                    if (!appSettingStrings.SectionInformation.IsProtected)
                    {
                        if (!appSettingStrings.ElementInformation.IsLocked)
                        {
                            // Protect the section.
                            appSettingStrings.SectionInformation.ProtectSection(provider);

                            appSettingStrings.SectionInformation.ForceSave = true;
                            //config.SaveAs("e:/abc");
                            config.Save(saveMode, true);
                        }
                    }
                }


                // Get the section to protect.
                ConfigurationSection connStrings = config.ConnectionStrings;

                if (connStrings != null)
                {
                    if (!connStrings.SectionInformation.IsProtected)
                    {
                        if (!connStrings.ElementInformation.IsLocked)
                        {
                            // Protect the section.
                            connStrings.SectionInformation.ProtectSection(provider);

                            connStrings.SectionInformation.ForceSave = true;
                            //config.SaveAs("e:/abc1");
                            config.Save(saveMode, true);
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Can't protect, section {0} is locked", connStrings.SectionInformation.Name));
                        }
                    }
                }
                XtraMessageBox.Show("Lưu thông tin cấu hình máy chủ CSDL thành công.\nChương trình sẽ thoát để tải lại thông tin cấu hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.ExitThread();
                Application.Exit();
            }
            catch (Exception ex)
            {
                ClsMsgBox.Loi(ex.ToString());
                XtraMessageBox.Show("Lỗi khi lưu thông tin cấu hình máy chủ CSDL", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}