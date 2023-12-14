using System;
using System.Data;
using System.Windows.Forms;
using PhoHa7.Library.Froms.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Connection
{
    /// <summary>
    /// Summary description for ClsConnection.
    /// </summary>
    public class ClsConnection
    {
        private static Chuoi.EncryptDecrypt ed = new Chuoi.EncryptDecrypt();

        public delegate void DBChanger();
        public static event DBChanger DBEventChanging;
        public static event DBChanger DBEventChanged;

        private static SqlConnection sqlConn;

        private static SqlConnectionStringBuilder myCSB = new SqlConnectionStringBuilder();

        public static string Username
        {
            get
            {
                return ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_username);
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.m_username = ed.EncryptText(value);
                global::PhoHa7.Library.Properties.Settings.Default.Save();
            }
        }

        public static string Password
        {
            get
            {
                return ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_password);
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.m_password = ed.EncryptText(value);
                global::PhoHa7.Library.Properties.Settings.Default.Save();
            }
        }

        public static string Server
        {
            get
            {
                return ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_server);
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.m_server = ed.EncryptText(value);
                global::PhoHa7.Library.Properties.Settings.Default.Save();
            }
        }

        public static string Database
        {
            get
            {
                return ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_database);
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.m_database = ed.EncryptText(value);
                global::PhoHa7.Library.Properties.Settings.Default.Save();
            }
        }

        public static string ConnectionString
        {
            get
            {
                return global::PhoHa7.Library.Properties.Settings.Default.DBConnectionString;
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.DBConnectionString = value;
            }
        }

        public static string SqlConnectionString
        {
            get
            {
                string username = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_username);
                string csdl = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_database);
                string host = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_server);
                string password = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_password);
                //string username = global::PhoHa7.Library.Properties.Settings.Default.m_username;
                //string csdl = global::PhoHa7.Library.Properties.Settings.Default.m_database;
                //string host = global::PhoHa7.Library.Properties.Settings.Default.m_server;
                //string password = global::PhoHa7.Library.Properties.Settings.Default.m_password;
                //Server=BINHLAO-PC\SQLEXPRESS;Database=NCS;uid=sa;pwd=sa
                return string.Format("Server ={0};Database ={1};uid = {2}; pwd = {3};", host, csdl, username, password);
            }
        }

        public static SqlConnection MySqlConn
        {
            get
            {
                //nếu chưa nối kết thì thực hiện nối kết
                while (sqlConn == null || sqlConn.State != ConnectionState.Open)
                {

                    if (global::PhoHa7.Library.Properties.Settings.Default.m_username == null
                        || global::PhoHa7.Library.Properties.Settings.Default.m_username.Trim() == "")
                    {
                        if (MessageBox.Show("Không mở được file cấu hình!\nBạn có muốn cấu hình server không?",
                            "Thông báo", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2)
                            == DialogResult.Yes)
                        {
                            if (DBEventChanging != null)
                                DBEventChanging();
                            FrmCauHinhCSDL frmChSQL = new FrmCauHinhCSDL();
                            frmChSQL.ShowDialog();
                            if (DBEventChanged != null)
                                DBEventChanged();
                            continue;
                        }
                        else
                        {
                            sqlConn = null;
                            Environment.Exit(0);
                            break;
                        }
                    }
                    string username = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_username);
                    string csdl = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_database);
                    string host = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_server);
                    string password = ed.DecryptText(global::PhoHa7.Library.Properties.Settings.Default.m_password);
                    //string username = global::PhoHa7.Library.Properties.Settings.Default.m_username;
                    //string csdl = global::PhoHa7.Library.Properties.Settings.Default.m_database;
                    //string host = global::PhoHa7.Library.Properties.Settings.Default.m_server;
                    //string password = global::PhoHa7.Library.Properties.Settings.Default.m_password;

                    try
                    {
                        myCSB.DataSource = host;
                        myCSB.InitialCatalog = csdl;
                        myCSB.UserID = username;
                        myCSB.Password = password;
                        // myCSB.CharacterSet = "utf8";
                        //myCSB.AllowZeroDateTime = true;
                        //mySqlConn.ConnectionString = string.Format("Server = {0};database={1};Uid={2};Pwd={3}", host, csdl, username, password);
                        //mySqlConn.Open();

                        //myCSB.Pooling = true;
                        //myCSB.MinimumPoolSize = 1;
                        //myCSB.MaximumPoolSize = 99;
                        //myCSB.ConnectionReset = true;
                        //myCSB.ConnectionLifeTime = 7200;
                        //myCSB.ConnectionLifeTime = 0;

                        sqlConn = new SqlConnection(myCSB.ConnectionString);

                        if (sqlConn.State == ConnectionState.Open)
                            sqlConn.Close();

                        if (sqlConn.State == ConnectionState.Closed)
                            sqlConn.Open();



                    }
                    catch (Exception e)
                    {
                        // MessageBox.Show(e.Message);
                        if (MessageBox.Show("Không thể kết nối đến server!\nBạn có muốn cấu hình server không?",
                            "Thông báo", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2)
                            == DialogResult.Yes)
                        {
                            if (DBEventChanging != null)
                                DBEventChanging();
                            FrmCauHinhCSDL frmChSQL = new FrmCauHinhCSDL();
                            frmChSQL.ShowDialog();
                            if (DBEventChanged != null)
                                DBEventChanged();
                            sqlConn = null;
                            continue;
                        }
                        else
                        {
                            sqlConn = null;
                            Environment.Exit(0);
                            break;
                        }
                    }
                }

                return sqlConn;
            }
            set
            {
                sqlConn = value;
            }

        }


        public static string LocationSave
        {
            get
            {
                return global::PhoHa7.Library.Properties.Settings.Default.LocationSave;
            }
            set
            {
                global::PhoHa7.Library.Properties.Settings.Default.LocationSave = value;
                global::PhoHa7.Library.Properties.Settings.Default.Save();
            }
        }

    }
}
