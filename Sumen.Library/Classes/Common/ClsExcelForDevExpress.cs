using System;
using System.Data;
using DevExpress.XtraGrid.Columns;

namespace PhoHa7.Library.Classes.Common
{
    public class ClsExcelForDevExpress : AExportToExcel
    {
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
            catch
            {
                tb.Dispose();

                return null;
            }
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
                                            ojValues[j] = s;
                                            break;
                                        case "devexpress.xtraeditors.repository.repositoryitemdateedit":
                                            s = pGirdDex.GetRowCellDisplayText(i, pTableContain.Columns[j].ColumnName);
                                            ojValues[j] = s;
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

        /// <summary>
        /// Xuất dữ liệu sang Excel từ GridViewDevExpress
        /// </summary>
        /// <param name="XtraGridView">Truyền vào đối tượng DevExpress.XtraGrid.Views.Grid.GridView</param>
        /// <param name="DuongDanDenFileExcel">Truyền vào đường dẫn tới file excel</param>
        /// <param name="StartRowExportData">Truyền vào dòng trên excel bắt đầu xuất dữ liệu</param>
        /// <param name="StartRowHeader">Truyền vào dòng trên excel dành xuất tiêu đề</param>
        /// <param name="HeaderText">Truyền vào nội dung tiêu đề</param>
        /// <param name="UseHeaderFont">
        /// Thiết lập Font cho dữ liệu cần xuất thông qua property HeaderFont
        /// Truyền vào true để sử dụng, false nếu muốn theo thiết lập mặc định
        /// </param>
        /// <param name="UseDataFont">
        /// Thiết lập Font cho dữ liệu cần xuất thông qua property DataExportFont
        /// Truyền vào true để sử dụng, false nếu muốn theo thiết lập mặc định
        /// </param>
        /// <returns>true xuất thành công, false nếu xuất không thành công</returns>
        public static bool XtraGridExportToExcel
            (DevExpress.XtraGrid.Views.Grid.GridView XtraGridView,
                string DuongDanDenFileExcel, int StartRowExportData,
                    int StartRowHeader, string HeaderText, bool UseHeaderFont, bool UseDataFont)
        {
            bool KQua = true;

            if (StartRowExportData <= 1)
            {
                StartRowExportData = 2;
            }

            if (XtraGridView == null
                || XtraGridView.RowCount <= 0
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

                                if (StartRowHeader < StartRowExportData - 1 && StartRowHeader >= 1 && !string.IsNullOrEmpty(HeaderText))
                                {
                                    excelRng = excelS.get_Range(excel.Cells[StartRowHeader, 1], excel.Cells[StartRowHeader, iColVisible]);

                                    try
                                    {
                                        if (UseHeaderFont)
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

                                    excel.Cells[StartRowHeader, 1] = HeaderText;

                                    excelRng = excelS.get_Range(excel.Cells[StartRowHeader, 1], excel.Cells[StartRowHeader, 1]);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                }

                                excelRng = excelS.get_Range(excel.Cells[StartRowExportData, 1], excel.Cells[StartRowExportData, iColVisible]);

                                try
                                {
                                    if (UseDataFont)
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
                                    if (UseDataFont)
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
                        ClsBaoLoi.Loi("Thông báo", ex.Message, ex);
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

        public static bool CopyDataFromXtraGridExportToExcel
            (DevExpress.XtraGrid.Views.Grid.GridView grid,
                int[] ColNeedSet, string[] ColType, int RowHeader, string HeaderReport)
        {
            // Kết quả xuất toàn bộ toàn bộ số dòng trên lưới sang excel
            bool result = false;
            // Khởi tạo các đối tượng xuất excel

            ClsExcel.TemplateName = "DanhsachKhongDinhDang.xls";

            if (Init(null, null))
            {
                int numRow = 0, c = 0;

                iRow = lStartRow;

                try
                {
                    // Lấy các dòng cần xuất
                    DataTable sourceTable = GetTableFromDataGrid(grid);

                    SetGirdDexToDataTable(sourceTable, grid);

                    /// Xuất tiêu đề cột                    
                    int HeaderColCount = 0;

                    for (int i = 0; i < sourceTable.Columns.Count; i++)
                    {
                        if (grid.Columns[sourceTable.Columns[i].ColumnName] != null && grid.Columns[sourceTable.Columns[i].ColumnName].Visible)
                        {
                            if (i == 0)
                            {
                                excel.Cells[iRow - 1, 1] = "Stt";

                                excel.Cells[iRow - 1, 2] = grid.Columns[sourceTable.Columns[i].ColumnName].Caption;

                                HeaderColCount = 2;
                            }
                            else if (i == 1)
                            {
                                excel.Cells[iRow - 1, 3] = grid.Columns[sourceTable.Columns[i].ColumnName].Caption;
                                HeaderColCount = 3;
                            }
                            else
                            {
                                excel.Cells[iRow - 1, HeaderColCount + 1] = grid.Columns[sourceTable.Columns[i].ColumnName].Caption;
                                HeaderColCount++;
                            }
                        }
                    }

                    excelRng = excelS.get_Range(excel.Cells[iRow - 1, 1], excel.Cells[iRow - 1, HeaderColCount]);

                    excelRng.Font.Bold = true;
                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    // Hiển thị cửa sổ Excel				
                    excel.UserControl = true;
                    excel.Cursor = Excel.XlMousePointer.xlWait;
                    c = sourceTable.Columns.Count + 1;

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


                    if (ColNeedSet != null && ColType != null && ColNeedSet.Length > 0 && ColType.Length > 0 && ColNeedSet.Length == ColType.Length)
                    {
                        for (int i = 0; i < ColType.Length; i++)
                        {
                            switch (ColType[i])
                            {
                                case "DateTime":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Copy(excelRng);
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
                                case "Currency":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 5], excel.Cells[1, 5]).Copy(excelRng);
                                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    excelRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    break;
                                case "DateTime1":
                                    excelRng = excelS.get_Range(excel.Cells[lRowFormat, ColNeedSet[i]], excel.Cells[lRowFormat, ColNeedSet[i]]);
                                    excelS.get_Range(excel.Cells[1, 6], excel.Cells[1, 6]).Copy(excelRng);
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

                    for (int i = 0; i < sourceTable.Rows.Count; i++)
                    {
                        excelRng = excelS.get_Range(excel.Cells[iRow, 1], excel.Cells[iRow, c]);
                        if (missing && i < sourceTable.Rows.Count - 1)
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

                        excelS.get_Range(excel.Cells[iRow, 2], excel.Cells[iRow, c]).Value2 = sourceTable.Rows[i].ItemArray;

                        iRow++;
                    }

                    excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]).Borders.Value = true;

                    //Kết thúc
                    excelRng = excelS.get_Range(excel.Cells[ColumnHeader, 1], excel.Cells[iRow - 1, c]);
                    excelRng.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    excelRng.EntireColumn.AutoFit();

                    excelRng.EntireRow.AutoFit();
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    excel.Visible = true;
                    result = true;
                }
                catch (Exception e)
                {
                    excel.Cursor = Excel.XlMousePointer.xlDefault;
                    ClsBaoLoi.Loi("Thông báo", e.Message, e);
                }
                finally
                {
                    // Hủy các đối tượng xuất excel
                    Dispose();
                }
            }

            return result;
        }
    }
}
