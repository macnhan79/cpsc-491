using System.Data;

namespace PhoHa7.Library.UserControl.DanhMuc
{
    public partial class FrmListUITypeEditor : DevExpress.XtraEditors.XtraUserControl
    {
        public DataRowCollection Values;
        DataTable _dtShowCols;
        public FrmListUITypeEditor()
        {
            InitializeComponent();
        }

        public FrmListUITypeEditor(DataRowCollection dataTableValues)
        {
            Values = dataTableValues;
            InitializeComponent();
           
            _dtShowCols = new DataTable("ShowCols");
            _dtShowCols.Columns.Add(new DataColumn("TableName"));            
            _dtShowCols.Columns.Add(new DataColumn("Name"));
            _dtShowCols.Columns.Add(new DataColumn("Text"));
            _dtShowCols.AcceptChanges();

            foreach (DataRow var in Values)
            {
                DataRow dr = _dtShowCols.NewRow();
                dr["TableName"] = var[0];
                dr["Name"] = var[1];
                dr["Text"] = var[2];
                _dtShowCols.Rows.Add(dr);
            }
            
            _dtShowCols.AcceptChanges();

            gridControl.DataSource = _dtShowCols;
        }

        public DataRowCollection GetValues()
        {
            return _dtShowCols.Rows;
        }
    }
}