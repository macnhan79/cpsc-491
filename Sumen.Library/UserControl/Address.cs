#region Main
//  File name    	:	Address.cs
//  Purpose		    :	Control cho phép nhập địa chỉ (tỉnh/huyện/xã) trên cùng một ô nhập
//  Creater date	:	11-09-2009
//  Author		    :	Phạm Văn Duy
//  Version		    :	V1.0
//  Copyright		:	Cusc
#endregion

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;

namespace Library.UserControl
{
    /// <summary>
    /// Control cho phép nhập địa chỉ (tỉnh/huyện/xã) trên cùng một ô nhập
    /// </summary>
    [Designer(typeof(AddressControlDesigner))]
    public partial class Address : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Sự kiện phát sinh khi nhập đầy đủ thông tin tỉnh, huyện, xã
        /// </summary>
        public event ControlEventHandler IsValid;
        /// <summary>
        /// Lớp lấy danh sách tỉnh, huyện, xã trong dataset
        /// </summary>
        ClsAddress dataAccess;
        /// <summary>
        /// Trạng thái địa chỉ hiện tại
        /// </summary>
        private AddressType currentType = AddressType.TINH;
        /// <summary>
        /// Trạng thái nhập liệu hay không
        /// </summary>
        private bool isEdit = true;
        /// <summary>
        /// Có bắt buộc nhập hay không
        /// </summary>
        private bool requiredField = false;
        /// <summary>
        /// Chuỗi thông báo khi nhập địa chỉ không hợp lệ
        /// </summary>
        private string erorrMessage = "Địa chỉ không hợp lệ.\nĐề nghị kiểm tra lại.";
        /// <summary>
        /// Id tỉnh
        /// </summary>
        private int idTinh = 0;
        /// <summary>
        /// Mã tỉnh
        /// </summary>
        private string maTinh = string.Empty;
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        private string tenTinh = string.Empty;
        /// <summary>
        /// Id huyện
        /// </summary>
        private int idHuyen = 0;
        /// <summary>
        /// Mã huyện
        /// </summary>
        private string maHuyen = string.Empty;
        /// <summary>
        /// Tên huyện
        /// </summary>
        private string tenHuyen = string.Empty;
        /// <summary>
        /// Id xã
        /// </summary>
        private int idXa = 0;
        /// <summary>
        /// Mã xã
        /// </summary>
        private string maXa = string.Empty;
        /// <summary>
        /// Tên xã
        /// </summary>
        private string tenXa = string.Empty;

        /// <summary>
        /// Lấy point hiện tại của con trỏ trên ô nhập
        /// </summary>
        /// <param name="point">Vị trí con trỏ trên ô nhập</param>
        /// <returns>Số int cho biết thực hiện được hay không</returns>
        [DllImport("user32")]
        private static extern int GetCaretPos(ref Point point);

        /// <summary>
        /// Control cho phép nhập địa chỉ (tỉnh/huyện/xã) trên cùng một ô nhập
        /// </summary>
        public Address()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Có bắt buộc nhập hay không
        /// </summary>
        public bool RequiredField
        {
            get { return requiredField; }
            set { requiredField = value; }
        }
        /// <summary>
        /// Chuỗi thông báo khi nhập địa chỉ không hợp lệ
        /// </summary>
        public string ErorrMessage
        {
            get { return erorrMessage; }
            set { erorrMessage = value; }
        }
        /// <summary>
        /// Lấy point hiện tại của con trỏ trên ô nhập
        /// </summary>
        /// <value>
        /// Point struct
        /// </value>
        public Point GetCaretPosition
        {
            get
            {
                Point pt = Point.Empty;
                // Lấy point hiện tại của con trỏ trên ô nhập
                GetCaretPos(ref pt);
                return pt;
            }
        }

        /// <summary>
        /// Id tỉnh
        /// </summary>
        public int IdTinh
        {
            get { return idTinh; }
            set { idTinh = value; }
        }
        /// <summary>
        /// Mã tỉnh
        /// </summary>
        public string MaTinh
        {
            get { return maTinh; }
            set { maTinh = value; }
        }
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        public string TenTinh
        {
            get { return tenTinh; }
            set { tenTinh = value; }
        }
        /// <summary>
        /// Id huyện
        /// </summary>
        public int IdHuyen
        {
            get { return idHuyen; }
            set { idHuyen = value; }
        }
        /// <summary>
        /// Mã huyện
        /// </summary>
        public string MaHuyen
        {
            get { return maHuyen; }
            set { maHuyen = value; }
        }
        /// <summary>
        /// Tên huyện
        /// </summary>
        public string TenHuyen
        {
            get { return tenTinh; }
            set { tenTinh = value; }
        }
        /// <summary>
        /// Id xã
        /// </summary>
        public int IdXa
        {
            get { return idXa; }
            set { idXa = value; }
        }
        /// <summary>
        /// Mã xã
        /// </summary>
        public string MaXa
        {
            get { return maXa; }
            set { maXa = value; }
        }
        /// <summary>
        /// Tên xã
        /// </summary>
        public string TenXa
        {
            get { return tenXa; }
            set { tenXa = value; }
        }


        private void AddressInfo_Load(object sender, EventArgs e)
        {
            // Lớp lấy danh sách tỉnh, huyện, xã trong dataset
            dataAccess = new ClsAddress();
            // Lấy danh sách tỉnh
            dataAccess.FillTinh();
            // Lấy danh sách huyện
            dataAccess.FillHuyen();
            // Lấy danh sách xã
            dataAccess.FillXa();
            // Khởi tạo danh sách tỉnh, huyện, xã
            InitTree();
            // Thêm control lưới tìm kiếm vào form cha
            AddGridControl();
            // Datasource lưới tìm kiếm nhanh hiện tại là Tỉnh
            ChangeDataSource(currentType);
            // Lấy thông tin địa chỉ hiện tại
            LoadDefault();
        }

        private void popupContainerEditAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgTinhThanh.Visible = true;
                Point point = GetCaretPosition;
                point.X = this.Location.X + popupContainerEditAddress.Location.X + point.X;
                point.Y = this.Location.Y + popupContainerEditAddress.Location.Y + point.Y + popupContainerEditAddress.Size.Height + 2;
                dgTinhThanh.Location = point;
                dgTinhThanh.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CheckAddress();
            }
            else
            {
                dgTinhThanh.Visible = true;
                Point point = GetCaretPosition;
                point.X = this.Location.X + popupContainerEditAddress.Location.X + point.X;
                point.Y = this.Location.Y + popupContainerEditAddress.Location.Y + point.Y + popupContainerEditAddress.Size.Height + 2;
                dgTinhThanh.Location = point;
            }
        }

        private void popupContainerEditAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (isEdit)
            {
                ChangeAddressType();
            }
        }

        private void popupContainerEditAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (isEdit)
            {
                ChangeAddressType();
            }
        }

        private void popupContainerEditAddress_TextChanged(object sender, EventArgs e)
        {
            if (isEdit)
            {
                try
                {
                    string value = popupContainerEditAddress.Text.Trim();
                    string[] array = value.Split(',');
                    if (currentType == AddressType.XA)
                    {
                        value = array[2].Trim();
                    }
                    else if (currentType == AddressType.HUYEN)
                    {
                        value = array[1].Trim();
                    }
                    else
                    {
                        if (array.Length > 0)
                        {
                            value = array[0].Trim();
                        }
                    }
                    gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, string.Format("[DC_TEN] like '%{0}%'", value), string.Empty);
                }
                catch
                {
                    gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();
                }
            }
        }

        private void popupContainerEditAddress_Leave(object sender, EventArgs e)
        {
            dgTinhThanh.Visible = dgTinhThanh.IsFocused;

            if (requiredField)
            {
                if (idXa == 0 && !dgTinhThanh.Visible)
                {
                    MessageBox.Show(erorrMessage);
                    popupContainerEditAddress.Focus();
                }
            }
        }

        private void gvTinhThanh_DoubleClick(object sender, EventArgs e)
        {
            GetAddressInfo();
        }

        private void dgTinhThanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                popupContainerEditAddress.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                GetAddressInfo();
            }
        }

        private void dgTinhThanh_Leave(object sender, EventArgs e)
        {
            dgTinhThanh.Visible = popupContainerEditAddress.Focused;
        }

        private void treeListAddress_DoubleClick(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void treeListAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                popupContainerControlAddress.FindForm().Validate();
                ClosePopup();
            }
        }

        private void popupContainerEditAddress_QueryPopUp(object sender, CancelEventArgs e)
        {
            treeListAddress.ExpandAll();
            treeListAddress.Selection.Clear();
            dgTinhThanh.Visible = false;
            if (!string.IsNullOrEmpty(tenXa))
            {
                treeListAddress.Selection.Add(treeListAddress.FindNodeByFieldValue("DC_TEN", tenXa));
            }
            else if (!string.IsNullOrEmpty(tenHuyen))
            {
                treeListAddress.Selection.Add(treeListAddress.FindNodeByFieldValue("DC_TEN", tenHuyen));
            }
            else if (!string.IsNullOrEmpty(tenTinh))
            {
                treeListAddress.Selection.Add(treeListAddress.FindNodeByFieldValue("DC_TEN", tenTinh));
            }
        }

        private void popupContainerEditAddress_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(treeListAddress.FocusedNode.GetDisplayText("DC_TINH")) && string.IsNullOrEmpty(treeListAddress.FocusedNode.GetDisplayText("DC_QUAN")))
                {
                    idTinh = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("DC_ID_TINH"));
                    maTinh = treeListAddress.FocusedNode.GetDisplayText("DC_MA_TINH");
                    tenTinh = treeListAddress.FocusedNode.GetDisplayText("DC_TEN_TINH");

                    idHuyen = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("DC_ID_HUYEN"));
                    maHuyen = treeListAddress.FocusedNode.GetDisplayText("DC_MA_HUYEN");
                    tenHuyen = treeListAddress.FocusedNode.GetDisplayText("DC_TEN_HUYEN");

                    idXa = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("ID"));
                    maXa = treeListAddress.FocusedNode.GetDisplayText("DC_MA");
                    tenXa = treeListAddress.FocusedNode.GetDisplayText("DC_TEN");

                    e.Value = string.Format("{0}, {1}, {2}", tenTinh, tenHuyen, tenXa);
                }
                else if (!string.IsNullOrEmpty(treeListAddress.FocusedNode.GetDisplayText("DC_QUAN")))
                {
                    idTinh = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("DC_ID_TINH"));
                    maTinh = treeListAddress.FocusedNode.GetDisplayText("DC_MA_TINH");
                    tenTinh = treeListAddress.FocusedNode.GetDisplayText("DC_TEN_TINH");

                    idHuyen = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("ID"));
                    maHuyen = treeListAddress.FocusedNode.GetDisplayText("DC_MA");
                    tenHuyen = treeListAddress.FocusedNode.GetDisplayText("DC_TEN");

                    idXa = 0;
                    maXa = string.Empty;
                    tenXa = string.Empty;

                    e.Value = string.Format("{0}, {1}, ", tenTinh, tenHuyen);
                }
                else if (!string.IsNullOrEmpty(treeListAddress.FocusedNode.GetDisplayText("DC_TINH")))
                {
                    idTinh = Convert.ToInt32(treeListAddress.FocusedNode.GetDisplayText("ID"));
                    maTinh = treeListAddress.FocusedNode.GetDisplayText("DC_MA");
                    tenTinh = treeListAddress.FocusedNode.GetDisplayText("DC_TEN");

                    idHuyen = 0;
                    maHuyen = string.Empty;
                    tenHuyen = string.Empty;

                    idXa = 0;
                    maXa = string.Empty;
                    tenXa = string.Empty;

                    e.Value = string.Format("{0}, ", tenTinh);
                }
            }
            catch { }
        }

        /// <summary>
        /// Loại địa chỉ
        /// </summary>
        public enum AddressType
        {
            TINH,
            HUYEN,
            XA
        }

        /// <summary>
        /// Lấy thông tin địa chỉ hiện tại
        /// </summary>
        private void LoadDefault()
        {
            isEdit = false;

            if (idXa > 0)
            {
                DataRow row = dataAccess.GetAddressInfo(idXa);
                if (row != null)
                {
                    idTinh = Convert.ToInt32(row["DC_ID_TINH"]);
                    maTinh = row["DC_MA_TINH"].ToString();
                    tenTinh = row["DC_TEN_TINH"].ToString();
                    idHuyen = Convert.ToInt32(row["DC_ID_HUYEN"]);
                    maHuyen = row["DC_MA_HUYEN"].ToString();
                    tenHuyen = row["DC_TEN_HUYEN"].ToString();
                    maXa = row["DC_MA"].ToString();
                    tenXa = row["DC_TEN"].ToString();

                    popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", tenTinh, tenHuyen, tenXa);
                }
            }
            dgTinhThanh.DataSource = dataAccess.DsAddress;
            dgTinhThanh.DataMember = dataAccess.BangTinh;
            treeListAddress.Size = new Size(treeListAddress.Size.Width, treeListAddress.Size.Height);

            isEdit = true;
        }

        /// <summary>
        /// Thêm control lưới tìm kiếm nhanh vào form cha
        /// </summary>
        private void AddGridControl()
        {
            if (this.Controls["dgTinhThanh"] != null)
            {
                this.Controls.Remove(this.Controls["dgTinhThanh"]);
            }
            Control form = this.TopLevelControl;
            if (form != null)
            {
                form.Controls.Add(dgTinhThanh);
            }
        }

        /// <summary>
        /// Khởi tạo danh sách tỉnh, huyện, xã
        /// </summary>
        private void InitTree()
        {

            #region Theo cách củ ne
            //// Xóa các dữ liệu trên tree
            //treeListAddress.Nodes.Clear();
            //// Lấy danh sách các tỉnh
            //dataAccess.FillTinh();
            //// Thêm các node tỉnh
            //foreach (DataRow row in dataAccess.DsAddress.Tables[dataAccess.BangTinh].Rows)
            //{
            //    treeListAddress.AppendNode(row, -1);
            //}
            //// Lấy danh sách các huyện
            //dataAccess.FillHuyen();
            //// Thêm các node huyện
            //foreach (DataRow row in dataAccess.DsAddress.Tables[dataAccess.BangHuyen].Rows)
            //{
            //    treeListAddress.AppendNode(row, treeListAddress.FindNodeByFieldValue("DC_TEN", row["TINH"]));
            //}
            //// Lấy danh sách các node xã
            //dataAccess.FillXa();
            //// Thêm các node xã
            //foreach (DataRow row in dataAccess.DsAddress.Tables[dataAccess.BangXa].Rows)
            //{
            //    treeListAddress.AppendNode(row, treeListAddress.FindNodeByFieldValue("DC_TEN", row["HUYEN"]));
            //}
            #endregion

            treeListAddress.DataSource = dataAccess.GetAddressTree();
            treeListAddress.ExpandAll();
        }

        /// <summary>
        /// Thay đổi lưới tìm kiếm nhanh theo tỉnh, huyện, xã
        /// </summary>
        private void ChangeAddressType()
        {
            string value = popupContainerEditAddress.Text;
            if (value.Length == 0)
            {
                currentType = AddressType.TINH;
                ChangeDataSource(currentType);
                return;
            }
            int count = value.Length - popupContainerEditAddress.SelectionStart == 0 ? value.Length : popupContainerEditAddress.SelectionStart;
            string compare = value.Substring(0, count);
            if (compare.IndexOf(",") != -1 && compare.LastIndexOf(",") != -1 && compare.IndexOf(",") != compare.LastIndexOf(","))
            {
                currentType = AddressType.XA;
                DataRow row = dataAccess.GetHuyenByTenHuyen(value.Split(',')[1].Trim());
                if (row != null)
                {
                    idHuyen = Convert.ToInt32(row["DC_ID"]);
                    maHuyen = row["DC_MA"].ToString();
                    tenHuyen = row["DC_TEN"].ToString();
                }
                else
                {
                    idHuyen = 0;
                    maHuyen = string.Empty;
                    tenHuyen = string.Empty;
                }
            }
            else if (compare.IndexOf(",") != -1)
            {
                currentType = AddressType.HUYEN;
                DataRow row = dataAccess.GetTinhByTenTinh(value.Split(',')[0].Trim());
                if (row != null)
                {
                    idTinh = Convert.ToInt32(row["DC_ID"]);
                    maTinh = row["DC_MA"].ToString();
                    tenTinh = row["DC_TEN"].ToString();
                }
                else
                {
                    idTinh = 0;
                    maTinh = string.Empty;
                    tenTinh = string.Empty;
                }
            }
            else
            {
                currentType = AddressType.TINH;
            }
            ChangeDataSource(currentType);
        }

        /// <summary>
        /// Thay đổi datasource lưới tìm kiếm nhanh theo tỉnh, huyện, xã
        /// </summary>
        /// <param name="type">Loại địa chỉ</param>
        private void ChangeDataSource(AddressType type)
        {
            if (type == AddressType.XA)
            {
                // Lấy danh sách các xã
                dgTinhThanh.DataSource = dataAccess.DsAddress;
                dgTinhThanh.DataMember = dataAccess.BangXa;
                DC_TEN_.Caption = "Xã/Phường";
                gvTinhThanh.Columns["CHA_DC_ID"].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, string.Format("[CHA_DC_ID] = {0}", idHuyen), string.Empty);
            }
            else if (type == AddressType.HUYEN)
            {
                // Lấy danh sách các huyện
                dgTinhThanh.DataSource = dataAccess.DsAddress;
                dgTinhThanh.DataMember = dataAccess.BangHuyen;
                DC_TEN_.Caption = "Huyện/Quận";
                gvTinhThanh.Columns["CHA_DC_ID"].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, string.Format("[CHA_DC_ID] = {0}", idTinh), string.Empty);
            }
            else
            {
                // Lấy danh sách các tỉnh
                dgTinhThanh.DataSource = dataAccess.DsAddress;
                dgTinhThanh.DataMember = dataAccess.BangTinh;
                DC_TEN_.Caption = "Tỉnh/Thành phố";
                gvTinhThanh.Columns["CHA_DC_ID"].FilterInfo = new ColumnFilterInfo();
            }
        }

        /// <summary>
        /// Lấy thông tin địa chỉ trên lưới tìm kiếm nhanh
        /// </summary>
        private void GetAddressInfo()
        {
            if (gvTinhThanh.SelectedRowsCount > 0 && !gvTinhThanh.IsFilterRow(gvTinhThanh.GetSelectedRows()[0]))
            {
                isEdit = false;

                string value = popupContainerEditAddress.Text.Trim();
                string[] array = value.Split(',');
                if (currentType == AddressType.XA)
                {
                    idXa = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                    maXa = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString();
                    tenXa = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString();

                    popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), array[1].Trim(), tenXa);

                    dgTinhThanh.Visible = false;

                    popupContainerEditAddress.Focus();
                    popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                    if (IsValid != null)
                    {
                        IsValid(popupContainerEditAddress, new ControlEventArgs(popupContainerEditAddress));
                    }
                }
                else if (currentType == AddressType.HUYEN)
                {
                    idHuyen = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                    maHuyen = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString().Trim();
                    tenHuyen = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString().Trim();

                    if (array.Length >= 3)
                    {
                        popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), tenHuyen, array[2].Trim());
                    }
                    else
                    {
                        popupContainerEditAddress.Text = string.Format("{0}, {1}, ", array[0].Trim(), tenHuyen);
                    }

                    popupContainerEditAddress.Focus();
                    popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                    currentType = AddressType.XA;
                    ChangeDataSource(AddressType.XA);
                    gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();
                }
                else
                {
                    idTinh = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                    maTinh = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString().Trim();
                    tenTinh = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString().Trim();

                    if (array.Length >= 3)
                    {
                        popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", tenTinh, array[1].Trim(), array[2].Trim());
                    }
                    else if (array.Length == 2)
                    {
                        popupContainerEditAddress.Text = string.Format("{0}, {1}, ", tenTinh, array[0].Trim());
                    }
                    else
                    {
                        popupContainerEditAddress.Text = string.Format("{0}, ", tenTinh);
                    }

                    popupContainerEditAddress.Focus();
                    popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                    currentType = AddressType.HUYEN;
                    ChangeDataSource(AddressType.HUYEN);
                    gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();
                }

                Point point = GetCaretPosition;
                point.X = this.Location.X + popupContainerEditAddress.Location.X + point.X;
                point.Y = this.Location.Y + popupContainerEditAddress.Location.Y + point.Y + popupContainerEditAddress.Size.Height + 2;
                dgTinhThanh.Location = point;

                isEdit = true;
            }
        }

        /// <summary>
        /// Kiểm tra địa chỉ trên ô nhập
        /// </summary>
        private void CheckAddress()
        {
            isEdit = false;

            try
            {
                string value = popupContainerEditAddress.Text.Trim();
                string[] array = value.Split(',');
                if (currentType == AddressType.XA)
                {
                    value = array[2].Trim();
                    DataRow row = dataAccess.GetXaByTenXa(value);
                    if (row != null)
                    {
                        idXa = Convert.ToInt32(row["DC_ID"]);
                        maXa = row["DC_MA"].ToString();
                        tenXa = row["DC_TEN"].ToString();

                        popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), array[1].Trim(), tenXa);

                        dgTinhThanh.Visible = false;

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        if (IsValid != null)
                        {
                            IsValid(popupContainerEditAddress, new ControlEventArgs(popupContainerEditAddress));
                        }
                    }
                    else if (gvTinhThanh.RowCount > 0)
                    {
                        idXa = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                        maXa = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString();
                        tenXa = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString();

                        popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), array[1].Trim(), tenXa);

                        dgTinhThanh.Visible = false;

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        if (IsValid != null)
                        {
                            IsValid(popupContainerEditAddress, new ControlEventArgs(popupContainerEditAddress));
                        }
                    }
                    else
                    {
                        idXa = 0;
                        maXa = string.Empty;
                        tenXa = string.Empty;
                    }
                }
                else if (currentType == AddressType.HUYEN)
                {
                    value = array[1].Trim();
                    DataRow row = dataAccess.GetHuyenByTenHuyen(value);
                    if (row != null)
                    {
                        idHuyen = Convert.ToInt32(row["DC_ID"]);
                        maHuyen = row["DC_MA"].ToString();
                        tenHuyen = row["DC_TEN"].ToString();

                        if (array.Length >= 3)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), tenHuyen, array[2].Trim());
                        }
                        else
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, ", array[0].Trim(), tenHuyen);
                        }

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        currentType = AddressType.XA;
                        ChangeDataSource(AddressType.XA);
                        gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();
                    }
                    else if (gvTinhThanh.RowCount > 0)
                    {
                        idHuyen = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                        maHuyen = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString().Trim();
                        tenHuyen = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString().Trim();

                        if (array.Length >= 3)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", array[0].Trim(), tenHuyen, array[2].Trim());
                        }
                        else
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, ", array[0].Trim(), tenHuyen);
                        }

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        currentType = AddressType.XA;
                        ChangeDataSource(AddressType.XA);
                        gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();

                        Point point = GetCaretPosition;
                        point.X = this.Location.X + popupContainerEditAddress.Location.X + point.X;
                        point.Y = this.Location.Y + popupContainerEditAddress.Location.Y + point.Y + popupContainerEditAddress.Size.Height + 2;
                        dgTinhThanh.Location = point;
                    }
                    else
                    {
                        idHuyen = 0;
                        maHuyen = string.Empty;
                        tenHuyen = string.Empty;

                        idXa = 0;
                        maXa = string.Empty;
                        tenXa = string.Empty;
                    }
                }
                else
                {
                    if (array.Length > 0)
                    {
                        value = array[0].Trim();
                    }
                    DataRow row = dataAccess.GetTinhByTenTinh(value.Split(',')[0].Trim());
                    if (row != null)
                    {
                        idTinh = Convert.ToInt32(row["DC_ID"]);
                        maTinh = row["DC_MA"].ToString();
                        tenTinh = row["DC_TEN"].ToString();

                        if (array.Length >= 3)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", tenTinh, array[1].Trim(), array[2].Trim());
                        }
                        else if (array.Length == 2)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, ", tenTinh, array[0].Trim());
                        }
                        else
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, ", tenTinh);
                        }

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        currentType = AddressType.HUYEN;
                        ChangeDataSource(AddressType.HUYEN);
                        gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();
                    }
                    else if (gvTinhThanh.RowCount > 0)
                    {
                        idTinh = Convert.ToInt32(gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_ID"));
                        maTinh = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_MA").ToString().Trim();
                        tenTinh = gvTinhThanh.GetRowCellValue(gvTinhThanh.GetSelectedRows()[0], "DC_TEN").ToString().Trim();

                        if (array.Length >= 3)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, {2}", tenTinh, array[1].Trim(), array[2].Trim());
                        }
                        else if (array.Length == 2)
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, {1}, ", tenTinh, array[0].Trim());
                        }
                        else
                        {
                            popupContainerEditAddress.Text = string.Format("{0}, ", tenTinh);
                        }

                        popupContainerEditAddress.Focus();
                        popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;

                        currentType = AddressType.HUYEN;
                        ChangeDataSource(AddressType.HUYEN);
                        gvTinhThanh.Columns["DC_TEN"].FilterInfo = new ColumnFilterInfo();

                        Point point = GetCaretPosition;
                        point.X = this.Location.X + popupContainerEditAddress.Location.X + point.X;
                        point.Y = this.Location.Y + popupContainerEditAddress.Location.Y + point.Y + popupContainerEditAddress.Size.Height + 2;
                        dgTinhThanh.Location = point;
                    }
                    else
                    {
                        idTinh = 0;
                        maTinh = string.Empty;
                        tenTinh = string.Empty;

                        idHuyen = 0;
                        maHuyen = string.Empty;
                        tenHuyen = string.Empty;

                        idXa = 0;
                        maXa = string.Empty;
                        tenXa = string.Empty;
                    }
                }

            }
            catch { }

            isEdit = true;
        }

        void ClosePopup()
        {
            if (popupContainerControlAddress.OwnerEdit != null)
            {
                popupContainerControlAddress.OwnerEdit.ClosePopup();
                popupContainerEditAddress.Focus();
                popupContainerEditAddress.SelectionStart = popupContainerEditAddress.Text.Length;
            }
        }
    }
}