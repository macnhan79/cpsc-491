using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public class SqlHelperWeb
{
    /// <summary>
    /// Connection string to mysql
    /// </summary>
    /// <returns>Connection mysql</returns>
    SqlConnection _conn;

    public SqlConnection ConSql { get; set; }

    public SqlHelperWeb(string connString)
    {
        this.ConnectionString = connString;
    }

    //public SqlHelperWeb(SqlConnection _conn)
    //{
    //    this._conn = _conn;
    //}

    public int WriteException(Exception ex)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();

            string sql = "INSERT INTO [Error]([ErrorText],[ErrorDetails],[ErrorDate],[ErrorType],[InnerException],[InnerSource],[InnerExceptionStackTrace])" +
                            " VALUES (@ErrorText,@ErrorDetails,@ErrorDate)";
            int count = ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate" },
                new object[] { ex.Message, ex.ToString(), DateTime.Now, ex.InnerException.GetType().ToString(), ex.InnerException.Message, ex.InnerException.Source, ex.InnerException.StackTrace + string.Empty });
            conn.Close();
            return count;
        }
    }

    public void WriteExceptionToFile(Exception exc, string source)
    {
        string logFile = "App_Data/ErrorLog.txt";

        // Open the log file for append and write the log
        StreamWriter sw = new StreamWriter(logFile, true);
        sw.WriteLine("********** {0} **********", DateTime.Now);
        if (exc.InnerException != null)
        {
            sw.Write("Inner Exception Type: ");
            sw.WriteLine(exc.InnerException.GetType().ToString());
            sw.Write("Inner Exception: ");
            sw.WriteLine(exc.InnerException.Message);
            sw.Write("Inner Source: ");
            sw.WriteLine(exc.InnerException.Source);
            if (exc.InnerException.StackTrace != null)
            {
                sw.WriteLine("Inner Stack Trace: ");
                sw.WriteLine(exc.InnerException.StackTrace);
            }
        }
        sw.Write("Exception Type: ");
        sw.WriteLine(exc.GetType().ToString());
        sw.WriteLine("Exception: " + exc.Message);
        sw.WriteLine("Source: " + source);
        sw.WriteLine("Stack Trace: ");
        if (exc.StackTrace != null)
        {
            sw.WriteLine(exc.StackTrace);
            sw.WriteLine();
        }
        sw.WriteLine("Details: " + exc.ToString());
        sw.Close();
    }

    /// <summary>
    /// Connection string to MSSQL 2005
    /// </summary>
    /// <returns>Connection string</returns>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Execute a procedure store and return a instance of MySqlDataReader
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>A instance of MySqlDataReader</returns>
    public SqlDataReader ExecuteReader(string pProcName, object[] paraName, params object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            //SqlConnection conn = null;
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                //conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return reader;
        }
    }

    /// <summary>
    /// Execute a procedure store and return number of record to be affected( Procudure)
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>Number of record to be affected</returns>
    public int ExecuteNonQuery(string pProcName, object[] paraName, object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            try
            {
                //conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return count;
        }
    }

    /// <summary>
    /// Execute a procedure store and return number of record to be affected
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>Number of record to be affected</returns>
    public int ExecuteNonQuery(string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            try
            {
                //conn = _conn;
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
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return count;
        }
    }

    /// <summary>
    /// Execute a procedure store and return first row, first column
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>Return first row, first columnreturns>
    public object ExecuteScalar(string pProcName, object[] paraName, object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            try
            {
                // conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
    }

    /// <summary>
    /// Execute a procedure store and return first row, first column
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>Return first row, first columnreturns>
    public object ExecuteScalar(string pProcName, CommandType cmdType, object[] paraName, params object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            try
            {
                //conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
    }

    /// <summary>
    /// Execute a procedure store and return result fill in DataSet
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>A instance of DataSet</returns>
    public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName, object[] paraName, params object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                //conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            conn.Close();
            conn.Dispose();
            return ds;
        }
    }

    /// <summary>
    /// Execute a procedure store and return result fill in DataSet
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>A instance of DataSet</returns>
    public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName, CommandType cmdType, object[] paraName, params object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                //conn = _conn;
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
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
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

            conn.Close();
            conn.Dispose();
            return ds;
        }
    }

    /// <summary>
    /// Execute a Text SQL and return result fill in DataSet in command text
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pTableName">Paramemter List</param>
    /// <returns>A instance of DataSet</returns>
    public DataSet ExecuteDataSet(DataSet ds, string pTableName, string pProcName)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = null;
            try
            {
                //conn = _conn;
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, pTableName);
            }
            catch (SqlException ex)
            {
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                } throw ex;
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
            conn.Close();
            conn.Dispose();
            return ds;
        }
    }



    /// <summary>
    /// Execute a procedure store and return result fill in DataSet
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>A instance of DataTable</returns>
    public DataTable ExecuteDataTable(string pProcName, object[] paraName, object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            try
            {
                //conn = _conn;
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
                cmd.Dispose();
                adapter.Dispose();
            }
            catch (SqlException ex)
            {
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                }
                throw ex;
            }
            finally
            {
                //if (cmd != null)
                //{
                //    cmd.Dispose();
                //}
                //if (adapter != null)
                //{
                //    adapter.Dispose();
                //}
            }
            conn.Close();
            conn.Dispose();
            return dtb;
        }
    }

    /// <summary>
    /// Execute a procedure store and return result fill in DataSet
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>A instance of DataTable</returns>
    public DataTable ExecuteDataTable(string pProcName, CommandType cmdType, object[] paraName, object[] paraValue)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            // SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            try
            {
                // conn = _conn;
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

                cmd.Dispose();
                adapter.Dispose();
            }
            catch (SqlException ex)
            {
                try
                {
                    WriteException(ex);
                    conn.Close();
                }
                catch (System.Exception ex1)
                {
                    WriteExceptionToFile(ex, "SqlHelper");
                    conn.Close();
                }
                throw ex;
            }
            finally
            {

            }
            conn.Close();
            conn.Dispose();
            return dtb;
        }
    }


    public SqlTransaction _transaction;
    public void BeginTransaction()
    {
        _conn = new SqlConnection(ConnectionString);
        _conn.Open();
        _transaction = _conn.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
        _conn.Close();
        _conn.Dispose();
        _conn = null;
    }

    public void RollBack()
    {
        _transaction.Rollback();
        _conn.Close();
        _conn.Dispose();
        _conn = null;
    }

    /// <summary>
    /// Execute a procedure store and return number of record to be affected
    /// </summary>
    /// <param name="pProcName">Store Procedure Name</param>
    /// <param name="pProcParams">Paramemter List</param>
    /// <returns>Number of record to be affected</returns>
    public int ExecuteNonQuery(string pProcName, CommandType cmdType, bool hasTransaction, object[] paraName, object[] paraValue)
    {
        SqlConnection conn = null;
        SqlCommand cmd = new SqlCommand();
        int count = 0;
        try
        {
            conn = _conn;
            cmd.Connection = conn;
            cmd.CommandText = pProcName;
            cmd.CommandType = cmdType;
            if (hasTransaction)
            {
                cmd.Transaction = _transaction;
            }
            
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
            try
            {
                WriteException(ex);
                WriteExceptionToFile(ex, "SqlHelper");
                throw ex;
            }
            catch (System.Exception ex1)
            {
                throw ex;
            }
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
    public object ExecuteScalar(string pProcName, CommandType cmdType, bool hasTransaction, object[] paraName, object[] paraValue)
    {
        SqlConnection conn = null;
        SqlCommand cmd = new SqlCommand();
        object obj = null;
        try
        {
            conn = _conn;
            cmd.Connection = conn;
            cmd.CommandText = pProcName;
            cmd.CommandType = cmdType;
            if (hasTransaction)
            {
                cmd.Transaction = _transaction;
            }
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
            try
            {
                WriteException(ex);
                conn.Close();
            }
            catch (System.Exception ex1)
            {
                WriteExceptionToFile(ex, "SqlHelper");
                conn.Close();
            } throw ex;
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
    /// <returns>A instance of DataTable</returns>
    public DataTable ExecuteDataTable(string pProcName, CommandType cmdType, bool hasTransaction, object[] paraName, object[] paraValue)
    {
        SqlConnection conn = null;
        SqlCommand cmd = new SqlCommand();
        DataTable dtb = new DataTable();
        SqlDataAdapter adapter = null;
        try
        {
            conn = _conn;
            cmd.Connection = conn;
            cmd.CommandText = pProcName;
            cmd.CommandType = cmdType;
            if (hasTransaction)
            {
                cmd.Transaction = _transaction;
            }
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
        catch (SqlException ex)
        {
            try
            {
                WriteException(ex);
                conn.Close();
            }
            catch (System.Exception ex1)
            {
                WriteExceptionToFile(ex, "SqlHelper");
                conn.Close();
            } throw ex;
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

