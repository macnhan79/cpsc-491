using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DA_Connection
/// </summary>
public class DA_Connection
{
    public DA_Connection()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static SqlConnection conn;

    public static SqlConnection Conn
    {
        get
        {
            if (conn != null)
            {
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    else if (conn.State == System.Data.ConnectionState.Broken)
                    {
                        conn = new SqlConnection(GetConnectionString());
                        conn.Open();
                    }
                    //else
                    //{
                    //    conn = new SqlConnection(GetConnectionString());
                    //    conn.Open();
                    //}
                    return conn;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        conn = new SqlConnection(GetConnectionString());
                        conn.Open();
                        return conn;
                    }
                    catch
                    {
                        ExceptionUtility.LogException(ex, "Connection");
                        throw ex;
                    }
                }
            }
            else
            {
                try
                {
                    conn = new SqlConnection(GetConnectionString());
                    conn.Open();
                    return conn;
                }
                catch (SqlException ex)
                {
                    ExceptionUtility.LogException(ex, "Connection");
                    throw ex;
                }

            }

        }
    }
    /// <summary>
    /// Connection string to MSSQL 2005
    /// </summary>
    /// <returns>Connection string</returns>
    public static string GetConnectionString()
    {
        return WebConfigurationManager.ConnectionStrings["PhoHa7"].ConnectionString;
    }
}