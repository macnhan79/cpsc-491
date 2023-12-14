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

namespace PhoMac.Main.POS.Views
{
    public partial class Pos_TicketView : UserControl
    {
        public Pos_TicketView()
        {
            InitializeComponent();
        }

        private void Pos_TicketView_Load(object sender, EventArgs e)
        {
            //ucPaymentType2.init1();

            if (!DesignMode)
            {
                //Dao dao = new Dao();
                //ICollection<Product> list = dao.GetAll<Product>();
                //int maxRow = list.Max(x => x.Row) ?? 0;
                //ucGridPanel1.NumberOfRows = maxRow;
                //int maxCol = list.Max(x => x.Col) ?? 0;
                //ucGridPanel1.NumberOfColumns = maxCol;
                //foreach (var item in list)
                //{
                //    UCProduct panelItem = new UCProduct();
                    //panelItem.TableName = item.TableName;
                    //panelItem.FrmClick += new UCPanelTable.FrmClickHandler(HandlePanelClick);
                    //DevExpress.XtraBars.Docking2010.Views.Widget.Document newDoc = ucGridPanel1.WidgetView.AddDocument(panelItem) as DevExpress.XtraBars.Docking2010.Views.Widget.Document;
                    //newDoc.Properties.AllowMaximize = DevExpress.Utils.DefaultBoolean.False;
                    //newDoc.Properties.ShowBorders = DevExpress.Utils.DefaultBoolean.False;
                    //newDoc.RowIndex = item.Row ?? 0;
                    //newDoc.ColumnIndex = item.Col ?? 0;

                //}
            }
        }




    }
}
