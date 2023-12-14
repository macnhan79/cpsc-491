using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Nodes;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7.Library.UserControl.PopupTree
{
    [Designer(typeof(ItemControlDesigner))]
    public partial class PopupTree : DevExpress.XtraEditors.XtraUserControl
    {
        private ClsPopupTreeView dataAccess;
        private PopupGrid content;
        private Popup popup;
        Point point;


        /// <summary>
        /// Su kien phat sinh khi primary key thay doi
        /// </summary>
        public delegate void DelegatePrimaryKeyChanged(object sender, EventArgs e);
        public event EventHandler PrimaryKeyChanged;
        protected virtual void PrimaryKeyChange(EventArgs e)
        {
            if (PrimaryKeyChanged != null)
                PrimaryKeyChanged(this, e);
        }
        /// <summary>
        /// Sự kiện phát sinh khi nhập đầy đủ thông tin tỉnh, huyện, xã
        /// </summary>
        public event ControlEventHandler IsValid;
        /// <summary>
        /// Trạng thái nhập liệu hay không
        /// </summary>
        private bool isEdit = true;
        /// <summary>
        /// Đối tượng hiện tại
        /// </summary>
        private Item currentItem;
        /// <summary>
        /// DataTable lọc dữ liệu
        /// </summary>
        public DataTable dtFilter;
        /// <summary>
        /// DataView lọc dữ liệu
        /// </summary>
        private DataView dvFilter;
        /// <summary>
        /// Khóa chính
        /// </summary>
        private object primaryKey;
        /// <summary>
        /// Khóa ngoại của bảng cha
        /// </summary>
        private object parentKey;
        /// <summary>
        /// Tên đối tượng
        /// </summary>
        private object value;
        /// <summary>
        /// Mã đối tượng
        /// </summary>
        private object code;
        /// <summary>
        /// Có hợp lệ hay chưa
        /// </summary>
        private bool isValidated;

        //string Cat_ID_Name = Cat_ID_Name;
        //string Cat_Title_Name = Cat_Title_Name;
        //string Cat_Parent_id_Name = Cat_Parent_id_Name;
        //string Cat_CODE_Name= Cat_CODE_Name;


        string Cat_ID_Name = "ProductID";
        string Cat_Title_Name = "ProductName";
        string Cat_Parent_id_Name = "ExpandCategoryID";
        string Cat_CODE_Name = "BarCode";
        /// <summary>
        /// Danh sách các đối tượng
        /// </summary>
        private ItemCollection items = new ItemCollection();

        //private ClsPopupTreeView dataAccessMySQL;

        public PopupTree()
        {
            InitializeComponent();

            popup = new Popup(content = new PopupGrid());
            content.dgKhoa.DoubleClick += new EventHandler(dgKhoa_DoubleClick);
            content.dgKhoa.KeyDown += new KeyEventHandler(dgKhoa_KeyDown);
            content.dgKhoa.Leave += new EventHandler(dgKhoa_Leave);
            popup.AutoClose = false;
            popup.FocusOnOpen = false;
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                popup.ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
                popup.HidingAnimation = PopupAnimations.Center;
            }
            else
            {
                popup.ShowingAnimation = popup.HidingAnimation = PopupAnimations.None;
            }
        }

        /// <summary>
        /// Danh sách các đối tượng
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ItemCollection Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
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
        /// Khóa chính
        /// </summary>
        public object PrimaryKey
        {
            get { return primaryKey; }
            set
            {
                primaryKey = value;
                PrimaryKeyChange(EventArgs.Empty);
                try
                {
                    if (primaryKey == null || string.IsNullOrEmpty(primaryKey + ""))
                    {
                        return;
                    }

                    isEdit = false;
                    bool lastRow = true;
                    string result = string.Empty;
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        if (dataAccess == null)
                        {
                            dataAccess = new ClsPopupTreeView(items);
                        }
                        dtFilter = dataAccess.Ds.Tables[items[i].TableName].Copy();
                        dvFilter = dtFilter.DefaultView;
                        if (lastRow)
                        {
                            dvFilter.RowFilter = string.Format("Cat_ID = {0}", primaryKey);
                            if (dvFilter.Count == 0)
                            {
                                continue;
                            }
                            items[i].PrimaryKey = dvFilter[0][Cat_ID_Name];
                            items[i].Name = dvFilter[0][Cat_Title_Name];
                            //items[i].Code = dvFilter[0][Cat_CODE_Name];
                            items[i].ParentKey = dvFilter[0][Cat_Parent_id_Name];

                            result = string.Format("{0}", items[i].Name);
                            lastRow = false;
                        }
                        else
                        {
                            dvFilter.RowFilter = string.Format("ID = {0}", items[i + 1].ParentKey);
                            if (dvFilter.Count == 0)
                            {
                                continue;
                            }
                            items[i].PrimaryKey = dvFilter[0][Cat_ID_Name];
                            items[i].Name = dvFilter[0][Cat_Title_Name];
                            //items[i].Code = dvFilter[0][Cat_CODE_Name];
                            items[i].ParentKey = dvFilter[0][Cat_Parent_id_Name];

                            result = string.Format("{0}, ", items[i].Name) + result;
                        }
                        isValidated = true;
                    }

                    popupContainerEditKhoa.Text = result;
                    isEdit = true;
                }
                catch { }
            }
        }

        List<object> _childListPrimaryKey = new List<object>();
        public List<object> ChildListPrimaryKey
        {
            get
            {
                if (string.IsNullOrEmpty(primaryKey + string.Empty) || primaryKey == "0")
                {
                    return null;
                }
                else
                {
                    _childListPrimaryKey.Clear();
                    GetChildListPrimaryKey(treeListKhoa.FocusedNode.Id);
                    _childListPrimaryKey.Add(treeListKhoa.FocusedNode.GetValue(Cat_ID));
                    return _childListPrimaryKey;
                }

            }
        }
        public void GetChildListPrimaryKey(int pPrimaryKey)
        {
            int a = Convert.ToInt32(pPrimaryKey);
            //var nodeParent = treeListKhoa.FindNodeByFieldValue(Cat_ID_Name, pPrimaryKey);
            var nodeParent = treeListKhoa.FindNodeByID(pPrimaryKey);
            if (nodeParent != null && nodeParent.HasChildren)
            {
                foreach (TreeListNode item in nodeParent.Nodes)
                {
                    _childListPrimaryKey.Add(item.GetValue(Cat_ID));
                    if (item.HasChildren)
                    {
                        GetChildListPrimaryKey(item.Id);
                        //GetChildListPrimaryKey(item.GetValue(Cat_ID_Name));
                    }
                }
            }
        }

        /// <summary>
        /// Khóa ngoại của bảng cha
        /// </summary>
        public object ParentKey
        {
            get { return parentKey; }
            set { parentKey = value; }
        }
        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public object Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// Có hợp lệ hay chưa
        /// </summary>
        public bool IsValidated
        {
            get { return isValidated; }
            set { isValidated = value; }
        }

        public string EditText
        {
            set
            {
                popupContainerEditKhoa.Text = value;
            }
            get
            {
                return popupContainerEditKhoa.Text;
            }
        }


        private void Khoa_Load(object sender, EventArgs e)
        {
            //pbcuong
            // Khởi tạo datasource
            InitDataSource();
            // Khởi cây phân cấp
            InitTree();
        }

        void ParentForm_Load(object sender, EventArgs e)
        {
            // Khởi tạo datasource
            InitDataSource();
            // Khởi cây phân cấp
            InitTree();
        }

        /// <summary>
        /// Load lại dữ liệu trên treeview
        /// </summary>
        public void ReLoad()
        {
            treeListKhoa.ClearNodes();
            // Khởi tạo datasource
            InitDataSource();
            // Khởi cây phân cấp
            InitTree();
        }

        private void popupContainerEditKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
            {
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                point = GetCaretPosition;
                popup.Show(popupContainerEditKhoa, point.X);
                content.dgKhoa.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CheckInfo();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                popup.Close();
            }
            else
            {
                point = GetCaretPosition;
                popup.Show(popupContainerEditKhoa, point.X);
            }
        }

        private void popupContainerEditKhoa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
            {
                return;
            }
            if (isEdit)
            {
                ChangeType();
            }
        }

        private void popupContainerEditKhoa_EditValueChanged(object sender, EventArgs e)
        {
            if (isEdit)
            {
                ChangeType();
            }
        }

        private void popupContainerEditKhoa_TextChanged(object sender, EventArgs e)
        {
            if (isEdit)
            {
                try
                {
                    string value = popupContainerEditKhoa.Text.Trim();
                    string[] array = value.Split(',');

                    if (value.Length <= 1)
                    {
                        foreach (Item item in items)
                        {
                            item.Name = null;
                            item.Code = null;
                            item.PrimaryKey = null;
                            item.ParentKey = null;
                        }
                    }

                    value = array[currentItem.Index].Trim();

                    content.gvKhoa.Columns[Cat_Title_Name].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, string.Format("[" + Cat_Title_Name + "] like '%{0}%'", value), string.Empty);
                }
                catch
                {
                    content.gvKhoa.Columns[Cat_Title_Name].FilterInfo = new ColumnFilterInfo();
                }
            }
        }

        private void popupContainerEditKhoa_Leave(object sender, EventArgs e)
        {
            if (content.dgKhoa.IsFocused)
            {
                popup.Show(popupContainerEditKhoa, point.X);
            }
            else
            {
                popup.Close();
                this.PrimaryKey = primaryKey;
            }
        }

        private void popupContainerEditKhoa_QueryPopUp(object sender, CancelEventArgs e)
        {
            treeListKhoa.ExpandAll();
            treeListKhoa.Selection.Clear();
            popup.Close();

            try
            {
                treeListKhoa.Selection.Add(treeListKhoa.FindNodeByFieldValue(Cat_Title_Name, currentItem.Name));
            }
            catch { }
        }

        private void popupContainerEditKhoa_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            isEdit = false;
            bool flag = false;
            //treeListKhoa.Selection.Clear();
            DevExpress.XtraTreeList.Nodes.TreeListNode itemNoteSelected = null;
            DevExpress.XtraTreeList.TreeListMultiSelection itemSelectionMulti = treeListKhoa.Selection;
            if (itemSelectionMulti.Count == 0)
            {
                if (treeListKhoa.FocusedNode.Focused)
                {
                    itemNoteSelected = treeListKhoa.FocusedNode;
                }
            }
            else
            {
                itemNoteSelected = itemSelectionMulti[0];
            }
            //foreach (DevExpress.XtraTreeList.Nodes.TreeListNode itemNote in treeListKhoa.Nodes)
            //{
            //    if (itemNote.HasChildren)
            //    {
            //        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode itemNoteChild in itemNote.Nodes)
            //        {
            //            if (itemNoteChild.Selected || itemNoteChild.Focused)
            //            {
            //                itemNoteSelected = itemNoteChild;
            //                break;
            //            }
            //        }
            //    }

            //    if (itemNote.Selected || itemNote.Focused)
            //    {
            //        itemNoteSelected = itemNote;
            //        break;
            //    }
            //}
            if (itemNoteSelected != null)
            {
                //itemNoteSelected = itemSelectionMulti[0];
                //var a = treeListKhoa.Selection[0].GetDisplayText(Cat_Title_Name).Trim();
                //items[treeListKhoa.Selection[0].Level].PrimaryKey = treeListKhoa.Selection[0].GetDisplayText(Cat_ID_Name).Trim();
                //items[treeListKhoa.Selection[0].Level].Name = treeListKhoa.Selection[0].GetDisplayText(Cat_Title_Name).Trim();
                //items[treeListKhoa.Selection[0].Level].Code = treeListKhoa.Selection[0].GetDisplayText(Cat_CODE_Name).Trim();
                //items[treeListKhoa.Selection[0].Level].ParentKey = treeListKhoa.Selection[0].GetDisplayText(Cat_Parent_id_Name).Trim();
                items[0].PrimaryKey = itemNoteSelected.GetDisplayText(Cat_ID_Name).Trim();
                items[0].Name = itemNoteSelected.GetDisplayText(Cat_Title_Name).Trim();
                items[0].Code = itemNoteSelected.GetDisplayText(Cat_CODE_Name).Trim();
                items[0].ParentKey = itemNoteSelected.GetDisplayText(Cat_Parent_id_Name).Trim();
                this.primaryKey = items[0].PrimaryKey;
                this.value = items[0].Name;
                this.code = items[0].Code;
                this.parentKey = items[0].ParentKey;


                if (itemNoteSelected.GetDisplayText(Cat_ID_Name).Trim().Length == 0)
                {
                    isValidated = true;
                    try
                    {
                        IsValid(popupContainerEditKhoa, new ControlEventArgs(popupContainerEditKhoa));
                    }
                    catch (Exception ex)
                    {

                    }
                    return;
                }
            }


            string result;
            if (items[0].Index == items.Count - 1)
            {
                result = string.Format("{0}", items[0].Name);
                flag = true;
            }
            else
            {
                result = string.Format("{0}, ", items[0].Name);
            }

            if (items[0].Index == 0)
            {
                e.Value = result;
            }
            else
            {
                for (int i = items[0].Index - 1; i >= 0; i--)
                {
                    dtFilter = dataAccess.Ds.Tables[items[i].TableName].Copy();
                    dvFilter = dtFilter.DefaultView;

                    dvFilter.RowFilter = string.Format("Cat_ID = {0}", items[i + 1].ParentKey);

                    if (dvFilter.Count == 0)
                    {
                        break;
                    }
                    items[i].PrimaryKey = dvFilter[0][Cat_ID_Name];
                    items[i].Name = dvFilter[0][Cat_Title_Name];
                    items[i].Code = dvFilter[0][Cat_CODE_Name];
                    items[i].ParentKey = dvFilter[0][Cat_Parent_id_Name];

                    result = string.Format("{0}, ", items[i].Name) + result;
                }
                e.Value = result;
            }

            if (flag)
            {
                this.primaryKey = items[items.Count - 1].PrimaryKey;
                this.value = items[items.Count - 1].Name;
                this.code = items[items.Count - 1].Code;
                this.parentKey = items[items.Count - 1].ParentKey;
                popup.Close();
                if (IsValid != null)
                {
                    isValidated = true;
                    IsValid(popupContainerEditKhoa, new ControlEventArgs(popupContainerEditKhoa));
                }
            }
            else
            {
                for (int i = items[0].Index + 1; i < items.Count; i++)
                {
                    items[i].PrimaryKey = null;
                    items[i].Name = null;
                    items[i].Code = null;
                    items[i].ParentKey = null;
                }
                //if (items[treeListKhoa.FocusedNode.Level].Index < items.Count - 1)
                //{
                //    currentItem = items[treeListKhoa.FocusedNode.Level + 1];

                //    content.gvKhoa.Columns["NAME"].FilterInfo = new ColumnFilterInfo();
                //    ChangeDataSource();

                //    point = GetCaretPosition;
                //    popup.Show(popupContainerEditKhoa, point.X);
                //}
                if (IsValid != null)
                {
                    isValidated = true;
                    IsValid(popupContainerEditKhoa, new ControlEventArgs(popupContainerEditKhoa));
                }
            }
            isEdit = true;
        }

        private void treeListKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                popupContainerControlKhoa.FindForm().Validate();
                ClosePopup();
            }
        }

        private void treeListKhoa_DoubleClick(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void dgKhoa_DoubleClick(object sender, EventArgs e)
        {
            GetInfo();
        }

        private void dgKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                popupContainerEditKhoa.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                GetInfo();
            }
        }

        private void dgKhoa_Leave(object sender, EventArgs e)
        {
            if (popupContainerEditKhoa.Focused)
            {
                popup.Show(popupContainerEditKhoa, point.X);
            }
            else
            {
                popup.Close();
            }
        }

        /// <summary>
        /// Lấy point hiện tại của con trỏ trên ô nhập
        /// </summary>
        /// <param name="point">Vị trí con trỏ trên ô nhập</param>
        /// <returns>Số int cho biết thực hiện được hay không</returns>
        [DllImport("user32")]
        private static extern int GetCaretPos(ref Point point);

        /// <summary>
        /// Khởi tạo datasource
        /// </summary>
        private void InitDataSource()
        {
            if (items.Count == 0)
            {
                items.Add(new Item("category", "category", "DanhMuc"));
            }
            dataAccess = new ClsPopupTreeView(items);


            // Đối tượng hiện tại
            currentItem = items[0];
            // Thay đổi datasource lưới tìm kiếm nhanh
            ChangeDataSource();
        }

        /// <summary>
        /// Khởi tạo cây phân cấp
        /// </summary>
        private void InitTree()
        {
            treeListKhoa.AppendNode(new object[] { "None", 0, "" }, -1);
            try
            {
                foreach (Item item in items)
                {
                    foreach (DataRow row in dataAccess.Ds.Tables[item.TableName].Rows)
                    {
                        if (string.IsNullOrEmpty(item.ParentTableName))
                        {
                            //treeListKhoa.AppendNode(new object[] { row["NAME"], row[Cat_ID_Name], row[Cat_CODE_Name], row[Cat_Parent_id_Name], row["PARENTNAME"] }, -1);
                            treeListKhoa.AppendNode(new object[] { row[Cat_Title_Name], row[Cat_ID_Name], row[Cat_Parent_id_Name] }, -1);
                        }
                        else
                        {
                            //object a = row[Cat_ID_Name];
                            //object b = treeListKhoa.FindNodeByFieldValue(Cat_ID_Name, row[Cat_ID_Name]);
                            //treeListKhoa.AppendNode()
                            treeListKhoa.AppendNode(new object[] { row[Cat_Title_Name], row[Cat_ID_Name], row[Cat_Parent_id_Name] }, treeListKhoa.FindNodeByFieldValue(Cat_ID_Name, row[Cat_Parent_id_Name]));
                            // treeListKhoa.AppendNode(new object[] { row["NAME"], row[Cat_ID_Name], row[Cat_CODE_Name], row[Cat_Parent_id_Name], row["PARENTNAME"] }, treeListKhoa.FindNodeByFieldValue("NAME", row["PARENTNAME"]));
                        }
                    }
                }
                treeListKhoa.ExpandAll();
            }
            catch { }
        }

        /// <summary>
        /// Thay đổi lưới tìm kiếm nhanh
        /// </summary>
        private void ChangeType()
        {
            try
            {
                string value = popupContainerEditKhoa.Text;
                if (value.Length == 0)
                {
                    currentItem = items[0];
                    return;
                }
                popupContainerEditKhoa.SelectionStart = value.Length;
                int count = value.Length - popupContainerEditKhoa.SelectionStart == 0 ? value.Length : popupContainerEditKhoa.SelectionStart;
                string compare = value.Substring(0, count);

                int index = compare.Split(',').Length > items.Count ? items.Count : compare.Split(',').Length;
                index = index - 1;
                currentItem = items[index];
                ChangeDataSource();
            }
            catch { }
        }

        /// <summary>
        /// Thay đổi datasource lưới tìm kiếm nhanh
        /// </summary>
        private void ChangeDataSource()
        {
            try
            {
                content.dgKhoa.DataSource = dataAccess.Ds;
                content.dgKhoa.DataMember = currentItem.TableName;
                content.gvKhoa.Columns[Cat_Title_Name].Caption = currentItem.Caption;

                if (currentItem.Index > 0)
                {
                    try
                    {
                        content.gvKhoa.Columns[Cat_Parent_id_Name].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, string.Format("[Parent_id] = {0}", items[currentItem.Index - 1].PrimaryKey), string.Empty);
                    }
                    catch { }
                }
                else
                {
                    content.gvKhoa.Columns[Cat_Parent_id_Name].FilterInfo = new ColumnFilterInfo();
                }
            }
            catch { }
        }

        private void ClosePopup()
        {
            if (popupContainerControlKhoa.OwnerEdit != null)
            {
                popupContainerControlKhoa.OwnerEdit.ClosePopup();
                popupContainerEditKhoa.Focus();
                popupContainerEditKhoa.SelectionStart = popupContainerEditKhoa.Text.Length;

                if (string.IsNullOrEmpty(this.primaryKey + string.Empty))
                {
                    popupContainerEditKhoa.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Lấy thông tin trên lưới tìm kiếm nhanh
        /// </summary>
        private void GetInfo()
        {
            if (content.gvKhoa.SelectedRowsCount > 0 && !content.gvKhoa.IsFilterRow(content.gvKhoa.GetSelectedRows()[0]))
            {
                isEdit = false;
                bool flag = false;

                currentItem.PrimaryKey = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_ID_Name);
                currentItem.Code = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_CODE_Name);
                currentItem.Name = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_Title_Name);

                this.primaryKey = currentItem.PrimaryKey;
                this.value = currentItem.Name;
                this.code = currentItem.Code;
                this.parentKey = currentItem.ParentKey;

                if (currentItem.Index < items.Count - 1)
                {
                    currentItem = items[currentItem.Index + 1];
                }

                string value = string.Empty;
                foreach (Item item in items)
                {
                    if (item.Name != null || !string.IsNullOrEmpty(item.Name + ""))
                    {
                        if (item.Index == items.Count - 1)
                        {
                            value += string.Format("{0}", item.Name);
                            flag = true;
                        }
                        else
                        {
                            value += string.Format("{0}, ", item.Name);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                popupContainerEditKhoa.Text = value;
                popupContainerEditKhoa.Focus();
                popupContainerEditKhoa.SelectionStart = popupContainerEditKhoa.Text.Length;
                isEdit = true;

                if (!flag)
                {
                    content.gvKhoa.Columns[Cat_Title_Name].FilterInfo = new ColumnFilterInfo();
                    ChangeDataSource();

                    point = GetCaretPosition;
                    popup.Show(popupContainerEditKhoa, point.X);

                    isValidated = false;

                }
                else
                {
                    this.primaryKey = items[items.Count - 1].PrimaryKey;
                    this.value = items[items.Count - 1].Name;
                    this.code = items[items.Count - 1].Code;
                    this.parentKey = items[items.Count - 1].ParentKey;
                    popup.Close();
                    if (IsValid != null)
                    {
                        isValidated = true;
                        IsValid(popupContainerEditKhoa, new ControlEventArgs(popupContainerEditKhoa));
                    }
                }
            }
        }

        private void CheckInfo()
        {
            try
            {
                isEdit = false;
                bool flag = false;
                string value = popupContainerEditKhoa.Text.Trim();
                string[] array = value.Split(',');

                value = array[currentItem.Index];

                dtFilter = dataAccess.Ds.Tables[currentItem.TableName].Copy();
                dvFilter = dtFilter.DefaultView;
                dvFilter.RowFilter = string.Format("NAME = '{0}'", value);

                if (dvFilter.Count > 0)
                {
                    currentItem.PrimaryKey = dvFilter[0][Cat_ID_Name];
                    currentItem.Name = dvFilter[0][Cat_Title_Name];
                    currentItem.Code = dvFilter[0][Cat_CODE_Name];
                    currentItem.ParentKey = dvFilter[0][Cat_Parent_id_Name];
                }
                else if (content.gvKhoa.RowCount > 0)
                {
                    currentItem.PrimaryKey = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_ID_Name);
                    currentItem.Name = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_Title_Name);
                    currentItem.Code = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_CODE_Name);
                    currentItem.ParentKey = content.gvKhoa.GetRowCellValue(content.gvKhoa.GetSelectedRows()[0], Cat_Parent_id_Name);
                }

                this.primaryKey = currentItem.PrimaryKey;
                this.value = currentItem.Name;
                this.code = currentItem.Code;
                this.parentKey = currentItem.ParentKey;

                string result;
                if (currentItem.Index == items.Count - 1)
                {
                    result = string.Format("{0}", currentItem.Name);
                }
                else
                {
                    result = string.Format("{0}, ", currentItem.Name);
                }

                if (currentItem.Index > 0)
                {
                    for (int i = currentItem.Index - 1; i >= 0; i--)
                    {
                        dtFilter = dataAccess.Ds.Tables[items[i].TableName].Copy();
                        dvFilter = dtFilter.DefaultView;
                        dvFilter.RowFilter = string.Format("ID = {0}", items[i + 1].ParentKey);
                        if (dvFilter.Count == 0)
                        {
                            break;
                        }
                        items[i].PrimaryKey = dvFilter[0][Cat_ID_Name];
                        items[i].Name = dvFilter[0][Cat_Title_Name];
                        items[i].Code = dvFilter[0][Cat_CODE_Name];
                        items[i].ParentKey = dvFilter[0][Cat_Parent_id_Name];

                        result = string.Format("{0}, ", items[i].Name) + result;
                    }
                }

                for (int i = currentItem.Index + 1; i < items.Count; i++)
                {
                    items[i].PrimaryKey = null;
                    items[i].Name = null;
                    items[i].Code = null;
                    items[i].ParentKey = null;
                }

                if (currentItem.Index == items.Count - 1)
                {
                    flag = true;
                }
                else if (currentItem.Index < items.Count - 1)
                {
                    currentItem = items[currentItem.Index + 1];
                }

                popupContainerEditKhoa.Text = result;
                popupContainerEditKhoa.Focus();
                popupContainerEditKhoa.SelectionStart = popupContainerEditKhoa.Text.Length;
                isEdit = true;

                if (!flag)
                {
                    content.gvKhoa.Columns[Cat_Title_Name].FilterInfo = new ColumnFilterInfo();
                    ChangeDataSource();

                    point = GetCaretPosition;
                    popup.Show(popupContainerEditKhoa, point.X);
                }
                else
                {
                    this.primaryKey = items[items.Count - 1].PrimaryKey;
                    this.value = items[items.Count - 1].Name;
                    this.code = items[items.Count - 1].Code;
                    this.parentKey = items[items.Count - 1].ParentKey;
                    popup.Close();
                    if (IsValid != null)
                    {
                        IsValid(popupContainerEditKhoa, new ControlEventArgs(popupContainerEditKhoa));
                    }
                }
            }
            catch { }
        }

        public DataRow GetFirstRow()
        {
            return dataAccess.Ds.Tables[items[items.Count - 1].TableName].Rows.Count > 0 ?
                dataAccess.Ds.Tables[items[items.Count - 1].TableName].Rows[0] : null;

        }

        public int GetFirstRowEditValue()
        {
            return dataAccess.Ds.Tables[items[items.Count - 1].TableName].Rows.Count > 0 ?
            ClsChangeType.change_int(dataAccess.Ds.Tables[items[items.Count - 1].TableName].Rows[0][Cat_ID_Name]) : 0;

        }
    }
}