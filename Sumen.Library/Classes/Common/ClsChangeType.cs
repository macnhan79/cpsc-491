using System;
using System.Text.RegularExpressions;

namespace PhoHa7.Library.Classes.Common
{
    /// <summary>
    /// Lớp dùng chung trong dùng để chuyển đổi kiểu dữ liệu.
    /// </summary>
    public class ClsChangeType
    {
        /// <summary>
        /// Thủ tục trả về số cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static double change_double(object pstring)
        {
            double v_num = 0;
            try
            {
                Decimal d = 0;
                if (pstring.GetType() == d.GetType())
                {
                    v_num = Decimal.ToDouble((Decimal)pstring);
                }
                else
                {
                    v_num = double.Parse(pstring.ToString());
                }
            }
            catch (Exception ex)
            {
                if (ex != null) { }
            }
            return v_num;
        }

        /// <summary>
        /// Thủ tục trả về số cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static long change_long(string pstring)
        {
            long v_num = 0;
            try
            {
                v_num = long.Parse(pstring);
            }
            catch (Exception ex)
            {
                if (ex != null) { }
            }
            return v_num;
        }

        /// <summary>
        /// Thủ tục trả về số cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static int change_int(object pstring)
        {
            int v_num = 0;
            if (pstring+ string .Empty == "")
            {
                v_num = 0;
            }
            else
            {
                try
                {
                    Decimal d = 0;
                    if (pstring.GetType() == d.GetType())
                    {
                        v_num = Decimal.ToInt32((Decimal)pstring);
                    }
                    else
                    {
                        v_num = int.Parse(pstring.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) { }
                }
            }
            return v_num;
        }

        /// <summary>
        /// Thủ tục trả về số cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static Int64 change_int64(object pstring)
        {
            Int64 v_num = 0;
            try
            {
                Decimal d = 0;
                if (pstring.GetType() == d.GetType())
                {
                    v_num = Decimal.ToInt64((Decimal)pstring);
                }
                else
                {
                    v_num = Int64.Parse(pstring.ToString());
                }
            }
            catch (Exception ex)
            {
                if (ex != null) { }
            }
            return v_num;
        }

        /// <summary>
        /// Thủ tục trả về số cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static float change_float(object pstring)
        {
            float v_num = 0;
            try
            {
                v_num = float.Parse(pstring.ToString());
            }
            catch (Exception ex)
            {
                if (ex != null) { }
            }
            return v_num;
        }

        /// <summary>
        /// Thủ tục trả về bool cho 1 chuỗi có kiểm tra chuỗi rỗng
        /// </summary>
        /// <param name="pstring"></param>
        /// <returns></returns>
        public static bool change_bool(object pstring)
        {
            bool v_bool = false;
            try
            {
                v_bool = Convert.ToBoolean(pstring);
            }
            catch { }
            return v_bool;
        }


        /// <summary>
        /// Thủ tục trả về chuỗi số của SQL
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string NumSQL(string Num)
        {
            double v_num = 0;
            try
            {
                v_num = double.Parse(Num);
                return v_num.ToString("N", nfSQL);
            }
            catch { }
            return "";
        }

        /// <summary>
        /// Thủ tục trả về chuỗi số của SQL
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string DoubleNumSQL(string Num)
        {
            Double v_num = 0;
            try
            {
                v_num = Double.Parse(Num);
                return v_num.ToString("N", nfSQL);
            }
            catch { }
            return "";
        }

        /// <summary>
        /// Thủ tục trả về chuỗi số của SQL
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string IntNumSQL(string Num)
        {
            int v_num = 0;
            try
            {
                v_num = Int32.Parse(Num);
                return v_num.ToString("N", nfSQLInt);
            }
            catch { }
            return "";
        }

        /// <summary>
        /// Định dạng cho số theo chuẩn của SQL
        /// </summary>
        public static System.Globalization.NumberFormatInfo nfSQL
        {
            get
            {
                System.Globalization.NumberFormatInfo NF = new System.Globalization.NumberFormatInfo();
                NF.NumberDecimalSeparator = ".";
                NF.NumberDecimalDigits = 5;
                NF.NumberGroupSeparator = "";
                return NF;
            }
        }

        /// <summary>
        /// Định dạng cho số theo chuẩn của SQL
        /// </summary>
        public static System.Globalization.NumberFormatInfo nfSQLInt
        {
            get
            {
                System.Globalization.NumberFormatInfo NF = new System.Globalization.NumberFormatInfo();
                NF.NumberDecimalSeparator = ".";
                NF.NumberDecimalDigits = 0;
                NF.NumberGroupSeparator = "";
                return NF;
            }
        }

        /// <summary>
        /// Chuyển Date thành kiểu ngày của SQL
        /// </summary>
        /// <param name="pNgay"></param>
        /// <returns>yyyy/MM/dd</returns>
        public static string Ngay_SQL_Ngay(DateTime pNgay)
        {
            string ngay, thang, nam, kq;
            ngay = pNgay.Day.ToString().PadLeft(2, '0');
            thang = pNgay.Month.ToString().PadLeft(2, '0');
            nam = pNgay.Year.ToString();
            kq = nam + "-" + thang + "-" + ngay;
            return kq;

        }

        /// <summary>
        /// Chuyển chuỗi thành kiểu ngày của SQL
        /// </summary>
        /// <param name="ngay">dd/MM/yyyy</param>
        /// <returns>yyyy/MM/dd</returns>
        public static string Ngay_SQL_Chuoi(string ngay)
        {
            if (ngay == "")
                return "";
            DateTime ng;
            string d, m, y, kq;
            ng = DateTime.Parse(ngay);
            d = ng.Day.ToString().PadLeft(2, '0');
            m = ng.Month.ToString().PadLeft(2, '0');
            y = ng.Year.ToString();
            kq = y + "-" + m + "-" + d;
            return kq;
        }
        /// <summary>
        /// Chuyển chuỗi thành kiểu ngày dd/MM/yyy
        /// </summary>
        /// <param name="ngay">dd/MM/yyyy</param>
        /// <returns>yyyy/MM/dd</returns>
        public static string Chuoi_Thanh_Ngay(string ngay)
        {
            try
            {
                if (ngay == "")
                    return "";
                DateTime ng;
                string d, m, y, kq;
                ng = DateTime.Parse(ngay);
                d = ng.Day.ToString().PadLeft(2, '0');
                m = ng.Month.ToString().PadLeft(2, '0');
                y = ng.Year.ToString();
                kq = d + "/" + m + "/" + y;
                return kq;
            }
            catch 
            {
                return ngay;
            }
        }
        /// <summary>
        /// Chuyển chuỗi thành ngày
        /// </summary>
        /// <param name="ntnam">dd/MM/yyyy</param>
        /// <returns>DateTime</returns>
        public static DateTime Ngay_C_NTNam(string ntnam)
        {
            int day = int.Parse(ntnam.Substring(0, 2).ToString());
            int month = int.Parse(ntnam.Substring(3, 2).ToString());
            int year = int.Parse(ntnam.Substring(6, 4).ToString());

            DateTime kq = new DateTime(year, month, day);

            return kq;
        }

        /// <summary>
        /// Kiểm chuỗi có phải là ngày không
        /// </summary>
        /// <param name="ngay">dd/MM/yyyy</param>
        /// <returns>bool</returns>
        public static bool Ngay_Kiem_NTNam(string ngay) // ngay/thang/nam
        {
            if (ngay == "")
                return true; // ngay = null
            else if ((ngay.Length > 10) || (ngay.Length < 10))
                return false;
            else
            {
                DateTime ng;
                try
                {
                    ng = DateTime.Parse(ngay);
                    if (ng.Year > 1900)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ng"></param>
        /// <returns></returns>
        public static string Ngay_Chuyen_NgayGio(DateTime ng)
        {
            string d, M, y, h, mm, kq;
            //ng = DateTime.Parse(ngay);//……giôø ……; ngaøy……/……/………
            d = ng.Day.ToString().PadLeft(2, '0');
            M = ng.Month.ToString().PadLeft(2, '0');
            y = ng.Year.ToString();
            h = ng.Hour.ToString().PadLeft(2, '0');
            mm = ng.Minute.ToString().PadLeft(2, '0');

            kq = h + " giờ " + mm + "; ngày " + d + "/" + M + "/" + y;

            return kq;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ng"></param>
        /// <param name="FontStyle"></param>
        /// <returns>Cân thơ, ngày dd tháng MM năm yyyy</returns>
        public static string Ngay_Chuyen_CanThoNTN(DateTime ng, Chuoi.FontEnum FontStyle)
        {
            string d, M, y, kq;
            d = ng.Day.ToString().PadLeft(2, '0');
            M = ng.Month.ToString().PadLeft(2, '0');
            y = ng.Year.ToString();
            if (FontStyle == Chuoi.FontEnum.Unicode)
            {
                kq = "Cần thơ, Ngày " + d + " tháng " + M + " năm " + y;
            }
            else
            {
                kq = "Caàn thô, Ngaøy " + d + " thaùng " + M + " naêm " + y;
            }
            return kq;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ng"></param>
        /// <returns>dd/MM/yyyy</returns>
        public static string Ngay_Chuyen_NTNam(object ngay)
        {
            try
            {
                if ((ngay + string.Empty).Trim().Length <= 10)
                    ngay = ngay + " 00:00:00 AM";
                DateTime ng = (DateTime)ngay;
                string d, M, y, kq;
                d = ng.Day.ToString().PadLeft(2, '0');
                M = ng.Month.ToString().PadLeft(2, '0');
                y = ng.Year.ToString();

                kq = "" + d + "/" + M + "/" + y;
                    
                return kq;
            }
            catch
            {
                return "";
            }
        }

        public static string DDMMYYY_TO_ORACLE(object value)
        {
            string target = value + string.Empty;
            if (string.IsNullOrEmpty(target))
            {
                return string.Empty;
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ng"></param>
        /// <returns></returns>
        public static DateTime Ngay_Chuyen_Ngay(object ng)
        {
            try
            {

                DateTime kq = Convert.ToDateTime(ng);
                //kq = (DateTime)ng;
                return kq;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime ddMMyyyyToMMddyyyy(object value)
        {
            try
            {
                string[] array = value.ToString().Split('/');
                return new DateTime(ClsChangeType.change_int(array[2]), ClsChangeType.change_int(array[1]), ClsChangeType.change_int(array[0]));
            }
            catch
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch 
                {
                    return DateTime.Now;
                }                
            }          
        }

        /// <summary>
        /// Viết thường ký tự đầu tiên của chuỗi.
        /// </summary>
        /// <param name="str">CHUỖI</param>
        /// <returns>cHUỖI</returns>
        public static string toLowerFirstChar(string str)
        {
            if (str != null && str.Length > 0)
            {
                return str.Substring(0, 1).ToLower() + str.Substring(1);
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Viết hoa ký tự đầu tiên của chuỗi.
        /// </summary>
        /// <param name="str">chuỗi</param>
        /// <returns>Chuỗi</returns>
        public static string toUpperFirstChar(string str)
        {
            if (str != null && str.Length > 0)
            {
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Kiểm tra giá trị ngày có phải bằng 1/1/0001 12:00:00 AM hay không
        /// </summary>
        /// <param name="value">Giá trị cần kiểm tra</param>
        /// <returns>True nếu bằng 1/1/0001 12:00:00 AM, ngược lại là False</returns>
        public static bool IsNullDate(object value)
        {
            try
            {
                return Convert.ToDateTime(value) == new DateTime(1, 1, 1) ? true : false;
            }
            catch
            {
                return true;
            }
        }

        // Add by 0036 14/11/2011
        /// <summary>
        /// Phán đoán 1 chuỗi đưa vào có chứa ki tự chữ hay không?
        /// </summary>
        /// <param name="input">Chuỗi muốn kiểm tra</param>
        /// <returns>True: nếu chuỗi không chứa kí tự nào là chữ</returns>
        public static bool IsNumber(string input)
        {
            input = input.Trim();
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsNumber(input, i))
                    return false;
            }
            return true;  
        }

        // add by 0036 21/11/11
        /// <summary>
        /// Kiểm tra địa chỉ Email có đúng định dạng không?
        /// </summary>
        /// <param name="email">chuỗi địa chỉ email cần kiểm tra</param>
        /// <returns></returns>
        public static bool IsEmailAddress(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
               @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
               @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match =
                Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return true;
            else
                return false;
        }
        //public static string ImageToBase64String(System.Drawing.Image img)
        //{
        //    System.Windows.Forms.PictureBox pctAnhNVien = new System.Windows.Forms.PictureBox();
        //    pctAnhNVien.Image = img;
        //    System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)pctAnhNVien.Image;
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    bmp.Save(ms, bmp.RawFormat);
        //    Byte[] arrImage = ms.GetBuffer();
        //    ms.Close();
        //    return Convert.ToBase64String(arrImage);
        //}

        //public static System.Drawing.Image ImageFromBase64String(string str)
        //{
        //    if (str == null) return null;
        //    System.Byte[] bytes = Convert.FromBase64String(str);
        //    System.Drawing.Image image = null;
        //    if (bytes != null)
        //    {
        //        System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);
        //        image = System.Drawing.Image.FromStream(stream);
        //        stream.Close();
        //    }

        //    return image;
        //}

    }
}