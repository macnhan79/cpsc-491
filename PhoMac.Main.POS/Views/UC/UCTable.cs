using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoHa7.Library.Froms;
using PhoMac.Business.Data;
using PhoMac.Business.Presenter;
using DevExpress.XtraEditors;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCTable : XtraUserControlKira
    {

        #region Property

        DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                lblTime.Text = String.Format("h:mm tt", _time);
            }
        }

        public string TableName
        {
            get
            {
                return lblTableName.Text;
            }
            set
            {
                lblTableName.Text = value;
            }
        }

        public string ServerName
        {
            get
            {
                return lblServerName.Text;
            }
            set
            {
                lblServerName.Text = "Server Name: " + value;
            }
        }

        public string StatusTable
        {
            get
            {
                return lblStatus.Text;
            }
            set
            {
                lblStatus.Text = "Status: " + value;
            }
        }

        /// <summary>
        /// Color for entire user control
        /// </summary>
        Color _uCColor;
        public Color UCColor
        {
            get { return _uCColor; }
            set
            {
                _uCColor = value;
                this.BackColor = _uCColor;
            }
        }

        /// <summary>
        /// Set Forc=eColor
        /// </summary>
        Color _uCForeColor;
        public Color UCForeColor
        {
            get { return _uCForeColor; }
            set
            {
                _uCForeColor = value;
                lblTime.ForeColor = _uCForeColor;
                lblServerName.ForeColor = _uCForeColor;
                lblStatus.ForeColor = _uCForeColor;
                lblTableName.ForeColor = _uCForeColor;
            }
        }

        int tableID;
        public int TableID
        {
            get { return tableID; }
            set
            {
                tableID = value;
                TableInfo.Tables = dao.GetById<PhoMac.Model.Table>(TableID);
                //init();
            }
        }


        bool _allowDrapAndDrop = false;
        public bool AllowDrop
        {
            get { return _allowDrapAndDrop; }
            set
            {
                _allowDrapAndDrop=value;
                lblTime.AllowDrop = value;
                lblTableName.AllowDrop = value;
                lblStatus.AllowDrop = value;
                lblServerName.AllowDrop = value;
                if (_allowDrapAndDrop)
                {
                    this.lblTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblTableName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblServerName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                }
                else
                {
                    this.lblTime.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblTableName.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblStatus.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                    this.lblServerName.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
                }
            }
        }

        public TablePresenter TableInfo { get; set; }
        Dao dao = new Dao();
        #endregion


        public UCTable()
        {
            InitializeComponent();
           // this.Dock = DockStyle.Fill;
            TableInfo = new TablePresenter();
        }



        #region Init

        public override void init()
        {
            TableInfo.Tables = dao.GetById<PhoMac.Model.Table>(TableID);
            //set table name
            TableName = TableInfo.TableName;
            TableID = TableInfo.TableID;
            this.Name = TableInfo.TableID + string.Empty;
            //set color for user control
            UCColor = TableInfo.ButtonColor;
            UCForeColor = TableInfo.oForeColor;
        }

        #endregion

        #region Method
        #endregion

        #region Drap and Drop Event

        #region Drap Enter

        private void lblTableName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }

        #endregion

        #region Drop

        private void lblTableName_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(UCTable)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lblTableName_DragDrop(object sender, DragEventArgs e)
        {
            LabelControl lbl = sender as LabelControl;


            UCTable objReceive = lbl.Parent.Parent as UCTable;
            UCTable objSend = e.Data.GetData(typeof(UCTable)) as UCTable;


            int tablIDReceive = objReceive.TableInfo.TableID;
            objReceive.TableID = objSend.TableID;
            objReceive.init();
            objSend.TableID = tablIDReceive;
            objSend.init();
            OnExChangeObject(this, new PhoMac.Main.POS.Views.UC.UCPanelLayoutTable.ExChangeObjectEventArgs(objSend, objReceive));

            //


            //change table ID, row, col
            //int rowSend = objSend.TableInfo.Row;
            //int colSend = objSend.TableInfo.Col;
            //int tempTableID = objSend.TableID;

            //objSend.TableID = objReceive.TableID;
            //objReceive.TableID = tempTableID;

            ////change row, col of table
            //objSend.TableInfo.Row = objReceive.TableInfo.Row;
            //objSend.TableInfo.Col = objReceive.TableInfo.Col;
            //objReceive.TableInfo.Row = rowSend;
            //objReceive.TableInfo.Col = colSend;

            ////not link to viewmanager
            ////keep list in ViewManager to handle Cancel function
            ////Create a class or UC to store modificantion Object-->Update
            //ViewManager view = ViewManager.getInstance();
            //UCTable temp = view.ListUCTables.Where(x => x.TableInfo.TableID == 10).First();
            //int row = temp.TableInfo.Row;
            //int col = temp.TableInfo.Col;

            ////mark is change for table in order to update to database


            ////link object to ViewManager?????



            //objSend.init();
            //objReceive.init();
        }

        public event PhoMac.Main.POS.Views.UC.UCPanelLayoutTable.ExChangeObjectHandler ExChangeObject;

        protected void OnExChangeObject(object sender, PhoMac.Main.POS.Views.UC.UCPanelLayoutTable.ExChangeObjectEventArgs frmClickInfo)
        {
            if (ExChangeObject != null)
            {
                ExChangeObject(this, frmClickInfo);
            }
        }

        #endregion

        #region Enable Drap and Drop
        bool _EnableDrapAndDrop = false;
        public bool EnableDrapAndDrop
        {
            get { return _EnableDrapAndDrop; }
            set
            {
                _EnableDrapAndDrop = value;
                lblTime.AllowDrop = _EnableDrapAndDrop;
                lblTableName.AllowDrop = _EnableDrapAndDrop;
                lblStatus.AllowDrop = _EnableDrapAndDrop;
                lblServerName.AllowDrop = _EnableDrapAndDrop;
            }
        }
        #endregion

        #endregion


        #region Event Form Click

        private void lblTime_Click(object sender, EventArgs e)
        {
            int row = TableInfo.Row;
            int col = TableInfo.Col;
            OnFrmClick(this, new FrmClickEventArgs(TableID));
        }

        private void lblTableName_Click(object sender, EventArgs e)
        {
            OnFrmClick(this, new FrmClickEventArgs(TableID));
        }

        private void lblServerName_Click(object sender, EventArgs e)
        {
            OnFrmClick(this, new FrmClickEventArgs(TableID));
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            OnFrmClick(this, new FrmClickEventArgs(TableID));
        }

        //public delegate void FrmClickHandler(object sender, FrmClickEventArgs frmClickInfo);

        //public event FrmClickHandler FrmClick;

        //protected void OnFrmClick(object sender, FrmClickEventArgs frmClickInfo)
        //{
        //    if (FrmClick != null)
        //    {
        //        FrmClick(this, frmClickInfo);
        //    }
        //}

        //public class FrmClickEventArgs : EventArgs
        //{
        //    public string _name;
        //    public FrmClickEventArgs(string name)
        //    {
        //        _name = name;
        //    }
        //}

        #endregion


        //private void lblTime_DoubleClick(object sender, EventArgs e)
        //{
        //    int row = TableInfo.Row;
        //    int col = TableInfo.Col;
        //}

        private void lblTableName_SizeChanged(object sender, EventArgs e)
        {
            Size s = lblTableName.Size;
            if (s.Height < 60)
            {
                tableLayoutPanel1.SetRow(lblTableName, 0);
                tableLayoutPanel1.SetRowSpan(lblTableName, 4);
            }
            else
            {
                tableLayoutPanel1.SetRow(lblTableName, 1);
                tableLayoutPanel1.SetRowSpan(lblTableName, 1);
            }


            //if only show label Table Name--> 
            //lblTableName.AllowHtmlString = true;
            //lblTableName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }






    }
}
