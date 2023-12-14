using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoMac.Main.POS.Views.UC;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraBars.Docking2010.Customization;
using PhoHa7.Library.Froms;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using PhoMac.Business.Presenter;

namespace PhoMac.Main.POS.Views
{
    public partial class Pos_MainView : XtraUserControlKira, ISupportNavigation
    {
        WindowsUIView generalView;
        Page detailsPage;
        public Pos_MainView()
        {
            InitializeComponent();
        }

        #region Event

        public void OnNavigatedFrom(INavigationArgs args)
        {
            args.Parameter = "Parameter";
        }

        public void OnNavigatedTo(INavigationArgs args)
        {
            generalView = args.View;
            detailsPage = args.Parameter as Page;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            generalView.Controller.Activate(detailsPage);
        }

        #endregion

        #region Method
        #endregion
         
        private void gridPanel1_Load(object sender, EventArgs e)
        {

        }

        WidgetView WidgetView;

        private void Pos_MainView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //Dao dao = new Dao();
                //ICollection<Table> list = dao.GetAll<Table>();
                //int maxRow = list.Max(x => x.Row) ?? 0;
                //gridPanel1.Rows = maxRow;
                //int maxCol = list.Max(x => x.Col) ?? 0;
                //gridPanel1.Columns = maxCol;
                //foreach (var item in list)
                //{
                //    UCTable panelItem = new UCTable();
                //    panelItem.TableName = item.TableName;
                //    panelItem.FrmClick += new UCPanelTable.FrmClickHandler(HandlePanelClick);
                //    DevExpress.XtraBars.Docking2010.Views.Widget.Document newDoc = gridPanel1.WidgetView.AddDocument(panelItem) as DevExpress.XtraBars.Docking2010.Views.Widget.Document;
                //    newDoc.Properties.AllowMaximize = DevExpress.Utils.DefaultBoolean.False;
                //    newDoc.Properties.ShowBorders = DevExpress.Utils.DefaultBoolean.False;
                //    newDoc.RowIndex = item.Row ?? 0;
                //    newDoc.ColumnIndex = item.Col ?? 0;
                //}
                ////
                Dao dao = new Dao();
                TablePresenter table = new TablePresenter();
                table.CopyToList(dao.FindByMultiColumnAnd<Table>(new[] { "CategoryID" }, 1).ToList());
            ucPanelLayoutTable1.NumberOfRows   = table.ListTables.Max(p => p.Row);
            ucPanelLayoutTable1.NumberOfColumns = table.ListTables.Max(p => p.Col);
              ucPanelLayoutTable1.  initLayout();
                for (int i = 0; i < table.ListTables.Count; i++)
                {
                    UCTable panelTable = new UCTable();
                    panelTable.TableID = table.ListTables[i].TableID;
                    ucPanelLayoutTable1.PushOject(panelTable, table.ListTables[i].Row - 1, table.ListTables[i].Col - 1);
                }
            }
        }

        public void HandlePanelClick(object sender, PhoMac.Main.POS.Views.UC.UCTable.FrmClickEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction action = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction();
            action.Caption = "Flyout Action";
            action.Description = "Flyout Action Description";
            action.Commands.Add(DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand.OK);

            FlyoutDialog.Show(this.FindForm(), action);
        }






    }
}
