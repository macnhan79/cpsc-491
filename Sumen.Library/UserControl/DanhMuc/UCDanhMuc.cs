using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PhoHa7.Library.Classes.Common;
using System.Data.SqlClient;

namespace PhoHa7.Library.UserControl.DanhMuc
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [Serializable]
    public partial class UCDanhMuc : XtraUserControl
    {
        private string _msError = "Lỗi trong khi cập nhật dữ liệu";
        private string _msDelete = "Thông tin sẽ bị xóa";
        private bool _throwEx = true;

        private ClsDanhMuc clsDanhMuc = new ClsDanhMuc();

        private DataTable _dtShowCols;
        private DataTable _dtParentTable;

        public delegate void setValidate(GridView g, ValidateRowEventArgs e);
        public setValidate mySetValidate;

        /// <summary>
        /// Có quyền thêm, sửa, xóa hay không. True nếu có, ngược lại là False
        /// </summary>
        private bool permission = false;
        private bool permissionAdd = false;
        private bool permissionXoa = false;
        public UCDanhMuc()
        {
            _dtShowCols = new DataTable("ShowCols");
            _dtShowCols.Columns.Add(new DataColumn("TableName"));
            _dtShowCols.Columns.Add(new DataColumn("Name"));
            _dtShowCols.Columns.Add(new DataColumn("Text"));
            _dtShowCols.AcceptChanges();

            _dtParentTable = new DataTable("ShowCols");
            _dtParentTable.Columns.Add(new DataColumn("TableName"));
            _dtParentTable.Columns.Add(new DataColumn("Name"));
            _dtParentTable.Columns.Add(new DataColumn("Text"));
            _dtParentTable.AcceptChanges();

            InitializeComponent();
            btnArray.btnState = ButtonsArray.btnStateEnum.Int;

            gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

            if (gridView.RowCount > 0)
            {
                btnArray.btnSua.Enabled = true;
                btnArray.btnXoa.Enabled = true;
            }
            else
            {
                btnArray.btnSua.Enabled = false;
                btnArray.btnXoa.Enabled = false;
            }
            //this.ParentForm.Activated += new EventHandler(ParentForm_Activated);
        }

        void ParentForm_Activated(object sender, EventArgs e)
        {
            clsDanhMuc.Fill();
            this.CreateGrid();
        }

        /// <summary>
        /// Có quyền thêm, sửa, xóa hay không. True nếu có, ngược lại là False
        /// </summary>
        public bool Permission
        {
            get { return permission; }
            set
            {
                permission = value;
                btnArray.btnThem.Enabled = btnArray.btnSua.Enabled = btnArray.btnXoa.Enabled = permission;
            }
        }


        #region UI Designer
        [Category("Danh Mục")]
        [DefaultValue(typeof(string), "Lỗi trong khi cập nhật dữ liệu")]
        [Description("Câu thông báo lỗi trong khi cập nhật dữ liệu")]
        public string MessageTextError
        {
            get
            {
                return _msError;
            }
            set
            {
                _msError = value;
            }
        }

        [Category("Danh Mục")]
        [DefaultValue(typeof(bool), "true")]
        [Description("Có ném lỗi ra không")]
        public bool ThrowEx
        {
            get
            {
                return _throwEx;
            }
            set
            {
                _throwEx = value;
            }
        }

        [Category("Danh Mục")]
        [DefaultValue(typeof(string), "Thông tin sẽ bị xóa")]
        [Description("Câu thông báo xoá dữ liệu")]
        public string MessageTextDelete
        {
            get
            {
                return _msDelete;
            }
            set
            {
                _msDelete = value;
            }
        }

        [Category("Danh Mục")]
        [DefaultValue(true)]
        [Description("Hiển thị Button")]
        public bool ButtonArrayVisibility
        {
            get
            {
                return this.layoutButtonArray.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            set
            {
                this.layoutButtonArray.Visibility = value ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        #endregion

        #region Data

        [Category("Danh Mục - Connection")]
        [Description("MySqlConnection dùng để kết nối vào database")]
        [DefaultValue(null)]
        public SqlConnection Connection
        {
            get
            {//clsDanhMuc.ConnectionDanhMuc
                return new SqlConnection("Server = localhost; Data Source =sumen; UID = root; Password = 123456;");
            }
            set
            {
                clsDanhMuc.ConnectionDanhMuc = value;
                clsDanhMuc.CreateSQL();
                if (!DesignMode)
                    clsDanhMuc.Fill();
                this.CreateGrid();
            }
        }

        [DefaultValue(typeof(string), "")]
        [Description("Tên bảng làm danh mục")]
        [Category("Danh Mục - Connection")]
        public string TableName
        {
            get
            {
                return clsDanhMuc.TableName;
            }
            set
            {
                clsDanhMuc.TableName = value;
                clsDanhMuc.CreateSQL();
                if (!DesignMode)
                    clsDanhMuc.Fill();

                this.CreateGrid();
            }
        }

        private void CreateGrid()
        {
            if (clsDanhMuc.dsDanhMuc != null && clsDanhMuc.dsDanhMuc.Tables.Contains("SchemaTable"))
            {
                if (this.DesignMode)
                {
                    this.dgDanhMuc.DataSource = null;

                    this.dgDanhMuc.DataSource = clsDanhMuc.dsDanhMuc.Tables["SchemaTable"];
                    this.dgDanhMuc.RefreshDataSource();
                    this.dgDanhMuc.Refresh();

                }
                else
                {
                    this.dgDanhMuc.DataSource = clsDanhMuc.dsDanhMuc.Tables[TableName];
                    this.dgDanhMuc.RefreshDataSource();
                    this.dgDanhMuc.Refresh();

                }
                this.labelColDesign.Text = "";
                foreach (DataColumn var in clsDanhMuc.dsDanhMuc.Tables["SchemaTable"].Columns)
                {
                    this.labelColDesign.Text += "'" + var.ColumnName + "',";
                    if (_dtShowCols.Select("Name = '" + var.ColumnName + "'").Length == 0)
                    {
                        DataRow dr = _dtShowCols.NewRow();
                        dr["TableName"] = TableName;
                        dr["Name"] = var.ColumnName;
                        _dtShowCols.Rows.Add(dr);
                    }
                }
                _dtShowCols.AcceptChanges();
                foreach (DataRow dr in _dtShowCols.Select("Name not in (" + this.labelColDesign.Text + "'Null')"))
                {
                    dr.Delete();
                }
                _dtShowCols.AcceptChanges();

                this.labelColDesign.Text = "";
                foreach (DataRow dr in clsDanhMuc.dsDanhMuc.Tables["ParentTable"].Rows)
                {
                    foreach (DataColumn var in clsDanhMuc.dsDanhMuc.Tables["SchemaTable" + dr["parent_table_name"]].Columns)
                    {
                        this.labelColDesign.Text += "'" + var.ColumnName + "',";
                        if (_dtParentTable.Select("Name = '" + var.ColumnName + "'").Length == 0)
                        {
                            DataRow drParent = _dtParentTable.NewRow();
                            drParent["TableName"] = dr["parent_table_name"].ToString();
                            drParent["Name"] = var.ColumnName;
                            _dtParentTable.Rows.Add(drParent);
                        }
                    }
                    _dtParentTable.AcceptChanges();
                    foreach (DataRow var in _dtParentTable.Select("TableName = '" + dr["parent_table_name"] + "' and Name not in (" + this.labelColDesign.Text + "'Null')"))
                    {
                        var.Delete();
                    }
                    _dtParentTable.AcceptChanges();
                }

                CreateCol();
            }
        }

        bool fFormatCol = true;
        private void CreateCol()
        {
            fFormatCol = false;
            if (clsDanhMuc.dsDanhMuc != null && clsDanhMuc.dsDanhMuc.Tables.Contains("SchemaTable"))
            {
                // Hiển thị Column's head là các Text, chứ không phải Name
                foreach (DataRow dr in _dtShowCols.Rows)
                {
                    if (gridView.Columns.Contains(gridView.Columns[dr["Name"].ToString()]))
                    {
                        gridView.Columns[dr["Name"].ToString()].Visible = (dr["Text"].ToString() != "");
                        gridView.Columns[dr["Name"].ToString()].Caption = "" + dr["Text"];
                    }
                }

                foreach (DataRow dr in clsDanhMuc.dsDanhMuc.Tables["ParentTable"].Rows)
                {
                    if (gridView.Columns.Contains(gridView.Columns[dr["column_name"].ToString()]))
                    {
                        gridView.Columns[dr["column_name"].ToString()].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                        gridView.Columns[dr["column_name"].ToString()].ColumnEdit.NullText = "Chọn";
                        RepositoryItemLookUpEdit rItem = (gridView.Columns[dr["column_name"].ToString()].ColumnEdit as RepositoryItemLookUpEdit);
                        rItem.Name = dr["parent_table_name"].ToString();
                        rItem.DataSource = clsDanhMuc.dsDanhMuc.Tables["Parent" + dr["parent_table_name"].ToString()];
                        rItem.ValueMember = dr["parent_column_name"].ToString();

                        foreach (DataRow drParent in _dtParentTable.Rows)
                        {
                            if (drParent["TableName"].ToString().ToLower() == rItem.Name.ToLower())
                            {
                                DevExpress.XtraEditors.Controls.LookUpColumnInfo rItemColum = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("" + drParent["Name"]);
                                rItemColum.Caption = "" + drParent["Text"];

                                rItemColum.Visible = (drParent["Text"].ToString() != "");
                                rItemColum.Caption = "" + drParent["Text"];
                                rItem.Columns.Add(rItemColum);

                                // Chú ý dấu "*" , để load dữ liệu từ parent table
                                // Trong Caption của dropdownlist phải thêm dấu "*"
                                if (drParent["Text"].ToString().Trim() != "" && drParent["Text"].ToString().Trim().Substring(0, 1) == "*")
                                {
                                    rItem.DisplayMember = "" + drParent["Name"];
                                    rItemColum.Caption = "" + drParent["Text"].ToString().Trim().Substring(1);
                                }

                            }
                        }

                        this.dgDanhMuc.RefreshDataSource();
                        this.dgDanhMuc.Refresh();
                    }
                }

                foreach (DataRow dr in _dtShowCols.Rows)
                {
                    if (gridView.Columns.Contains(gridView.Columns[dr["Name"].ToString()]))
                    {
                        this.ColDisplayFormat(dr["Name"].ToString());
                    }
                }
            }
            fFormatCol = true;
        }


        private void ColDisplayFormat(string columnName)
        {
            DataColumn dataColumn = clsDanhMuc.dsDanhMuc.Tables["SchemaTable"].Columns[columnName];
            if (gridView.Columns[columnName].ColumnEdit != null)
                return;

            if (dataColumn.ColumnName.ToLower().Contains("actived"))
            {
                foreach (var showCol in ShowCols)
                {
                    var a = showCol;
                }

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit checkedit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

                checkedit.ValueChecked = 1;
                checkedit.ValueGrayed = 0;
                checkedit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                checkedit.NullText = "0";
                gridView.Columns[columnName].ColumnEdit = checkedit;
            }
            if (dataColumn.ColumnName == "Opt_ID")
            {
                gridView.Columns[columnName].OptionsColumn.ReadOnly = true;
            }
            switch (dataColumn.DataType.ToString())
            {
                case "System.DateTime":
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridView.Columns[columnName].DisplayFormat.FormatString = "dd/MM/yyyy";
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    break;
                case "System.Boolean":
                    if (dataColumn.MaxLength == 1)
                    {
                        gridView.Columns[columnName].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                        (gridView.Columns[columnName].ColumnEdit as RepositoryItemCheckEdit).ValueChecked = "1";
                        (gridView.Columns[columnName].ColumnEdit as RepositoryItemCheckEdit).ValueUnchecked = "0";
                    }
                    else
                    {
                        gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    }
                    break;
                case "System.Decimal":
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0 VNĐ}";
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    break;
                case "System.Double":
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0.00}";
                    //gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0.00}";
                    //gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    // add by 0036 22/11/11
                    gridView.Columns[columnName].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    gridView.Columns[columnName].ColumnEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView.Columns[columnName].ColumnEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    break;
                case "System.Int16":
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0}";
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    break;
                case "System.Int32":
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0}";
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    break;
                case "System.Int64":
                    gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView.Columns[columnName].DisplayFormat.FormatString = "{0:#,##0}";
                    gridView.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    break;
                default:
                    break;
            }
        }

        [Browsable(true)]
        [Editor(typeof(ListUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(typeof(DataTable), null)]
        [Category("Danh Mục - Connection")]
        [Description("Các col hiển thị trong danh mục")]
        public DataRowCollection ShowCols
        {
            get
            {
                return _dtShowCols.Rows;
            }
            set
            {
                _dtShowCols.Rows.Clear();
                foreach (DataRow var in value)
                {
                    DataRow dr = _dtShowCols.NewRow();
                    dr["TableName"] = TableName;
                    dr["Name"] = var[1];
                    dr["Text"] = var[2];
                    _dtShowCols.Rows.Add(dr);
                }

                _dtShowCols.AcceptChanges();

                this.CreateCol();
            }
        }

        [Browsable(false)]
        [Category("Danh Mục - Connection")]
        [Description("Các col hiển thị trong danh mục")]
        public string strShowCols
        {
            get
            {
                string value = "";
                foreach (DataRow var in _dtShowCols.Rows)
                {
                    value += "" + var["Name"] + "=" + var["Text"] + "|";
                }
                return value;
            }
            set
            {
                foreach (string var in value.Split('|'))
                {
                    if (var.Split('=').Length == 2 && var.Split('=')[1] != "")
                    {
                        if (_dtShowCols.Select("Name = '" + var.Split('=')[0] + "'").Length == 0)
                        {
                            DataRow dr = _dtShowCols.NewRow();
                            dr["TableName"] = TableName;
                            dr["Name"] = var.Split('=')[0];
                            dr["Text"] = var.Split('=')[1];
                            _dtShowCols.Rows.Add(dr);
                        }
                        else
                        {
                            _dtShowCols.Select("Name = '" + var.Split('=')[0] + "'")[0]["TableName"] = TableName;
                            _dtShowCols.Select("Name = '" + var.Split('=')[0] + "'")[0]["Text"] = var.Split('=')[1];
                        }
                    }
                }
                _dtShowCols.AcceptChanges();
            }
        }

        [Browsable(true)]
        [Category("Danh Mục - Connection")]
        [Description("Default Value trong danh mục")]
        public string DefaultValue
        {
            get
            {
                return clsDanhMuc.DefaultValue;
            }
            set
            {
                clsDanhMuc.DefaultValue = value;
                clsDanhMuc.CreateSQL();
                if (!DesignMode)
                    clsDanhMuc.Fill();

                this.CreateGrid();
            }
        }

        [Browsable(true)]
        [Editor(typeof(ListUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(typeof(DataTable), null)]
        [Category("Danh Mục - Connection")]
        [Description("Các col hiển thị trong danh mục cha \"Chú ý cần để dấu * trước col cần làm DisplayMember\"")]
        public DataRowCollection ShowParentCols
        {
            get
            {
                return _dtParentTable.Rows;
            }
            set
            {
                _dtParentTable.Rows.Clear();
                foreach (DataRow var in value)
                {
                    DataRow dr = _dtParentTable.NewRow();
                    dr["TableName"] = var[0];
                    dr["Name"] = var[1];
                    dr["Text"] = var[2];
                    _dtParentTable.Rows.Add(dr);
                }

                _dtParentTable.AcceptChanges();

                this.CreateCol();
            }
        }


        [Browsable(false)]
        [Category("Danh Mục - Connection")]
        [Description("Các col hiển thị trong danh mục cha")]
        public string strShowParentCols
        {
            get
            {
                string value = "";
                foreach (DataRow var in _dtParentTable.Rows)
                {
                    value += "" + var["TableName"] + "." + var["Name"] + "=" + var["Text"] + "|";
                }
                return value;
            }
            set
            {
                foreach (string var in value.Split('|'))
                {
                    string[] varTable = var.Split('=');

                    if (varTable.Length == 2 && varTable[1] != "")
                    {
                        if (_dtParentTable.Select(
                            "TableName = '" + varTable[0].Split('.')[0] + "' and " + "Name = '" + varTable[0].Split('.')[1] + "'").Length == 0)
                        {
                            DataRow dr = _dtParentTable.NewRow();
                            dr["TableName"] = varTable[0].Split('.')[0];
                            dr["Name"] = varTable[0].Split('.')[1];
                            dr["Text"] = varTable[1];
                            _dtParentTable.Rows.Add(dr);
                        }
                        else
                        {
                            _dtParentTable.Select(
                            "TableName = '" + varTable[0].Split('.')[0] + "' and " + "Name = '" + varTable[0].Split('.')[1] + "'")[0]["Text"] = var.Split('=')[1];
                        }
                    }
                }
                _dtShowCols.AcceptChanges();
            }
        }
        #endregion

        #region Function

        public void Add()
        {
            btnArray.btnState = ButtonsArray.btnStateEnum.Add;
            gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            gridView.OptionsBehavior.Editable = true;
            this.dgDanhMuc.Focus();
            //this.gridView.new.SelectCell.Row = -1;
            //this.dgDanhMuc.Col = 0;
        }

        public void Edit()
        {
            btnArray.btnState = ButtonsArray.btnStateEnum.Update;
            gridView.OptionsBehavior.Editable = true;
            this.dgDanhMuc.Focus();
        }

        public void Revert()
        {
            btnArray.btnState = ButtonsArray.btnStateEnum.Revert;

            gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

            clsDanhMuc.reload();

            if (gridView.RowCount > 0)
            {
                btnArray.btnSua.Enabled = true;
                btnArray.btnXoa.Enabled = true;
            }
            else
            {
                btnArray.btnSua.Enabled = false;
                btnArray.btnXoa.Enabled = false;
            }
        }

        private bool isSave = false;
        public bool IsSave
        {
            get { return isSave; }
            set { isSave = value; }
        }
        public void Save()
        {
            try
            {
                try
                {
                    clsDanhMuc.Update();
                    isSave = true;
                }
                catch (SqlException myex)
                {
                    if (myex.ErrorCode == -2147467259)
                    {
                        if (myex.Number == 1364)
                            ClsBaoLoi.Loi("Dòng dữ liệu không được rỗng.");
                        else if (myex.Number == 1062)
                            ClsBaoLoi.Loi("Dòng dữ liệu bị trùng.");
                    }
                    else
                    {
                        ClsBaoLoi.Loi(_msError, myex);
                    }
                    return;
                }
                catch (Exception ex)
                {
                    ClsBaoLoi.ThongTin("Dòng dữ liệu đang được sử dụng.\nChọn \"Bỏ qua\" hoặc mở lại form để cập nhật.");
                    return;
                }

                btnArray.btnState = ButtonsArray.btnStateEnum.Save;

                gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                gridView.OptionsBehavior.Editable = false;
                gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

                if (gridView.RowCount > 0)
                {
                    btnArray.btnSua.Enabled = true;
                    btnArray.btnXoa.Enabled = true;
                }
                else
                {
                    btnArray.btnSua.Enabled = false;
                    btnArray.btnXoa.Enabled = false;
                }

                clsDanhMuc.reload();
            }
            catch (SqlException Ex)
            {
                if (_throwEx)
                    throw Ex;
                else
                {
                    if (Ex.ErrorCode == -2147467259)
                    {
                        if (Ex.Number == 1062)
                            ClsBaoLoi.ThongTin("Số liệu nhập vào trùng! Xin kiểm tra lại.");
                        if (Ex.Number == 1364)
                            ClsBaoLoi.ThongTin("Số liệu nhập vào thiếu! Xin kiểm tra lại.");
                        if (Ex.Number == 1406)
                            ClsBaoLoi.ThongTin("Số liệu nhập vào quá lớn! Xin kiểm tra lại.");
                    }
                    else
                    {
                        ClsBaoLoi.Loi(_msError, Ex);
                    }
                }
            }
        }

        public void Delete()
        {
            try
            {
                if (ClsBaoLoi.XacNhan(_msDelete))
                {
                    gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
                    gridView.DeleteSelectedRows();

                    if (!clsDanhMuc.Update())
                    {
                        clsDanhMuc.reload();
                    }

                    gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            catch (SqlException Ex)
            {
                clsDanhMuc.reload();
                if (_throwEx)
                    throw Ex;
                else
                {
                    if (Ex.ErrorCode == -2147467259)
                    {
                        ClsBaoLoi.ThongTin("Thông tin đã được sử dụng. Không thể xóa.");
                    }
                    else
                    {
                        ClsBaoLoi.Loi(_msError, Ex);
                    }
                }
            }
            catch (System.Data.DBConcurrencyException myex)
            {
                ClsBaoLoi.ThongTin("Thông tin đã thay đổi. Xin kiểm tra lại.");
            }
        }

        public void Delete(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            try
            {
                if (MessageBox.Show(text, caption, buttons, icon) == DialogResult.Yes)
                {
                    gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
                    gridView.DeleteSelectedRows();

                    if (!clsDanhMuc.Update())
                    {
                        clsDanhMuc.reload();
                    }

                    gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            catch (Exception Ex)
            {
                clsDanhMuc.reload();
                if (_throwEx)
                    throw Ex;
                else
                {
                    ClsBaoLoi.Loi(_msError, Ex);
                }
            }
        }
        #endregion

        #region Event
        private void btnArray_btnEventAdd_click(object sender, System.EventArgs e)
        {
            this.Add();
            //MessageBox.Show("A");
        }

        private void btnArray_btnEventUpdate_click(object sender, System.EventArgs e)
        {
            this.Edit();
        }
        private void btnArray_btnEventDelete_click(object sender, EventArgs e)
        {
            this.Delete();
        }
        private void btnArray_btnEventSave_click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        private void btnArray_btnEventRevert_click(object sender, System.EventArgs e)
        {
            this.Revert();
        }

        private void btnArray_btnEventClose_click(object sender, System.EventArgs e)
        {
            if (this.Parent.GetType().BaseType == typeof(XtraForm))
            {
                (Parent as XtraForm).Close();
            }
            else if (this.Parent.GetType().BaseType == typeof(Form))
            {
                (Parent as Form).Close();
            }
            else
            {
                (Parent.Parent as XtraForm).Close();
            }
        }

        private void gridView_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView.RowCount > 0 && gridView.OptionsBehavior.AllowAddRows == DevExpress.Utils.DefaultBoolean.False)
            {
                btnArray.btnSua.Enabled = true;
                btnArray.btnXoa.Enabled = true;
            }
            else
            {
                btnArray.btnSua.Enabled = false;
                btnArray.btnXoa.Enabled = false;
            }
        }

        // add by 0036 7/11/11: Kiểm tra dữ liệu trong các hàng khi edit or add
        private void gridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            // Dùng try catch để kiểm tra            
            ColumnView view = sender as ColumnView;
            DataTable source;
            if (dgDanhMuc.DataSource != null)
            {
                source = dgDanhMuc.DataSource as DataTable;
                //e.Valid = Check_MA_ALL(gridView, source.TableName.ToString() , 0);

                if (mySetValidate != null)
                {
                    mySetValidate(gridView, e);
                }

                // Kiểm tra Danh mục Chuyên ngành
                if (source.TableName == "position_x_inventory")
                {

                }
                else if (source.TableName == "position_y_inventory")
                {

                }
                else if (source.TableName == "province")
                {
                    e.Valid = Check_NULL(gridView, "Proc_Name", "Không được rỗng");
                    if (!e.Valid) return;
                }
                else if (source.TableName == "district")
                {
                    e.Valid = Check_NULL(gridView, "Dist_District_Name", "Không được rỗng");
                    if (!e.Valid) return;
                    e.Valid = Check_NULL(gridView, "Dist_PROVINCE_ID", "Vui lòng chọn");
                    if (!e.Valid) return;
                }

                else if (source.TableName == "product_type")
                {
                    e.Valid = Check_NULL(gridView, "Prodt_Name", "Không được rỗng");
                    if (!e.Valid) return;
                }
                else if (source.TableName == "provider")
                {
                    e.Valid = Check_NULL(gridView, "Prov_ID", "Không được rỗng");
                    if (!e.Valid) return;
                    e.Valid = Check_NULL(gridView, "Prov_Name", "Không được rỗng");
                    if (!e.Valid) return;
                }
                else if (source.TableName == "discount")
                {

                }
            }
        }

        // add by 0036 7/11/11
        private void gridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        #endregion

        private void gridView_ColumnChanged(object sender, EventArgs e)
        {
            if (!DesignMode && fFormatCol)
            {
                this.CreateCol();
            }
        }

        public void SetFillter(string strFillter)
        {
            if (!string.IsNullOrEmpty(strFillter))
            {
                clsDanhMuc.dsDanhMuc.Tables[TableName].DefaultView.RowFilter = strFillter;
                this.dgDanhMuc.DataSource = clsDanhMuc.dsDanhMuc.Tables[TableName].DefaultView;
                this.dgDanhMuc.RefreshDataSource();
                this.dgDanhMuc.Refresh();
            }
            else
            {
                clsDanhMuc.dsDanhMuc.Tables[TableName].DefaultView.RowFilter = null;
                this.dgDanhMuc.DataSource = clsDanhMuc.dsDanhMuc.Tables[TableName];
                this.dgDanhMuc.RefreshDataSource();
                this.dgDanhMuc.Refresh();
            }
        }

        public void SetFillterParent(string ParentTable, string ParentCol, string strFillter)
        {
            if (gridView.Columns.Contains(gridView.Columns[ParentCol]))
            {
                clsDanhMuc.dsDanhMuc.Tables[ParentTable].DefaultView.RowFilter = ParentCol + "=" + strFillter;
                (gridView.Columns[ParentCol].ColumnEdit as RepositoryItemLookUpEdit).DataSource =
                    clsDanhMuc.dsDanhMuc.Tables[ParentTable].DefaultView;
                this.dgDanhMuc.RefreshDataSource();
                this.dgDanhMuc.Refresh();
            }
        }

        private void gridView_ShownEditor(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView.ActiveEditor.GetType() == typeof(TextEdit))
            {
                TextEdit textEdit = gridView.ActiveEditor as TextEdit;
                textEdit.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(textEdit_ParseEditValue);
            }
        }

        void textEdit_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null && e.Value.ToString().Trim() == "")
            {
                e.Value = DBNull.Value;
                e.Handled = true;
            }
        }

        private void UCDanhMuc_Load(object sender, EventArgs e)
        {
            //this.ParentForm.Load += new EventHandler(ParentForm_Load);
        }

        void ParentForm_Load(object sender, EventArgs e)
        {
            btnArray.btnThem.Enabled = btnArray.btnSua.Enabled = btnArray.btnXoa.Enabled = permission;
        }

        public void reLoad()
        {
            clsDanhMuc.reload();

            this.CreateGrid();
        }

        public void reLoadParent()
        {
            clsDanhMuc.reLoadParent();
        }

        // Add by 0036 22/11/11 
        /// <summary>
        /// Kiểm tra MA chức năng: không được null, không được trùng nhau
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="tableName">biến hiện tại chưa dùng</param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool Check_MA(GridView gridView, string tableName, string columnName)
        {
            // Dùng try catch để kiểm tra            
            string value;
            int iRow;
            GridColumn column;
            // Mã KHÔNG THỂ NULL
            iRow = gridView.FocusedRowHandle;
            column = gridView.Columns[columnName];
            value = gridView.GetRowCellValue(iRow, column)+string.Empty;
            if (string.IsNullOrEmpty(value))
            {
                gridView.SetColumnError(column, "Bạn chưa nhập Mã chức năng! ESC để bỏ qua.");
                return false;
            }
            else
            {
                // Kiểm tra Mã không được trùng nhau
                for (int j = 0; j < gridView.RowCount; j++)
                {
                    if (gridView.GetDataRow(j)[columnName].ToString().Trim() == value
                         && j != gridView.FocusedRowHandle)
                    {
                        gridView.SetColumnError(column, string.Format("Mã chức năng: {0} đã tồn tại. ESC để bỏ qua.", value));
                        return false;
                    }
                }
                gridView.SetColumnError(column, string.Empty);
            }
            return true;
        }

        // Add by 0036 22/11/11 
        public bool Check_MA_ALL(GridView gridView, string tableName, int columnNum)
        {
            DataTable source;
            source = dgDanhMuc.DataSource as DataTable;
            try
            {
                if (source.TableName.Contains(tableName) && source.TableName == tableName)
                {
                    // Dùng try catch để kiểm tra            
                    string value;
                    int iRow;
                    GridColumn column;
                    // Mã KHÔNG THỂ NULL
                    iRow = gridView.FocusedRowHandle;
                    column = gridView.Columns[columnNum];
                    value = gridView.GetRowCellValue(iRow, column) + string.Empty;
                    if (string.IsNullOrEmpty(value))
                    {
                        gridView.SetColumnError(column, "Bạn chưa nhập Mã chức năng! ESC để bỏ qua.");
                        return false;
                    }
                    else
                    {
                        gridView.SetColumnError(column, string.Empty);
                    }

                    // Kiểm tra Mã không được trùng nhau
                    for (int j = 0; j < gridView.RowCount; j++)
                    {
                        if (gridView.GetDataRow(j)[columnNum].ToString().Trim() == value
                             && j != gridView.FocusedRowHandle)
                        {
                            gridView.SetColumnError(column, string.Format("Mã chức năng: {0} đã tồn tại. ESC để bỏ qua.", value));
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return true;
                // Nếu không tồn tại table thì tiếp tục, không cần xử lý catch;
            }

        }

        // Add by 0036 23/11/11
        public bool Check_NUM(GridView gridView, string columnName, string error)
        {
            string value;
            int iRow;
            GridColumn column;
            iRow = gridView.FocusedRowHandle;
            column = gridView.Columns[columnName];
            value = gridView.GetRowCellValue(iRow, column).ToString().Trim();
            try
            {
                if (ClsChangeType.change_double(value) < 0)
                {
                    gridView.SetColumnError(column, string.Format("{0} không thể là số âm. ESC để bỏ qua.", error));
                    return false;
                }
                else
                {
                    gridView.SetColumnError(column, string.Empty);
                }
            }
            catch (Exception ex)
            {
                gridView.SetColumnError(column, string.Format("{0} phải là số lớn hơn 0. ESC để bỏ qua.", error));
                return false;
            }
            return true;
        }

        // Add by 0036 23/11/11
        public bool Check_NULL(GridView gridView, string columnName, string error)
        {
            string value;
            int iRow;
            GridColumn column;
            iRow = gridView.FocusedRowHandle;
            column = gridView.Columns[columnName];
            value = gridView.GetRowCellValue(iRow, column).ToString().Trim();
            if (string.IsNullOrEmpty(value))
            {
                gridView.SetColumnError(column, string.Format("Bạn chưa nhập {0}! ESC để bỏ qua.", error));
                return false;
            }
            else
            {
                gridView.SetColumnError(column, string.Empty);
            }
            return true;
        }

        public bool Check_Length(GridView gridView, string columnName, int lenghtMin, int lengthMax)
        {
            string value;
            int iRow;
            GridColumn column;
            iRow = gridView.FocusedRowHandle;
            column = gridView.Columns[columnName];
            value = gridView.GetRowCellValue(iRow, column).ToString().Trim();
            if (value.Length < lenghtMin || value.Length > lengthMax)
            {
                gridView.SetColumnError(column, string.Format("Chỉ được từ {0} đến {1} ký tự! ESC để bỏ qua.", lenghtMin, lengthMax));
                return false;
            }
            else
            {
                gridView.SetColumnError(column, string.Empty);
            }
            return true;
        }

    }
}
