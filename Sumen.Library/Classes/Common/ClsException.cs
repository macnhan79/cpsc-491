using System;

namespace PhoHa7.Library.Classes.Common
{
    class ClsException : Exception
    {
        string _Message;
        public override string Message
        {
            get
            {
                return _Message;
            }
        }
        public ClsException(Exception Ex)
        {
            string[] ms = Ex.Message.Split('\n')[0].Split(':');
            if (Ex.GetType() == typeof(System.Data.OracleClient.OracleException) && ms.Length > 1)
            {
                Library.Resources.ErrorMessagesDataSetTableAdapters.CacLoiThuongGapTableAdapter err = new Library.Resources.ErrorMessagesDataSetTableAdapters.CacLoiThuongGapTableAdapter();
                Library.Resources.ErrorMessagesDataSet.CacLoiThuongGapDataTable dt = err.GetDataByMALOI(ms[0]);
                if (dt.Rows.Count > 0)
                {
                    string[] value = Ex.Message.Split('\n')[0].Split(
                        dt.Rows[0]["LOITIENGANH"].ToString().Trim().Split(new string[] { "%s" }, StringSplitOptions.RemoveEmptyEntries),
                        StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < value.Length; i++)
                    {
                        Library.Resources.ErrorMessagesDataSetTableAdapters.YNGHIACACCOTTableAdapter YNghiaTA = new Library.Resources.ErrorMessagesDataSetTableAdapters.YNGHIACACCOTTableAdapter();
                        Library.Resources.ErrorMessagesDataSet.YNGHIACACCOTDataTable YNghiadt = YNghiaTA.GetDataByTENCOT(value[i]);

                        if (YNghiadt.Rows.Count > 0)
                        {
                            value[i] = YNghiadt.Rows[0]["DIENGIAI"] + "";
                        }
                    }
                    _Message = string.Format(dt.Rows[0]["LOITIENGVIET"].ToString(), value);
                    throw this;
                }
                else
                {
                    throw Ex;
                }
            }
            else
            {
                throw Ex;
            }
        }
    }
}
