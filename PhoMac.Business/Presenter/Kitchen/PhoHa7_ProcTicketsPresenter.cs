using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model;

namespace PhoMac.Business.Presenter
{
    public class PhoHa7_ProcTicketsPresenter
    {
        PhoHa7_ProcTickets dic;
        public List<PhoHa7_ProcTicketsPresenter> ListKitchenProcTickets;
        public PhoHa7_ProcTickets KitchenProcTickets
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;
                this.TicketID = dic.TicketID;
                this.CustomerName = dic.CustomerName;
                this.EmployeeName = dic.EmployeeName;
                this.TicketNum = Convert.ToInt32(dic.TicketNum);
                this.DateTimeIssue = Convert.ToDateTime(dic.DateTimeIssue);
                this.Tax = Convert.ToDecimal(dic.Tax);
                this.Total = Convert.ToDecimal(dic.Total);
                this.Discount = Convert.ToDecimal(dic.Discount);
                this.TableName = dic.TableName;
                this.DTicketNum = Convert.ToInt32(dic.DTicketNum);
                this.TakeOut = Convert.ToBoolean(dic.TakeOut);
                this.Done = Convert.ToBoolean(dic.Done);
                this.ToKitchen = Convert.ToBoolean(dic.ToKitchen);
                this.IsChange = Convert.ToBoolean(dic.IsChange);
                this.TicketID_Root = Convert.ToInt32(dic.TicketID_Root);
                this.TableID = Convert.ToInt32(dic.TableID);
                this.Emergency = Convert.ToBoolean(dic.Emergency);
                //this.ListProduct = dic.ListProduct;
                //this.CountItemDonePrint = dic.CountItemDonePrint;
                //this.CountTotalItemPrint = dic.CountTotalItemPrint;
                //this.Payment = dic.Payment;
            }
        }

        public PhoHa7_ProcTicketsPresenter()
        {
            dic = new PhoHa7_ProcTickets();
            ListKitchenProcTickets = new List<PhoHa7_ProcTicketsPresenter>();
        }

        public void CopyToList(List<PhoHa7_ProcTickets> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                PhoHa7_ProcTicketsPresenter obj = new PhoHa7_ProcTicketsPresenter();
                obj.KitchenProcTickets = pListDic[i];
                ListKitchenProcTickets.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.TicketID = TicketID;
            dic.CustomerName = CustomerName;
            dic.EmployeeName = EmployeeName;
            dic.TicketNum = TicketNum;
            dic.DateTimeIssue = DateTimeIssue;
            dic.Tax = Tax;
            dic.Total = Total;
            dic.Discount = Discount;
            dic.TableName = TableName;
            dic.DTicketNum = DTicketNum;
            dic.TakeOut = TakeOut;
            dic.Done = Done;
            dic.ToKitchen = ToKitchen;
            dic.IsChange = IsChange;
            dic.TicketID_Root = TicketID_Root;
            dic.TableID = TableID;
            dic.Emergency = Emergency;
            //dic.ListProduct = ListProduct;
            //dic.CountItemDonePrint = CountItemDonePrint;
            //dic.CountTotalItemPrint = CountTotalItemPrint;
            //dic.Payment = Payment;
        }

        

        #region Property

        public int TicketID { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public int TicketNum { get; set; }
        public System.DateTime DateTimeIssue { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public string TableName { get; set; }
        public int DTicketNum { get; set; }
        public bool TakeOut { get; set; }
        public bool Done { get; set; }
        public bool ToKitchen { get; set; }
        public bool IsChange { get; set; }
        public int TicketID_Root { get; set; }
        public int TableID { get; set; }
        public bool Emergency { get; set; }

        public List<Product> ListProduct { get; set; }
        public int CountItemDonePrint { get; set; }
        public int CountTotalItemPrint { get; set; }
        public string Payment { get; set; }
        #endregion
    }
}
