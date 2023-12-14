using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model;

namespace PhoMac.Business.Presenter
{
    public class PhoHa7_ProcSaleItemPresenter
    {
        PhoHa7_ProcSaleItem dic;
        public List<PhoHa7_ProcSaleItemPresenter> ListKitchenSaleItem;
        public PhoHa7_ProcSaleItem KitchenSaleItem
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

                this.SaleItemID = dic.SaleItemID;
                this.TicketID = Convert.ToInt32(dic.TicketID);
                this.ProductID = Convert.ToInt32(dic.ProductID);
                this.TicketNum = Convert.ToInt32(dic.TicketNum);
                this.Description = dic.Description;
                this.Qty = Convert.ToInt32(dic.Qty);
                this.Price = Convert.ToDecimal(dic.Price);
                this.Extra = Convert.ToDecimal(dic.Extra);
                this.Employee = dic.Employee;
                this.Done = Convert.ToBoolean(dic.Done);
                this.ToKitchen = Convert.ToBoolean(dic.ToKitchen);
                this.TakeOut = Convert.ToBoolean(dic.TakeOut);
                this.NotSaleItem = Convert.ToBoolean(dic.NotSaleItem);
                this.Done1 =Convert.ToBoolean( dic.Done1);
                this.Category = Convert.ToInt32(dic.Category);
                this.Cancel = Convert.ToBoolean(dic.Cancel);
                this.IsChange = Convert.ToBoolean(dic.IsChange);
                this.BarCode = dic.BarCode;
                this.ExtraName = dic.ExtraName;
                this.ExtraPrice = dic.ExtraPrice;
                this.OptionRequire = dic.OptionRequire;
                this.IsSmall = Convert.ToBoolean(dic.IsSmall);
                this.MType = Convert.ToInt32(dic.MType);
                this.SaleItemID_Root = Convert.ToInt32(dic.SaleItemID_Root);
                this.ExtraWith = dic.ExtraWith;
                this.ExtraWithout = dic.ExtraWithout;
                this.CustomSelect = dic.CustomSelect;
            }
        }

        public PhoHa7_ProcSaleItemPresenter()
        {
            dic = new PhoHa7_ProcSaleItem();
            ListKitchenSaleItem = new List<PhoHa7_ProcSaleItemPresenter>();
        }

        public void CopyToList(List<PhoHa7_ProcSaleItem> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                PhoHa7_ProcSaleItemPresenter obj = new PhoHa7_ProcSaleItemPresenter();
                obj.KitchenSaleItem = pListDic[i];
                ListKitchenSaleItem.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.SaleItemID = SaleItemID;
            dic.TicketID = TicketID;
            dic.ProductID = ProductID;
            dic.TicketNum = TicketNum;
            dic.Description = Description;
            dic.Qty = Qty;
            dic.Price = Price;
            dic.Extra = Extra;
            dic.Employee = Employee;
            dic.Done = Done;
            dic.ToKitchen = ToKitchen;
            dic.TakeOut = TakeOut;
            dic.NotSaleItem = NotSaleItem;
            dic.Done1 = Done1;
            dic.Category = Category;
            dic.Cancel = Cancel;
            dic.IsChange = IsChange;
            dic.BarCode = BarCode;
            dic.ExtraName = ExtraName;
            dic.ExtraPrice = ExtraPrice;

            dic.OptionRequire = OptionRequire;
            dic.IsSmall = IsSmall;
            dic.MType = MType;
            dic.SaleItemID_Root = SaleItemID_Root;
            dic.ExtraWith = ExtraWith;
            dic.ExtraWithout = ExtraWithout;
            dic.CustomSelect = CustomSelect;
        }



        #region Property

        public int SaleItemID { get; set; }
        public int TicketID { get; set; }
        public int ProductID { get; set; }
        public int TicketNum { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Extra { get; set; }
        public string Employee { get; set; }
        public bool Done { get; set; }
        public bool ToKitchen { get; set; }
        public bool TakeOut { get; set; }
        public bool NotSaleItem { get; set; }
        public bool Done1 { get; set; }
        public int Category { get; set; }
        public bool Cancel { get; set; }
        public bool IsChange { get; set; }
        public string BarCode { get; set; }
        public string ExtraName { get; set; }
        public string ExtraPrice { get; set; }
        public string OptionRequire { get; set; }
        public bool IsSmall { get; set; }
        public int MType { get; set; }
        public int SaleItemID_Root { get; set; }
        public string ExtraWith { get; set; }
        public string ExtraWithout { get; set; }
        public string CustomSelect { get; set; }

        #endregion


        public string customDisplayKitchenName()
        {
            string kitchenName = Description;
            string optionRequire = OptionRequire;
            string extraName = ExtraName;
            bool isSmall = Convert.ToBoolean(IsSmall);
            int mType = Convert.ToInt32(MType);
            string extraWith = ExtraWith;
            string extraWithout = ExtraWithout;
            string customSelect = CustomSelect;
            string name = "";
            //custom select
            if (customSelect != "")
            {
                string[] arrCustomSelect = customSelect.ToString().Split('|');
                for (int i = 0; i < arrCustomSelect.Length; i++)
                {
                    name = name + " " + arrCustomSelect[i];
                }
            }
            if (extraName != "")
            {
                name += "(";
                string[] arrExtraName = extraName.ToString().Split('|');
                for (int i = 0; i < arrExtraName.Length; i++)
                {
                    name += arrExtraName[i];
                    if (i == (arrExtraName.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            //extra with
            if (extraWith != "")
            {
                name += "(";
                string[] arrExtraWith = extraWith.ToString().Split('|');
                for (int i = 0; i < arrExtraWith.Length; i++)
                {
                    name += arrExtraWith[i];
                    if (i == (arrExtraWith.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            //extra without
            if (extraWithout != "")
            {
                name += "(";
                string[] arrExtraWithout = extraWithout.ToString().Split('|');
                for (int i = 0; i < arrExtraWithout.Length; i++)
                {
                    name += arrExtraWithout[i];
                    if (i == (arrExtraWithout.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            if (optionRequire != "")
            {
                name += "(" + optionRequire + string.Empty + ")";
            }
            if (mType == 4)
            {
                if (isSmall)
                {
                    name += "(Nhỏ)";
                }
                else
                {
                    name += "(Lớn)";
                }
            }

            return kitchenName + " " + name;
        }

    }
}
