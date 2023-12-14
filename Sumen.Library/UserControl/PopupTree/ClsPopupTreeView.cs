using System.Data;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.UserControl.PopupTree
{
    public class ClsPopupTreeView
    {
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        private SqlCommand cmd;
        /// <summary>
        /// Lập dữ liệu
        /// </summary>
        private SqlDataAdapter da;
        /// <summary>
        /// Chứa thông tin khoa và bộ môn
        /// </summary>
        private DataSet ds;
        private ItemCollection items;

        public ClsPopupTreeView(ItemCollection items)
        {
            this.items = items;
            //
            // Lấy dữ liệu
            //
            cmd = new SqlCommand();
           // cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ClsConnection.MySqlConn;
            //
            // Lập dữ liệu
            //
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //
            // Chứa thông tin khoa và bộ môn
            //
            ds = new DataSet();
            int index = 0;

            try
            {
                foreach (Item item in items)
                {
                  //  cmd.CommandText = string.Format("{0}_SELECT_PU", item.TableName);
                    cmd.CommandText = "select * from products";
                    if (item.TableName == "category")
                    {
                        // tam thoi khong xet quyen, nen khong can bien nay, by 0036
                        //cmd.Parameters.Add("@ORIGINAL_NV_ID", MySqlDbType.Text, 50);
                        //cmd.Parameters["@ORIGINAL_NV_ID"].Value = ClassLib.ADAuthentication.UserAccount.UserName;
                        da.Fill(ds, item.TableName);
                        //cmd.Parameters.RemoveAt("@ORIGINAL_NV_ID");
                    }
                    else
                    {
                        da.Fill(ds, item.TableName);
                    }
                    item.Index = index;
                    index++;
                }
            }
            catch (SqlException ex)
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