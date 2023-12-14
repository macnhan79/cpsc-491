using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraGrid.Columns;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Common
{
    public class ClsExcel : AExportToExcel
    {
        public static Chuoi.EncryptDecrypt ed = new Chuoi.EncryptDecrypt();
        public static DevExpress.Utils.WaitDialogForm waitDialogForm;

        /// <summary>
        /// Hiển thị thông tin của các thuộc tính
        /// </summary>
        /// <param name="source">source chứa thông tin cần xuất</param>
        public static void ShowProperties(object source)
        {
            if (isSum)
            {
                if (source.GetType() == typeof(DataTable))
                {
                    Sum((source as DataTable).Rows.Count, sumRowIndex, sumColumnIndex);
                }
                else
                {
                    Sum((source as DevExpress.XtraGrid.Views.Grid.GridView).RowCount, sumRowIndex, sumColumnIndex);
                }
            }
            if (showReportDate)
            {
                setValue(showReportDateRow, showReportDateColumn, showReportDateValue);
                showReportDate = false;
                showReportDateRow = 0;
                showReportDateColumn = 0;
            }
            if (showBetweenDate)
            {
                setValue(showStartDateRow, showStartDateColumn, showStartDateValue);
                setValue(showEndDateRow, showEndDateColumn, showEndDateValue);
                showBetweenDate = false;
                showStartDateRow = 0;
                showStartDateColumn = 0;
                showEndDateRow = 0;
                showEndDateColumn = 0;
            }
        }


        /// <summary>
        /// Khởi tạo các đối tượng xuất excel
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool Init()
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
                string diachi = Get_SystemValue("DIACHI");
                string tendonvi = Get_SystemValue("TENDONVI").ToUpper();
                string conghoa = Get_SystemValue("CONGHOA");
                string doclap = Get_SystemValue("DOCLAP");
                excel.Cells[2, 1] = tendonvi;
                excel.Cells[2, 7] = conghoa;
                excel.Cells[3, 1] = diachi;
                excel.Cells[3, 7] = doclap;

                excel.get_Range(excel.Cells[2, 7], excel.Cells[2, 7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                excel.get_Range(excel.Cells[3, 7], excel.Cells[3, 7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 0;
                result = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        /// <summary>
        /// Xuất toàn bộ datatable sang excel
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool DataTableToExcel()
        {
            return DataTableToExcel(0,null ,null,null);
        }
        public static bool DataTableToExcel11(string ubnd,string donvi)
        {
            return DataTableToExcel(0, ubnd, donvi, null);
        }
        /// <summary>
        /// Xuất toàn bộ datatable sang excel
        /// </summary>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        /// 
        public static bool DataTableToExcel(int CotCuoiKgXuat, string ubnd, string tendvvh, string tinhtp)
        {
            waitDialogForm = new DevExpress.Utils.WaitDialogForm("Đang xuất excel ...", "Vui lòng chờ giây lát !");
            // Kết quả xuất toàn bộ datatable sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel

            try
            {
                // Hiển thị cửa sổ Excel
                SqlCommand selcmd = new SqlCommand("HT_THAMSO_SELECT", ClsConnection.MySqlConn);
                selcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter HT_THAMSO_ADAPT = new SqlDataAdapter();
                HT_THAMSO_ADAPT.SelectCommand = selcmd;
                selcmd.CommandTimeout = 0;
                DataSet dsThamSo = new DataSet();
                HT_THAMSO_ADAPT.Fill(dsThamSo, "TB_THAMSO");

                string ten_thamso = string.Empty;
                string ubnd_new = string.Empty;
                string donvi = string.Empty;
                string tinh = string.Empty;
                string thutruongdonvi = string.Empty;

                for (int i = 0; i < dsThamSo.Tables["TB_THAMSO"].Rows.Count; i++)
                {
                    ten_thamso = dsThamSo.Tables["TB_THAMSO"].Rows[i]["THAMSO"].ToString();
                    switch (ten_thamso)
                    {
                        case "UBND":
                            ubnd_new = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                            break;
                        case "TENDONVIVH":
                            donvi = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                            break;
                        case "TINH/TP":
                            tinh = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                            break;
                        case "THUTRUONGDONVI_BC":
                            thutruongdonvi = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                            break;
                        default:
                            break;
                    }
                }
                bool init_excel = false;
                if (ubnd.Trim().Length > 1 && tendvvh.Trim().Length > 1)
                {
                    init_excel = Init(ubnd, tendvvh);
                }
                else
                {
                    init_excel = Init(ubnd_new, donvi);
                }

                if (init_excel)
                {
                   
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = lTable.Columns.Count + 1 - CotCuoiKgXuat;
                    iRow = lStartRow;

                    for (int i = 0; i < lTable.Rows.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < lTable.Rows.Count - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 20;
                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = lTable.Rows[i].ItemArray;

                        waitDialogForm.Caption = string.Format("{0} của {1}", i + 1, lTable.Rows.Count);

                        iRow++;
                    }
                    //excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(lTable);
                    // Kết thúc
                    //excelRng.EntireRow.AutoFit();

                 
                    excel.Cells[iRow + 3, 2] = "Người lập";
                    excel.Cells[iRow + 2, c - 1] = string.Format("'" + tinh + ", ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year);
                    excel.Cells[iRow + 3, c - 1] = thutruongdonvi;

                    excelS.get_Range(excel.Cells[iRow + 2, 1], excel.Cells[iRow + 2, c]).Font.Italic = 1;
                    excelS.get_Range(excel.Cells[iRow + 3, 1], excel.Cells[iRow + 3, c]).Font.Bold = 1;


                    excel.Visible = true;
                    result = true;
                }
                else
                {
                    result = true;
                }

            }
            catch (Exception e)
            {
                waitDialogForm.Close();
                excel.Cursor = Excel.XlMousePointer.xlDefault;
                ClsBaoLoi.Loi("Thông báo", e.Message, e);
            }// Hủy các đối tượng xuất excel
            waitDialogForm.Close();
            excel.Cursor = Excel.XlMousePointer.xlDefault;
            Dispose();
            return result;
        }

        public static bool DataTableToExcel(int CotCuoiKgXuat)
        {
            return DataTableToExcel(CotCuoiKgXuat, "", "", "");
        }

        /// <summary>
        /// add by 0036 13/12/11
        /// Xuất các cột được hiển thị trên grid ra excel
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool VisibleColsToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, string ubnd, string donvi,string tinh, string thutruongdonvi)
        {
            waitDialogForm = new DevExpress.Utils.WaitDialogForm("Đang xuất excel ...", "Vui lòng chờ giây lát !");
            // Kết quả xuất toàn bộ datatable sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel


           
                try
                {
                    // Hiển thị cửa sổ Excel
                    SqlCommand selcmd = new SqlCommand("HT_THAMSO_SELECT", ClsConnection.MySqlConn);
                    selcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter HT_THAMSO_ADAPT = new SqlDataAdapter();
                    HT_THAMSO_ADAPT.SelectCommand = selcmd;
                    selcmd.CommandTimeout = 0;
                    DataSet dsThamSo = new DataSet();
                    HT_THAMSO_ADAPT.Fill(dsThamSo, "TB_THAMSO");

                    string ten_thamso = string.Empty;
                    string ubnd_new = string.Empty;
                    string donvi_new = string.Empty;
                    string tinh_new = string.Empty;
                    string thutruongdonvi_new = string.Empty;

                    for (int i = 0; i < dsThamSo.Tables["TB_THAMSO"].Rows.Count; i++)
                    {
                        ten_thamso = dsThamSo.Tables["TB_THAMSO"].Rows[i]["THAMSO"].ToString();
                        switch (ten_thamso)
                        {
                            case "UBND":
                                ubnd_new = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                                break;
                            case "TENDONVIVH":
                                donvi_new = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                                break;
                            case "TINH/TP":
                                tinh_new = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                                break;
                            case "THUTRUONGDONVI_BC":
                                thutruongdonvi_new = dsThamSo.Tables["TB_THAMSO"].Rows[i]["GIATRI"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (tinh.Trim().Length < 1)
                        tinh = tinh_new;
                    if (thutruongdonvi.Trim().Length < 1)
                        thutruongdonvi = thutruongdonvi_new;

                    bool init_excel = false;
                    if (ubnd.Trim().Length > 1 && donvi.Trim().Length > 1)
                    {
                        init_excel = Init(ubnd, donvi);
                    }
                    else
                    {
                        init_excel = Init(ubnd_new, donvi_new);
                    }


                    if (init_excel)
                    {
                        // Hiển thị cửa sổ Excel				
                        excel.UserControl = true;
                        excel.Cursor = Excel.XlMousePointer.xlWait;
                        int numRow = 0, c = 0, r = 0;
                        bool isNV_GIOI_TINH_NAM = false;

                        iRow = lStartRow;

                        // Loại bỏ các cột không có hoặc ẩn trên lưới
                        for (int i = 0; i < lTable.Columns.Count; i++)
                        {
                            if (grid.Columns[lTable.Columns[i].ColumnName] == null || !grid.Columns[lTable.Columns[i].ColumnName].Visible)
                            {
                                lTable.Columns.Remove(lTable.Columns[i]);
                                i = i - 1;
                                // visibleCols++;
                            }
                        }

                        // UBND va Truong

                        //

                        c = lTable.Columns.Count + 1;
                        r = lTable.Rows.Count + lStartRow + 2;

                        // Xét xem table có chứa cột Giới tính không?
                        if (lTable.Columns.Contains("NV_GIOI_TINH_NAM"))
                        {
                            isNV_GIOI_TINH_NAM = true;
                        }
                        else
                        {
                            isNV_GIOI_TINH_NAM = false;
                        }

                        for (int i = 0; i < lTable.Rows.Count; i++)
                        {
                            excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                            if (missing && i < lTable.Rows.Count - 1)
                            {
                                excelRng.Insert(oMissing);
                            }
                            excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                            excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                            excelRng.Borders.Value = borders;
                            excelRng.RowHeight = 20;
                            // Nội dung
                            numRow++;
                            excel.Cells[iRow, 1] = numRow;

                            if (isNV_GIOI_TINH_NAM)
                            {
                                if (lTable.Rows[i]["NV_GIOI_TINH_NAM"].ToString() == "0")
                                    lTable.Rows[i]["NV_GIOI_TINH_NAM"] = "X";
                                else
                                    lTable.Rows[i]["NV_GIOI_TINH_NAM"] = "";
                            }

                            excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = lTable.Rows[i].ItemArray;

                            waitDialogForm.Caption = string.Format("{0} của {1}", i + 1, lTable.Rows.Count);

                            iRow++;
                        }
                        //excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                        // Hiển thị thông tin của các thuộc tính
                        ShowProperties(lTable);
                        // Kết thúc
                        //excelRng.EntireRow.AutoFit();


                        excel.Cells[iRow + 3, 2] = "Người lập";
                       

                        // 0036 15/12/11: nếu bảng có nhiều cột, thì sử dụng 4 cột cuối
                        if (c > 8)
                        {
                            excel.Cells[r, c - 3] = string.Format("'" + tinh + ", ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year);
                            excel.get_Range(excel.Cells[r, c - 3], excel.Cells[r, c]).Merge();
                            excel.get_Range(excel.Cells[r, c - 3], excel.Cells[r, c - 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            excel.Cells[r + 1, c - 2] = thutruongdonvi;
                            excel.get_Range(excel.Cells[r + 1, c - 3], excel.Cells[r + 1, c]).Merge();
                            excel.get_Range(excel.Cells[r + 1, c - 3], excel.Cells[r + 1, c - 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            excel.get_Range(excel.Cells[r + 1, c - 3], excel.Cells[r + 1, c - 3]).Font.Bold = true;
                        }
                        // không thì chỉ dùng 2 cột cuối
                        else
                        {
                            excel.Cells[r, c - 1] = string.Format("'" + tinh + ", ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year);
                            excel.get_Range(excel.Cells[r, c - 1], excel.Cells[r, c]).Merge();
                            excel.get_Range(excel.Cells[r, c - 1], excel.Cells[r, c - 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            excel.Cells[r + 1, c - 1] = thutruongdonvi;
                            excel.get_Range(excel.Cells[r + 1, c - 1], excel.Cells[r + 1, c]).Merge();
                            excel.get_Range(excel.Cells[r + 1, c - 1], excel.Cells[r + 1, c - 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            excel.get_Range(excel.Cells[r + 1, c - 1], excel.Cells[r + 1, c - 1]).Font.Bold = true;
                        }
                        excelS.get_Range(excel.Cells[iRow + 2, 1], excel.Cells[iRow + 2, c]).Font.Italic = 1;
                        excelS.get_Range(excel.Cells[iRow + 3, 1], excel.Cells[iRow + 3, c]).Font.Bold = 1;

                        waitDialogForm.Close();
                        excel.Cursor = Excel.XlMousePointer.xlDefault;
                        excel.Visible = true;
                        result = true;
                    }
                }
                catch (Exception e)
                {
                    waitDialogForm.Close();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.ThongTin("Dữ liệu bị lỗi. Không kết xuất được.");
                }
                
            
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }

        public static bool VisibleColsToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            return VisibleColsToExcel(grid, "", "", "", "");
        }

        /// <summary>
        /// Xuất toàn bộ nội dung trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lươi cần xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridV9ToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, string ubnd, string donvi, string tinh,string thutruongdonvi)//1
        {
            return GridV9ToExcel(grid, 0, ubnd, donvi, tinh, thutruongdonvi);
        }


        /// <summary>
        /// Xuất toàn bộ nội dung trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lươi cần xuất</param>
        /// <param name="soCotKhongXuat">Số cột cần xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridV9ToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int soCotKhongXuat, string ubnd, string donvi, string tinh, string thutruongdonvi)//2
        {
            // Kết quả xuất toàn bộ datatable sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel
            if (Init(null, null))
            {
                try
                {
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = grid.Columns.Count - soCotKhongXuat + 1;
                    iRow = lStartRow;
                    for (int i = 0; i < grid.RowCount; i++)
                    {
                        if (!grid.IsDataRow(i))
                        {
                            continue;
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < grid.RowCount - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 20;

                        for (int j = 0; j < grid.Columns.Count; j++ )
                        {
                            if (!grid.Columns[j].Visible)
                            {
                                continue;
                            }
                            excel.Cells[iRow, j+2] = grid.GetRowCellDisplayText(i, grid.Columns[j].FieldName);
                        }
                        
                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;
                        iRow++;
                    }
                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(lTable);
                    //Kết thúc
                    //excelRng.EntireRow.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    //excel.get_Range(excel.Cells[9, 1], excel.Cells[iRow - 1, 16]).Columns.AutoFit();
                    //excel.get_Range(excel.Cells[9, 1], excel.Cells[iRow - 1, 16]).Rows.AutoFit();

                    // Hiển thị cuối
                    iRow = iRow + 2;
                   // excel.Cells[iRow, c - 1] = string.Format("dszjklf, ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year);
                    excel.Cells[iRow, c - 1] = string.Format("'" + tinh + ", ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year);
                    excel.get_Range(excel.Cells[iRow, c - 1], excel.Cells[iRow, c]).Merge();
                    excel.get_Range(excel.Cells[iRow, c - 1], excel.Cells[iRow, c - 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    excel.Cells[iRow + 1, c - 1] = thutruongdonvi;
                    excel.get_Range(excel.Cells[iRow + 1, c - 1], excel.Cells[iRow + 1, c]).Merge();
                    excel.get_Range(excel.Cells[iRow + 1, c - 1], excel.Cells[iRow + 1, c - 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.get_Range(excel.Cells[iRow + 1, c - 1], excel.Cells[iRow + 1, c - 1]).Font.Bold = true;


                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 2]).Merge();
                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).Font.Bold = true;
                    excel.Cells[2, 1] = ubnd.ToUpper();

                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 2]).Merge();
                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.get_Range(excel.Cells[3, 1], excel.Cells[3, 1]).Font.Bold = true;
                    excel.Cells[3, 1] = donvi.ToUpper();

                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }

        /// <summary>
        /// Xuất toàn bộ datatable sang excel
        /// </summary>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool DataTableToExcel(int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            return DataTableToExcel(0,null,null,null);
        }

        /// <summary>
        /// Xuất toàn bộ datatable sang excel
        /// </summary>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool DataTableToExcel(int CotCuoiKgXuat, int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            return DataTableToExcel(CotCuoiKgXuat,null,null,null);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel1(DevExpress.XtraGrid.Views.Grid.GridView grid,string ubnd, string donvi)//1
        {
            return GridViewToExcel(grid, false, false, null, null, ubnd, donvi);
        }
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid)//1
        {
            return GridViewToExcel(grid, false, false,null,null,null,null);
        }
        //1
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, bool SetColumName, bool XuatTungDong, string ubnd,string donvi, string tinh)
        {
            return GridViewToExcel(grid,SetColumName,XuatTungDong,null,null,ubnd,donvi);           
        }
        //2
        public static bool GridViewToExcel
            (DevExpress.XtraGrid.Views.Grid.GridView grid, bool SetColumName, bool XuatTungDong,
                int[] ColNeedSet, string[] ColType,string ubnd, string donvi)//2
        {
            return GridViewToExcel(grid, SetColumName, XuatTungDong, ColNeedSet, ColType, 0, string.Empty,ubnd,donvi);
        }
        //3
        public static bool GridViewToExcel
            (DevExpress.XtraGrid.Views.Grid.GridView grid, bool SetColumName, bool XuatTungDong,
                int[] ColNeedSet, string[] ColType, int RowHeader, string HeaderReport,string ubnd, string donvi)//3
        {
            return GridViewToExcel(grid, SetColumName, XuatTungDong, ColNeedSet, ColType, RowHeader, HeaderReport, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, 1,ubnd,donvi);
        }
        //4
        public static bool GridViewToExcel
            (DevExpress.XtraGrid.Views.Grid.GridView grid, bool SetColumName, bool XuatTungDong,
                int[] ColNeedSet, string[] ColType, int RowHeader, string HeaderReport, Excel.XlBorderWeight SetXlBorderWeight, Excel.XlColorIndex SetXlColorIndex, int AllBorder, string ubnd, string donvi)
        {
            // Kết quả xuất toàn bộ toàn bộ số dòng trên lưới sang excel
            bool result = false;

            // Khởi tạo các đối tượng xuất excel
            if (Init(ubnd, donvi))
            {
                int numRow = 0, c = 0;

                iRow = lStartRow;

                try
                {
                    // Lấy các dòng cần xuất
                    DataView source = null;

                    if (XuatTungDong)
                    {
                        DataTable sourceTable = GetTableFromDataGrid(grid);

                        SetGirdDexToDataTable(sourceTable, grid);

                        if (sourceTable != null)
                        {
                            source = sourceTable.DefaultView;
                        }
                    }
                    else
                    {
                        source = grid.DataSource as DataView;

                        source = source.Table.Copy().DefaultView;

                        source.RowFilter = grid.RowFilter;

                        DevExpress.XtraGrid.Columns.GridColumnSortInfoCollection sorts = grid.SortInfo;

                        if (sorts.Count > 0)
                        {
                            source.Sort = string.Format("{0} {1}", sorts[0].Column.FieldName, sorts[0].SortOrder.ToString().StartsWith("Ascending") ? "ASC" : "DESC");
                        }

                        // Loại bỏ các cột không có hoặc ẩn trên lưới
                        for (int i = 0; i < source.Table.Columns.Count; i++)
                        {
                            if (grid.Columns[source.Table.Columns[i].ColumnName] == null || !grid.Columns[source.Table.Columns[i].ColumnName].Visible)
                            {
                                source.Table.Columns.Remove(source.Table.Columns[i]);
                                i = i - 1;
                            }
                        }
                    }                   

                    /// Xuất tiêu đề cột                    
                    if (SetColumName)
                    {
                        int HeaderColCount = 0;

                        for (int i = 0; i < source.Table.Columns.Count; i++)
                        {
                            if (grid.Columns[source.Table.Columns[i].ColumnName] != null && grid.Columns[source.Table.Columns[i].ColumnName].Visible)
                            {
                                if (i == 0)
                                {
                                    excel.Cells[iRow - 1, 1] = "STT";

                                    excel.Cells[iRow - 1, 2] = grid.Columns[source.Table.Columns[i].ColumnName].Caption;

                                    HeaderColCount = 2;
                                }
                                else if (i == 1)
                                {
                                    excel.Cells[iRow - 1, 3] = grid.Columns[source.Table.Columns[i].ColumnName].Caption;
                                    HeaderColCount = 3;
                                }
                                else
                                {
                                    excel.Cells[iRow - 1, HeaderColCount + 1] = grid.Columns[source.Table.Columns[i].ColumnName].Caption;
                                    HeaderColCount++;
                                }
                            }
                        }

                        excelRng = excelS.get_Range(excel.Cells[iRow - 1, 1], excel.Cells[iRow - 1, HeaderColCount]);
                        excelRng.Font.Bold = true;
                        // edit by ntv 20/02/2012: canh giữa trong phần kết thúc
                        //excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        //excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    }

                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    c = source.Table.Columns.Count + 1;



                    // excel.Visible = true;
                    if (ColNeedSet != null && ColType != null && ColNeedSet.Length > 0 && ColType.Length > 0 && ColNeedSet.Length == ColType.Length)
                    {
                        ClsExcel.TemplateName = "DanhsachKhongDinhDang.xls";

                        for (int i = 0; i < ColType.Length; i++)
                        {
                            switch (ColType[i])
                            {
                                case "DateTime":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1,1] ,excel.Cells[1, 1]).Copy(excelRng);
                                    excelRng.ClearFormats();                                    
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    break;
                                case "Number0":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 2], excel.Cells[1, 2]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    break;
                                case "Number2":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 3], excel.Cells[1, 3]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    break;
                                case "Text":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 4], excel.Cells[1, 4]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                    break;
                                case "TextCenter":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 4], excel.Cells[1, 4]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    break;
                                case "Currency":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 5], excel.Cells[1, 5]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    break;
                                case "DateTime1":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 6], excel.Cells[1, 6]).Copy(excelRng);
                                    excelRng.ClearFormats();
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;                                    
                                    break;
                                default:
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                    break;
                            }
                        }

                        excelS.get_Range(excel.Cells[1, 1], excel.Cells[1, c]).ClearFormats();
                    }

                    for (int i = 0; i < source.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < source.Count - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 25;

                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = source[i].Row.ItemArray;

                        iRow++;
                    }

                    if (AllBorder <= 0)
                    {
                        excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).BorderAround(1, SetXlBorderWeight, SetXlColorIndex, 1);
                    }
                    else
                    {
                        excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).Borders.Value = AllBorder;
                    }

                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(grid);
                    // Kết thúc
                    // Canh giữa tiêu đề cột
                    excelRng = excelS.get_Range(excel.Cells[lStartRow - 1, 1], excel.Cells[lStartRow - 1, c]);
                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    excelRng = excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]);
                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    excelRng.Columns.AutoFit();
                    excelRng.Rows.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;

                    // Xuất tiêu đề
                    if (RowHeader > 0)
                    {
                        excelRng = excelS.get_Range(excel.Cells[RowHeader, 1], excel.Cells[RowHeader, c]);
                        excelRng.Font.Size = 14;
                        excelRng.MergeCells = true;
                        excelRng.Font.Bold = true;
                        excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        excel.Cells[RowHeader, 1] = HeaderReport;
                    }

                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }        

        public static bool DanhChoNguoiLamBiengXuatExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView, string DuongDanDenFileExcel)
        {
            return DanhChoNguoiLamBiengXuatExcel(XtraGridView, DuongDanDenFileExcel, 1);
        }

        public static bool DanhChoNguoiLamBiengXuatExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView, string DuongDanDenFileExcel, int StartRowExportData)
        {
            return DanhChoNguoiLamBiengXuatExcel(XtraGridView, DuongDanDenFileExcel, StartRowExportData, 0, string.Empty, false);
        }

        public static bool DanhChoNguoiLamBiengXuatExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView, string DuongDanDenFileExcel,
                int StartRowExportData, int RowHeaderReport, string HeaderReport)
        {
            return DanhChoNguoiLamBiengXuatExcel(XtraGridView, DuongDanDenFileExcel, StartRowExportData, RowHeaderReport, HeaderReport, false);
        }

        public static bool DanhChoNguoiLamBiengXuatExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView, string DuongDanDenFileExcel,
                int StartRowExportData, int RowHeaderReport, string HeaderReport, bool IsUseHeaderFontInClsExcel)
        {
            return DanhChoNguoiLamBiengXuatExcel(XtraGridView, DuongDanDenFileExcel, StartRowExportData, RowHeaderReport, HeaderReport, IsUseHeaderFontInClsExcel, false);
        }

        public static bool DanhChoNguoiLamBiengXuatExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView, string DuongDanDenFileExcel,
                int StartRowExportData, int RowHeaderReport, string HeaderReport, bool IsUseHeaderFontInClsExcel, bool IsUseDataExportFontInClsExcel)
        {
            bool KQua = true;

            if (StartRowExportData <= 1)
            {
                StartRowExportData = 2;
            }

            if (XtraGridView == null
                || XtraGridView.RowCount<=0
                || string.IsNullOrEmpty(DuongDanDenFileExcel))
            {
                KQua = false;
            }
            else
            {
                if (DuongDanDenFileExcel.EndsWith(".xls") 
                    || DuongDanDenFileExcel.EndsWith(".xlsm") 
                    || DuongDanDenFileExcel.EndsWith(".xlsx") 
                    || DuongDanDenFileExcel.EndsWith(".xlsb"))
                {
                    try
                    {
                        if (System.IO.File.Exists(DuongDanDenFileExcel))
                        {
                            DevExpress.XtraPrinting.XlsExportOptions oXlsExportOptions = new DevExpress.XtraPrinting.XlsExportOptions();

                            //oXlsExportOptions.Ignore256ColumnsLimit = true;
                            //oXlsExportOptions.Ignore65536RowsLimit = true;
                            oXlsExportOptions.ShowGridLines = true;                      

                            XtraGridView.ExportToXls(DuongDanDenFileExcel, oXlsExportOptions);                        

                            if (Init(DuongDanDenFileExcel, 1))
                            {
                                excel.UserControl = true;
                                excel.Cursor = Excel.XlMousePointer.xlWait;

                                for (int i = 0; i < StartRowExportData - 1; i++)
                                {
                                    excelS.get_Range(excel.Cells[1, 1], excel.Cells[1, XtraGridView.Columns.Count]).Insert(oMissing);
                                }

                                int iColVisible = 0;

                                for (int i = 0; i < XtraGridView.Columns.Count; i++)
                                {
                                    if (XtraGridView.Columns[i].Visible)
                                    {
                                        iColVisible++;
                                    }
                                }

                                if (RowHeaderReport < StartRowExportData - 1 && RowHeaderReport >= 1 && !string.IsNullOrEmpty(HeaderReport))
                                {
                                    excelRng = excelS.get_Range(excel.Cells[RowHeaderReport, 1], excel.Cells[RowHeaderReport, iColVisible]);
                                    
                                    try
                                    {
                                        if (IsUseHeaderFontInClsExcel)
                                        {
                                            excelRng.Font.Size = (decimal)HeaderFont[0];
                                            excelRng.Font.Bold = (bool)HeaderFont[1];
                                            excelRng.Font.Italic = (bool)HeaderFont[2];
                                            if (string.IsNullOrEmpty((string)HeaderFont[3]))
                                            {
                                                excelRng.Font.FontStyle = HeaderFont[3];
                                            }
                                        }
                                        else
                                        {
                                            excelRng.Font.Size = 14;
                                            excelRng.Font.Bold = true;
                                            excelRng.Font.Italic = false;
                                        }
                                    }
                                    catch
                                    {
                                        excelRng.Font.Size = 14;
                                        excelRng.Font.Bold = true;
                                        excelRng.Font.Italic = false;
                                    }                                    
                                    
                                    excelRng.MergeCells = true;

                                    excel.Cells[RowHeaderReport, 1] = HeaderReport;

                                    excelRng = excelS.get_Range(excel.Cells[RowHeaderReport, 1], excel.Cells[RowHeaderReport, 1]);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    
                                }

                                excelRng = excelS.get_Range(excel.Cells[StartRowExportData, 1], excel.Cells[StartRowExportData, iColVisible]);

                                try
                                {
                                    if (IsUseDataExportFontInClsExcel)
                                    {
                                        excelRng.Font.Size = (decimal)DataExportFont[0];
                                        excelRng.Font.Bold = true;
                                        excelRng.Font.Italic = (bool)DataExportFont[2];
                                        if (string.IsNullOrEmpty((string)DataExportFont[3]))
                                        {
                                            excelRng.Font.FontStyle = DataExportFont[3];
                                        }
                                    }
                                    else
                                    {
                                        excelRng.Font.Size = 10;
                                        excelRng.Font.Bold = true;
                                        excelRng.Font.Italic = false;
                                    }
                                }
                                catch
                                {
                                    excelRng.Font.Size = 10;
                                    excelRng.Font.Bold = true;
                                    excelRng.Font.Italic = false;
                                }

                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                excelRng = excelS.get_Range(excel.Cells[StartRowExportData + 1, 1], excel.Cells[XtraGridView.RowCount + StartRowExportData + 1, iColVisible]);

                                try
                                {
                                    if (IsUseDataExportFontInClsExcel)
                                    {
                                        excelRng.Font.Size = (decimal)DataExportFont[0];
                                        excelRng.Font.Bold = (bool)DataExportFont[1];
                                        excelRng.Font.Italic = (bool)DataExportFont[2];
                                        if (string.IsNullOrEmpty((string)DataExportFont[3]))
                                        {
                                            excelRng.Font.FontStyle = DataExportFont[3];
                                        }
                                    }
                                    else
                                    {
                                        excelRng.Font.Size = 10;
                                        excelRng.Font.Bold = false;
                                        excelRng.Font.Italic = false;
                                    }
                                }
                                catch
                                {
                                    excelRng.Font.Size = 10;
                                    excelRng.Font.Bold = false;
                                    excelRng.Font.Italic = false;
                                }

                                string ColumnType = string.Empty;
                                string ColEditType = string.Empty;

                                excelRng = excelS.get_Range(excel.Cells[StartRowExportData, 1], excel.Cells[XtraGridView.RowCount + StartRowExportData, iColVisible]);
                                excelRng.WrapText = false;
                                excelRng.EntireColumn.AutoFit();
                                excelRng.EntireRow.AutoFit();

                                iColVisible = 0;

                                for (int i = 0; i < XtraGridView.Columns.Count; i++)
                                {
                                    if (XtraGridView.Columns[i].Visible)
                                    {
                                        iColVisible++;                                       

                                        ColumnType = XtraGridView.Columns[i].ColumnType.Name.ToLower();

                                        ColEditType = string.Empty;

                                        if (XtraGridView.Columns[i].ColumnEdit != null)
                                        {                                            
                                            ColEditType = XtraGridView.Columns[i].ColumnEdit.GetType().ToString().ToLower();
                                        }

                                        excelRng = excelS.get_Range(excel.Cells[StartRowExportData + 1, iColVisible], excel.Cells[XtraGridView.RowCount + StartRowExportData, iColVisible]);

                                        switch (ColumnType)
                                        {
                                            case "string":
                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                                break;
                                            case "decimal":
                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                                break;                                                                   
                                            default:
                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                                break;
                                        }

                                        switch (ColEditType)
                                        {
                                            case "devexpress.xtraeditors.repository.repositoryitemcheckedit":
                                                excelRng.RowHeight = 20;
                                                if (ClsChangeType.change_int(excelRng.ColumnWidth) < 4)
                                                {
                                                    excelRng.ColumnWidth = 4;
                                                }
                                                try{
                                                    //string s = excel.Cells.ClearContents(StartRowExportData + 1, iColVisible);
                                                }
                                                catch{}

                                                excel.Cells[StartRowExportData + 1, iColVisible] = "X";

                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                                break;
                                            case "devexpress.xtraeditors.repository.repositoryitemlookupedit":
                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                                break;
                                            case "devexpress.xtraeditors.repository.repositoryitemdateedit":
                                                excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }                                
                                excel.Cursor = Excel.XlMousePointer.xlDefault;
                                excel.Visible = true;
                            }
                        }
                        else
                        {
                            KQua = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                        KQua = false;
                    }
                    finally
                    {
                        Dispose();
                    }
                }
                else
                {
                    KQua = false;
                }
            }

            return KQua;
        }
        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcelWithAutoFit(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            // Kết quả xuất toàn bộ toàn bộ số dòng trên lưới sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel
            if (Init(null, null))
            {
                try
                {
                    // Lấy các dòng cần xuất
                    DataView source = grid.DataSource as DataView;
                    source = source.Table.Copy().DefaultView;
                    source.RowFilter = grid.RowFilter;

                    DevExpress.XtraGrid.Columns.GridColumnSortInfoCollection sorts = grid.SortInfo;
                    if (sorts.Count > 0)
                    {
                        source.Sort = string.Format("{0} {1}", sorts[0].Column.FieldName, sorts[0].SortOrder.ToString().StartsWith("Ascending") ? "ASC" : "DESC");
                    }
                    // Loại bỏ các cột không có hoặc ẩn trên lưới
                    for (int i = 0; i < source.Table.Columns.Count; i++)
                    {
                        if (grid.Columns[source.Table.Columns[i].ColumnName] == null)
                        {
                            source.Table.Columns.Remove(source.Table.Columns[i]);
                            i = i - 1;
                        }
                        else if (!grid.Columns[source.Table.Columns[i].ColumnName].Visible)
                        {
                            source.Table.Columns.Remove(source.Table.Columns[i]);
                            i = i - 1;
                        }
                    }
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = source.Table.Columns.Count + 1;
                    iRow = lStartRow;
                    for (int i = 0; i < source.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < source.Count - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c - 1]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c - 1]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 25;
                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c - 1]).Value2 = source[i].Row.ItemArray;

                        iRow++;
                    }
                    //excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c - 1]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(grid);
                    //Kết thúc
                    //excelRng.EntireRow.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel mà ko có cột STT
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcelNoOrder(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            // Kết quả xuất toàn bộ toàn bộ số dòng trên lưới sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel
            if (Init(null, null))
            {
                try
                {
                    // Lấy các dòng cần xuất
                    DataView source = grid.DataSource as DataView;
                    source = source.Table.Copy().DefaultView;
                    source.RowFilter = grid.RowFilter;

                    DevExpress.XtraGrid.Columns.GridColumnSortInfoCollection sorts = grid.SortInfo;
                    if (sorts.Count > 0)
                    {
                        source.Sort = string.Format("{0} {1}", sorts[0].Column.FieldName, sorts[0].SortOrder.ToString().StartsWith("Ascending") ? "ASC" : "DESC");
                    }
                    // Loại bỏ các cột không có hoặc ẩn trên lưới
                    for (int i = 0; i < source.Table.Columns.Count; i++)
                    {
                        if (grid.Columns[source.Table.Columns[i].ColumnName] == null)
                        {
                            source.Table.Columns.Remove(source.Table.Columns[i]);
                            i = i - 1;
                        }
                        else if (!grid.Columns[source.Table.Columns[i].ColumnName].Visible)
                        {
                            source.Table.Columns.Remove(source.Table.Columns[i]);
                            i = i - 1;
                        }
                    }
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = source.Table.Columns.Count + 1;
                    iRow = lStartRow;
                    for (int i = 0; i < source.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < source.Count - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c - 1]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c - 1]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 25;
                        // Nội dung
                        numRow++;
                        //excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c - 1]).Value2 = source[i].Row.ItemArray;

                        iRow++;
                    }
                   // excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c - 1]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(grid);
                    //Kết thúc
                    //excelRng.EntireRow.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat)
        {
            // Kết quả xuất toàn bộ số dòng trên lưới sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel
            if (Init(null, null))
            {
                try
                {
                    // Lấy các dòng cần xuất
                    DataView source = grid.DataSource as DataView;
                    source.RowFilter = grid.RowFilter;

                    DevExpress.XtraGrid.Columns.GridColumnSortInfoCollection sorts = grid.SortInfo;
                    if (sorts.Count > 0)
                    {
                        source.Sort = string.Format("{0} {1}", sorts[0].Column.FieldName, sorts[0].SortOrder.ToString().StartsWith("Ascending") ? "ASC" : "DESC");
                    }
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = source.Table.Columns.Count + 1 - CotCuoiKgXuat;
                    iRow = lStartRow;
                    for (int i = 0; i < source.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < source.Count - 1)
                        {
                            excelRng.Insert(oMissing);
                        }
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 20;
                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = source[i].Row.ItemArray;

                        iRow++;
                    }
                    excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    // Hiển thị thông tin của các thuộc tính
                    ShowProperties(grid);
                    //Kết thúc
                    //excelRng.EntireRow.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    excel.Visible = true;
                    result = true;
                    source.RowFilter = string.Empty;
                    source.Sort = string.Empty;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }

        #region Không cần xem

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            return GridViewToExcel(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcelWithAutoFit(DevExpress.XtraGrid.Views.Grid.GridView grid, int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            //return GridViewToExcel(grid);
            return GridViewToExcelWithAutoFit(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel mà không hiển thị cột STT
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcelNoOrder(DevExpress.XtraGrid.Views.Grid.GridView grid, int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            return GridViewToExcelNoOrder(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat, int sumRowIndex, int[] sumColumnIndex)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            return GridViewToExcel(grid, CotCuoiKgXuat);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="showReportDateRow">Vị trí cột hiển thị ngày</param>
        /// <param name="showReportDateColumn">Vị trí dòng hiển thị ngày</param>
        /// <param name="showReportDateValue">Giá trị ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int showReportDateRow, int showReportDateColumn, DateTime showReportDateValue)
        {
            AExportToExcel.showReportDate = true;
            AExportToExcel.showReportDateRow = showReportDateRow;
            AExportToExcel.showReportDateColumn = showReportDateColumn;
            AExportToExcel.showReportDateValue = showReportDateValue;
            return GridViewToExcel(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="showReportDateRow">Vị trí cột hiển thị ngày</param>
        /// <param name="showReportDateColumn">Vị trí dòng hiển thị ngày</param>
        /// <param name="showReportDateValue">Giá trị ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat, int showReportDateRow, int showReportDateColumn, DateTime showReportDateValue)
        {
            AExportToExcel.showReportDate = true;
            AExportToExcel.showReportDateRow = showReportDateRow;
            AExportToExcel.showReportDateColumn = showReportDateColumn;
            AExportToExcel.showReportDateValue = showReportDateValue;
            return GridViewToExcel(grid, CotCuoiKgXuat);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <param name="showReportDateRow">Vị trí cột hiển thị ngày</param>
        /// <param name="showReportDateColumn">Vị trí dòng hiển thị ngày</param>
        /// <param name="showReportDateValue">Giá trị ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int sumRowIndex, int[] sumColumnIndex, int showReportDateRow, int showReportDateColumn, DateTime showReportDateValue)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            AExportToExcel.showReportDate = true;
            AExportToExcel.showReportDateRow = showReportDateRow;
            AExportToExcel.showReportDateColumn = showReportDateColumn;
            AExportToExcel.showReportDateValue = showReportDateValue;
            return GridViewToExcel(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <param name="showReportDateRow">Vị trí cột hiển thị ngày</param>
        /// <param name="showReportDateColumn">Vị trí dòng hiển thị ngày</param>
        /// <param name="showReportDateValue">Giá trị ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat, int sumRowIndex, int[] sumColumnIndex, int showReportDateRow, int showReportDateColumn, DateTime showReportDateValue)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            AExportToExcel.showReportDate = true;
            AExportToExcel.showReportDateRow = showReportDateRow;
            AExportToExcel.showReportDateColumn = showReportDateColumn;
            AExportToExcel.showReportDateValue = showReportDateValue;
            return GridViewToExcel(grid, CotCuoiKgXuat);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="showStartDateRow">Vị trí dòng hiển thị từ ngày</param>
        /// <param name="showStartDateColumn">Vị trí cột hiển thị từ ngày</param>
        /// <param name="showStartDateValue">Giá trị từ ngày cần hiển thị</param>
        /// <param name="showEndDateRow">Vị trí dòng hiển thị đến ngày</param>
        /// <param name="showEndDateColumn">Vị trí cột hiển thị đến ngày</param>
        /// <param name="showEndDateValue">Giá trị đến ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int showStartDateRow, int showStartDateColumn, DateTime showStartDateValue, int showEndDateRow, int showEndDateColumn, DateTime showEndDateValue)
        {
            AExportToExcel.ShowBetweenDate = true;
            AExportToExcel.showStartDateRow = showStartDateRow;
            AExportToExcel.showStartDateColumn = showStartDateColumn;
            AExportToExcel.showStartDateValue = showStartDateValue;
            AExportToExcel.showEndDateRow = showEndDateRow;
            AExportToExcel.showEndDateColumn = showEndDateColumn;
            AExportToExcel.showEndDateValue = showEndDateValue;
            return GridViewToExcel(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="showStartDateRow">Vị trí dòng hiển thị từ ngày</param>
        /// <param name="showStartDateColumn">Vị trí cột hiển thị từ ngày</param>
        /// <param name="showStartDateValue">Giá trị từ ngày cần hiển thị</param>
        /// <param name="showEndDateRow">Vị trí dòng hiển thị đến ngày</param>
        /// <param name="showEndDateColumn">Vị trí cột hiển thị đến ngày</param>
        /// <param name="showEndDateValue">Giá trị đến ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat, int showStartDateRow, int showStartDateColumn, DateTime showStartDateValue, int showEndDateRow, int showEndDateColumn, DateTime showEndDateValue)
        {
            AExportToExcel.ShowBetweenDate = true;
            AExportToExcel.showStartDateRow = showStartDateRow;
            AExportToExcel.showStartDateColumn = showStartDateColumn;
            AExportToExcel.showStartDateValue = showStartDateValue;
            AExportToExcel.showEndDateRow = showEndDateRow;
            AExportToExcel.showEndDateColumn = showEndDateColumn;
            AExportToExcel.showEndDateValue = showEndDateValue;
            return GridViewToExcel(grid, CotCuoiKgXuat);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <param name="showStartDateRow">Vị trí dòng hiển thị từ ngày</param>
        /// <param name="showStartDateColumn">Vị trí cột hiển thị từ ngày</param>
        /// <param name="showStartDateValue">Giá trị từ ngày cần hiển thị</param>
        /// <param name="showEndDateRow">Vị trí dòng hiển thị đến ngày</param>
        /// <param name="showEndDateColumn">Vị trí cột hiển thị đến ngày</param>
        /// <param name="showEndDateValue">Giá trị đến ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int sumRowIndex, int[] sumColumnIndex, int showStartDateRow, int showStartDateColumn, DateTime showStartDateValue, int showEndDateRow, int showEndDateColumn, DateTime showEndDateValue)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            AExportToExcel.ShowBetweenDate = true;
            AExportToExcel.showStartDateRow = showStartDateRow;
            AExportToExcel.showStartDateColumn = showStartDateColumn;
            AExportToExcel.showStartDateValue = showStartDateValue;
            AExportToExcel.showEndDateRow = showEndDateRow;
            AExportToExcel.showEndDateColumn = showEndDateColumn;
            AExportToExcel.showEndDateValue = showEndDateValue;
            return GridViewToExcel(grid);
        }

        /// <summary>
        /// Xuất toàn bộ số dòng trên lưới sang excel
        /// </summary>
        /// <param name="grid">Lưới cần xuất</param>
        /// <param name="CotCuoiKgXuat">Số lượng cột không xuất</param>
        /// <param name="sumRowIndex">Dòng hiển thị giá trị tổng cộng</param>
        /// <param name="sumColumnIndex">Vị trí các cột cần tính tổng cộng</param>
        /// <param name="showStartDateRow">Vị trí dòng hiển thị từ ngày</param>
        /// <param name="showStartDateColumn">Vị trí cột hiển thị từ ngày</param>
        /// <param name="showStartDateValue">Giá trị từ ngày cần hiển thị</param>
        /// <param name="showEndDateRow">Vị trí dòng hiển thị đến ngày</param>
        /// <param name="showEndDateColumn">Vị trí cột hiển thị đến ngày</param>
        /// <param name="showEndDateValue">Giá trị đến ngày cần hiển thị</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public static bool GridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView grid, int CotCuoiKgXuat, int sumRowIndex, int[] sumColumnIndex, int showStartDateRow, int showStartDateColumn, DateTime showStartDateValue, int showEndDateRow, int showEndDateColumn, DateTime showEndDateValue)
        {
            AExportToExcel.isSum = true;
            AExportToExcel.sumRowIndex = sumRowIndex;
            AExportToExcel.sumColumnIndex = sumColumnIndex;
            AExportToExcel.ShowBetweenDate = true;
            AExportToExcel.showStartDateRow = showStartDateRow;
            AExportToExcel.showStartDateColumn = showStartDateColumn;
            AExportToExcel.showStartDateValue = showStartDateValue;
            AExportToExcel.showEndDateRow = showEndDateRow;
            AExportToExcel.showEndDateColumn = showEndDateColumn;
            AExportToExcel.showEndDateValue = showEndDateValue;
            return GridViewToExcel(grid, CotCuoiKgXuat);
        }
        #endregion

        /// <summary>
        /// Trả về bảng có cấu trúc giống cấu trúc RootTable của XtraGrid
        /// </summary>
        /// <param name="pDexGrid">Truyền vào đối tượng DevExpress.XtraGrid.Views.Grid.GridView (đới tượng datagird của Janus)</param>
        /// <returns>Trả về System.Data.DataTable có cấu trúc bảng giống cấu trúc RootTable của XtraGrid</returns>
        public static System.Data.DataTable GetTableFromDataGrid(DevExpress.XtraGrid.Views.Grid.GridView pDexGrid)
        {
            System.Data.DataTable tb = new System.Data.DataTable();

            try
            {
                if (pDexGrid.Columns.Count > 0)
                {
                    for (int i = 0; i < pDexGrid.Columns.Count; i++)
                    {
                        if (pDexGrid.Columns[i].Visible)
                        {
                            tb.Columns.Add(pDexGrid.Columns[i].FieldName);
                        }
                    }

                    if (tb.HasErrors)
                    {
                        tb.RejectChanges();
                    }
                    else
                    {
                        tb.AcceptChanges();
                    }

                    return tb;
                }
                else
                {
                    tb.Dispose();
                    return null;
                }
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                tb.Dispose();

                return null;
            }
        }

        public static bool DataTableToExcel_TimKiem(int CotCuoiKgXuat, List<string> tieude)
        {
            waitDialogForm = new DevExpress.Utils.WaitDialogForm("Đang xuất excel ...", "Vui lòng chờ giây lát !");
            // Kết quả xuất toàn bộ datatable sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel
            if (Init())
            {
                try
                {
                    excelS.get_Range(excel.Cells[2, 1], excel.Cells[2, 5]).Merge(oMissing);
                    excelS.get_Range(excel.Cells[3, 1], excel.Cells[3, 5]).Merge(oMissing);
                    for (int i = 0; i < tieude.Count; i++)
                    {
                        excel.Cells[lStartRow - 1, i + 2] = tieude[i];
                        excelS.get_Range(excel.Cells[lStartRow - 1, i + 2], excel.Cells[lStartRow - 1, i + 2]).Font.Bold = true;
                        excelS.get_Range(excel.Cells[lStartRow - 1, i + 2], excel.Cells[lStartRow - 1, i + 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    }
                    excelS.get_Range(excel.Cells[5, 1], excel.Cells[5, tieude.Count + 1]).Merge(oMissing);
                    excel.Cells[5, 1] = "KẾT QUẢ TÌM KIẾM";
                    excelS.get_Range(excel.Cells[5, 1], excel.Cells[5, 1]).Font.Bold = true;
                    excelS.get_Range(excel.Cells[5, 1], excel.Cells[5, 1]).Font.Size = 20;
                    excelS.get_Range(excel.Cells[5, 1], excel.Cells[5, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[lStartRow - 1, 1] = "STT";
                    excelS.get_Range(excel.Cells[lStartRow - 1, 1], excel.Cells[lStartRow - 1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    excelS.get_Range(excel.Cells[lStartRow - 1, 1], excel.Cells[lStartRow - 1, 1]).Font.Bold = true;
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    int numRow = 0, c = 0;
                    c = lTable.Columns.Count + 1 - CotCuoiKgXuat;
                    for (int i = 0; i < lTable.Columns.Count; i++)
                    {
                        if (lTable.Columns[i].DataType == System.Type.GetType("System.String"))
                        {
                            excelS.get_Range(excel.Cells[lStartRow, i + 2], excel.Cells[lStartRow + lTable.Rows.Count, i + 2]).NumberFormat = "@";
                        }
                        else if (lTable.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                        {
                            if (lTable.Columns[i].ToString() == "DM_L_BAC_LUONG")
                            {
                                excelS.get_Range(excel.Cells[lStartRow, i + 2], excel.Cells[lStartRow + lTable.Rows.Count, i + 2]).NumberFormat = "#,##0.00";
                            }
                            else
                            {
                                //  excelS.get_Range(excel.Cells[lStartRow, i + 2], excel.Cells[lStartRow + lTable.Rows.Count, i + 2]).NumberFormat = "##0";
                            }
                        }
                        else if (lTable.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                        {
                            excelS.get_Range(excel.Cells[lStartRow, i + 2], excel.Cells[lStartRow + lTable.Rows.Count, i + 2]).NumberFormat = "dd-MM-yyyy";
                        }
                        else
                        {
                            excelS.get_Range(excel.Cells[lStartRow, i + 2], excel.Cells[lStartRow + lTable.Rows.Count, i + 2]).NumberFormat = "###";
                        }
                    }
                    iRow = lStartRow;
                    for (int i = 0; i < lTable.Rows.Count; i++)
                    {
                        //excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        //if (missing && i < lTable.Rows.Count - 1)
                        //{
                        //    excelRng.Insert(oMissing);
                        //}
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        //  excelS.get_Range(excel.Cells[lRowFormat, 1], excel.Cells[lRowFormat, c]).Copy(excelRng);
                        excelRng.Borders.Value = borders;
                        excelRng.RowHeight = 20;
                        // Nội dung
                        numRow++;
                        excel.Cells[iRow, 1] = numRow;

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = lTable.Rows[i].ItemArray;

                        waitDialogForm.Caption = string.Format("{0} của {1}", i + 1, lTable.Rows.Count);

                        iRow++;
                    }
                    excelRng = excel.get_Range(excel.Cells[lStartRow - 1, 1], excel.Cells[lStartRow - 1, c]);
                    excelRng.Borders.Value = borders;
                    excelRng = excel.get_Range(excel.Cells[7, 1], excel.Cells[lTable.Rows.Count + lStartRow, c]);
                    //excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).BorderAround(1, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    // Hiển thị thông tin của các thuộc tính
                    //  ShowProperties(lTable);
                    //Kết thúc
                    excelRng.EntireColumn.AutoFit();
                    excelRng.EntireRow.AutoFit();
                    waitDialogForm.Close();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    waitDialogForm.Close();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            // Hủy các đối tượng xuất excel
            Dispose();
            return result;
        }


        /// <summary>
        /// Dùng chuyển các dòng dữ liệu đang hiển thị trên XtraGrid vào System.Data.DataTable cần chứa dữ liệu.
        /// </summary>
        /// <param name="pTableContain">Truyền vào đối trượng System.Data.DataTable dùng chứa dữ liệu cần chuyển</param>
        /// <param name="pGirdDex">Truyền vào đối tượng DevExpress.XtraGrid.Views.Grid.GridView (đới tượng datagird của Janus)</param>
        public static void SetGirdDexToDataTable
            (System.Data.DataTable pTableContain, DevExpress.XtraGrid.Views.Grid.GridView pGirdDex)
        {

            foreach (GridColumn column in pGirdDex.Columns)
            {
                column.GroupInterval = 0;
                column.SortMode = 0;
            }

            pTableContain.Clear();

            if (pGirdDex.RowCount > 0)
            {
                object[] ojValues = new object[pTableContain.Columns.Count];

                string s = string.Empty;

                try
                {
                    for (int i = 0; i < pGirdDex.RowCount; i++)
                    {
                        pGirdDex.MoveFirst();

                        for (int j = 0; j < pTableContain.Columns.Count; j++)
                        {
                            s = string.Empty;


                            //DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
                            if (pGirdDex.Columns[pTableContain.Columns[j].ColumnName].ColumnEdit != null)
                            {
                                try
                                {
                                    s = pGirdDex.Columns[pTableContain.Columns[j].ColumnName].ColumnEdit.GetType().ToString().ToLower();

                                    switch (s)
                                    {
                                        case "devexpress.xtraeditors.repository.repositoryitemcheckedit":
                                            s = pGirdDex.GetRowCellDisplayText(i, pTableContain.Columns[j].ColumnName);
                                            if (s == "0" || s == "Unchecked" || s == "Indeterminate")
                                            {
                                                s = string.Empty;
                                            }
                                            else
                                            {
                                                s = "X";
                                            }
                                            ojValues[j] = s;
                                            break;
                                        case "devexpress.xtraeditors.repository.repositoryitemlookupedit":
                                            s = pGirdDex.GetRowCellDisplayText(i, pTableContain.Columns[j].ColumnName);
                                            ojValues[j] = s.Trim();
                                            break;
                                        case "devexpress.xtraeditors.repository.repositoryitemdateedit":
                                            s = pGirdDex.GetRowCellDisplayText(i, pTableContain.Columns[j].ColumnName);
                                            ojValues[j] = s.Trim();
                                            break;
                                        case "devexpress.xtraeditors.repository.repositoryitempopupcontaineredit":
                                            s = pGirdDex.GetRowCellDisplayText(i, pTableContain.Columns[j].ColumnName);
                                            ojValues[j] = s.Trim();
                                            break;
                                        default:
                                            try
                                            {
                                                ojValues[j] = pGirdDex.GetDataRow(i)[pTableContain.Columns[j].ColumnName];
                                            }
                                            catch { }
                                            break;
                                    }
                                }
                                catch
                                {
                                    s = string.Empty;
                                }
                            }
                            else
                            {

                                try
                                {
                                    ojValues[j] = pGirdDex.GetDataRow(i)[pTableContain.Columns[j].ColumnName];
                                }
                                catch { }
                            }
                            
                        }

                        pTableContain.Rows.Add(ojValues);
                    }

                    pTableContain.AcceptChanges();

                    ojValues = null;

                }
                catch (Exception e)
                {
                    ojValues = null;
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
            }
            else
            {
                return;
            }
        }        
    }
}
