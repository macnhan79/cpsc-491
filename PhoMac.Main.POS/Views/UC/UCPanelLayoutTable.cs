using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using PhoHa7.Library.Froms;
using DevExpress.XtraLayout;
using PhoMac.Business.Data;
using PhoMac.Business.Presenter;
using PhoMac.Model;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCPanelLayoutTable : XtraUserControlKira
    {
        Dao dao = new Dao();
        List<object> ListItems;
        public UCPanelLayoutTable()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            ListItems = new List<object>();
        }

        public override void init()
        {
            base.init();
            //TablePresenter table = new TablePresenter();

            //table.CopyToList(dao.FindByMultiColumnAnd<Table>(new[] { "CategoryID" }, 1).ToList());
            //Rows = table.ListTables.Max(p => p.Row);
            //Columns = table.ListTables.Max(p => p.Col);
            //initTableLayout();
            //for (int i = 0; i < table.ListTables.Count; i++)
            //{
            //    UCTable panelTable = new UCTable();
            //    panelTable.TableID = table.ListTables[i].TableID;
            //    PushOject(panelTable, table.ListTables[i].Row - 1, table.ListTables[i].Col - 1);
            //}
        }

        #region Method

        /// <summary>
        /// Place a control to layout with position (row,col)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void PushOject(Control control, int row, int col)
        {
            Panel panel = layoutControl.GetControlByName("" + row + "|" + col) as Panel;
            if (panel.Controls.Count > 0)
            {
                panel.Controls.Clear();
            }
            panel.Controls.Add(control);
            //LayoutControlItem layoutControlItem = new LayoutControlItem();
            //layoutControlItem.Control = control;
            //layoutControlItem.OptionsTableLayoutItem.RowIndex = row;
            //layoutControlItem.OptionsTableLayoutItem.ColumnIndex = col;
            //layoutControlItem.TextVisible = false;
            
            //layoutControlGroup.Items.AddRange(new BaseLayoutItem[] { layoutControlItem });


            ListItems.Add(control);
        }

        

        #endregion

        #region Empty Panel

        /// <summary>
        /// Create empty panel
        /// </summary>
        public Panel createEmptyPanel(string name)
        {
            Panel panel = new Panel();
            panel.Name = name;
            panel.Dock = DockStyle.Fill;
            if (ShowBorderItems)
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
            }
            if (AllowDrapAndDrop)
            {
                panel.AllowDrop = true;
                panel.DragDrop += panel_DragDropToExchangeObject;
                panel.MouseDown += panel_MouseDown;
                panel.DragEnter += panel_DragEnter;
            }

            return panel;
        }

        void panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(UCTable)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }

        void panel_DragDropToExchangeObject(object sender, DragEventArgs e)
        {
            Panel objReceive = sender as Panel;
            UCTable objSend = e.Data.GetData(typeof(UCTable)) as UCTable;

            //


            //change table ID, row, col
            //int rowSend = objSend.TableInfo.Row;
            //int colSend = objSend.TableInfo.Col;
            //int tempTableID = objSend.TableID;

            //UCTable temp = objSend.Parent.Controls[0] as UCTable;
            //objSend.Parent.Controls.RemoveAt(0);
            //objReceive.Controls.Add(temp);

            OnExChangeObject(this, new ExChangeObjectEventArgs(objSend, objReceive));

            //change row, col of table
            //objSend.TableInfo.Row = objReceive.TableInfo.Row;
            //objSend.TableInfo.Col = objReceive.TableInfo.Col;
            //objReceive.TableInfo.Row = rowSend;
            //objReceive.TableInfo.Col = colSend;








            //tao delegate drap and drop uctable here
        }

        public delegate void ExChangeObjectHandler(object sender, ExChangeObjectEventArgs frmClickInfo);

        public event ExChangeObjectHandler ExChangeObject;

        protected void OnExChangeObject(object sender, ExChangeObjectEventArgs frmClickInfo)
        {
            if (ExChangeObject != null)
            {
                ExChangeObject(this, frmClickInfo);
            }
        }

        public class ExChangeObjectEventArgs : EventArgs
        {
            public object _ObjectFrom;
            public object _ObjectTo;
            public ExChangeObjectEventArgs(object ObjectFrom, object ObjectTo)
            {
                _ObjectFrom = ObjectFrom;
                _ObjectTo = ObjectTo;
            }
        }

#endregion

        #region Init
        public LayoutControl layoutControl;
        public LayoutControlGroup layoutControlGroup;
        /// <summary>
        /// init Table Layout
        /// If Row == 0, Row height = 200px, enable scroll
        /// </summary>
        public void initLayout()
        {
            //percent for height
            double percentHeight = 100.0 / _rows;
            //percent for width
            double percentWidth = 100.0 / _cols;

            
            

            layoutControl.AllowDrop = true;

            // 
            // layoutControl
            // 
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Root = this.layoutControlGroup;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup.GroupBordersVisible = true;
            this.layoutControlGroup.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            this.layoutControlGroup.OptionsTableLayoutGroup.RowDefinitions.RemoveAt(1);
            this.layoutControlGroup.OptionsTableLayoutGroup.RowDefinitions.RemoveAt(0);
            this.layoutControlGroup.OptionsTableLayoutGroup.ColumnDefinitions.RemoveAt(1);
            this.layoutControlGroup.OptionsTableLayoutGroup.ColumnDefinitions.RemoveAt(0);

            //init column
            ColumnDefinition[] colList = new ColumnDefinition[_cols];
            for (int i = 0; i < _cols; i++)
            {
                colList[i] = new ColumnDefinition();
                colList[i].SizeType = System.Windows.Forms.SizeType.Percent;
                colList[i].Width = percentWidth;
            }
            //add column to layout
            this.layoutControlGroup.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(colList);

            if (_rows == 0)
            {
                _rows = LengthItems % _cols + 1;
            }
            //init row
            RowDefinition[] rowList = new RowDefinition[_rows];
            for (int i = 0; i < _rows; i++)
            {
                rowList[i] = new RowDefinition();
                if (_rows == 0)
                {
                    rowList[i].Height = DefaultHeightRowPx;
                    rowList[i].SizeType = System.Windows.Forms.SizeType.Absolute;
                }
                else
                {
                    rowList[i].Height = percentHeight;
                    rowList[i].SizeType = System.Windows.Forms.SizeType.Percent;
                }

            }
            //add rows to layout
            this.layoutControlGroup.OptionsTableLayoutGroup.RowDefinitions.AddRange(rowList);
            this.layoutControlGroup.TextVisible = false;


            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    LayoutControlItem layoutControlItem = new LayoutControlItem();
                    layoutControlItem.Name = "" + r + "|" + c;
                    Panel panel = createEmptyPanel("" + r + "|" + c);
                    layoutControlItem.Control = panel;
                    layoutControlItem.OptionsTableLayoutItem.RowIndex = r;
                    layoutControlItem.OptionsTableLayoutItem.ColumnIndex = c;
                    layoutControlItem.TextVisible = false;
                    //layoutControlItem. = false;

                    layoutControlGroup.Items.AddRange(new BaseLayoutItem[] { layoutControlItem });
                }
            }

            
            this.Controls.Add(layoutControl);
        }

        #endregion

        #region Property

        public int DefaultHeightRowPx = 200;

        public int LengthItems
        {
            get;
            set;
        }

        int _rows = 0, _cols = 0;
        /// <summary>
        /// If Row == 0, Enable Scroll for Table Layout
        /// Please input LengthItems to calculate number of rows
        /// </summary>
        public int NumberOfRows
        {
            get { return _rows; }
            set
            {
                _rows = value;

            }
        }

        public int NumberOfColumns
        {
            get { return _cols; }
            set
            {
                _cols = value;

            }
        }

        public bool AllowDrapAndDrop
        {
            get { return layoutControl.AllowDrop; }
            set { layoutControl.AllowDrop = value; }
        }

        bool _showBorderItems;
        public bool ShowBorderItems
        {
            get { return _showBorderItems; }
            set
            {
                _showBorderItems = value;
                if (_showBorderItems)
                {
                    layoutControl.OptionsView.ShareLookAndFeelWithChildren = false;
                    layoutControl.LookAndFeel.SetFlatStyle();
                    layoutControl.OptionsView.DrawItemBorders = true;
                    layoutControl.OptionsView.ItemBorderColor = Color.Black;
                }
                else
                {
                    layoutControl.OptionsView.ShareLookAndFeelWithChildren = false;
                    layoutControl.LookAndFeel.SetFlatStyle();
                    layoutControl.OptionsView.DrawItemBorders = false;
                    layoutControl.OptionsView.ItemBorderColor = Color.Transparent;
                }
            }
        }

        #endregion


        //////////////////////////////////////////////////////////////////////////
        //init table
        ////
        //TablePresenter table = new TablePresenter();
        //table.CopyToList(dao.FindByMultiColumnAnd<Table>(new[] { "CategoryID" }, 1).ToList());
        //Rows = table.ListTables.Max(p => p.Row);
        //Columns = table.ListTables.Max(p => p.Col);
        //initLayout();
        //for (int i = 0; i < table.ListTables.Count; i++)
        //{
        //    UCTable panelTable = new UCTable();
        //    panelTable.TableID = table.ListTables[i].TableID;
        //    PushOject(panelTable, table.ListTables[i].Row - 1, table.ListTables[i].Col - 1);
        //}


    }
}
