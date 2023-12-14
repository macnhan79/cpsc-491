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
using PhoMac.Business.Presenter;
using PhoMac.Business.Data;
using PhoMac.Model;
using PhoMac.Main.POS.Views.UC;
using DevExpress.XtraReports.Design;
using DevExpress.XtraTab;

namespace PhoMac.Main.POS.Views
{
    public partial class TableEditView : XtraUserControlKira
    {
        Dao dao;
        public TableEditView()
        {
            InitializeComponent();
            // tabControl1.h
            //ucPanelTable.ExChangeObject += OnExChangeObject;
        }




        #region Init

        UCObject selectedTab;
        XtraTabControl tabControl;
        public override void init()
        {
            base.init();
            //init table
            ////
            dao = new Dao();
            TabCategoryPresenter cat = new TabCategoryPresenter();
            cat.CopyToList(dao.GetAll<TabCategory>().ToList());
            ucPanelCategory.NumberOfColumns = 1;
            ucPanelCategory.NumberOfRows = cat.ListTabCategories.Count;
            ucPanelCategory.initLayout();
            createTabTable();
            for (int i = 0; i < cat.ListTabCategories.Count; i++)
            {
                UCObject panelCat = new UCObject();
                panelCat.FrmClick += panelCat_FrmClick;
                panelCat.ObjectInfos.ObjectReference = cat.ListTabCategories[i];
                panelCat.ObjectInfos.ID = cat.ListTabCategories[i].CategoryID;
                panelCat.ObjectInfos.Name = cat.ListTabCategories[i].CategoryName;
                ucPanelCategory.PushOject(panelCat, cat.ListTabCategories[i].Row - 1, 0);
                if (i == 0)
                {
                    panelCat.Selected = true;
                    selectedTab = panelCat;
                }
                //create tab table
                addTabPage(cat.ListTabCategories[i]);
            }
        }

        void panelCat_FrmClick(object sender, XtraUserControlKira.FrmClickEventArgs frmClickInfo)
        {
            string tabid = (frmClickInfo.Parameters as UCObject).ObjectInfos.ID + string.Empty;
            selectedTab.Selected = false;
            selectedTab = sender as UCObject;
            selectedTab.Selected = true;
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                if (tabControl.TabPages[i].Name == tabid)
                {
                    tabControl.SelectedTabPage = tabControl.TabPages[i];
                    break;
                }
            }
        }

        void createTabTable()
        {
            tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.panelTable.Controls.Add(tabControl);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        void addTabPage(TabCategoryPresenter pCat)
        {
            XtraTabPage xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            xtraTabPage.Name = pCat.CategoryID + string.Empty;
            UCPanelLayoutTable panelLayoutTable = new UCPanelLayoutTable();
            //panelLayoutTable.AllowDrapAndDrop = false;
            panelLayoutTable.ExChangeObject += OnExChangeObject;

            TablePresenter table = new TablePresenter();
            table.CopyToList(dao.FindByMultiColumnAnd<Table>(new[] { "CategoryID" }, pCat.CategoryID).ToList());
            panelLayoutTable.NumberOfColumns = pCat.Cols;
            panelLayoutTable.NumberOfRows = pCat.Rows;
            panelLayoutTable.initLayout();
            for (int i = 0; i < table.ListTables.Count; i++)
            {
                UCTable pTable = new UCTable();
                pTable.AllowDrop = false;
                pTable.ExChangeObject += pTable_ExChangeObject;
                pTable.TableID = table.ListTables[i].TableID;
                pTable.init();
                panelLayoutTable.PushOject(pTable, table.ListTables[i].Row - 1, table.ListTables[i].Col - 1);
            }
            xtraTabPage.Controls.Add(panelLayoutTable);
            tabControl.TabPages.Add(xtraTabPage);
        }

        void pTable_ExChangeObject(object sender, UCPanelLayoutTable.ExChangeObjectEventArgs e)
        {
            object objectfrom = e._ObjectFrom;
            object objectTo = e._ObjectTo;
            UCTable objSend = objectfrom as UCTable;
            UCTable objReceive = objectTo as UCTable;


            //int tablIDReceive = objReceive.TableInfo.TableID;
            //objReceive.TableID = objSend.TableID;
            //objReceive.init();
            //objSend.TableID = tablIDReceive;
            //objSend.init();

           int rowReceive= objReceive.TableInfo.Row;
           int colReceive = objReceive.TableInfo.Col;
            objReceive.TableInfo.Row = Convert.ToInt32(objSend.TableInfo.Row);
            objReceive.TableInfo.Col = Convert.ToInt32(objSend.TableInfo.Col);
            objSend.TableInfo.Row = Convert.ToInt32(rowReceive);
            objSend.TableInfo.Col = Convert.ToInt32(colReceive);
            ListTableChange.Add(objReceive.TableInfo);
            ListTableChange.Add(objSend.TableInfo);

        }

        #endregion



        #region Handle exchange object

        List<TablePresenter> ListTableChange = new List<TablePresenter>();

        //handle event drap and drop change position UCTable
        void OnExChangeObject(object sender, PhoMac.Main.POS.Views.UC.UCPanelLayoutTable.ExChangeObjectEventArgs e)
        {
            object objectfrom = e._ObjectFrom;
            object objectTo = e._ObjectTo;




            UCTable objSend = objectfrom as UCTable;
            Panel objReceive = objectTo as Panel;
            if (objReceive != null)
            {
                TablePresenter tableInfor = objSend.TableInfo;
                string[] rowCol = objReceive.Name.Split('|');
                //change table ID, row, col
                int rowSend = Convert.ToInt32(rowCol[0]);
                int colSend = Convert.ToInt32(rowCol[1]);
                int tableID = objSend.TableID;

                tableInfor.Row = rowSend;
                tableInfor.Col = colSend;
                ListTableChange.Add(tableInfor);

                UCTable temp = objSend.Parent.Controls[0] as UCTable;
                objSend.Parent.Controls.RemoveAt(0);
                objReceive.Controls.Add(temp);
            }
        }




        #endregion


        #region Event

        private void BtnImage_Click(object sender, EventArgs e)
        {

        }

        private void btnNewTable_Click(object sender, EventArgs e)
        {

        }

        private void btnDelTable_Click(object sender, EventArgs e)
        {

        }

        private void btnNewCat_Click(object sender, EventArgs e)
        {

        }

        private void btnDelCat_Click(object sender, EventArgs e)
        {

        }

        private void btnSetCat_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }


    }
}
