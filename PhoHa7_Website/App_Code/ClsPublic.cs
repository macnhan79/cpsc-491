using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoHa7.Library.Classes.Connection;
using System.Data;
using PhoMac.Model.Data;
using PhoMac.Model;

/// <summary>
/// Summary description for ClsPublic
/// </summary>
public static class ClsPublic
{

    public static int WriteException(Exception ex)
    {
        using (Entities obj = new Entities())
        {
            //Dao dao = new Dao();
            Error er = new Error();
            er.ErrorText = ex.Message;
            er.ErrorDetails = ex.ToString();
            er.ErrorDate = DateTime.Now;
            er.ErrorType = ex.InnerException == null ? "" : ex.InnerException.GetType().ToString();
            er.InnerException = ex.InnerException == null ? "" : ex.InnerException.Message;
            er.InnerSource = ex.InnerException == null ? "" : ex.InnerException.Source;
            er.InnerExceptionStackTrace = ex.InnerException == null ? "" : ex.InnerException.StackTrace + string.Empty;


            obj.Set<Error>().Add(er);

            int count = obj.SaveChanges();

            return count;
        }
        //DbSet<T> abc = 
        //abc.Add((T)pObject);
        //return ;
        

        //SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        //string sql = "INSERT INTO [Error]([ErrorText],[ErrorDetails],[ErrorDate],[ErrorType],[InnerException],[InnerSource],[InnerExceptionStackTrace])" +
        //                " VALUES (@ErrorText,@ErrorDetails,@ErrorDate,@ErrorType,@InnerException,@InnerSource,@InnerExceptionStackTrace)";
        //int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate", "@ErrorType", "@InnerException", "@InnerSource", "@InnerExceptionStackTrace" },
        //    new object[] { ex.Message, ex.ToString(), DateTime.Now, ex.InnerException.GetType().ToString(), ex.InnerException.Message, ex.InnerException.Source, ex.InnerException.StackTrace+string.Empty });
        //return count;

    }

   


}