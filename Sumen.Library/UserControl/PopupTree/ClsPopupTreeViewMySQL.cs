using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

using ClassLib.Classes.Connection;
using ClassLib.Classes.Common;

namespace ClassLib.UserControl.PopupTree
{
    public class ClsPopupTreeViewMySQL
    {
        
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        private MySqlCommand cmd;
        /// <summary>
        /// Lập dữ liệu
        /// </summary>
        private MySqlDataAdapter da;
        /// <summary>
        /// Chứa thông tin khoa và bộ môn
        /// </summary>
        private DataSet ds;
        private ItemCollection items;

        public ClsPopupTreeViewMySQL(ItemCollection items)
        {
            this.items = items;
            
            // Lấy dữ liệu
            cmd = new MySqlCommand();
            cmd.Connection = ClsConnection.MySqlConn;

            // Lập dữ liệu
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
           
            // Chứa thông tin khoa và bộ môn
            ds = new DataSet();
            int index = 0;
                      
            try
            {
                foreach (Item item in items)
                {

                    //cmd.CommandText = string.Format("Call {0}_SELECT_PU('" + ClassLib.ADAuthentication.UserAccount.UserName + "')", item.TableName);
                    //cmd.CommandType = CommandType.Text;
                                        
                    //if (item.TableName == "DM_DV_KHOA" || item.TableName == "DM_DV_BO_MON")
                    //{
                    //    //cmd.Parameters.Add("ORIGINAL_NV_ID", MySqlDbType.VarChar, 50);
                    //    //cmd.Parameters["ORIGINAL_NV_ID"].Value = ClassLib.ADAuthentication.UserAccount.UserName;
                    //    //da.SelectCommand = cmd;
                    //    //da.Fill(ds, item.TableName);
                    //    //cmd.Parameters.RemoveAt("ORIGINAL_NV_ID");

                    //    //test
                    //    da.SelectCommand = cmd;
                    //    da.Fill(ds, item.TableName);
                    //}
                    //else
                    //{
                    //    da.Fill(ds, item.TableName);
                    //}
                    item.Index = index;
                    index++;
                }


            //    MySqlConnection conn = new MySqlConnection("server = 172.16.160.172;database = hrm-bentre;user id = pbcuong;password = pbcuong");
                //MySqlConnection conn = new MySqlConnection(ClsConnection.MySqlConnectionString);
                //conn = ClsConnection.MySqlConn;
                //MySqlCommand cmd1 = new MySqlCommand("CALL DM_DV_KHOA_SELECT_PU", ClsConnection.MySqlConn);
               
                //MySqlDataAdapter da1 = new MySqlDataAdapter();
                //cmd1.CommandType = CommandType.Text;
                //da1.SelectCommand = cmd1;

                
            }
            catch (Exception ex)
            {
                ClsBaoLoi.Loi(ex.Message);
            }
        }

        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }
    }
}
