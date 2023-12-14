using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManagerOrder
/// </summary>
public class ManagerOrder
{
    public int TableID { get; set; }
    public bool TableTakeOut { get; set; }
    public int TicketID { get; set; }
    public DataTable ListItems { get; set; }
    public List<DataTable> ListItemsHistory { get; set; }
    public int PositionUndo = 0;
    public DataTable ListItemsDel { get; set; }
    public DataTable ItemModify { get; set; }
    //public PhoHa7.Library.Enum.EnumFormStatus ItemStatus { get; set; }
    public DataTable ListExtraWith { get; set; }
    public DataTable ListExtraWithout { get; set; }
    public DataTable ListCustomSelect { get; set; }
    public int SelectedSaleItemID = -1;


    decimal subTotal;
    decimal totalAmount;
    public decimal SubTotal
    {
        get
        {
            return decimal.Round(subTotal, 2, MidpointRounding.AwayFromZero);
        }
        set { subTotal = value; }
    }
    public decimal TotalAmount{
        get
        {
            return decimal.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
        }
        set { totalAmount = value; }
    }

    public ManagerOrder()
    {

    }

    public ManagerOrder(int tableID)
    {
        this.TableID = tableID;
    }

    public ManagerOrder(int tableID, DataTable dt)
    {
        this.ListItems = dt;
        this.TableID = tableID;
        ListItemsHistory = new List<DataTable>();
        PositionUndo = -1;
    }

    public void addUndoListItems(DataTable dt)
    {
        if (ListItemsHistory == null)
        {
            ListItemsHistory = new List<DataTable>();
        }
        if (ListItemsHistory.Count < 5)
        {
            ListItemsHistory.Add(dt);
            PositionUndo = ListItemsHistory.Count - 1;
        }
        else
        {
            ListItemsHistory.Add(dt);
            ListItemsHistory.RemoveAt(0);
            PositionUndo = ListItemsHistory.Count - 1;
        }
    }

    //public DataRow getRowBySaleItemID()
    //{
    //    DataRow dr = null;
    //    for (int i = 0; i < ListItems.Rows.Count; i++)
    //    {
    //        if (Convert.ToInt32(ListItems.Rows[i]["SaleItemID"]) == SaleItem)
    //        {
    //            dr = ListItems.Rows[i];
    //            break;
    //        }
    //    }
    //    return dr;
    //}

}