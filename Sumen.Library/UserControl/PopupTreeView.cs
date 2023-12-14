using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace PhoHa7.Library.UserControl
{
    public partial class PopupTreeView : DevExpress.XtraEditors.XtraUserControl
    {
        private object dataTable;

        [Category("PopupTreeView")]
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Editor("System.Windows.Forms.Design.DataSourceListEditor, System.Design", typeof(UITypeEditor))]
        public object DataSource
        {
            get { return dataTable; }
            set
            {
                try
                {
                    if (value.GetType() == typeof(BindingSource))
                        dataTable = ((value as BindingSource).DataSource as DataSet).Tables[(value as BindingSource).DataMember];
                    else
                        dataTable = value;
                    LoadTreeView();
                    if (dataTable != null)
                    {
                        (dataTable as DataTable).RowChanged += new DataRowChangeEventHandler(PopupTreeView_RowChanged);
                    }
                }
                catch { }
            }
        }

        private string strKey = "";
        [Category("PopupTreeView")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
        public string DataMember
        {
            get { return strKey; }
            set { strKey = value; LoadTreeView(); }
        }

        private string strParentKey = "";
        [Category("PopupTreeView")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
        public string DataParentMember
        {
            get { return strParentKey; }
            set { strParentKey = value; LoadTreeView(); }
        }

        private string strText = "";
        [Category("PopupTreeView")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
        public string DisplayMember
        {
            get { return strText; }
            set { strText = value; LoadTreeView(); }
        }

        private string objSelectedValue;
        [Category("PopupTreeView")]
        public string SelectedValue
        {
            get { return objSelectedValue; }
            set { objSelectedValue = value; }
        }

        [Category("PopupTreeView")]
        public string SelectedText
        {
            get { return popupContainerEdit.Text; }
            set { popupContainerEdit.Text = value; }
        }

        public PopupTreeView()
        {
            InitializeComponent();
            LoadTreeView();
        }

        void PopupTreeView_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            LoadTreeView();
        }

        public void LoadTreeView()
        {
            try
            {
                if (dataTable != null && strParentKey.Trim() != "" && strKey.Trim() != "" && strText.Trim() != "")
                {
                    treeList.ClearNodes();
                    foreach (DataRow dr in (dataTable as DataTable).Select(strParentKey + " is null"))
                    {
                        NodesAdd(treeList.AppendNode(new object[] { dr[strKey], dr[strText] }, -1), "" + dr[strKey]);
                    }
                }
            }
            catch {}

        }

        private void NodesAdd(TreeListNode Nodes, string strParentValue)
        {
            foreach (DataRow dr in (dataTable as DataTable).Select(strParentKey + " ='" + strParentValue + "'"))
            {
                NodesAdd(Nodes.TreeList.AppendNode(new object[] { dr[strKey], dr[strText] }, Nodes), "" + dr[strKey]);
            }
        }

        private void treeList_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            popupContainerEdit.Text = ""+e.Node.GetValue(1);
            objSelectedValue = ""+e.Node.GetValue(0);
            if (popupContainerControl.OwnerEdit != null && e.Node.Nodes.Count == 0)
                popupContainerControl.OwnerEdit.ClosePopup();
        }

        private void PopupTreeView_SizeChanged(object sender, EventArgs e)
        {
            this.Height = popupContainerEdit.Height;
            popupContainerControl.Width = popupContainerEdit.Width;

        }
    }
}
