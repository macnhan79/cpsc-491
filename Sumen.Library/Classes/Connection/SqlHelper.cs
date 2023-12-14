using System;
using System.Data;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Froms.MsgBox;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Connection
{
    public class SqlHelper
    {
        ClsException cls_ex;
        /// <summary>
        /// Connection string to mysql
        /// </summary>
        /// <returns>Connection mysql</returns>
        static SqlConnection _conn = ClsConnection.MySqlConn;

        static SqlConnection GetMySqlConnection()
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
                return _conn;
            }
            else
            {
                return _conn;
            }
        }

        public SqlHelper()
        {

        }



        /// <summary>
        /// Execute a procedure store and return a instance of MySqlDataReader
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of MySqlDataReader</returns>
        public SqlDataReader ExecuteReader(string pProcName, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                reader = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                cls_ex = new ClsException(ex);
                FrmError frmMsg = new FrmError(ex.Message, cls_ex);
                frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
                frmMsg.Text = "Lỗi";
                frmMsg.emailTo = "";
                frmMsg.ShowDialog();
                //throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return reader;
        }

        /// <summary>
        /// Execute a procedure store and return number of record to be affected( Procudure)
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Number of record to be affected</returns>
        public int ExecuteNonQuery(string pProcName, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return count;
        }

        /// <summary>
        /// Execute a procedure store and return number of record to be affected
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Number of record to be affected</returns>
        public int ExecuteNonQuery(string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        string name = paraName[i].ToString();
                        string value = paraValue[i].ToString();
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return count;
        }

        /// <summary>
        /// Execute a procedure store and return number of record to be affected
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Number of record to be affected</returns>
        public int ExecuteNonQuery(string pProcName, CommandType cmdType, SqlTransaction transaction, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                cmd.Transaction = transaction;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        string name = paraName[i].ToString();
                        string value = paraValue[i].ToString();
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return count;
        }


        /// <summary>
        /// Execute a procedure store and return first row, first column
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Return first row, first columnreturns>
        public object ExecuteScalar(string pProcName, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return obj;
        }

        /// <summary>
        /// Execute a procedure store and return first row, first column
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Return first row, first columnreturns>
        public object ExecuteScalar(string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return obj;
        }

        /// <summary>
        /// Execute a procedure store and return first row, first column
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Return first row, first columnreturns>
        public object ExecuteScalar(string pProcName, CommandType cmdType, SqlTransaction hasTransaction, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                cmd.Transaction = hasTransaction;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return obj;
        }

        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataSet</returns>
        public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, pTableName);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return ds;
        }

        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataSet</returns>
        public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, pTableName);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return ds;
        }

        /// <summary>
        /// Execute a Text SQL and return result fill in DataSet in command text
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pTableName">Paramemter List</param>
        /// <returns>A instance of DataSet</returns>
        public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, pTableName);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return ds;
        }



        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataTable</returns>
        public DataTable ExecuteDataTable(string pProcName, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtb);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return dtb;
        }


        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataTable</returns>
        public DataTable ExecuteDataTable(string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtb);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return dtb;
        }


        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataTable</returns>
        public DataTable ExecuteDataTable(string pProcName, CommandType cmdType, SqlTransaction transaction, object[] paraName, object[] paraValue)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            try
            {
                conn = GetMySqlConnection();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = cmdType;
                cmd.Transaction = transaction;
                if (paraName != null)
                {
                    for (int i = 0; i < paraName.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paraName[i].ToString(), paraValue[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtb);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                }
            }
            return dtb;
        }

    }
}
