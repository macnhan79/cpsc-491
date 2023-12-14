using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication1
{
    public class SqlDataHelper
    {
        /// <summary>
        /// Connection string to MSSQL 2005
        /// </summary>
        /// <returns>Connection string</returns>
        static string GetConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PhoHa7"].ConnectionString;
        }

        public SqlDataHelper()
        {

        }

        public string getConnection(){
            SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();
            return conn.State.ToString();
        }

        /// <summary>
        /// Execute a procedure store and return a instance of SqlDataReader
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string pProcName, params SqlParameter[] pProcParams)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 3600;
            try
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pProcParams != null)
                {
                    for (int i = 0; i < pProcParams.Length; i++)
                    {
                        cmd.Parameters.Add(pProcParams[i]);
                    }
                }
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            return reader;
        }

        /// <summary>
        /// Execute a procedure store and return number of record to be affected
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Number of record to be affected</returns>
        public int ExecuteNonQuery(string pProcName, params SqlParameter[] pProcParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            int count = 0;
            cmd.CommandTimeout = 3600;
            try
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pProcParams != null)
                {
                    for (int i = 0; i < pProcParams.Length; i++)
                    {
                        cmd.Parameters.Add(pProcParams[i]);
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
                conn.Close();
            }
            return count;
        }

        /// <summary>
        /// Execute a procedure store and return first row, first column
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>Return first row, first columnreturns>
        public object ExecuteScalar(string pProcName, params SqlParameter[] pProcParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            object obj = null;
            cmd.CommandTimeout = 3600;
            try
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pProcParams != null)
                {
                    for (int i = 0; i < pProcParams.Length; i++)
                    {
                        cmd.Parameters.Add(pProcParams[i]);
                    }
                }
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
                conn.Close();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                conn.Close();
            }
            return obj;
        }

        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataSet</returns>
        public DataSet ExecuteDataSet(string pProcName, params SqlParameter[] pProcParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = null;
            cmd.CommandTimeout = 3600;
            try
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pProcParams != null)
                {
                    for (int i = 0; i < pProcParams.Length; i++)
                    {
                        cmd.Parameters.Add(pProcParams[i]);
                    }
                }
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
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
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// Execute a procedure store and return result fill in DataSet
        /// </summary>
        /// <param name="pProcName">Store Procedure Name</param>
        /// <param name="pProcParams">Paramemter List</param>
        /// <returns>A instance of DataTable</returns>
        public DataTable ExecuteDataTable(string pProcName, params SqlParameter[] pProcParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = null;
            cmd.CommandTimeout = 3600;
            try
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = pProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pProcParams != null)
                {
                    for (int i = 0; i < pProcParams.Length; i++)
                    {
                        cmd.Parameters.Add(pProcParams[i]);
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
                conn.Close();
            }
            return dtb;
        }
    }
}