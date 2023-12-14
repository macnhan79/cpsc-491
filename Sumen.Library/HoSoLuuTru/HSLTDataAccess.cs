using System.Data;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.HoSoLuuTru
{
    public class HSLTDataAccess
    {

#region sql create table MSSQl
        const string _createTableLHS = "CREATE TABLE [dbo].[NV_DMLOAIHOSO]("
                                + "[LHS_ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,"
                                + "[LHS_MA] [nvarchar](50) NULL,"
                                + "[LHS_TEN] [nvarchar](50) NULL,"
                                + ")";
        const string _createTableHSLT = "CREATE TABLE NV_HOSOLUUTRU("
                                        + "HSLT_ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,"
                                        + "[HSLT_MA] [nvarchar](50) NULL,"
                                        + "[HSLT_LHS_ID] [int] FOREIGN KEY REFERENCES [dbo].[NV_DMLOAIHOSO](LHS_ID),"
                                        + "[HSLT_TRICHYEU] [nvarchar](1000) NULL,"
                                        + "[HSLT_DUONGDAN] [text] NULL,"
                                        + "[HSLT_SOLUONG] [int] NULL"
                                        + ")";
#endregion

#region sql create table MySql
        const string _createTableHSLT_MySql = "CREATE TABLE `nv_hosoluutru` ("
                                      + "`HSLT_ID` int(11) NOT NULL AUTO_INCREMENT,"
                                      + "`HSLT_MA` varchar(50)  DEFAULT NULL,"
                                      + "`HSLT_LHS_ID` int(11) DEFAULT NULL,"
                                      + "`HSLT_TRICHYEU` varchar(1000) DEFAULT NULL,"
                                      + "`HSLT_DUONGDAN` text,"
                                      + "`HSLT_SOLUONG` int(11) DEFAULT NULL,"
                                      + "PRIMARY KEY (`HSLT_ID`),"
                                      + " KEY `Ref_LHS_ID_1` (`HSLT_LHS_ID`),"
                                      + "CONSTRAINT `Ref_LHS_ID_1` FOREIGN KEY (`HSLT_LHS_ID`) REFERENCES `nv_dmloaihoso` (`LHS_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION"
                                      + ") ENGINE=InnoDB DEFAULT CHARSET=utf8";

        const string _createTableLHS_MySql = "CREATE TABLE `nv_dmloaihoso` ("
                                          + "`LHS_ID` int(11) NOT NULL AUTO_INCREMENT,"
                                          + "`LHS_MA` varchar(50) DEFAULT NULL,"
                                          + "`LHS_TEN` varchar(50) DEFAULT NULL,"
                                          + "PRIMARY KEY (`LHS_ID`)"
                                          + ") ENGINE=InnoDB DEFAULT CHARSET=utf8";
#endregion

#region sql check exist table MSSQL
        const string _sqlCheckExistTableHSLT = "select count(*) from sysobjects where type = 'U' and name = 'NV_HOSOLUUTRU'";
        const string _sqlCheckExistTableLHS = "select count(*) from sysobjects where type = 'U' and name = 'NV_DMLOAIHOSO'";
#endregion

#region sql check exist table MySql
        const string _sqlCheckExistTableLHS_MySql = "select count(*) FROM information_schema.tables where table_name = 'NV_DMLOAIHOSO'";
        const string _sqlCheckExistTableHSLT_MySql = "select count(*) FROM information_schema.tables where table_name = 'NV_HOSOLUUTRU'";
#endregion
        

        SqlCommand comm;
        SqlDataAdapter adap;
        SqlConnection Conn = null;


        public HSLTDataAccess()
        {
            comm = new SqlCommand("");
            adap = new SqlDataAdapter(comm);
        }

        #region Vùng của bảng HoSoLuuTru

        public bool CreateTableHSLT()
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.CommandText = _createTableHSLT_MySql;
            try
            {
                comm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public bool CheckExistTableHSLT()
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.CommandText = _sqlCheckExistTableHSLT_MySql;
            DataTable dt = new DataTable("HSLT");
            adap.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetHSLTFillByID(string hslt_ID)
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.Parameters.Clear();
            comm.CommandText = "select * from product where Prod_Product_ID=@HSLT_ID";
            comm.Parameters.Add("@HSLT_ID", SqlDbType.VarChar);
            comm.Parameters[0].Value = hslt_ID;
            DataTable dt = new DataTable("HSLT");
            try
            {
                adap.Fill(dt);
            }
            catch (System.Exception ex)
            {
                ClsMsgBox.LoiChung(ex, false);
            }
            return dt;
        }

        public bool UpdateHSLT(DataTable pDtb)
        {
            //int pLHS_ID, string pTrichYeu, string pDuongDan, int pSoLuong, int pHSLT_ID
            try
            {
                comm.Connection = ClsConnection.MySqlConn;
                comm.Parameters.Clear();
                comm.CommandText = "select * from product";
                SqlCommandBuilder cmdB = new SqlCommandBuilder(adap);

                adap.UpdateCommand = cmdB.GetUpdateCommand();
                adap.DeleteCommand = cmdB.GetDeleteCommand();
                adap.InsertCommand = cmdB.GetInsertCommand();
                adap.InsertCommand.CommandText += "; SELECT LAST_INSERT_ID()";
                adap.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

                SqlDataAdapter daAutoNum = new SqlDataAdapter();
                daAutoNum.DeleteCommand = adap.DeleteCommand;
                daAutoNum.InsertCommand = adap.InsertCommand;
                daAutoNum.UpdateCommand = adap.UpdateCommand;
                daAutoNum.Update(pDtb);
                pDtb.AcceptChanges();
                return true;
            }
            catch (SqlException ex)
            {

                if (ex.Number == 1452)
                {
                    ClsMsgBox.Loi("Không tìm thấy Loại Hồ Sơ.\rVui lòng cập nhật Loại Hồ Sơ");
                   /// XtraMessageBox.Show("Không tìm thấy Loại Hồ Sơ.\rVui lòng cập nhật Loại Hồ Sơ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ClsMsgBox.LoiChung(ex, false);
                    //MessageBox.Show(ex.Message);
                }
                return false;
            }
        }

        //public bool AddNewHSLT(DataTable pDtb)
        //{
        //    //int pLHS_ID, string pTrichYeu, string pDuongDan, int pSoLuong
        //    try
        //    {
        //        comm.Connection = ClsConnection.Conn;
        //        comm.Parameters.Clear();
        //        comm.CommandText = "select * from NV_HOSOLUUTRU";
        //        SqlCommandBuilder cmdB = new SqlCommandBuilder(adap);
        //    //comm.Parameters.Add("@HSLT_LHS_ID", SqlDbType.Int);
        //    //comm.Parameters.Add("@HSLT_TRICHYEU", SqlDbType.NVarChar);
        //    //comm.Parameters.Add("@HSLT_DUONGDAN", SqlDbType.Text);
        //    //comm.Parameters.Add("@HSLT_SOLUONG", SqlDbType.Int);

        //    //comm.Parameters[0].Value = pLHS_ID;
        //    //comm.Parameters[1].Value = pTrichYeu;
        //    //comm.Parameters[2].Value = pDuongDan;
        //    //comm.Parameters[3].Value = pSoLuong;

        //        //comm.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //public bool DelHSLT(int pHSLT_ID)
        //{
        //    comm.Connection = ClsConnection.Conn;
        //    comm.Parameters.Clear();
        //    comm.CommandText = "delete NV_HOSOLUUTRU where HSLT_ID=@HSLT_ID";
        //    comm.Parameters.Add("@HSLT_ID", SqlDbType.Int);
        //    comm.Parameters[0].Value = pHSLT_ID;
        //    try
        //    {
        //        comm.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }

        //}

        //public string GetIndentityHSLT()
        //{
        //    comm.Connection = ClsConnection.Conn;
        //    comm.CommandText = "select max(HSLT_ID)+1 as Max from NV_HOSOLUUTRU";
        //    DataTable dt = new DataTable("Identity");
        //    try
        //    {
        //        adap.Fill(dt);
        //        string newIdentity = dt.Rows[0][0].ToString();
        //        return newIdentity;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return "";
        //    }
        //}

        #endregion


        #region Vùng của LoaiHoSo

        public bool CreateTableLHS()
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.CommandText = _createTableLHS_MySql;
            try
            {
                comm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public bool CheckExistTableLHS()
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.CommandText = _sqlCheckExistTableLHS_MySql;
            DataTable dt = new DataTable("LHS");
            adap.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetDmLHS()
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.CommandText = "select * from NV_DMLOAIHOSO";
            DataTable dt = new DataTable("LHS");
            try
            {
                adap.Fill(dt);
            }
            catch (System.Exception ex)
            {
                ClsMsgBox.LoiChung(ex, false);
               // MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public bool AddNewLHS(string pLHS_MA, string pLHS_TEN)
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.Parameters.Clear();
            comm.CommandText = "insert into NV_DMLOAIHOSO(LHS_MA,LHS_TEN) values(@LHS_MA,@LHS_TEN)";
            comm.Parameters.Add("@LHS_MA", SqlDbType.VarChar);
            comm.Parameters.Add("@LHS_TEN", SqlDbType.VarChar);
            comm.Parameters[0].Value = pLHS_MA;
            comm.Parameters[1].Value = pLHS_TEN;
            try
            {
                comm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                ClsMsgBox.LoiChung(ex, false);
                return false;
            }
        }

        public bool UpdateLHSByID(int pLHS_ID, string pLHS_MA, string pLHS_TEN)
        {
            comm.Connection = ClsConnection.MySqlConn;
            comm.Parameters.Clear();
            comm.CommandText = "update NV_DMLOAIHOSO set LHS_MA=@LHS_MA , LHS_TEN=@LHS_TEN where LHS_ID=@LHS_ID";
            comm.Parameters.Add("@LHS_MA", SqlDbType.VarChar);
            comm.Parameters.Add("@LHS_TEN", SqlDbType.VarChar);
            comm.Parameters.Add("@LHS_ID", SqlDbType.Int);
            comm.Parameters[0].Value = pLHS_MA;
            comm.Parameters[1].Value = pLHS_TEN;
            comm.Parameters[2].Value = pLHS_ID;
            try
            {
                comm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                ClsMsgBox.LoiChung(ex, false);
                return false;
            }
        }

        #endregion





        public string LocationSave
        {
            get
            {

                return ClsConnection.LocationSave;
            }
            set
            {
                ClsConnection.LocationSave = value;
            }
        }




    }
}
