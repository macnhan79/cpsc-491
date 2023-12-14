using System;
using System.Data;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Common
{
    public abstract class AExportToExcel
    {
        protected static Excel.Application excel;
        protected static Excel._Workbook excelB;
        protected static Excel.Workbooks excelBooks;
        protected static Excel._Worksheet excelS;
        protected static Excel.Sheets excelSheets;
        protected static Excel.Range excelRng;
        public static object oMissing = Type.Missing;
        protected static string oTemplate;

        // Dòng hiện tại
        protected static int iRow;

        /// <summary>
        /// Bảng dữ liệu chuyển
        /// </summary>
        protected static DataTable lTable;
        /// <summary>
        /// Dòng bắt đầu ghi
        /// </summary>
        protected static int lStartRow = 9;
        /// <summary>
        /// Cột bắt đầu ghi
        /// </summary>
        protected static int lStartCol = 1;
        /// <summary>
        /// Dòng định dạng
        /// </summary>
        protected static int lRowFormat = lStartRow - 2;
        /// <summary>
        /// Tên file excel template
        /// </summary>
        protected static string templateName = string.Empty;
        /// <summary>
        /// True nếu insert dòng mới, False nếu xuất theo tuần tự
        /// </summary>
        protected static bool missing = true;
        /// <summary>
        /// Có tính tổng hay không
        /// </summary>
        protected static bool isSum;
        /// <summary>
        /// Mãng vị trí các cột tính tổng
        /// </summary>
        protected static int[] sumColumnIndex;
        /// <summary>
        /// Vị trí cột tính tổng
        /// </summary>
        protected static int sumRowIndex;
        /// <summary>
        /// Có hiển thị cột ngày lập hay không
        /// </summary>
        protected static bool showReportDate;
        /// <summary>
        /// Vị trí dòng hiển thị ngày
        /// </summary>
        protected static int showReportDateRow;
        /// <summary>
        /// Vị trí cột hiển thị ngày
        /// </summary>
        protected static int showReportDateColumn;
        /// <summary>
        /// Giá trị ngày cần hiển thị
        /// </summary>
        protected static DateTime showReportDateValue;
        /// <summary>
        /// Có hiển thị giá trị từ ngày đến ngày hay không
        /// </summary>
        protected static bool showBetweenDate = false;
        /// <summary>
        /// Vị trí dòng hiển thị từ ngày
        /// </summary>
        protected static int showStartDateRow;
        /// <summary>
        /// Vị trí cột hiển thị từ ngày
        /// </summary>
        protected static int showStartDateColumn;
        /// <summary>
        /// Giá trị từ ngày cần hiển thị
        /// </summary>
        protected static DateTime showStartDateValue;
        /// <summary>
        /// Vị trí dòng hiển thị đến ngày
        /// </summary>
        protected static int showEndDateRow;
        /// <summary>
        /// Vị trí cột hiển thị đến ngày
        /// </summary>
        protected static int showEndDateColumn;
        /// <summary>
        /// Giá trị đến ngày cần hiển thị
        /// </summary>
        protected static DateTime showEndDateValue;
        /// <summary>
        /// Dường viền của các ô
        /// </summary>
        public static Excel.XlBorderWeight borders = Excel.XlBorderWeight.xlHairline;
        /// <summary>
        /// Vị trí dòng tiêu đề của bảng
        /// </summary>
        protected static int columnHeader = lStartRow -1;

        /// <summary>
        /// Bảng dữ liệu chuyển
        /// </summary>
        public static DataTable Table
        {
            set
            {
                lTable = value;
            }
        }
        /// <summary>
        /// Dòng bắt đầu ghi
        /// </summary>
        public static int StartRow
        {
            set
            {
                lStartRow = value;
            }
        }
        /// <summary>
        /// Cột bắt đầu ghi
        /// </summary>
        public static int StartCol
        {
            set
            {
                lStartCol = value;
            }
        }
        /// <summary>
        /// Dòng định dạng
        /// </summary>
        public static int RowFormat
        {
            set
            {
                lRowFormat = value;
            }
        }
        /// <summary>
        /// Tên file excel template
        /// </summary>
        public static string TemplateName
        {
            get { return templateName; }
            set { templateName = value; }
        }
        /// <summary>
        /// True nếu insert dòng mới, False nếu xuất theo tuần tự
        /// </summary>
        public static bool Missing
        {
            get { return missing; }
            set { missing = value; }
        }
        /// <summary>
        /// Có tính tổng hay không
        /// </summary>
        public static bool IsSum
        {
            get { return isSum; }
            set { isSum = value; }
        }
        /// <summary>
        /// Mãng vị trí các cột tính tổng
        /// </summary>
        public static int[] SumColumnIndex
        {
            get { return sumColumnIndex; }
            set { sumColumnIndex = value; }
        }
        /// <summary>
        /// Vị trí cột tính tổng
        /// </summary>
        public static int SumRowIndex
        {
            get { return sumRowIndex; }
            set { sumRowIndex = value; }
        }
        /// <summary>
        /// Có hiển thị ngày hay không
        /// </summary>
        public static bool ShowReportDate
        {
            get { return showReportDate; }
            set { showReportDate = value; }
        }
        /// <summary>
        /// Vị trí dòng hiển thị ngày
        /// </summary>
        public static int ShowReportDateRow
        {
            get { return showReportDateRow; }
            set { showReportDateRow = value; }
        }
        /// <summary>
        /// Vị trí cột hiển thị ngày
        /// </summary>
        public static int ShowReportDateColumn
        {
            get { return showReportDateColumn; }
            set { showReportDateColumn = value; }
        }
        /// <summary>
        /// Giá trị ngày cần hiển thị
        /// </summary>
        public static DateTime ShowReportDateValue
        {
            get { return showReportDateValue; }
            set { showReportDateValue = value; }
        }

        public static void VerticalAlignment(int row, int startColumn, int endColumn, Excel.XlVAlign align)
        {
            excelS.get_Range(excel.Cells[row, startColumn], excel.Cells[row, endColumn]).VerticalAlignment = align;
        }

        public static Excel.Application objExcel
        {
            get
            {
                return excel;
            }
        }

        public static Excel._Worksheet objExcelS
        {
            get
            {
                return excelS;
            }
        }


        public static Excel.Range objExcelRng
        {
            get
            {
                return excelRng;
            }
            set
            {
                excelRng = value;
            }
        }

        /// <summary>
        /// Có hiển thị giá trị từ ngày đến ngày hay không
        /// </summary>
        public static bool ShowBetweenDate
        {
            get { return showBetweenDate; }
            set { showBetweenDate = value; }
        }
        /// <summary>
        /// Vị trí dòng hiển thị từ ngày
        /// </summary>
        public static int ShowStartDateRow
        {
            get { return showStartDateRow; }
            set { showStartDateRow = value; }
        }
        /// <summary>
        /// Vị trí cột hiển thị từ ngày
        /// </summary>
        public static int ShowStartDateColumn
        {
            get { return showStartDateColumn; }
            set { showStartDateColumn = value; }
        }
        /// <summary>
        /// Giá trị từ ngày cần hiển thị
        /// </summary>
        public static DateTime ShowStartDateValue
        {
            get { return showStartDateValue; }
            set { showStartDateValue = value; }
        }
        /// <summary>
        /// Vị trí dòng hiển thị đến ngày
        /// </summary>
        public static int ShowEndDateRow
        {
            get { return showEndDateRow; }
            set { showEndDateRow = value; }
        }
        /// <summary>
        /// Vị trí cột hiển thị đến ngày
        /// </summary>
        public static int ShowEndDateColumn
        {
            get { return showEndDateColumn; }
            set { showEndDateColumn = value; }
        }
        /// <summary>
        /// Giá trị đến ngày cần hiển thị
        /// </summary>
        public static DateTime ShowEndDateValue
        {
            get { return showEndDateValue; }
            set { showEndDateValue = value; }
        }
        /// <summary>
        /// Dường viền của các ô
        /// </summary>
        public static Excel.XlBorderWeight Borders
        {
            get { return AExportToExcel.borders; }
            set { AExportToExcel.borders = value; }
        }
        /// <summary>
        /// Vị trí dòng tiêu đề của bảng
        /// </summary>
        public static int ColumnHeader
        {
            get
            {
                if (columnHeader == 0)
                {
                    return lRowFormat + 1;
                }
                else
                {
                    return columnHeader;
                }
            }
            set { columnHeader = value; }
        }

        /// <summary>
        /// HeaderFont[0] : Font size = 10 (decimal);
        /// HeaderFont[1] : Font bold = false (bool);
        /// HeaderFont[2] : Font Italic = false (bool);
        /// HeaderFont[3] : Font Italic = string.Empty  (string);
        /// </summary>
        protected static object[] oDataExportFont = { 10, false, false, string.Empty };

        public static object[] DataExportFont
        {
            get { return ClsExcel.oHeaderFont; }
            set { ClsExcel.oHeaderFont = value; }
        }


        /// <summary>
        /// HeaderFont[0] : Font size = 14 (decimal);
        /// HeaderFont[1] : Font bold = true (bool);
        /// HeaderFont[2] : Font Italic = false (bool);
        /// HeaderFont[3] : Font Italic = string.Empty  (string);
        /// </summary>
        protected static object[] oHeaderFont = { 14, true, false, string.Empty };

        public static object[] HeaderFont
        {
            get { return ClsExcel.oHeaderFont; }
            set { ClsExcel.oHeaderFont = value; }
        }

        /// <summary>
        /// Khởi tạo các đối tượng xuất excel
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool Init(string diachi, string tendonvi )
        {
            // Kết quả khởi tạo các đối tượng xuất excel
            bool result = false;
            try
            {
                
                    oTemplate = System.Windows.Forms.Application.StartupPath + @"\Templates\" + templateName;


                    if (excel == null)
                    {
                        excel = new Excel.Application();
                        excelBooks = excel.Workbooks;
                        excelB = excelBooks.Add(oTemplate);
                        excelSheets = excel.Worksheets;
                        excelS = (Excel._Worksheet)excelSheets.get_Item(1);
                    }



                    //string diachi = Get_SystemValue("DIACHI");
                    //string tendonvi = Get_SystemValue("TENDONVI").ToUpper();
                    //string conghoa = Get_SystemValue("CONGHOA");
                    //string doclap = Get_SystemValue("DOCLAP");
                    //diachi = "UBND TỈNH SÓC TRĂNG";
                    //tendonvi = "TRƯỜNG CAO ĐẲNG CĐ SÓC TRĂNG";
                    //string conghoa = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                    //string doclap = "Độc lập - Tự do - Hạnh phúc";



                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 4]).Merge();
                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).Font.Size = 11;

                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 4]).Merge();
                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).Font.Size = 11;
                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).Font.Bold = true;

                    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 20;

                    excel.Cells[2, 1] = (diachi + "").ToUpper();
                    //excel.Cells[2, 7] = conghoa;
                    excel.Cells[3, 1] = (tendonvi + "").ToUpper();
                    //excel.Cells[3, 7] = doclap;

                    result = true;

                
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }
        /// <summary>
        /// Khởi tạo các đối tượng xuất excel có kèm theo tham số dòng bắt đầu của tên đơn vị, cộng hòa xã hội chủ nghĩa ....
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool Init(string diachi, string tendonvi ,bool v_sudung_dingdang)
        {
            // Kết quả khởi tạo các đối tượng xuất excel
            bool result = false;
            try
            {

                oTemplate = System.Windows.Forms.Application.StartupPath + @"\Templates\" + templateName;


                if (excel == null)
                {
                    excel = new Excel.Application();
                    excelBooks = excel.Workbooks;
                    excelB = excelBooks.Add(oTemplate);
                    excelSheets = excel.Worksheets;
                    excelS = (Excel._Worksheet)excelSheets.get_Item(1);
                }



                //string diachi = Get_SystemValue("DIACHI");
                //string tendonvi = Get_SystemValue("TENDONVI").ToUpper();
                //string conghoa = Get_SystemValue("CONGHOA");
                //string doclap = Get_SystemValue("DOCLAP");
                //diachi = "UBND TỈNH SÓC TRĂNG";
                //tendonvi = "TRƯỜNG CAO ĐẲNG CĐ SÓC TRĂNG";
                //string conghoa = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                //string doclap = "Độc lập - Tự do - Hạnh phúc";


                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 4]).Merge();
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).Font.Size = 11;

                excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 4]).Merge();
                excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).Font.Size = 11;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).Font.Bold = true;

                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 20;

                excel.Cells[2, 1] = (diachi + "").ToUpper();
                //excel.Cells[2, 7] = conghoa;
                excel.Cells[3, 1] = (tendonvi + "").ToUpper();

                result = true;


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public static bool Init(string DuongDanDenFileExcel, int SheetIndex)
        {
            // Kết quả khởi tạo các đối tượng xuất excel
            bool result = false;
            try
            {
                oTemplate = DuongDanDenFileExcel;

                if (excel == null)
                {
                    excel = new Excel.Application();
                    excelBooks = excel.Workbooks;
                    excelB = excelBooks.Add(oTemplate);
                    excelSheets = excel.Worksheets;
                    excelS = (Excel._Worksheet)excelSheets.get_Item((SheetIndex <= 0) ? 1 : SheetIndex);
                }

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Set value for cell in excel
        /// </summary>
        /// <param name="RowIndex">vị trí dòng</param>
        /// <param name="ColIndex">vị trí cột</param>
        /// <param name="IsMerge">True Merge trước khi gán</param>
        /// <param name="ValueSet">Giá trị cần set</param>       
       /// <param name="ValueSet"></param>
        public static void SetValueForCel(int RowIndex, int ColIndex, bool IsMerge, object ValueSet)
        {
            if (excel != null && !string.IsNullOrEmpty(oTemplate.ToString()))
            {
                excelS.get_Range(excel.Cells[RowIndex, ColIndex], excel.Cells[RowIndex, ColIndex]).MergeCells = IsMerge;
                excelS.get_Range(excel.Cells[RowIndex, ColIndex], excel.Cells[RowIndex, ColIndex]).Value2 = ValueSet;
            }
        }

        public static void FormatRangeCells
            (Excel.Range excelRng, decimal FontSize, bool IsBold, bool IsItalic, 
                string FontStyle, Excel.XlVAlign VerticalAlignment, Excel.XlHAlign HorizontalAlignment, bool IsBorder)
        {
            try
            {
                excelRng.Font.Size = FontSize;
                excelRng.Font.Bold = IsBold;
                excelRng.Font.Italic = IsItalic;

                if (string.IsNullOrEmpty(FontStyle))
                {
                    excelRng.Font.FontStyle = FontStyle;
                }

                excelRng.Borders.Value = IsBorder;

                excelRng.VerticalAlignment = VerticalAlignment;
                excelRng.HorizontalAlignment = HorizontalAlignment;
            }
            catch
            {
                excelRng.Font.Size = 10;
                excelRng.Font.Bold = false;
                excelRng.Font.Italic = false;
                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            }            
        }
        

        /// <summary>
        /// Hủy các đối tượng xuất excel
        /// </summary>
        public static void Dispose()
        {
            // Giai phong bo nho:
            excelRng = null;
            excelS = null;
            excelB = null;
            excel = null;

            GC.Collect();
        }

        /// <summary>
        /// Set gía trị cho một ô trên file template
        /// </summary>
        /// <param name="row">Tọa độ dòng</param>
        /// <param name="col">Tọa độ cột</param>
        /// <returns></returns>
        public static void setValue(int row, int col, object Value)
        {
            Init(null,null);
            excelS.Cells[row, col] = Value;
            //excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col]).EntireRow.AutoFit();
        }


        /// <summary>
        /// Set gía trị cho một ô trên file template
        /// </summary>
        /// <param name="row">Tọa độ dòng</param>
        /// <param name="col">Tọa độ cột</param>
        /// <returns></returns>
        public static void setValue(int row, int col, int col2, object Value, string ubnd, string donvi, string tinh)
        {
            Init(ubnd,donvi);
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).Merge(oMissing);
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).EntireRow.AutoFit();
            excelS.Cells[row, col] = Value;            
        }
        public static void setValue(int row, int col, int col2, object Value,bool bold,int size)
        {
            Init(null, null);
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).Merge(oMissing);
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).EntireRow.AutoFit();
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).Font.Bold = bold;
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).Font.Size = size;
            excelS.Cells[row, col] = Value;
        }

        // add by 0036 14/12/11
        public static void setValue_Not_Init(int row, int col, int col2, object Value)
        {
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).Merge(oMissing);
            excelS.get_Range(excel.Cells[row, col], excel.Cells[row, col2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            excelS.Cells[row, col] = Value;
        }


        /// <summary>
        /// Hiển thị giá trị tổng cộng cho các cột
        /// </summary>
        /// <param name="rowCout">Cột bắt đầu ghi</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        public static void Sum(int rowCout, int sumRowIndex, int[] sumColumnIndex)
        {
            sumRowIndex = lStartRow + rowCout + (sumRowIndex - lStartRow - 1);
            string column = string.Empty;
            string formula = string.Empty;
            for (int i = 0; i < sumColumnIndex.Length; i++)
            {
                column = ConverNumToCharExcel(sumColumnIndex[i]);
                formula = string.Format("=SUM({0}{1}:{2}{3})", column, lStartRow, column, lStartRow + rowCout - 1);
                excelS.get_Range(excel.Cells[sumRowIndex, sumColumnIndex[i]], excel.Cells[sumRowIndex, sumColumnIndex[i]]).Formula = formula;
            }
            isSum = false;
            sumRowIndex = 0;
            sumColumnIndex = null;
        }
        

        /// <summary>
        /// Chuyển số thành địa chỉ cột trong Excel
        /// </summary>
        /// <param name="num">Số cần chuyễn</param>
        /// <returns>Địa chỉ ô trên excel tương ứng</returns>
        /// <example>num = 27 kết quả là AA</example>
        public static string ConverNumToCharExcel(int num)
        {
            string strReturn = string.Empty;
            try
            {
                int i = num;
                int j = 0;
                while (i > 0)
                {
                    j = i % 26;
                    if (j == 0)
                    {
                        j = 26;
                        i = (i / 26 - 1);
                    }
                    else
                    {
                        i = i / 26;
                    }
                    strReturn = (char)(j + 64) + strReturn;
                }
            }
            catch
            {
                strReturn = string.Empty;
            }
            return strReturn;
        }
        /// <summary>
        /// lấy giá trị trong bảng tham số hệ thống
        /// </summary>
        /// <param name="stringValue">tên giá trị cần truyền vào phương thức</param>
        /// <returns>giá trị trong bảng tham số</returns>
        public static string Get_SystemValue(string stringValue)
        {
            string stringReturn = "";
            try
            {
                // 0036: for MySql 
                if (ClsConnection.MySqlConn.State == ConnectionState.Closed)
                    ClsConnection.MySqlConn.Open();

                SqlCommand com = new SqlCommand("SELECT GIATRI FROM HT_THAMSO WHERE THAMSO = '" + stringValue + "'");
                com.Connection = ClsConnection.MySqlConn;
              
                //ExecuteOracleScalar: Executes the query, and returns the first column of the first row in the result set returned by the query as an Oracle-specific data type. Extra columns or rows are ignored.
                stringReturn = com.ExecuteScalar() + string.Empty;

            }
            catch
            {
                stringReturn = string.Empty;
            }
            return stringReturn;
        }
    }
}
