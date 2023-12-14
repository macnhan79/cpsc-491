using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using PhoMac.Model;
using PhoMac.Model.Presenter.Sys;
using PhoHa7.Library.Enum;
using PhoMac.Model.Presenter.Permission;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;

public partial class TicketViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        listTicket.DataSource = loadTicket();
        listTicket.DataBind();
    }


    DataTable loadTicket()
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from  dbo.PhoHa7_ProcTickets p where (select COUNT(*) from dbo.PhoHa7_ProcSaleItem s where s.TicketID=p.TicketID and Done=0)>0 order by ticketid asc";
        DataTable count = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
        return count;
    }

    DataTable loadTableFromDatabase(int ticketID)
    {
        DataTable dt = createTable();
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from PhoHa7_ProcSaleItem p left join Products s on p.productid=s.productid where TicketID = @TicketID and done = 0";
        DataTable dtDatabase = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
        for (int i = 0; i < dtDatabase.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ProductName"] = dtDatabase.Rows[i]["Description"].ToString();
            dr["Qty"] = Convert.ToInt32(dtDatabase.Rows[i]["Qty"]);
            dr["BarCode"] = dtDatabase.Rows[i]["BarCode"].ToString();
            dr["ProductID"] = Convert.ToInt32(dtDatabase.Rows[i]["ProductID"]);
            dr["KitchenName"] = dtDatabase.Rows[i]["KitchenName"].ToString();
            //dr["CategoryID"] = dtDatabase.Rows[i]["CategoryID"];
            dr["Extra"] = 0;
            //check small size
            double lPrice = Convert.ToDouble(dtDatabase.Rows[i]["Price"]);
            //double sPrice = Convert.ToDouble(dtDatabase.Rows[i]["CPrice"]);
            dr["LPrice"] = lPrice;
            //dr["SPrice"] = sPrice;
            //dr["SmallSize"] = dtDatabase.Rows[i]["SmallSize"];


            dr["TakeOut"] = Convert.ToBoolean(dtDatabase.Rows[i]["TakeOut"]);
            //dr["EmployeeName"] = dtDatabase.Rows[i]["EmployeeName"].ToString();
            dr["SaleItemID"] = Convert.ToInt32(dtDatabase.Rows[i]["SaleItemID"]);
            dr["MType"] = Convert.ToInt32(dtDatabase.Rows[i]["MType"]);
            //
            dr["ExtraName"] = dtDatabase.Rows[i]["ExtraName"].ToString();
            dr["ExtraPrice"] = dtDatabase.Rows[i]["ExtraPrice"].ToString();
            dr["OptionRequire"] = dtDatabase.Rows[i]["OptionRequire"].ToString();
            dr["ExtraWith"] = dtDatabase.Rows[i]["ExtraWith"] + string.Empty;
            dr["ExtraWithout"] = dtDatabase.Rows[i]["ExtraWithout"] + string.Empty;
            dr["CustomSelect"] = dtDatabase.Rows[i]["CustomSelect"] + string.Empty;
            dr["Cancel"] = dtDatabase.Rows[i]["Cancel"] + string.Empty;
            //language select
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;
            if (languageFlag == 0)
            {
                //vietnamese
                itemName = dr["KitchenName"] + string.Empty;
            }
            else
            {
                //english
                itemName = dr["ProductName"] + string.Empty;
            }
            dr["CustomKitchenName"] = customDisplayKitchenName(itemName,
                                                                dtDatabase.Rows[i]["OptionRequire"] + string.Empty,
                                                                dtDatabase.Rows[i]["ExtraName"] + string.Empty,
                                                                Convert.ToBoolean(dtDatabase.Rows[i]["IsSmall"] + string.Empty),
                                                                Convert.ToInt32(dtDatabase.Rows[i]["MType"] + string.Empty),
                                                                dtDatabase.Rows[i]["ExtraWith"] + string.Empty,
                                                                dtDatabase.Rows[i]["ExtraWithout"] + string.Empty,
                                                                dtDatabase.Rows[i]["CustomSelect"] + string.Empty);
            dr["Status"] = EnumFormStatus.View;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        //ManagerOrder obj = new ManagerOrder();
        //obj.TableID = tableID;
        //obj.TicketID = Convert.ToInt32(dtDatabase.Rows[0]["TicketID"]);
        //obj.ListItems = dt;
        //Session[tableID.ToString()] = obj;
        return dt;
    }


    private void BindGrid(ASPxGridView GridView, int ticketID)
    {
        GridView.DataSource = loadTableFromDatabase(ticketID);
        GridView.DataBind();
    }

    //Method from the data access class
    DataTable getTicketDetails(int ticketID)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from PhoHa7_ProcSaleItem where TicketID = @Ticket and done = 0";
        DataTable count = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@Ticket" }, new object[] { ticketID });
        return count;
    }

    protected void listTicket_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ASPxGridView gridResponses = (ASPxGridView)e.Item.FindControl("gridResponses");
        BindGrid(gridResponses, (int)listTicket.DataKeys[e.Item.ItemIndex]);
    }




    private DataTable createTable()
    {
        System.Data.DataTable dt;
        dt = new System.Data.DataTable();
        dt.Columns.Add("SaleItemID", typeof(int));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Qty", typeof(int));
        dt.Columns.Add("BarCode", typeof(string));
        dt.Columns.Add("ProductID", typeof(int));
        dt.Columns.Add("KitchenName", typeof(string));
        dt.Columns.Add("CategoryID", typeof(int));
        dt.Columns.Add("LPrice", typeof(double));
        dt.Columns.Add("SPrice", typeof(double));
        dt.Columns.Add("MType", typeof(int));
        dt.Columns.Add("Extra", typeof(double));
        dt.Columns.Add("SmallSize", typeof(bool));
        dt.Columns.Add("TakeOut", typeof(bool));
        dt.Columns.Add("Cancel", typeof(bool));
        dt.Columns.Add("EmployeeName", typeof(string));
        dt.Columns.Add("ExtraName", typeof(string));
        dt.Columns.Add("ExtraPrice", typeof(string));
        dt.Columns.Add("OptionRequire", typeof(string));
        dt.Columns.Add("OptionRequireVNese", typeof(string));
        dt.Columns.Add("ExtraWith", typeof(string));
        dt.Columns.Add("ExtraWithout", typeof(string));
        dt.Columns.Add("CustomSelect", typeof(string));
        dt.Columns.Add("Status", typeof(EnumFormStatus));
        dt.Columns.Add("CustomKitchenName", typeof(string));
        dt.Columns.Add("TotalPrice", typeof(double));
        dt.PrimaryKey = new DataColumn[] { dt.Columns["SaleItemID"] };
        dt.Columns["SaleItemID"].AutoIncrement = true;
        dt.Columns["SaleItemID"].AutoIncrementStep = 1;
        dt.Columns["SaleItemID"].AutoIncrementSeed = 1;
        return dt;
    }


    //string customDisplayKitchenName(string kitchenName, string optionRequire, string extraName, string extraWith, string extraWithout, string customSelect)
    //{
    //    string itemName = string.Empty;
    //    Employee emp = (Employee)Session["LoginEmp"];
    //    int languageFlag = emp.Language ?? 0;
    //    Dao dao = new Dao();
    //    string name = "";
    //    //custom select
    //    if (customSelect != "")
    //    {
    //        string[] arrCustomSelect = customSelect.ToString().Split('|');
    //        for (int i = 0; i < arrCustomSelect.Length; i++)
    //        {
    //            string customSelectName = string.Empty;
    //            if (languageFlag == 0)
    //            {
    //                //vietnamese
    //                customSelectName = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i])).KitchenName;
    //            }
    //            else
    //            {
    //                //english
    //                customSelectName = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i])).ProductName;
    //            }
    //            name = name + " " + customSelectName;
    //        }
    //    }
    //    //extra name
    //    if (extraName != "")
    //    {
    //        name += "(";
    //        string[] arrExtraName = extraName.ToString().Split('|');
    //        for (int i = 0; i < arrExtraName.Length; i++)
    //        {
    //            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraName[i] == string.Empty ? "0" : arrExtraName[i]));
    //            string nameExtraName = string.Empty;
    //            if (pro != null)
    //            {
    //                if (languageFlag == 0)
    //                {
    //                    //vietnamese
    //                    nameExtraName = pro.KitchenName;
    //                }
    //                else
    //                {
    //                    //english
    //                    nameExtraName = pro.ProductName;
    //                }
    //            }
    //            name += nameExtraName;
    //            //name += arrExtraName[i];
    //            if (i == (arrExtraName.Length - 1))
    //            {
    //                name += ")";
    //            }
    //            else
    //            {
    //                name += ", ";
    //            }
    //        }
    //    }
    //    //extra with
    //    if (extraWith != "")
    //    {
    //        name += "(";
    //        string[] arrExtraWith = extraWith.ToString().Split('|');
    //        for (int i = 0; i < arrExtraWith.Length; i++)
    //        {
    //            string extraWithName = string.Empty;
    //            if (languageFlag == 0)
    //            {
    //                //vietnamese
    //                //extraWithName = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i])).KitchenName;
    //            }
    //            else
    //            {
    //                //english
    //                //extraWithName = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i])).ProductName;
    //            }
    //            name += extraWithName;
    //            if (i == (arrExtraWith.Length - 1))
    //            {
    //                name += ")";
    //            }
    //            else
    //            {
    //                name += ", ";
    //            }
    //        }
    //    }
    //    //extra without
    //    if (extraWithout != "")
    //    {
    //        name += "(";
    //        string[] arrExtraWithout = extraWithout.ToString().Split('|');
    //        for (int i = 0; i < arrExtraWithout.Length; i++)
    //        {
    //            string extraWithoutName = string.Empty;
    //            if (languageFlag == 0)
    //            {
    //                //vietnamese
    //                extraWithoutName = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i])).KitchenName;
    //            }
    //            else
    //            {
    //                //english
    //                extraWithoutName = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i])).ProductName;
    //            }
    //            name += extraWithoutName;
    //            if (i == (arrExtraWithout.Length - 1))
    //            {
    //                name += ")";
    //            }
    //            else
    //            {
    //                name += ", ";
    //            }
    //        }
    //    }

    //    if (optionRequire != "")
    //    {
    //        name += "(" + optionRequire + string.Empty + ")";
    //    }
    //    return kitchenName + " " + name;
    //}
    string customDisplayKitchenName(string kitchenName, string optionRequire, string extraName, bool isSmall, int mType, string extraWith, string extraWithout, string customSelect)
    {
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
        //if (e.Column.FieldName == "Description")
        //{
        //    string text = string.Format(e.DisplayText.Replace("(" + ClsPublic.SizeLarge + ")", "<color=" + ClsPublic.ForceColorLageSize.Name + ">(" + ClsPublic.SizeLarge + ")</color>"));
        //    e.DisplayText = text;
        //    //e.Handled = true;
        //}
        return kitchenName + " " + name;
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["LoginEmp"] = null;
        Session["MachineList"] = null;
        Session["MachineInfo"] = null;
        Response.Redirect("Login.aspx");
    }
    protected void gridResponses_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {
            if (e.RowType != GridViewRowType.Data) return;
            bool togo = Convert.ToBoolean(e.GetValue("TakeOut"));
            int qty = Convert.ToInt32(e.GetValue("Qty"));
            string barcode = e.GetValue("BarCode")+string.Empty;
            bool cancel = Convert.ToBoolean(e.GetValue("Cancel")+string.Empty);
            if (qty > 1)
                e.Row.ForeColor = System.Drawing.Color.Red;

            if (barcode == "45" || barcode == "45A" || barcode == "45B" || barcode == "45C" 
                || barcode == "50" || barcode == "50A"
                || barcode == "46" || barcode == "46B")
            {
                      e.Row.BackColor= System.Drawing.Color.Pink;
            }
            if (togo)
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            if(cancel)
                e.Row.Font.Strikeout= true;
            //else if (barcode == "45B")
            //{
            //}
        }
        catch (System.Exception ex)
        {
        	
        }
        

    }
    
}