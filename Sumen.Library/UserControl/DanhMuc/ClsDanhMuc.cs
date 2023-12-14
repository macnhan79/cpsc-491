using System;
using System.Collections.Generic;
using System.Data;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.UserControl.DanhMuc
{
    class ClsDanhMuc
    {
        private static Chuoi.EncryptDecrypt ed = new Chuoi.EncryptDecrypt();

        protected string sTableName = string.Empty;
        protected string sDefaultValue = string.Empty;
        string showDeleted = "1"; // 1 là hien thi deleted, 0 la khong


        //protected string sConnectionString;

        public DataSet dsDanhMuc;

        //public MySqlConnection ConnectionDanhMuc = new MySqlConnection();
        public SqlConnection ConnectionDanhMuc;
        //public MySqlConnection ConnectionDanhMuc_forDC;

        private SqlCommand SCommandDanhMuc; //Chọn
        //private MySqlCommand ICommandDanhMuc; //Thêm
        //private MySqlCommand UCommandDanhMuc; //Sửa
        //private MySqlCommand DCommandDanhMuc; //Xóa

        private SqlDataAdapter DataAdapterDanhMuc;

        private List<SqlDataAdapter> DataAdapterParentTable;

        internal void CreateSQL()
        {

            // Tạo Connection
            ConnectionDanhMuc = ClsConnection.MySqlConn;

            // Lấy tên database
           // Sumen.Library.Properties.Resources.

            string database = ClsConnection.Database;

            if (ConnectionDanhMuc != null && ConnectionDanhMuc.ConnectionString != "" && TableName != string.Empty && TableName.Trim() != "")
            {
               // showDeleted = LoadParamDeleted();

                string strSQL = "SELECT * FROM `" + database + "`.`" + TableName + "`";

                SCommandDanhMuc = new SqlCommand(strSQL, ConnectionDanhMuc);
                DataAdapterDanhMuc = new SqlDataAdapter();
                DataAdapterDanhMuc.SelectCommand = SCommandDanhMuc;
                dsDanhMuc = new DataSet();
                DataAdapterDanhMuc.FillSchema(dsDanhMuc, SchemaType.Source, "SchemaTable");



                // edit by 0036: edit câu sql để phù hợp với MySQL
                // Select dữ liệu từ TableName

                //if (showDeleted == "0")
                //{

                //    string[] prefix_col = dsDanhMuc.Tables["SchemaTable"].Columns[0].ColumnName.Split('_');
                //    strSQL += " where " + prefix_col[0] + "_Actived = 1";
                //}

                if (DefaultValue != string.Empty && DefaultValue.Trim() != "")
                {
                    strSQL += " where " + DefaultValue;
                }
                strSQL = strSQL + ";";
                DataAdapterDanhMuc.SelectCommand.CommandText = strSQL;


                // select relation // edit by 0036: edit câu sql để phù hợp với MySQL
                // select các quan hệ (FK) từ các bảng khác
                string mysql = "select  CONSTRAINT_NAME RELATION, TABLE_NAME, COLUMN_NAME, REFERENCED_TABLE_NAME PARENT_TABLE_NAME, REFERENCED_COLUMN_NAME PARENT_COLUMN_NAME from information_schema.key_column_usage WHERE REFERENCED_TABLE_SCHEMA = '" + database + "' ";
                mysql = mysql + "and TABLE_NAME = '" + TableName + "';";

                // chú ý: đây là biến cục bộ
                SqlCommand _SCommandDanhMuc = new SqlCommand(mysql, ConnectionDanhMuc);
                SqlDataAdapter _DataAdapterDanhMuc = new SqlDataAdapter(_SCommandDanhMuc);
                _DataAdapterDanhMuc.Fill(dsDanhMuc, "ParentTable");

                SqlCommandBuilder cb = new SqlCommandBuilder(DataAdapterDanhMuc);
                DataAdapterDanhMuc.InsertCommand = cb.GetInsertCommand();

                DataAdapterParentTable = new List<SqlDataAdapter>();

                // edit by 0036: edit câu sql để phù hợp với MySQL
                for (int i = 0; i < dsDanhMuc.Tables["ParentTable"].Rows.Count; i++)
                {
                    string strSQLParent = "select * from `" + database + "`.`" + dsDanhMuc.Tables["ParentTable"].Rows[i]["PARENT_TABLE_NAME"].ToString() + "`";

                    //strSQLParent = strSQLParent + ";";

                    DataAdapterParentTable.Add(new SqlDataAdapter(strSQLParent, ConnectionDanhMuc));
                    DataAdapterParentTable[i].Fill(dsDanhMuc, "SchemaTable" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                    //if (showDeleted == "0")
                    //{
                    //    string[] prefix_col = dsDanhMuc.Tables["SchemaTable" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString()].Columns[0].ColumnName.Split('_');
                    //    strSQLParent += " where " + prefix_col[0] + "_Actived = 1;";
                    //    dsDanhMuc.Tables.Remove("SchemaTable" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                    //    DataAdapterParentTable[i].SelectCommand.CommandText = strSQLParent;
                    //    DataAdapterParentTable[i].Fill(dsDanhMuc, "SchemaTable" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                    //}
                    
                    //DataAdapterParentTable[i].FillSchema(dsDanhMuc, SchemaType.Source, "SchemaTable" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                }
            }
        }

        private string LoadParamDeleted()
        {
            DataTable dtParam = new DataTable();

            SqlCommand cmd = new SqlCommand("select * from sys_option", ConnectionDanhMuc);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            adap.Fill(dtParam);
            string giatri = "";
            for (int i = 0; i < dtParam.Rows.Count; i++)
            {
                string sValue = dtParam.Rows[i]["Opt_ID"].ToString();
                switch (sValue)
                {
                    case "SHOW_DELETED":
                        giatri = dtParam.Rows[i]["Opt_Value"].ToString();
                        break;
                    default:
                        break;
                }
            }
            return giatri;
        }

        /// <summary>
        /// Tên bảng chứa dữ liệu
        /// </summary>
        public string TableName
        {
            get
            {
                return this.sTableName;
            }
            set
            {
                this.sTableName = value;
            }
        }

        /// <summary>
        /// Các giá trị defaule của col
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return sDefaultValue;
            }
            set
            {
                sDefaultValue = value;
            }
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            foreach (DataRow dr in dsDanhMuc.Tables[TableName].Rows)
            {
                if (dr.RowState != DataRowState.Unchanged && dr.RowState != DataRowState.Deleted)
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        foreach (string var in DefaultValue.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (var.Split('=').Length == 2 && dc.ColumnName.ToUpper() == var.Split('=')[0].Trim().ToUpper())
                            {
                                dr[dc.ColumnName] = var.Split('=')[1].Trim();
                            }
                        }

                        if (dr[dc.ColumnName] != DBNull.Value)
                        {
                            if (dr[dc.ColumnName].ToString().Trim() == "")
                            {
                                dr[dc.ColumnName] = DBNull.Value;
                            }
                            else
                            {
                                dr[dc.ColumnName] = dr[dc.ColumnName].ToString().Trim();
                            }
                        }
                    }
                }
            }

            //this.DataAdapterDanhMuc.Update(dsDanhMuc, TableName);
            //dsDanhMuc.AcceptChanges();
            try
            {
                this.DataAdapterDanhMuc.Update(dsDanhMuc, TableName);
            }
            catch (System.Data.DBConcurrencyException myex)
            {
                throw (myex);
            }
            return true;
        }


        public int Fill()
        {
            if (ConnectionDanhMuc != null && ConnectionDanhMuc.ConnectionString != "" && TableName != null && TableName.Trim() != "")
            {
                for (int i = 0; i < dsDanhMuc.Tables["ParentTable"].Rows.Count; i++)
                {
                    DataAdapterParentTable[i].Fill(dsDanhMuc, "Parent" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                }

                int fillNum = DataAdapterDanhMuc.Fill(dsDanhMuc, TableName);

                return fillNum;
            }
            else
                return -1;
        }

        public bool reLoadParent()
        {
            try
            {
                for (int i = 0; i < dsDanhMuc.Tables["ParentTable"].Rows.Count; i++)
                {
                    dsDanhMuc.Tables["Parent" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString()].Clear();
                    DataAdapterParentTable[i].Fill(dsDanhMuc, "Parent" + dsDanhMuc.Tables["ParentTable"].Rows[i]["parent_table_name"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Reload data
        /// </summary>
        /// <returns></returns>
        public bool reload()
        {
            try
            {
                dsDanhMuc.Tables[TableName].Clear();
                return DataAdapterDanhMuc.Fill(dsDanhMuc, TableName) > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
