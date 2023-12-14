using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PhoHa7.Library.Classes.Connection;
using System.Data;
using System.Security.Cryptography;

namespace PhoHa7
{
    public static class ClsPublic
    {
        public static int MAX_NUMBER_TICKET = 40;

        public static string KITCHEN_TYPE
        {
            get
            {
                string kitchenType = global::PhoHa7.Properties.Settings.Default.KitchenType;
                if (kitchenType == null)
                {
                    kitchenType = "1";
                }
                return kitchenType;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.KitchenType = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static int TIMER_ORDER_COUNT_DOWN
        {
            get
            {
                int time = global::PhoHa7.Properties.Settings.Default.TimeCounter;
                if (time == null || time == 0)
                {
                    time = 15;
                }
                return time;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.TimeCounter = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorLetterToGo
        {
            get
            {
                return global::PhoHa7.Properties.Settings.Default.ColorDisplayToGo;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.ColorDisplayToGo = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorBackgroundToGo
        {
            get
            {
                return global::PhoHa7.Properties.Settings.Default.ColorBackgroundToGo;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.ColorBackgroundToGo = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorLetterItemChange
        {
            get
            {
                return global::PhoHa7.Properties.Settings.Default.ColorItemChange;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.ColorItemChange = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorBackgroundItemChange
        {
            get
            {
                return global::PhoHa7.Properties.Settings.Default.ColorBackgroundItemChange;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.ColorBackgroundItemChange = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static int PaddingSizePrint
        {
            get
            {
                int padding = global::PhoHa7.Properties.Settings.Default.PaddingSizePrint;
                if (padding == null || padding == 0)
                {
                    padding = 15;
                }
                return padding;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.PaddingSizePrint = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static int FontSizePrint
        {
            get
            {
                int size = global::PhoHa7.Properties.Settings.Default.FontSizePrint;
                if (size == null || size == 0)
                {
                    size = 15;
                }
                return size;
            }
            set
            {
                global::PhoHa7.Properties.Settings.Default.FontSizePrint = value;
                global::PhoHa7.Properties.Settings.Default.Save();
            }
        }

        public static int WriteException(Exception ex)
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "INSERT INTO [DbProvider].[dbo].[Error]([ErrorText],[ErrorDetails],[ErrorDate])" +
                            " VALUES (@ErrorText,@ErrorDetails,@ErrorDate)";
            int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate" },
                new object[] { ex.Message, ex.ToString(), DateTime.Now });
            return count;

        }



        #region Encrypt

        public static string EncryptSHA512(string pStr)
        {
            try
            {
                byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(pStr.Trim());
                SHA512Managed hasSha512Managed = new SHA512Managed();
                //SHA256Managed hash = new SHA256Managed();
                byte[] hashBytes = hasSha512Managed.ComputeHash(inputBytes);
                string sEncrypt = Convert.ToBase64String(hashBytes);
                return sEncrypt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
