using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoHa7.Library.Classes.Connection;
using DevExpress.Web.ASPxGridView;
using PhoHa7.Library.Enum;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Web.ASPxCallback;
using PhoMac.Model;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;
using DevExpress.Web.ASPxTabControl;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
public partial class Orders : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //dateEditSchedule.TimeSectionProperties.Visible = true;
            if (Request.QueryString["tableid"] != null && Request.QueryString["type"] != null)
            {
                //check exist table in tables
                int tableID = Convert.ToInt32(Request.QueryString["tableid"]);
                DataTable tableList = checkExistTable(tableID);
                if (tableList.Rows.Count == 0)
                {
                    Response.Redirect("table.aspx");
                }
                gridLookup.GridView.Width = gridLookup.Width;

                SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
                DataTable dtProduct = sqlHelper.ExecuteDataTable("select * from Products where CategoryID > 0 and Active=1", CommandType.Text, null, null);
                gridLookup.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                gridLookup.GridViewProperties.Settings.ShowFilterRow = false;
                gridLookup.GridView.DataSource = dtProduct;
                gridLookup.GridView.DataBind();
                //new table
                if (Request.QueryString["type"].Equals("new"))
                {

                    //processing order new table
                    if (!checkAvaibleTable(tableID))
                    {
                        //refesh page
                        if (!IsPostBack)
                        {
                            grid.DataSource = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
                            grid.SettingsText.Title = "Table: " + tableList.Rows[0]["TableName"] + "(New)";
                            grid.DataBind();
                            //set items text to null
                            txtItemAdd.Text = "";
                            //load take out value table
                            //SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
                            bool a = Convert.ToBoolean(tableList.Rows[0]["TakeOut"]);
                            checkTakeOutTable.Value = tableList.Rows[0]["TakeOut"].ToString();
                            if (Convert.ToBoolean(checkTakeOutTable.Value) == true)
                            {
                                checkTakeOutTable.Visible = false;
                            }
                            checkTakeOutTable.DataBind();
                            //position undo
                            //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                            //if (obj.ListItemsHistory != null)
                            //{
                            //    obj.PositionUndo = obj.ListItemsHistory.Count == 0 ? 0 : (obj.ListItemsHistory.Count - 1);
                            //}
                        }
                        else
                        {
                            grid.DataSource = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
                            grid.DataBind();
                            //position undo
                            //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                            //if (obj.ListItemsHistory != null)
                            //{
                            //    obj.PositionUndo = obj.ListItemsHistory.Count == 0 ? 0 : (obj.ListItemsHistory.Count - 1);
                            //}
                            loadCbTableChange();
                        }
                    }
                    else
                    {
                        Response.Redirect("table.aspx");
                    }
                }
                //update table
                else if (Request.QueryString["type"].Equals("modify"))
                {

                    if (!IsPostBack)
                    {
                        //get infor table
                        string sql = "select * from ProcTickets where TableID = @TableID";
                        //SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
                        DataTable dtTableInfo = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID });
                        grid.SettingsText.Title = "Table: " + tableList.Rows[0]["TableName"] + "(Update)";
                        txtCustomerName.Text = dtTableInfo.Rows[0]["CustomerName"] + string.Empty;
                        checkTakeOutTable.Value = dtTableInfo.Rows[0]["TakeOut"].ToString();
                        if (Convert.ToBoolean(checkTakeOutTable.Value) == true)
                        {
                            checkTakeOutTable.Visible = false;
                        }
                        checkTakeOutTable.DataBind();
                        //load item from database ProcSaleItem
                        //load table from ProcSaleItem
                        grid.DataSource = loadTableFromDatabase(tableID);
                        grid.DataBind();
                        //position undo
                        //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                        //if (obj.ListItemsHistory != null)
                        //{
                        //    obj.PositionUndo = obj.ListItemsHistory.Count == 0 ? 0 : (obj.ListItemsHistory.Count - 1);
                        //}
                    }
                    else
                    {
                        loadCbTableChange();
                        if (Session[Request.QueryString["tableid"]] == null)
                        {
                            grid.DataSource = loadTableFromDatabase(tableID);
                            grid.DataBind();
                        }
                        else
                        {
                            grid.DataSource = ((ManagerOrder)Session[Request.QueryString["tableid"]]).ListItems;
                            grid.DataBind();
                        }
                        //position undo
                        //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                        //if (obj.ListItemsHistory != null)
                        //{
                        //    obj.PositionUndo = obj.ListItemsHistory.Count == 0 ? 0 : (obj.ListItemsHistory.Count - 1);
                        //}
                    }

                }
                else
                {
                    Response.Redirect("table.aspx");
                }
            }
            else
            {
                Response.Redirect("table.aspx");
            }
        }
        catch (System.Exception ex)
        {
            Response.Redirect("table.aspx");
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Dao dao = new Dao();
        var list = dao.GetAll<Category>().Where(p => p.WebActive == true).OrderBy(x => x.WebOrderBy).ToList();
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                TabPage tab = new TabPage();
                tab.Name = list[i].CategoryID + string.Empty;
                tab.Text = list[i].WebName + string.Empty;
                tab.TabStyle.Width = 10;

                UserControl_ItemList obj = LoadControl("~/UserControl/ItemList.ascx") as UserControl_ItemList;
                obj.CategoryId = list[i].CategoryID;
                obj.MType = list[i].WebShowType;
                //obj.DataSource = tableList;
                tab.Controls.Add(obj);
                ASPxPageControl1.TabPages.Add(tab);
            }

        }

    }

    #endregion

    #region Button Event

    protected void callBackToKitchen_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int count = 0;
        string msg = "";
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        System.Data.DataTable dt = obj.ListItems;
        int tableID = Convert.ToInt32(cbTableChange.Value);
        Employee emp = (Employee)Session["LoginEmp"];
        //table change status
        EnumFormStatus status;
        if (cbTableChange.SelectedItem != null)
        {
            if (cbTableChange.SelectedItem.Text.Split(';')[1].Trim() == "Update")
                status = EnumFormStatus.Modify;
            else
            {
                if (Request.QueryString["type"].Equals("modify"))
                    status = EnumFormStatus.Modify;
                else
                    status = EnumFormStatus.Add;
            }
        }
        else
        {
            if (Request.QueryString["type"].Equals("new"))

                status = EnumFormStatus.Add;
            else
                status = EnumFormStatus.Modify;
        }


        bool isTakeOutTable = false;

        if (dt.Rows.Count > 0)
        {
            obj.TableTakeOut = Convert.ToBoolean(checkTakeOutTable.Value);

            ProcTickets p = new ProcTickets();
            if (status == EnumFormStatus.Add)
            {
                //insert tickets

                //is change table
                if (tableID > 0)
                {
                    DataTable tableList = checkExistTable(tableID);
                    isTakeOutTable = Convert.ToBoolean(tableList.Rows[0]["TakeOut"]);
                    bool isEmergency = Convert.ToBoolean(hiddenEmergency.Value);
                    count = p.insertProcTicket(dt, null, tableID, txtCustomerName.Text, 0, emp.EmployeeID, emp.FullName + string.Empty, isTakeOutTable, status, 0, isEmergency);
                }
                //not change table
                else
                {
                    bool isEmergency = Convert.ToBoolean(hiddenEmergency.Value);
                    count = p.insertProcTicket(dt, null, obj.TableID, txtCustomerName.Text, 0, emp.EmployeeID, emp.FullName + string.Empty, obj.TableTakeOut, EnumFormStatus.Add, 0, isEmergency);
                }
            }
            else if (status == EnumFormStatus.Modify)
            {
                SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
                string sql;
                bool allowUpdate = true;
                if (obj.ListItemsDel != null)
                {
                    //check item is done in kitchen--if it is done--> do not allow update
                    for (int i = 0; i < obj.ListItemsDel.Rows.Count; i++)
                    {
                        sql = "select * from PhoHa7_ProcSaleItem where SaleItemID_Root = @SaleItemID";
                        int saleItemID = Convert.ToInt32(obj.ListItemsDel.Rows[i]["SaleItemID"]);
                        DataTable countSaleItemDel = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                        bool done = true;
                        if (countSaleItemDel.Rows.Count > 0)
                        {
                            done = (bool)countSaleItemDel.Rows[0]["Done"];
                            if (done)
                            {
                                allowUpdate = false;
                                string name = countSaleItemDel.Rows[0]["Description"] + string.Empty;
                                msg += "\n" + name;
                            }
                        }
                        else
                        {
                            allowUpdate = false;
                            string name = obj.ListItems.Rows[i]["CustomKitchenName"] + string.Empty;
                            msg += "\n" + name;
                        }
                    }
                }
                //get ticket id by tableID
                for (int i = 0; i < obj.ListItems.Rows.Count; i++)
                {
                    if (EnumFormStatus.Modify == ((EnumFormStatus)obj.ListItems.Rows[i]["Status"]))
                    {
                        //check item is done or not. If is done --> do not allow update
                        sql = "select * from PhoHa7_ProcSaleItem where SaleItemID_Root = @SaleItemID";
                        int saleItemID = Convert.ToInt32(obj.ListItems.Rows[i]["SaleItemID"]);
                        DataTable countSaleItemDel = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                        bool done = true;
                        if (countSaleItemDel.Rows.Count > 0)
                        {
                            done = (bool)countSaleItemDel.Rows[0]["Done"];
                            if (done)
                            {
                                allowUpdate = false;
                                string name = countSaleItemDel.Rows[0]["Description"] + string.Empty;
                                msg += "\n" + name;
                            }
                        }
                        else
                        {
                            allowUpdate = false;
                            string name = obj.ListItems.Rows[i]["CustomKitchenName"] + string.Empty;
                            msg += "\n" + name;
                        }
                    }

                }
                if (tableID > 0)
                {
                    ////change table: update table id
                    //if (status == EnumFormStatus.Add)
                    //{

                    //}
                    ////combine table: change ticketid in procsaleitem,phoha7_procsaleitem || delete table in proctickets, phoha7_proctickets
                    //else
                    //{

                    //}
                    DataTable tableList = checkExistTable(tableID);
                    isTakeOutTable = Convert.ToBoolean(tableList.Rows[0]["TakeOut"]);
                    bool isEmergency = Convert.ToBoolean(hiddenEmergency.Value);
                    if (allowUpdate)
                    {
                        //sql = "select top 1 t.TicketID from ProcSaleItem s left join ProcTickets t on s.TicketID = t.ticketID where t.TableID = @TableID and TicketID != @TicketID";
                        //int oldTicketID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID, obj.TicketID }));
                        //modify tickets
                        if (obj.TicketID == 0)
                        {
                            sql = "select TicketID from ProcTickets where TableID = @TableID";
                            obj.TicketID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID }));
                        }
                        count = p.insertProcTicket(dt, obj.ListItemsDel, tableID, txtCustomerName.Text, 0, emp.EmployeeID, emp.FullName + string.Empty, isTakeOutTable, EnumFormStatus.Modify, obj.TicketID, isEmergency);
                    }
                    else
                    {
                        callBackToKitchen.JSProperties["cpCompleteMsg"] = "Một số món đã hoàn thành. Không thể update." + msg;
                        //Response.Write("<script>alert('Một số món đã hoàn thành. Không thể update.')</script>");
                    }
                }
                else
                {
                    //if (allowUpdate)
                    //{
                    sql = "select top 1 t.TicketID from ProcSaleItem s left join ProcTickets t on s.TicketID = t.ticketID where t.TableID = @TableID";
                    int oldTicketID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { Request.QueryString["tableid"] }));
                    //modify tickets
                    bool isEmergency = Convert.ToBoolean(hiddenEmergency.Value);
                    count = p.insertProcTicket(dt, obj.ListItemsDel, obj.TableID, txtCustomerName.Text, 0, emp.EmployeeID, emp.FullName + string.Empty, obj.TableTakeOut, EnumFormStatus.Modify, oldTicketID, isEmergency);
                    //}
                    //else
                    //{
                    callBackToKitchen.JSProperties["cpCompleteMsg"] = "Một số món đã hoàn thành. Không thể update." + msg;
                    //Response.Write("<script>alert('Một số món đã hoàn thành. Không thể update.')</script>");
                    //}
                }


            }
            //    //return to table page
            if (count > 0)
            {
                Session[Request.QueryString["tableid"]] = null;
                callBackToKitchen.JSProperties["cpCompleteMsg"] = "Done";
                //Response.Redirect("table.aspx");
            }
        }
    }

    protected void callBackToKitchen_Init(object sender, EventArgs e)
    {
        ASPxCallback calBack = sender as ASPxCallback;
        calBack.JSProperties["cpCompleteMsg"] = "";
    }

    protected void listExtraName_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        DataRow dr = obj.ItemModify != null ? obj.ItemModify.Rows[0] : null;
        if (dr != null)
        {
            //extra name
            if (Convert.ToDouble(dr["Extra"]) != 0)
            {
                string[] arrExtraName = dr["ExtraName"].ToString().Split('|');
                string[] arrExtraPrice = dr["ExtraPrice"].ToString().Split('|');
                listExtraName.Items.Clear();
                for (int i = 0; i < arrExtraName.Length; i++)
                {
                    //listExtraName.Items.Add(new ListItem(arrExtraName[i], arrExtraPrice[i]));
                    listExtraName.Items.Add(arrExtraName[i], arrExtraPrice[i]);
                }
                listExtraName.DataBind();
            }
        }
    }

    protected void ASPxCallbackPayment_Init(object sender, EventArgs e)
    {
        ASPxCallback calBack = sender as ASPxCallback;
        calBack.JSProperties["cplblSubTotal"] = "";
        calBack.JSProperties["cplblTax"] = "";
        calBack.JSProperties["cplblTotal"] = "";
        
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (obj != null && obj.ListItems != null)
        {
            System.Data.DataTable dt = obj.ListItems;
            //dt.Columns.Add("TotalPrice");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            //    var LPrice = Convert.ToDecimal(dt.Rows[i]["LPrice"] ?? 0m);
            //    decimal ExtraPrice = 0;
            //    try
            //    {
            //        ExtraPrice = Convert.ToDecimal(dt.Rows[i]["ExtraPrice"] ?? 0m);
            //    }
            //    catch (System.Exception ex)
            //    {

            //    }

            //    var Qty = Convert.ToDecimal(dt.Rows[i]["Qty"] ?? 0m);
            //    var total = (LPrice + ExtraPrice) * Qty;
            //    dt.Rows[i]["TotalPrice"] = total;
            //}
            gridReviewOrder.DataSource = dt;
            gridReviewOrder.DataBind();
        }
    }

    protected void callBackPayButton_Init(object sender, EventArgs e)
    {
        ASPxCallback calBack = sender as ASPxCallback;
        calBack.JSProperties["cpOK"] = "";
    }

    protected void callBackPayButton_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {

        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (obj != null && obj.ListItems != null)
        {
            callBackPayButton.JSProperties["cpOK"] = "OK";
            callBackPayButton.JSProperties["cpTicketID"] = obj.TicketID;
            var selections = gridReviewOrder.GetSelectedFieldValues("SaleItemID");
            System.Data.DataTable dt = obj.ListItems;
            bool isNewRow = false;
            //dt.Columns.Add("TotalPrice");
            HashSet<decimal> discountPercent = new HashSet<decimal>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ///handle discount
                if (dt.Rows[i]["BarCode"].ToString().Contains("%"))
                {
                    discountPercent.Add(Convert.ToDecimal(dt.Rows[i]["LPrice"]));
                    dt.Rows[i]["TotalPrice"] = 0;
                    continue;
                }
                string[] extraPrice = dt.Rows[i]["ExtraPrice"].ToString().Split('|');
                var LPrice = Convert.ToDecimal(dt.Rows[i]["LPrice"] ?? 0m);
                decimal ExtraPrice = 0;
                foreach (var item in extraPrice)
                {
                    try
                    {
                        ExtraPrice += Convert.ToDecimal(item);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }

                var Qty = Convert.ToDecimal(dt.Rows[i]["Qty"] ?? 0m);
                var total = (LPrice + ExtraPrice) * Qty;
                dt.Rows[i]["TotalPrice"] = total;

                //check new row --> prevent check out
                if (dt.Rows[i]["Status"].ToString() == "2")
                {
                    isNewRow = true;
                }

            }


            decimal subtotal = 0;
            decimal discount = 0;

            //handle split bill
            if (selections.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string a = selections.ToString();
                    if (selections.Contains(dt.Rows[i]["SaleItemID"]))
                    {
                        callBackPayButton.JSProperties["cpTicketID"] = callBackPayButton.JSProperties["cpTicketID"] + "|" + dt.Rows[i]["SaleItemID"];
                        subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    callBackPayButton.JSProperties["cpTicketID"] = callBackPayButton.JSProperties["cpTicketID"] + "|" + dt.Rows[i]["SaleItemID"];
                    subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                }
            }




            //calc discount 
            foreach (var item in discountPercent)
            {
                discount += subtotal * (item / 100);
                subtotal = subtotal - subtotal * (item / 100);
            }
            decimal tax = subtotal * (decimal)0.0875;
            callBackPayButton.JSProperties["cplblSubTotal"] = subtotal.ToString("c2");
            if (isNewRow)
            {
                callBackPayButton.JSProperties["cpOK"] = "";
            }
        }
    }

    protected void ASPxCallbackPayment_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        var selections = gridReviewOrder.GetSelectedFieldValues("SaleItemID");
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (obj != null && obj.ListItems != null)
        {
            System.Data.DataTable dt = obj.ListItems;
            //dt.Columns.Add("TotalPrice");
            HashSet<decimal> discountPercent = new HashSet<decimal>();
            bool hasTogo = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ///handle discount
                if (dt.Rows[i]["BarCode"].ToString().Contains("%"))
                {
                    discountPercent.Add(Convert.ToDecimal(dt.Rows[i]["LPrice"]));
                    dt.Rows[i]["TotalPrice"] = 0;
                    continue;
                }
                string[] extraPrice = dt.Rows[i]["ExtraPrice"].ToString().Split('|');
                var LPrice = Convert.ToDecimal(dt.Rows[i]["LPrice"] ?? 0m);
                decimal ExtraPrice = 0;
                foreach (var item in extraPrice)
                {
                    try
                    {
                        ExtraPrice += Convert.ToDecimal(item);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }

                var Qty = Convert.ToDecimal(dt.Rows[i]["Qty"] ?? 0m);
                var total = (LPrice + ExtraPrice) * Qty;
                dt.Rows[i]["TotalPrice"] = total;

                //check has items togo
                if (!obj.TableTakeOut && Convert.ToBoolean(dt.Rows[i]["TakeOut"]))
                {
                    hasTogo = true;
                }
            }
            decimal subtotal = 0;
            decimal discount = 0;

            //handle split bill
            if (selections.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string a = selections.ToString();
                    if (selections.Contains(dt.Rows[i]["SaleItemID"]))
                    {
                        subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                }
            }

            gridReviewOrder.DataSource = dt;
            gridReviewOrder.DataBind();


            //calc discount 
            foreach (var item in discountPercent)
            {
                discount += subtotal * (item / 100);
                subtotal = subtotal - subtotal * (item / 100);
            }
            decimal tax = subtotal * (decimal)0.0875;
            ASPxCallbackPayment.JSProperties["cplblDiscount"] = string.Format("{0:C}", discount);
            ASPxCallbackPayment.JSProperties["cplblSubTotal"] = string.Format("{0:C}", subtotal);
            ASPxCallbackPayment.JSProperties["cplblTax"] = string.Format("{0:C}", tax);
            ASPxCallbackPayment.JSProperties["cplblTotal"] = string.Format("{0:C}", (subtotal + tax));
            if (hasTogo)
            {
                ASPxCallbackPayment.JSProperties["cpTakeOutAlert"] = "Yes";
            }


            obj.SubTotal = subtotal;
            obj.TotalAmount = subtotal + tax;
            Session[Request.QueryString["tableid"]] = obj;
            //ASPxCallbackPayment.JSProperties["cpTicketID"] = obj.TicketID;
        }
    }

    #endregion

    #region Grid Event
    //DETAILS ITEMS
    protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btnDetails")
        {
            int rowHandle = e.VisibleIndex;
            int saleItemID = Convert.ToInt32(grid.GetRowValues(rowHandle, "SaleItemID"));
            selectItemGrid(saleItemID);
            grid.JSProperties["cpShowPopup"] = "1";
        }
        else if (e.ButtonID == "btnDel")
        {
            DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
            int saleItemID = Convert.ToInt32(grid.GetRowValues(e.VisibleIndex, "SaleItemID"));
            //get list items delete
            DataTable listItemDel = getListDelItem();
            //get delete item
            DataRow dr = dt.Rows.Find(saleItemID);
            //new row
            DataRow temp = listItemDel.NewRow();
            temp["SaleItemID"] = dr["SaleItemID"];
            temp["ProductName"] = dr["ProductName"];
            EnumFormStatus status = (EnumFormStatus)dr["Status"];
            //add to delete list datatable
            if (status != EnumFormStatus.Add)
            {
                listItemDel.Rows.Add(temp);
                listItemDel.AcceptChanges();
            }

            //remove 
            dt.Rows.Remove(dr);

            int sandwich = 0;
            int drink = 0;
            int sandWichDiscount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["BarCode"].ToString() == "63" || dt.Rows[i]["BarCode"].ToString() == "63A" || dt.Rows[i]["BarCode"].ToString() == "63B" || dt.Rows[i]["BarCode"].ToString() == "63C")
                {
                    sandwich++;
                }
                if (dt.Rows[i]["BarCode"].ToString() == "A20" || dt.Rows[i]["BarCode"].ToString() == "A8" || dt.Rows[i]["BarCode"].ToString() == "A20A")
                {
                    drink++;
                }
                if (dt.Rows[i]["ProductName"].ToString() == "Sandwich Discount")
                {
                    sandWichDiscount++;
                }
            }
            int min = Math.Min(sandwich, drink);
            if (min != sandWichDiscount)
            {
                for (int i = 0; i < Math.Abs(min - sandWichDiscount); i++)
                {
                    for (int j = 0; i < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["ProductName"].ToString() == "Sandwich Discount")
                        {
                            dt.Rows.Remove(dt.Rows[j]);
                            break;
                        }
                    }
                }
            }



            dt.AcceptChanges();
            grid.DataSource = dt;
            ManagerOrder obj = (ManagerOrder)(Session[Request.QueryString["tableid"]]);
            obj.ListItems = dt;
            obj.ListItemsDel = listItemDel;
            Session[Request.QueryString["tableid"]] = obj;
            grid.Selection.UnselectAll();
            grid.DataBind();
        }
    }

    private void selectItemGrid(int saleItemID)
    {

        // hiddenSaleItemIDModify.Value = saleItemID.ToString();
        // hiddenSaleItemIDModify.DataBind();
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];

        DataTable itemList = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
        //find item in list
        DataRow dr = itemList.Rows.Find(saleItemID);
        //create item modify
        DataTable itemModify = createTable();
        DataRow drModify = itemModify.NewRow();
        drModify["SaleItemID"] = dr["SaleItemID"];
        drModify["ProductName"] = dr["ProductName"];
        drModify["BarCode"] = dr["BarCode"];
        drModify["ProductID"] = dr["ProductID"];
        drModify["CategoryID"] = dr["CategoryID"];
        drModify["LPrice"] = dr["LPrice"];
        drModify["SPrice"] = dr["SPrice"];
        drModify["MType"] = dr["MType"];
        drModify["KitchenName"] = dr["KitchenName"];
        drModify["OptionRequire"] = dr["OptionRequire"];
        drModify["Qty"] = dr["Qty"];
        drModify["TakeOut"] = dr["TakeOut"];
        drModify["SmallSize"] = dr["SmallSize"];
        drModify["Extra"] = dr["Extra"];
        drModify["ExtraName"] = dr["ExtraName"];
        drModify["ExtraPrice"] = dr["ExtraPrice"];
        drModify["EmployeeName"] = dr["EmployeeName"];
        drModify["ExtraWith"] = dr["ExtraWith"];
        drModify["ExtraWithout"] = dr["ExtraWithout"];
        drModify["CustomSelect"] = dr["CustomSelect"];
        drModify["Status"] = dr["Status"];
        drModify["CustomKitchenName"] = dr["CustomKitchenName"];

        itemModify.Rows.Add(drModify);
        itemModify.AcceptChanges();
        managerOrder.ItemModify = itemModify;
        managerOrder.ListExtraWith = getAllProductsExtraWith(Convert.ToInt32(dr["ProductID"].ToString()));
        managerOrder.ListExtraWithout = getAllProductsExtraWithout(Convert.ToInt32(dr["ProductID"].ToString()));
        managerOrder.ListCustomSelect = getAllProductsCustomName(Convert.ToInt32(dr["ProductID"].ToString()));
        Session[Request.QueryString["tableid"]] = managerOrder;
    }



    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string name = e.Parameters;
        //add extra item
        if (name == "phomacextra")
        {
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            //add undo
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);
            //
            DataTable itemModify = obj.ItemModify;
            DataRow currentItem = obj.ListItems.Rows.Find(itemModify.Rows[0]["SaleItemID"]);

            //check small size change
            if (Convert.ToBoolean(checkedSmallSize.Value) != Convert.ToBoolean(currentItem["SmallSize"]))
            {
                currentItem["SmallSize"] = Convert.ToBoolean(checkedSmallSize.Value);
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //check takeout item
            if (Convert.ToBoolean(checkedTakeOut.Value) != Convert.ToBoolean(currentItem["TakeOut"]))
            {
                currentItem["TakeOut"] = Convert.ToBoolean(checkedTakeOut.Value);
                if (Convert.ToBoolean(currentItem["TakeOut"]))
                    currentItem["SmallSize"] = false;
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //check quality change
            if (Convert.ToInt32(dropListQty.Value) != Convert.ToInt32(currentItem["Qty"]))
            {
                currentItem["Qty"] = Convert.ToInt32(dropListQty.Value);
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //check extra item
            string extraName = itemModify.Rows[0]["ExtraName"] + string.Empty;
            string extraCurrentName = currentItem["ExtraName"] + string.Empty;
            if (extraName != extraCurrentName)
            {
                currentItem["Extra"] = itemModify.Rows[0]["Extra"];
                currentItem["ExtraName"] = itemModify.Rows[0]["ExtraName"];
                currentItem["ExtraPrice"] = itemModify.Rows[0]["ExtraPrice"];
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //without option
            string nameWithout = hiddencbListWithout.Value;
            string currentNameWithout = itemModify.Rows[0]["ExtraWithout"] + string.Empty;
            //different name
            if (nameWithout != currentNameWithout)
            {
                currentItem["ExtraWithout"] = nameWithout;
                hiddencbListWithout.Value = "";
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }

            //name with
            string nameWith = hiddencbListWith.Value;
            string currentNameWith = itemModify.Rows[0]["ExtraWith"] + string.Empty;
            if (nameWith != currentNameWith)
            {
                currentItem["ExtraWith"] = nameWith;
                hiddencbListWith.Value = "";
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //name custom select
            string customSelect = hiddencbListCustomName.Value;
            string currentCustomSelect = itemModify.Rows[0]["CustomSelect"] + string.Empty;
            if (customSelect != currentCustomSelect)
            {
                currentItem["CustomSelect"] = customSelect;
                hiddencbListCustomName.Value = "";
                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }

            //check of option change
            if (txtOption.Value != currentItem["OptionRequire"].ToString())
            {
                currentItem["OptionRequire"] = txtOption.Value + string.Empty;
                //get translate option text
                currentItem["OptionRequireVNese"] = translateWord(txtOption.Value + string.Empty);
                //if ((txtOptionVNese.Value + string.Empty) == string.Empty)
                //{
                //    currentItem["OptionRequireVNese"] = txtOption.Value + string.Empty;
                //}
                //else
                //{
                //    currentItem["OptionRequireVNese"] = txtOptionVNese.Value + string.Empty;
                //}


                //modify item
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }
            }
            //change custom kitchen name
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;
            if (languageFlag == 0)
            {
                //vietnamese
                itemName = currentItem["KitchenName"] + string.Empty;
            }
            else
            {
                //english
                itemName = currentItem["ProductName"] + string.Empty;
            }

            currentItem["CustomKitchenName"] = customDisplayKitchenName(itemName,
                txtOption.Value + string.Empty,
                currentItem["ExtraName"] + string.Empty,
                currentItem["ExtraWith"] + string.Empty,
                currentItem["ExtraWithout"] + string.Empty,
                currentItem["CustomSelect"] + string.Empty);

            //reset control
            checkedTakeOut.Value = false;
            checkedSmallSize.Value = false;
            txtOption.Value = "";
            listExtraName.Items.Clear();

            obj.ListItems.AcceptChanges();
            //obj.ItemModify = null;
            Session[Request.QueryString["tableid"]] = obj;
            grid.DataSource = obj.ListItems;
            grid.Selection.UnselectAll();
            grid.DataBind();
            grid.JSProperties["cpShowPopup"] = "";
        }
        else if (name == "phomacextracancel")
        {
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];

            //obj.ItemModify = null;
            Session[Request.QueryString["tableid"]] = obj;
            grid.JSProperties["cpShowPopup"] = "";
            grid.Selection.UnselectAll();
            grid.DataBind();
        }
        //add extra item to item
        //else if (name.StartsWith("cpSubmitAddExtra"))
        //{
        //    string[] format = name.Split('|');
        //    string barcode = format[1].Trim();
        //    addExtraToItem(barcode);
        //}
        //clear all
        else if (name == "clearAll")
        {
            clearAll();
        }
        //Clear Selection
        else if (name == "ClearSelection")
        {
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            obj.ItemModify = null;
            obj.SelectedSaleItemID = -1;
            grid.Selection.UnselectAll();
            grid.DataBind();
        }
        ////selection changed
        //else if (name == "SelectionChanged")
        //{
        //    if (grid.Selection.Count > 0)
        //    {
        //        int saleItemID = (int)grid.GetSelectedFieldValues("SaleItemID")[0];
        //        //selectItemGrid(saleItemID);
        //        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //        managerOrder.SelectedSaleItemID = saleItemID;
        //        Session[Request.QueryString["tableid"]] = managerOrder;
        //    }
        //}
        //ChangeSizeGrid
        else if (name == "ChangeSizeGrid")
        {
            ChangeSizeItemInGrid();
        }
        //TakeOutGrid
        else if (name == "TakeOutGrid")
        {
            TakeOutGrid();
        }
        //DoubleItemsGrid
        else if (name == "DoubleItemsGrid")
        {
            doubleItemGrid();
        }
        //ScheduleSendToKitchen
        else if (name == "Emergency")
        {
            //bool emergency = Convert.ToBoolean(hiddenEmergency.Value == string.Empty ? "false" : hiddenEmergency.Value);
            //hiddenEmergency.Value = !emergency + string.Empty;
            //hiddenEmergency.DataBind();
        }
        //UndoGrid
        else if (name == "UndoGrid")
        {
            handleUndo();
        }
        //Add custom amount
        else if (name == "CustomAmount")
        {
            addCustomAmount(txtCustomAmountName.Text, Convert.ToDouble(txtCustomAmount.Text));
            grid.JSProperties["cpCustomAmountOK"] = "OK";
        }
        //Add discount 10%
        else if (name == "discount10PercentOK")
        {
            addDiscount10Percent();
            grid.JSProperties["cpDiscount10PercentOK"] = "OK";
        }
        else if (name == "PrintBill")
        {
            SocketClient client = new SocketClient();
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            client.ConnectAndSendMsg("127.0.0.1", 8888, "printReceipt|ProcTicket|" + obj.TicketID);
        }
        else if (name == "VoidTicket")
        {
            voidTicket();
            grid.JSProperties["cpVoidTicket"] = "OK";
        }
        else if (name == "OpenDrawer")
        {
            openDrawer();
        }
        else if (name == "SeparateItem")
        {
            separateItem();
        }
        //add item
        else
        {
            try
            {
                //add extra to item
                if (grid.Selection.Count > 0)
                {
                    int saleItemID = (int)grid.GetSelectedFieldValues("SaleItemID")[0];
                    addExtraToItem(saleItemID, name.Trim());
                }
                //add item to grid
                else
                {
                    addItemToGrid(name);
                    ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                    DataTable dt = obj.ListItems;
                    int sandwich = 0;
                    int drink = 0;
                    int sandwichDiscount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["BarCode"].ToString() == "63" || dt.Rows[i]["BarCode"].ToString() == "63A" || dt.Rows[i]["BarCode"].ToString() == "63B" || dt.Rows[i]["BarCode"].ToString() == "63C")
                        {
                            sandwich++;
                        }
                        if (dt.Rows[i]["BarCode"].ToString() == "A20" || dt.Rows[i]["BarCode"].ToString() == "A8" || dt.Rows[i]["BarCode"].ToString() == "A20A")
                        {
                            drink++;
                        }
                        if (dt.Rows[i]["ProductName"].ToString() == "Sandwich Discount")
                        {
                            sandwichDiscount++;
                        }
                    }
                    if (sandwich > 0 && drink > 0)
                    {
                        sandwich = sandwich > drink ? drink : sandwich;
                        for (int i = 0; i < Math.Abs(sandwich - sandwichDiscount); i++)
                        {
                            ReadServerConfig();
                            addCustomAmount("Sandwich Discount", SandwichDiscount);
                        }

                    }
                }



            }
            catch (System.Exception ex)
            {
                grid.JSProperties["cpmsg"] = "Lỗi. Vui lòng liên hệ admin.";
                ExceptionUtility.LogException(ex, "default.additem");
            }
        }


    }

    private void openDrawer()
    {
        ReadServerConfig();
        int a = serverPort;
        string b = serverAddress;

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.NoDelay = true;

        IPAddress ip = IPAddress.Parse(serverAddress);
        IPEndPoint ipep = new IPEndPoint(ip, serverPort);
        clientSocket.Connect(ipep);

        byte[] fileBytes = Encoding.Unicode.GetBytes(Globals.drawerCode);

        clientSocket.Send(fileBytes);
        clientSocket.Disconnect(false);
        clientSocket.Close();
    }



    private void voidTicket()
    {
        int tableID = 0;
        try
        {
            tableID = Convert.ToInt32(Request.QueryString["tableid"].ToString());
        }
        catch (System.Exception ex)
        {

        }
        if (tableID > 0)
        {
            using (Entities objEntities = new Entities())
            {
                Dao dao = new Dao();
                var procTicket = dao.FindByMultiColumnAnd<ProcTicket>(new[] { "TableID" }, tableID).FirstOrDefault();
                var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, procTicket.TicketID);
                var phoha7_ProcSaleItems = dao.FindByMultiColumnAnd<PhoHa7_ProcSaleItem>(new[] { "TicketNum" }, procTicket.TicketID);


                Ticket ticket = procTicket.copyProcTicket2Ticket();
                Employee emp = (Employee)Session["LoginEmp"];
                ticket.VoiDelReason = emp.FullName;
                ticket.Tax = 0;
                ticket.TotalP = 0;
                ticket.PaidCash = 0;
                ticket.Discount = 0;
                ticket.PaidCredit = 0;
                ticket.Voided = true;
                ticket = dao.Add1<Ticket>(ticket);
                //ticket = objEntities.Tickets.Add(ticket);

                foreach (var procSaleItem in procSaleItems)
                {
                    SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                    saleItem.Description = "Deleted by " + emp.FullName + " - " + saleItem.Description;
                    saleItem.TicketID = ticket.TicketID;
                    saleItem.Price = 0;
                    saleItem.TPrice = 0;
                    saleItem.Extra = 0;
                    saleItem.ProcDiscount = 0;
                    saleItem.OrgPrice = 0;
                    saleItem.CPrice = 0;
                    saleItem.SPrice = 0;
                    saleItem.Voided = true;
                    PhoHa7_ProcSaleItem phoHa7_ProcSaleItem = dao.FindByMultiColumnAnd<PhoHa7_ProcSaleItem>(new[] { "SaleItemID_Root" }, procSaleItem.SaleItemID).FirstOrDefault();
                    if (phoHa7_ProcSaleItem != null)
                    {
                        phoHa7_ProcSaleItem.Cancel = true;
                        phoHa7_ProcSaleItem.ToKitchen = false;
                        dao.Update<PhoHa7_ProcSaleItem>(phoHa7_ProcSaleItem);
                    }

                    dao.Add<SaleItem>(saleItem);
                    //dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                    objEntities.ProcSaleItems.Attach(procSaleItem);
                    objEntities.Entry(procSaleItem).State = System.Data.Entity.EntityState.Deleted;
                    //objEntities.ProcSaleItems.Remove(procSaleItem);
                }
                //if (phoha7_ProcSaleItems!=null && phoha7_ProcSaleItems.Count > 0)
                //{
                //    foreach (var phoha7_ProcSaleItem in phoha7_ProcSaleItems)
                //    {

                //    }
                //}
                //delete ProcTicket and ProcSaleItem
                //dao.Delete<ProcTicket>(procTicket.TicketID);
                objEntities.ProcTickets.Attach(procTicket);
                objEntities.Entry(procTicket).State = System.Data.Entity.EntityState.Deleted;
                //objEntities.ProcTickets.Remove(procTicket);
                objEntities.SaveChanges();
            }
            //Server.Transfer("table.aspx");
        }
    }


    protected void gridReviewOrder_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //if (obj != null && obj.ListItems != null)
        //{
        //    System.Data.DataTable dt = obj.ListItems;
        //    gridReviewOrder.DataSource = dt;
        gridReviewOrder.DataBind();
        //}
    }

    private void addItemToGrid(string barcode)
    {
        System.Data.DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
        //sqlHelper: get product detail
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataSet ds = new DataSet();
        sqlHelper.ExecuteDataSet(ds, "dt", "select KitchenName,ProductID,ProductName,CategoryID,Price as LPrice,CPrice as SPrice,BarCode,MType from Products where BarCode = @BarCode",
            CommandType.Text, new object[] { "@BarCode" }, new object[] { barcode });
        //check item exists

        if (ds.Tables["dt"].Rows.Count > 0)
        {
            //add undo
            ManagerOrder obj = ((ManagerOrder)Session[Request.QueryString["tableid"]]);
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);

            //add new row to DataTable
            //
            //select language: 0. Vietnamese 1. English
            //
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;


            DataRow dr = dt.NewRow();
            dr["ProductName"] = ds.Tables["dt"].Rows[0]["ProductName"].ToString();
            dr["KitchenName"] = ds.Tables["dt"].Rows[0]["KitchenName"];

            if (languageFlag == 0)
            {
                //vietnamese
                itemName = ds.Tables["dt"].Rows[0]["KitchenName"].ToString();
            }
            else
            {
                //english
                itemName = ds.Tables["dt"].Rows[0]["ProductName"].ToString();
            }
            dr["CustomKitchenName"] = customDisplayKitchenName(itemName, txtOptionAdd.Text + string.Empty, dr["ExtraName"] + string.Empty, dr["ExtraWith"] + string.Empty, dr["ExtraWithout"] + string.Empty, dr["CustomSelect"] + string.Empty);
            dr["Qty"] = 1;
            dr["BarCode"] = ds.Tables["dt"].Rows[0]["BarCode"];
            dr["ProductID"] = Convert.ToInt32(ds.Tables["dt"].Rows[0]["ProductID"]);
            dr["CategoryID"] = Convert.ToInt32(ds.Tables["dt"].Rows[0]["CategoryID"]);

            //dr["CategoryID"] = ds.Tables["dt"].Rows[0]["CategoryID"];
            dr["LPrice"] = ds.Tables["dt"].Rows[0]["LPrice"];
            dr["SPrice"] = ds.Tables["dt"].Rows[0]["SPrice"];
            dr["OptionRequire"] = txtOptionAdd.Text;
            //get translate option text
            dr["OptionRequireVNese"] = translateWord(txtOptionAdd.Text);
            //if ((txtOptionAddVNese.Value + string.Empty) == string.Empty)
            //{
            //    dr["OptionRequireVNese"] = txtOptionAdd.Text;
            //}
            //else
            //{
            //    dr["OptionRequireVNese"] = txtOptionAddVNese.Value;
            //}
            dr["Extra"] = 0;
            dr["TakeOut"] = checkedItemTakeOut.Checked;
            dr["EmployeeName"] = "";
            dr["Status"] = EnumFormStatus.Add;
            dr["MType"] = ds.Tables["dt"].Rows[0]["MType"];
            //
            // Temporary
            //
            if (dr["MType"].ToString() == "4")
            {
                dr["SmallSize"] = Convert.ToBoolean(radioSize.SelectedItem.Value);
            }
            else
            {
                dr["SmallSize"] = false;
            }
            if (checkedItemTakeOut.Checked)
            {
                if (dr["MType"].ToString() == "4")
                    dr["SmallSize"] = false;
            }
            if (Convert.ToBoolean(checkTakeOutTable.Value))
            {
                if (dr["MType"].ToString() == "4")
                    dr["SmallSize"] = false;
            }


            dt.Rows.Add(dr);
            dt.AcceptChanges();
            //set datasource
            grid.DataSource = dt;
            grid.DataBind();

            //save to Session
            obj.ListItems = dt;
            Session[Request.QueryString["tableid"]] = obj;
            grid.Selection.UnselectAll();
            grid.DataBind();
        }
        else
        {
            grid.JSProperties["cpmsg"] = "Lỗi. Không tìm thấy món.";
        }
    }


    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView gridView = sender as ASPxGridView;
        gridView.JSProperties["cpmsg"] = "";
        gridView.JSProperties["cpShowPopup"] = "";
    }

    void updateItem()
    {
        // if (e.ButtonID == "btnDetails")
        //{
        //    int saleItemID = Convert.ToInt32(grid.GetRowValues(e.VisibleIndex, "SaleItemID"));
        //    hiddenSaleItemIDModify.Value = saleItemID.ToString();
        //    hiddenSaleItemIDModify.DataBind();
        //    DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
        //    txtKitchenName.Value = dt.Rows[e.VisibleIndex]["KitchenName"];
        //    txtKitchenName.DataBind();
        //    //txtOption
        //    txtOption.Value = dt.Rows[e.VisibleIndex]["OptionRequire"].ToString();
        //    txtOption.DataBind();
        //    //Quality
        //    dropListQty.SelectedValue = dt.Rows[e.VisibleIndex]["Qty"].ToString();
        //    dropListQty.DataBind();
        //    //take out
        //    checkedTakeOut.Value = Convert.ToBoolean(dt.Rows[e.VisibleIndex]["TakeOut"]);
        //    checkedTakeOut.DataBind();
        //    //small size
        //    checkedSmallSize.Value = Convert.ToBoolean(dt.Rows[e.VisibleIndex]["SmallSize"]);
        //    checkedSmallSize.DataBind();
        //    //extra name
        //    if (Convert.ToDouble(dt.Rows[e.VisibleIndex]["Extra"]) != 0)
        //    {
        //        string[] arrExtraName = dt.Rows[e.VisibleIndex]["ExtraName"].ToString().Split('|');
        //        string[] arrExtraPrice = dt.Rows[e.VisibleIndex]["ExtraPrice"].ToString().Split('|');
        //        listExtraName.Items.Clear();
        //        for (int i = 0; i < arrExtraName.Length; i++)
        //        {
        //            //listExtraName.Items.Add(new ListItem(arrExtraName[i], arrExtraPrice[i]));
        //            listExtraName.Items.Add(arrExtraName[i], arrExtraPrice[i]);
        //        }
        //        listExtraName.DataBind();
        //    }
        //    ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //    obj.ItemStatus = PhoHa7.Library.Enum.EnumFormStatus.Modify;
        //    obj.SaleItem = saleItemID;
        //    Session[Request.QueryString["tableid"]] = obj;
        //    pcLogin.ShowOnPageLoad = true;
        //}
        //else if (e.ButtonID == "btnDel")
        //{
        //    DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
        //    int saleItemID = Convert.ToInt32(grid.GetRowValues(e.VisibleIndex, "SaleItemID"));
        //    //get list items delete
        //    DataTable listItemDel = getListDelItem();
        //    //get delete item
        //    DataRow dr = dt.Rows.Find(saleItemID);
        //    //add to delete list datatable
        //    listItemDel.Rows.Add(dr);
        //    listItemDel.AcceptChanges();
        //    //remove 
        //    dt.Rows.Remove(dr);
        //    dt.AcceptChanges();
        //    grid.DataSource = dt;
        //    ManagerOrder obj = (ManagerOrder)(Session[Request.QueryString["tableid"]]);
        //    obj.ListItems = dt;
        //    obj.ListItemsDel = listItemDel;
        //    Session[Request.QueryString["tableid"]] = obj;
        //    grid.DataBind();
        //}
    }

    protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
        if (e.Column.FieldName == "SPrice")
        {
            decimal price = (decimal)e.GetListSourceFieldValue("SPrice");
            int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Qty"));
            e.Value = price * quantity;
        }
        if (e.Column.FieldName == "Extra")
        {
            decimal price = (decimal)e.GetListSourceFieldValue("SPrice");
            int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Qty"));
            e.Value = price * (decimal)0.0875;
        }
    }


    #endregion

    #region Method

    private void addExtraToItem(int saleItemID, string productBarCode)
    {
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //add undo
        DataTable tempUndo = new DataTable();
        tempUndo = managerOrder.ListItems.Copy();
        managerOrder.addUndoListItems(tempUndo);
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Products where BarCode = @BarCode";
        //get product from database
        DataTable productRow = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@BarCode" }, new object[] { productBarCode });
        if (productRow.Rows.Count > 0)
        {
            int mType = (int)productRow.Rows[0]["MType"];

            DataRow currentItem = managerOrder.ListItems.Rows.Find(saleItemID);
            string extraName = currentItem["ExtraName"] + string.Empty;
            if (mType != 0 && mType != 4)
            {
                if (extraName == "")
                {
                    currentItem["ExtraName"] = currentItem["ExtraName"].ToString() + productRow.Rows[0]["ProductID"];
                    currentItem["ExtraPrice"] = currentItem["ExtraPrice"].ToString() + productRow.Rows[0]["Price"];
                }
                else
                {
                    currentItem["ExtraName"] = currentItem["ExtraName"].ToString() + "|" + productRow.Rows[0]["ProductID"];
                    currentItem["ExtraPrice"] = currentItem["ExtraPrice"].ToString() + "|" + productRow.Rows[0]["Price"];
                }
                currentItem["Extra"] = Convert.ToDouble(currentItem["Extra"]) + Convert.ToDouble(productRow.Rows[0]["Price"]);
                if (Request.QueryString["type"].Equals("modify"))
                {
                    if ((EnumFormStatus)currentItem["Status"] != EnumFormStatus.Add)
                    {
                        currentItem["Status"] = EnumFormStatus.Modify;
                    }
                }

                //change language
                string itemName = string.Empty;
                Employee emp = (Employee)Session["LoginEmp"];
                int languageFlag = emp.Language ?? 0;
                if (languageFlag == 0)
                {
                    //vietnamese
                    itemName = currentItem["KitchenName"] + string.Empty;
                }
                else
                {
                    //english
                    itemName = currentItem["ProductName"] + string.Empty;
                }
                //custom kitchen name
                //change custom kitchen name
                currentItem["CustomKitchenName"] = customDisplayKitchenName(itemName, "",
                    currentItem["ExtraName"] + string.Empty,
                    currentItem["ExtraWith"] + string.Empty,
                    currentItem["ExtraWithout"] + string.Empty,
                    currentItem["CustomSelect"] + string.Empty);

                managerOrder.ListItems.AcceptChanges();
                Session[Request.QueryString["tableid"]] = managerOrder;
                grid.DataSource = managerOrder.ListItems;
                grid.DataBind();
            }
            else
            {
                addItemToGrid(productBarCode);
            }
        }

    }

    private DataTable loadOrderItems(int tableID)
    {
        System.Data.DataTable dt;
        if (Session[tableID.ToString()] == null)
        {
            dt = createTable();
            ManagerOrder obj = new ManagerOrder(tableID, dt);
            Session[tableID.ToString()] = obj;
            return dt;
        }
        else
        {
            dt = ((ManagerOrder)Session[tableID.ToString()]).ListItems;
            return dt;
        }
    }

    private DataTable getListDelItem()
    {
        ManagerOrder obj = ((ManagerOrder)Session[Request.QueryString["tableid"].ToString()]);
        if (obj.ListItemsDel == null)
        {
            DataTable dt = createTable();
            obj.ListItemsDel = dt;
            return dt;
        }
        else
        {
            return obj.ListItemsDel;
        }
    }

    private void TakeOutGrid()
    {
        if (grid.Selection.Count > 0)
        {
            int saleItemID = (int)grid.GetSelectedFieldValues("SaleItemID")[0];
            bool takeOut = (bool)grid.GetSelectedFieldValues("TakeOut")[0];
            bool smallSize = (bool)grid.GetSelectedFieldValues("SmallSize")[0];


            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            //add undo
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);

            DataRow currentItem = obj.ListItems.Rows.Find(saleItemID);
            currentItem["TakeOut"] = !takeOut;
            if ((bool)currentItem["TakeOut"])
            {
                currentItem["SmallSize"] = false;
            }
            if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
            {
                currentItem["Status"] = EnumFormStatus.Modify;
            }
            obj.ListItems.AcceptChanges();
            Session[Request.QueryString["tableid"]] = obj;
            grid.DataSource = obj.ListItems;
            grid.DataBind();

        }
    }

    private void ChangeSizeItemInGrid()
    {
        if (grid.Selection.Count > 0)
        {
            int saleItemID = (int)grid.GetSelectedFieldValues("SaleItemID")[0];
            int mType = (int)grid.GetSelectedFieldValues("MType")[0];
            bool takeOut = (bool)grid.GetSelectedFieldValues("TakeOut")[0];
            bool smallSize = (bool)grid.GetSelectedFieldValues("SmallSize")[0];
            bool takeOutTable = Convert.ToBoolean((checkTakeOutTable.Value + string.Empty) == string.Empty ? "false" : checkTakeOutTable.Value);

            if (mType == 4)
            {
                ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
                //add undo
                DataTable tempUndo = new DataTable();
                tempUndo = obj.ListItems.Copy();
                obj.addUndoListItems(tempUndo);

                DataRow currentItem = obj.ListItems.Rows.Find(saleItemID);
                if (!takeOut && !takeOutTable)
                {
                    currentItem["SmallSize"] = !smallSize;
                }
                if (((EnumFormStatus)currentItem["Status"]) != EnumFormStatus.Add)
                {
                    currentItem["Status"] = EnumFormStatus.Modify;
                }
                obj.ListItems.AcceptChanges();
                Session[Request.QueryString["tableid"]] = obj;
                grid.DataSource = obj.ListItems;
                grid.DataBind();
            }
        }
    }

    private void separateItem()
    {
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //add undo
        DataTable dt = obj.ListItems;
        DataTable tempUndo = new DataTable();
        tempUndo = obj.ListItems.Copy();
        obj.addUndoListItems(tempUndo);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int qty = Convert.ToInt32(dt.Rows[i]["Qty"]);
            if (qty > 1)
            {

                for (int j = 0; j < qty - 1; j++)
                {
                    DataRow currentItem = dt.Rows[i];
                    DataRow newItem = obj.ListItems.NewRow();

                    //add item
                    newItem["ProductName"] = currentItem["ProductName"];
                    newItem["Qty"] = 1;
                    newItem["BarCode"] = currentItem["BarCode"];
                    newItem["ProductID"] = currentItem["ProductID"];
                    newItem["KitchenName"] = currentItem["KitchenName"];
                    newItem["CategoryID"] = currentItem["CategoryID"];
                    newItem["LPrice"] = currentItem["LPrice"];
                    newItem["SPrice"] = currentItem["SPrice"];
                    newItem["MType"] = currentItem["MType"];
                    newItem["Extra"] = currentItem["Extra"];
                    newItem["SmallSize"] = currentItem["SmallSize"];
                    newItem["TakeOut"] = currentItem["TakeOut"];
                    newItem["EmployeeName"] = currentItem["EmployeeName"];
                    newItem["ExtraName"] = currentItem["ExtraName"];
                    newItem["ExtraPrice"] = currentItem["ExtraPrice"];
                    newItem["OptionRequire"] = currentItem["OptionRequire"];
                    newItem["ExtraWith"] = currentItem["ExtraWith"];
                    newItem["ExtraWithout"] = currentItem["ExtraWithout"];
                    newItem["CustomSelect"] = currentItem["CustomSelect"];
                    newItem["CustomKitchenName"] = currentItem["CustomKitchenName"];
                    newItem["Status"] = EnumFormStatus.Add;
                    //accept change
                    obj.ListItems.Rows.Add(newItem);
                    obj.ListItems.AcceptChanges();
                }
                dt.Rows[i]["Qty"] = 1;
                dt.Rows[i]["Status"] = EnumFormStatus.Modify;
                //set datasource
                grid.DataSource = obj.ListItems;
                //save to Session
                Session[Request.QueryString["tableid"]] = obj;
                grid.DataBind();
            }
        }
    }

    private void doubleItemGrid()
    {
        if (grid.Selection.Count > 0)
        {
            int saleItemID = (int)grid.GetSelectedFieldValues("SaleItemID")[0];
            //get data from DataTable
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            //add undo
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);

            DataRow currentItem = obj.ListItems.Rows.Find(saleItemID);
            DataRow newItem = obj.ListItems.NewRow();

            //add item
            newItem["ProductName"] = currentItem["ProductName"];
            newItem["Qty"] = currentItem["Qty"];
            newItem["BarCode"] = currentItem["BarCode"];
            newItem["ProductID"] = currentItem["ProductID"];
            newItem["KitchenName"] = currentItem["KitchenName"];
            newItem["CategoryID"] = currentItem["CategoryID"];
            newItem["LPrice"] = currentItem["LPrice"];
            newItem["SPrice"] = currentItem["SPrice"];
            newItem["MType"] = currentItem["MType"];
            newItem["Extra"] = currentItem["Extra"];
            newItem["SmallSize"] = currentItem["SmallSize"];
            newItem["TakeOut"] = currentItem["TakeOut"];
            newItem["EmployeeName"] = currentItem["EmployeeName"];
            newItem["ExtraName"] = currentItem["ExtraName"];
            newItem["ExtraPrice"] = currentItem["ExtraPrice"];
            newItem["OptionRequire"] = currentItem["OptionRequire"];
            newItem["ExtraWith"] = currentItem["ExtraWith"];
            newItem["ExtraWithout"] = currentItem["ExtraWithout"];
            newItem["CustomSelect"] = currentItem["CustomSelect"];
            newItem["CustomKitchenName"] = currentItem["CustomKitchenName"];
            newItem["Status"] = EnumFormStatus.Add;
            //accept change
            obj.ListItems.Rows.Add(newItem);
            obj.ListItems.AcceptChanges();
            //set datasource
            grid.DataSource = obj.ListItems;
            //save to Session
            Session[Request.QueryString["tableid"]] = obj;
            grid.DataBind();
        }
    }

    private void handleUndo()
    {
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (obj.ListItemsHistory.Count > 0)
        {
            if (obj.PositionUndo < 0)
            {
                obj.PositionUndo = 4;
            }
            obj.ListItems.Clear();
            obj.ListItems = obj.ListItemsHistory[obj.PositionUndo].Copy();
            grid.DataSource = obj.ListItems;
            grid.Selection.UnselectAll();
            grid.DataBind();
        }
        obj.PositionUndo--;

        if (obj.PositionUndo > (obj.ListItemsHistory.Count - 1))
        {
            obj.PositionUndo = obj.ListItemsHistory.Count - 1;
        }
        Session[Request.QueryString["tableid"]] = obj;
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

    DataTable loadTableFromDatabase(int tableID)
    {
        DataTable dt = createTable();
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select s.*,t.TableID,t.TicketID from ProcSaleItem s left join ProcTickets t on s.TicketID = t.ticketID where t.TableID = @TableID";
        DataTable dtDatabase = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID });
        for (int i = 0; i < dtDatabase.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ProductName"] = dtDatabase.Rows[i]["Description"].ToString();
            dr["Qty"] = Convert.ToInt32(dtDatabase.Rows[i]["Qty"]);
            dr["BarCode"] = dtDatabase.Rows[i]["ItemCode"].ToString();
            dr["ProductID"] = Convert.ToInt32(dtDatabase.Rows[i]["ProductID"]);
            dr["KitchenName"] = dtDatabase.Rows[i]["KitchenName"].ToString();
            //dr["CategoryID"] = dtDatabase.Rows[i]["CategoryID"];
            dr["Extra"] = 0;
            //check small size
            double lPrice = Convert.ToDouble(dtDatabase.Rows[i]["Price"]);
            double sPrice = Convert.ToDouble(dtDatabase.Rows[i]["CPrice"]);
            dr["LPrice"] = lPrice;
            dr["SPrice"] = sPrice;
            dr["SmallSize"] = dtDatabase.Rows[i]["SmallSize"];


            dr["TakeOut"] = Convert.ToBoolean(dtDatabase.Rows[i]["TakeOut"]);
            dr["EmployeeName"] = dtDatabase.Rows[i]["EmployeeName"].ToString();
            dr["SaleItemID"] = Convert.ToInt32(dtDatabase.Rows[i]["SaleItemID"]);
            dr["MType"] = Convert.ToInt32(dtDatabase.Rows[i]["MType"]);
            //
            dr["ExtraName"] = dtDatabase.Rows[i]["ExtraName"].ToString();
            dr["ExtraPrice"] = dtDatabase.Rows[i]["ExtraPrice"].ToString();
            dr["OptionRequire"] = dtDatabase.Rows[i]["OptionRequire"].ToString();
            dr["ExtraWith"] = dtDatabase.Rows[i]["ExtraWith"] + string.Empty;
            dr["ExtraWithout"] = dtDatabase.Rows[i]["ExtraWithout"] + string.Empty;
            dr["CustomSelect"] = dtDatabase.Rows[i]["CustomSelect"] + string.Empty;

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
                                                                dtDatabase.Rows[i]["ExtraName"] + string.Empty + string.Empty,
                                                                dtDatabase.Rows[i]["ExtraWith"] + string.Empty + string.Empty,
                                                                dtDatabase.Rows[i]["ExtraWithout"] + string.Empty + string.Empty,
                                                                dtDatabase.Rows[i]["CustomSelect"] + string.Empty + string.Empty);
            dr["Status"] = EnumFormStatus.View;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        ManagerOrder obj = new ManagerOrder();
        obj.TableID = tableID;
        obj.TicketID = Convert.ToInt32(dtDatabase.Rows[0]["TicketID"]);
        obj.ListItems = dt;
        Session[tableID.ToString()] = obj;
        return dt;
    }

    DataTable checkExistTable(int tableID)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Tables where TableID = @TableID";
        DataTable count = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID });
        return count;
    }

    bool checkAvaibleTable(int tableID)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select count(*) from ProcTickets where TableID = @TableID";
        int count = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID }));
        if (count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    string customDisplayKitchenName(string kitchenName, string optionRequire, string extraName, string extraWith, string extraWithout, string customSelect)
    {
        string itemName = string.Empty;
        Employee emp = (Employee)Session["LoginEmp"];
        int languageFlag = emp.Language ?? 0;
        Dao dao = new Dao();
        string name = "";
        //custom select
        if (customSelect != "")
        {
            string[] arrCustomSelect = customSelect.ToString().Split('|');
            for (int i = 0; i < arrCustomSelect.Length; i++)
            {
                string customSelectName = string.Empty;
                if (languageFlag == 0)
                {
                    //vietnamese
                    customSelectName = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i])).KitchenName;
                }
                else
                {
                    //english
                    customSelectName = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i])).ProductName;
                }
                name = name + " " + customSelectName;
            }
        }
        //extra name
        if (extraName != "")
        {
            name += "(";
            string[] arrExtraName = extraName.ToString().Split('|');
            for (int i = 0; i < arrExtraName.Length; i++)
            {
                Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraName[i] == string.Empty ? "0" : arrExtraName[i]));
                string nameExtraName = string.Empty;
                if (pro != null)
                {
                    if (languageFlag == 0)
                    {
                        //vietnamese
                        nameExtraName = pro.KitchenName;
                    }
                    else
                    {
                        //english
                        nameExtraName = pro.ProductName;
                    }
                }
                name += nameExtraName;
                //name += arrExtraName[i];
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
                string extraWithName = string.Empty;
                if (languageFlag == 0)
                {
                    //vietnamese
                    extraWithName = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i])).KitchenName;
                }
                else
                {
                    //english
                    extraWithName = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i])).ProductName;
                }
                name += extraWithName;
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
                string extraWithoutName = string.Empty;
                if (languageFlag == 0)
                {
                    //vietnamese
                    extraWithoutName = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i])).KitchenName;
                }
                else
                {
                    //english
                    extraWithoutName = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i])).ProductName;
                }
                name += extraWithoutName;
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
        return kitchenName + " " + name;
    }

    void clearAll()
    {
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (Request.QueryString["type"].Equals("new"))
        {
            if (obj.ListItems != null)
            {
                obj.ListItems.Rows.Clear();
                obj.ListItems.AcceptChanges();
            }
            if (obj.ListItemsDel != null)
            {
                obj.ListItemsDel.Clear();
                obj.ListItemsDel.AcceptChanges();
            }
            Session[Request.QueryString["tableid"]] = obj;
            grid.DataSource = obj.ListItems;
            grid.DataBind();
        }
        else if (Request.QueryString["type"].Equals("modify"))
        {
            //get list items delete
            DataTable listItemDel = getListDelItem();
            int rowCount = obj.ListItems.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                if (obj.ListItems != null)
                {
                    //new row
                    DataRow temp = listItemDel.NewRow();
                    temp["SaleItemID"] = obj.ListItems.Rows[i]["SaleItemID"];
                    temp["ProductName"] = obj.ListItems.Rows[i]["ProductName"];
                    //add to delete list datatable
                    listItemDel.Rows.Add(temp);
                }
            }
            obj.ListItems.Rows.Clear();
            listItemDel.AcceptChanges();
            obj.ListItems.AcceptChanges();
            grid.DataSource = obj.ListItems;
            Session[Request.QueryString["tableid"]] = obj;
            grid.DataBind();
        }
    }

    public DataTable getAllProductsList()
    {
        if (Cache["AllProducts"] == null)
        {
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            string sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
                "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products]";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            Cache["AllProducts"] = dt;
            return dt;
        }
        else
        {
            return (DataTable)Cache["AllProducts"];
        }
    }

    public DataTable getAllProductsExtraWith(int productID)
    {
        string AllProductsExtraWith = string.Empty;
        string sql = string.Empty;
        Employee emp = (Employee)Session["LoginEmp"];
        int languageFlag = emp.Language ?? 0;
        if (languageFlag == 0)
        {
            //vietnamese
            AllProductsExtraWith = "AllProductsExtraWithVN";
            sql = "SELECT [ProductID],[ProductName] = [KitchenName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
               "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 99 order by OrderBy";
        }
        else
        {
            //english
            AllProductsExtraWith = "AllProductsExtraWithEng";
            sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
               "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 99 order by OrderBy";
        }
        DataTable dt1 = (DataTable)Cache[AllProductsExtraWith];
        if (dt1 == null || dt1.Rows.Count == 0)
        {
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            //string sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
            //    "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 99";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            Cache[AllProductsExtraWith] = dt;
            DataRow[] query = dt.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
        else
        {

            DataRow[] query = dt1.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
    }

    public DataTable getAllProductsExtraWithout(int productID)
    {
        string AllProductsExtraWithout = string.Empty;
        string sql = string.Empty;
        Employee emp = (Employee)Session["LoginEmp"];
        int languageFlag = emp.Language ?? 0;
        if (languageFlag == 0)
        {
            //vietnamese
            AllProductsExtraWithout = "AllProductsExtraWithoutVN";
            sql = sql = "SELECT [ProductID],[ProductName]=[KitchenName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
                "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 98 order by OrderBy";
        }
        else
        {
            //english
            AllProductsExtraWithout = "AllProductsExtraWithoutEng";
            sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
                "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 98 order by OrderBy";
        }
        DataTable dt1 = (DataTable)Cache[AllProductsExtraWithout];
        if (dt1 == null || dt1.Rows.Count == 0)
        {
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            //string sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
            //    "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 98";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            Cache[AllProductsExtraWithout] = dt;
            DataRow[] query = dt.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
        else
        {
            //DataTable dt = (DataTable)Cache[AllProductsExtraWithout];
            DataRow[] query = dt1.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
    }

    public DataTable getAllProductsCustomName(int productID)
    {
        string AllProductsCustomName = string.Empty;
        string sql = string.Empty;
        Employee emp = (Employee)Session["LoginEmp"];
        int languageFlag = emp.Language ?? 0;
        if (languageFlag == 0)
        {
            //vietnamese
            AllProductsCustomName = "AllProductsCustomNameVN";
            sql = sql = "SELECT [ProductID],[ProductName]=[KitchenName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
                "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 97 order by OrderBy";
        }
        else
        {
            //english
            AllProductsCustomName = "AllProductsCustomNameEng";
            sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
                "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 97 order by OrderBy";
        }
        DataTable dt1 = (DataTable)Cache[AllProductsCustomName];
        if (dt1 == null || dt1.Rows.Count == 0)
        {
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            //string sql = "SELECT [ProductID],[ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[SizeChoiceTxt],[MType],[CPrice]," +
            //    "[NotSaleItem],[KitchenName] ,[Description],[OrderBy],[ExpandCategoryID] FROM [Products] where [MType] = 97";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            Cache[AllProductsCustomName] = dt;
            DataRow[] query = dt.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
        else
        {
            //DataTable dt = (DataTable)Cache[AllProductsCustomName];
            DataRow[] query = dt1.Select("ExpandCategoryID =" + productID);
            return query.Length > 0 ? query.CopyToDataTable() : null;
        }
    }

    #endregion

    #region Popup control

    protected void pcLogin_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
    {
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];
        DataTable dt = managerOrder.ItemModify;
        if (dt != null)
        {
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;
            if (languageFlag == 0)
            {
                //vietnamese
                itemName = dt.Rows[0]["KitchenName"].ToString();
            }
            else
            {
                //english
                itemName = dt.Rows[0]["ProductName"].ToString();
            }
            txtKitchenName.Value = itemName;
            txtKitchenName.DataBind();
            //txtOption
            txtOption.Value = dt.Rows[0]["OptionRequire"].ToString();
            txtOption.DataBind();
            //Quality
            dropListQty.Value = dt.Rows[0]["Qty"].ToString();
            dropListQty.DataBind();
            //take out
            checkedTakeOut.Value = Convert.ToBoolean(dt.Rows[0]["TakeOut"]);
            checkedTakeOut.DataBind();
            //small size
            if (dt.Rows[0]["MType"].ToString() != "4")
            {
                checkedSmallSize.Visible = false;
                dt.Rows[0]["SmallSize"] = false;
            }
            if (Convert.ToBoolean(dt.Rows[0]["TakeOut"]))
            {
                checkedSmallSize.Visible = false;
                checkedSmallSize.Value = false;
                checkedSmallSize.DataBind();
            }
            else
            {
                checkedSmallSize.Value = Convert.ToBoolean(dt.Rows[0]["SmallSize"]);
                checkedSmallSize.DataBind();
            }

            //extra name
            if (Convert.ToDouble(dt.Rows[0]["Extra"]) != 0)
            {
                string[] arrExtraName = dt.Rows[0]["ExtraName"].ToString().Split('|');
                string[] arrExtraPrice = dt.Rows[0]["ExtraPrice"].ToString().Split('|');
                listExtraName.Items.Clear();
                for (int i = 0; i < arrExtraName.Length; i++)
                {
                    listExtraName.Items.Add(arrExtraName[i], arrExtraPrice[i]);
                }
                listExtraName.DataBind();
            }

            SqlHelperWeb sqlhelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            int productID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
            //load with
            //string sql = "select * from Products where ExpandCategoryID = @ProductID and MType = 99";
            //DataTable listExtraWith = sqlhelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { productID });
            //DataTable listExtraWith = getAllProductsExtraWith(Convert.ToInt32(dt.Rows[0]["ProductID"].ToString()));
            DataTable listExtraWith = managerOrder.ListExtraWith;
            if (listExtraWith != null)
            {
                cbListWith.DataSource = listExtraWith;
                hiddencbListWith.Value = dt.Rows[0]["ExtraWith"].ToString();
                cbListWith.DataBind();
                pcLogin.JSProperties["cpShowExtraWith"] = "1";
            }
            else
            {
                //lblcbListWith.Visible = false;
                //cbListWith.Visible = false;
                pcLogin.JSProperties["cpShowExtraWith"] = "0";
            }

            //load without
            //sql = "select * from Products where ExpandCategoryID = @ProductID and MType = 98";
            //DataTable listExtraWithout = sqlhelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { productID });
            //DataTable listExtraWithout = getAllProductsExtraWith(Convert.ToInt32(dt.Rows[0]["ProductID"].ToString()));
            DataTable listExtraWithout = managerOrder.ListExtraWithout;
            if (listExtraWithout != null)
            {
                cbListWithout.DataSource = listExtraWithout;
                hiddencbListWithout.Value = dt.Rows[0]["ExtraWithout"].ToString();
                cbListWithout.DataBind();
                pcLogin.JSProperties["cpShowExtraWithout"] = "1";
            }
            else
            {
                //lblcbListWithout.Visible = false;
                //cbListWithout.Visible = false;
                pcLogin.JSProperties["cpShowExtraWithout"] = "0";
            }
            //load custom name
            //sql = "select * from Products where ExpandCategoryID = @ProductID and MType = 97";
            //DataTable listCustomName = sqlhelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { productID });
            //DataTable listCustomName = getAllProductsCustomName(Convert.ToInt32(dt.Rows[0]["ProductID"].ToString()));
            DataTable listCustomName = managerOrder.ListCustomSelect;
            if (listCustomName != null)
            {
                cbListCustomName.DataSource = listCustomName;
                hiddencbListCustomName.Value = dt.Rows[0]["CustomSelect"].ToString();
                cbListCustomName.DataBind();
                pcLogin.JSProperties["cpShowCustomSelect"] = "1";
            }
            else
            {
                //lblcbListCustomName.Visible = false;
                //cbListCustomName.Visible = false;
                pcLogin.JSProperties["cpShowCustomSelect"] = "0";
            }
        }
        else
        {
            pcLogin.ShowOnPageLoad = false;
        }
    }

    protected void pcLogin_Init(object sender, EventArgs e)
    {
        ASPxPopupControl popUp = sender as ASPxPopupControl;
        popUp.JSProperties["cpShowExtraWith"] = "";
        popUp.JSProperties["cpShowExtraWithout"] = "";
        popUp.JSProperties["cpShowCustomSelect"] = "";
    }

    #endregion

    #region Combobox Table Change

    protected void cbTableChange_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        loadCbTableChange();
    }

    private void loadCbTableChange()
    {
        int tableID = Convert.ToInt32(Request.QueryString["tableid"]);
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "SELECT t.[TableID], t.[CategoryID], t.[TableName], t.[TakeOut],t.orderby, " +
"case when coalesce(p.tableid, '0') = 0 then 'Add' else 'Update' end  as Status " +
"FROM [Tables] as t left join ProcTickets as p on t.tableid=p.tableid " +
"WHERE t.Active = 1 and t.[TableID] != @TableID order by t.[CategoryID],t.orderby,t.TableID asc";
        DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID });
        DataRow dr = dt.NewRow();
        dr["TableID"] = 0;
        dr["TableName"] = "None";
        dr["orderby"] = -1;
        dt.DefaultView.Sort = "orderby asc";
        dt.DefaultView.Sort = "CategoryID asc";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        cbTableChange.DataSource = dt;
        cbTableChange.DataBind();
    }

    protected void cbTableChange_Init(object sender, EventArgs e)
    {
        loadCbTableChange();
    }

    #endregion



    //*******************************







    protected void btnUndo_Click(object sender, EventArgs e)
    {
        object a = cbListWith.SelectedItems;
    }





    protected void callBackDictionary_Callback(object source, CallbackEventArgs e)
    {
        string text = e.Parameter;
        //if (text != string.Empty)
        //{
        //    Dao dao = new Dao();
        //    string[] textWord = text.Split(' ');

        //    Entities obj = EntityFactory.getInstance().CreateEntities();
        //    foreach (string item in textWord)
        //    {
        //        var listDictionary = obj.Dictionaries.Where(oh => oh.Dic_SourceLanguage.Contains(item));
        //        if (listDictionary.Count() > 0)
        //        {
        //            Dictionary dic = listDictionary.First<Dictionary>();
        //            text = text.Replace(item, dic.Dic_TargetLanguage);
        //        }
        //    }
        //    txtOptionAddVNese.Value = text;
        //    txtOptionAddVNese.DataBind();
        //    txtOptionVNese.Value = text;
        //    txtOptionVNese.DataBind();
        //    callBackDictionary.JSProperties["cpCompleteMsgDictionary"] = text;
        // }

    }

    string translateWord(string text)
    {
        Dao dao = new Dao();
        if (text != string.Empty)
        {
            Employee emp = (Employee)Session["LoginEmp"];
            //english or vietnamese
            //english
            List<Dictionary> list;
            using (Entities obj = new Entities())
            {
                obj.Configuration.LazyLoadingEnabled = false;
                obj.Configuration.ProxyCreationEnabled = false;
                list = obj.Set<Dictionary>().ToList<Dictionary>();
            }
            text = text.ToLower();
            var listDictionary = (from d in list
                                  select new Dictionary()
                                  {
                                      Dic_Keyword = d.Dic_Keyword,
                                      Dic_SourceLanguage = d.Dic_SourceLanguage,
                                      Dic_TargetLanguage = d.Dic_TargetLanguage,
                                      CountWord = d.Dic_SourceLanguage.Split(' ').Length
                                  }).OrderByDescending(p => p.CountWord);
            foreach (Dictionary dic in listDictionary)
            {
                if (dic.CountWord == 1)
                {
                    continue;
                }
                if (text.Contains(dic.Dic_SourceLanguage.ToLower()))
                {

                    text = text.Replace(dic.Dic_SourceLanguage, dic.Dic_TargetLanguage);
                }
            }
            // }
            string[] textWord = text.Split(' ');
            if (textWord.Length > 0)
            {

                for (int i = 0; i < textWord.Length; i++)
                {
                    foreach (Dictionary dic in listDictionary)
                    {
                        if (dic.CountWord == 1)
                        {
                            string tempWord = textWord[i].Replace(',', ' ').Trim();
                            tempWord = tempWord.Replace('.', ' ').Trim();
                            tempWord = tempWord.Replace(';', ' ').Trim();
                            tempWord = tempWord.Replace(':', ' ').Trim();
                            if (tempWord == dic.Dic_SourceLanguage.ToLower())
                            {
                                textWord[i] = textWord[i].Replace(dic.Dic_SourceLanguage, dic.Dic_TargetLanguage);
                                //textWord[i] = dic.Dic_TargetLanguage;
                            }
                        }
                    }





                    //if (listDictionary1.Count() > 0)
                    //{
                    //    Dictionary dic = listDictionary.First<Dictionary>();
                    //    if (textWord[i] == dic.Dic_SourceLanguage)
                    //    {
                    //        textWord[i] = dic.Dic_TargetLanguage;
                    //    }
                    //}
                }
            }
            text = String.Join(" ", textWord);
        }

        return text;
    }

    protected void callBackDictionary_Init(object sender, EventArgs e)
    {
        ASPxCallback calBack = sender as ASPxCallback;
        calBack.JSProperties["cpCompleteMsgDictionary"] = "";
    }
    protected void gridReviewOrder_Init(object sender, EventArgs e)
    {
        //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //if (obj != null && obj.ListItems != null)
        //{
        //    System.Data.DataTable dt = obj.ListItems;
        //    gridReviewOrder.DataSource = dt;



        //    gridReviewOrder.DataBind();
        //}

    }

    protected void gridReviewOrder_SelectionChanged(object sender, EventArgs e)
    {
        //var selections = gridReviewOrder.GetSelectedFieldValues("SaleItemID");
        //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //decimal subtotal = 0;
        //if (obj != null && obj.ListItems != null)
        //{
        //    System.Data.DataTable dt = obj.ListItems;
        //    //dt.Columns.Add("TotalPrice");
        //    if (selections.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (selections.ToString().Contains(dt.Rows[i]["SaleItemID"].ToString()))
        //            {
        //                var LPrice = Convert.ToDecimal(dt.Rows[i]["LPrice"] ?? 0m);
        //                decimal ExtraPrice = 0;
        //                try
        //                {
        //                    ExtraPrice = Convert.ToDecimal(dt.Rows[i]["ExtraPrice"] ?? 0m);
        //                }
        //                catch (System.Exception ex)
        //                {

        //                }

        //                var Qty = Convert.ToDecimal(dt.Rows[i]["Qty"] ?? 0m);
        //                var total = (LPrice + ExtraPrice) * Qty;
        //                dt.Rows[i]["TotalPrice"] = total;
        //                subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
        //        }
        //    }








        //    decimal tax = subtotal * (decimal)0.0875;

        //    ASPxCallbackPayment.JSProperties["cplblSubTotal"] = string.Format("{0:C}", subtotal);
        //    ASPxCallbackPayment.JSProperties["cplblTax"] = string.Format("{0:C}", tax);
        //    ASPxCallbackPayment.JSProperties["cplblTotal"] = string.Format("{0:C}", (subtotal + tax));
        //    //ASPxCallbackPayment.JSProperties["cpTicketID"] = obj.TicketID;
        //}
    }
    //protected void btnOpenDrawer_Click(object sender, EventArgs e)
    //{
    //    ReadServerConfig();
    //    try
    //    {
    //        using (TcpClient client = new TcpClient(serverAddress, serverPort))
    //        {
    //            NetworkStream stream = client.GetStream();
    //            string message = "printReceipt|";
    //            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
    //            System.Data.DataTable dt = obj.ListItems;
    //            message += obj.TicketID;

    //            byte[] data = Encoding.UTF8.GetBytes(message);
    //            stream.Write(data, 0, data.Length);

    //            // You can implement the logic to read server responses if needed
    //        }

    //        //txtMessage.Text = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the error
    //        Console.WriteLine("Error sending message: " + ex.Message);
    //    }
    //}


    private string serverAddress;
    private int serverPort;
    private double SandwichDiscount;
    protected void ReadServerConfig()
    {
        try
        {
            string configFilePath = Server.MapPath("~/serverConfig.json");

            if (File.Exists(configFilePath))
            {
                string configJson = File.ReadAllText(configFilePath);

                // Parse the JSON content to get the server information using Newtonsoft.Json
                var configData = JsonConvert.DeserializeObject<ServerConfig>(configJson);

                if (configData != null)
                {
                    serverAddress = configData.IPAddress;
                    serverPort = configData.Port;
                    SandwichDiscount = configData.SandwichDiscount;
                }
                else
                {
                    // Handle if JSON data is invalid or missing
                    // You can provide default values or display an error message
                }
            }
            else
            {
                // Handle if the config file is missing
                // You can provide default values or display an error message
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions while reading the config file
            // You can provide default values or display an error message
            Console.WriteLine("Error reading server config: " + ex.Message);
        }
    }

    public class ServerConfig
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public double SandwichDiscount { get; set; }
    }


    void addCustomAmount(string name, double price)
    {
        try
        {
            System.Data.DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
            //sqlHelper: get product detail
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            //DataSet ds = new DataSet();
            //sqlHelper.ExecuteDataSet(ds, "dt", "select KitchenName,ProductID,ProductName,CategoryID,Price as LPrice,CPrice as SPrice,BarCode,MType from Products where BarCode = @BarCode",
            //    CommandType.Text, new object[] { "@BarCode" }, new object[] { barcode });
            //check item exists

            //add undo
            ManagerOrder obj = ((ManagerOrder)Session[Request.QueryString["tableid"]]);
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);

            //add new row to DataTable
            //
            //select language: 0. Vietnamese 1. English
            //
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;


            DataRow dr = dt.NewRow();
            dr["ProductName"] = name;
            dr["KitchenName"] = name;


            dr["CustomKitchenName"] = name;
            dr["Qty"] = 1;
            dr["BarCode"] = "";
            dr["ProductID"] = 0;
            dr["CategoryID"] = 1;

            //dr["CategoryID"] = ds.Tables["dt"].Rows[0]["CategoryID"];
            dr["LPrice"] = Convert.ToDouble(price);
            dr["SPrice"] = Convert.ToDouble(price);
            dr["OptionRequire"] = "";
            //get translate option text
            //dr["OptionRequireVNese"] = txtCustomAmountName.Text;
            //if ((txtOptionAddVNese.Value + string.Empty) == string.Empty)
            //{
            //    dr["OptionRequireVNese"] = txtOptionAdd.Text;
            //}
            //else
            //{
            //    dr["OptionRequireVNese"] = txtOptionAddVNese.Value;
            //}
            dr["Extra"] = 0;
            dr["TakeOut"] = false;
            dr["EmployeeName"] = "";
            dr["Status"] = EnumFormStatus.Add;
            dr["MType"] = 0;
            //
            // Temporary
            //
            dr["SmallSize"] = false;


            dt.Rows.Add(dr);
            dt.AcceptChanges();
            //set datasource
            grid.DataSource = dt;
            grid.DataBind();

            //save to Session
            obj.ListItems = dt;
            Session[Request.QueryString["tableid"]] = obj;
            callBackCustomAmount.JSProperties["cpOK"] = "OK";
            grid.Selection.UnselectAll();

            grid.DataBind();
        }
        catch (System.Exception ex)
        {

        }
    }


    void addDiscount10Percent()
    {
        try
        {
            System.Data.DataTable dt = loadOrderItems(Convert.ToInt32(Request.QueryString["tableid"]));
            //sqlHelper: get product detail
            SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
            //DataSet ds = new DataSet();
            //sqlHelper.ExecuteDataSet(ds, "dt", "select KitchenName,ProductID,ProductName,CategoryID,Price as LPrice,CPrice as SPrice,BarCode,MType from Products where BarCode = @BarCode",
            //    CommandType.Text, new object[] { "@BarCode" }, new object[] { barcode });
            //check item exists

            //add undo
            ManagerOrder obj = ((ManagerOrder)Session[Request.QueryString["tableid"]]);
            DataTable tempUndo = new DataTable();
            tempUndo = obj.ListItems.Copy();
            obj.addUndoListItems(tempUndo);

            //add new row to DataTable
            //
            //select language: 0. Vietnamese 1. English
            //
            string itemName = string.Empty;
            Employee emp = (Employee)Session["LoginEmp"];
            int languageFlag = emp.Language ?? 0;


            DataRow dr = dt.NewRow();
            dr["ProductName"] = "Discount 10%";
            dr["KitchenName"] = "Discount 10%";


            dr["CustomKitchenName"] = "Discount 10%";
            dr["Qty"] = 1;
            dr["BarCode"] = "10%";
            dr["ProductID"] = 0;
            dr["CategoryID"] = 1;

            //dr["CategoryID"] = ds.Tables["dt"].Rows[0]["CategoryID"];
            dr["LPrice"] = Convert.ToDouble(10);
            dr["SPrice"] = Convert.ToDouble(10);
            dr["OptionRequire"] = "";
            //get translate option text
            dr["OptionRequireVNese"] = "Discount 10%";
            //if ((txtOptionAddVNese.Value + string.Empty) == string.Empty)
            //{
            //    dr["OptionRequireVNese"] = txtOptionAdd.Text;
            //}
            //else
            //{
            //    dr["OptionRequireVNese"] = txtOptionAddVNese.Value;
            //}
            dr["Extra"] = 0;
            dr["TakeOut"] = false;
            dr["EmployeeName"] = "";
            dr["Status"] = EnumFormStatus.Add;
            dr["MType"] = 0;
            //
            // Temporary
            //
            dr["SmallSize"] = false;


            dt.Rows.Add(dr);
            dt.AcceptChanges();
            //set datasource
            grid.DataSource = dt;
            grid.DataBind();

            //save to Session
            obj.ListItems = dt;
            Session[Request.QueryString["tableid"]] = obj;
            callBackCustomAmount.JSProperties["cpOK"] = "OK";
            grid.Selection.UnselectAll();

            grid.DataBind();
        }
        catch (System.Exception ex)
        {

        }
    }

    protected void callBackCustomAmount_Callback(object source, CallbackEventArgs e)
    {



    }
    protected void callBackCustomAmount_Init(object sender, EventArgs e)
    {
        callBackCustomAmount.JSProperties["cpOK"] = "";
    }
    protected void callBackCash_Callback(object source, CallbackEventArgs e)
    {
        ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
        if (e.Parameter == "init")
        {
            if (obj != null && obj.ListItems != null)
            {
                callBackPayButton.JSProperties["cpOK"] = "OK";
                callBackPayButton.JSProperties["cpTicketID"] = obj.TicketID;
                var selections = gridReviewOrder.GetSelectedFieldValues("SaleItemID");
                System.Data.DataTable dt = obj.ListItems;
                bool isNewRow = false;
                //dt.Columns.Add("TotalPrice");
                HashSet<decimal> discountPercent = new HashSet<decimal>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ///handle discount
                    if (dt.Rows[i]["BarCode"].ToString().Contains("%"))
                    {
                        discountPercent.Add(Convert.ToDecimal(dt.Rows[i]["LPrice"]));
                        dt.Rows[i]["TotalPrice"] = 0;
                        continue;
                    }
                    string[] extraPrice = dt.Rows[i]["ExtraPrice"].ToString().Split('|');
                    var LPrice = Convert.ToDecimal(dt.Rows[i]["LPrice"] ?? 0m);
                    decimal ExtraPrice = 0;
                    foreach (var item in extraPrice)
                    {
                        try
                        {
                            ExtraPrice += Convert.ToDecimal(item);
                        }
                        catch (System.Exception ex)
                        {

                        }
                    }

                    var Qty = Convert.ToDecimal(dt.Rows[i]["Qty"] ?? 0m);
                    var total = (LPrice + ExtraPrice) * Qty;
                    dt.Rows[i]["TotalPrice"] = total;

                    //check new row --> prevent check out
                    if (dt.Rows[i]["Status"].ToString() == "2")
                    {
                        isNewRow = true;
                    }

                }


                decimal subtotal = 0;
                decimal discount = 0;

                //handle split bill
                if (selections.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string a = selections.ToString();
                        if (selections.Contains(dt.Rows[i]["SaleItemID"]))
                        {
                            callBackPayButton.JSProperties["cpTicketID"] = callBackPayButton.JSProperties["cpTicketID"] + "|" + dt.Rows[i]["SaleItemID"];
                            subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        callBackPayButton.JSProperties["cpTicketID"] = callBackPayButton.JSProperties["cpTicketID"] + "|" + dt.Rows[i]["SaleItemID"];
                        subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                    }
                }




                //calc discount 
                foreach (var item in discountPercent)
                {
                    discount += subtotal * (item / 100);
                    subtotal = subtotal - subtotal * (item / 100);
                }
                decimal tax = subtotal * (decimal)0.0875;
                callBackPayButton.JSProperties["cplblSubTotal"] = subtotal.ToString("c2");
                if (isNewRow)
                {
                    callBackPayButton.JSProperties["cpOK"] = "";
                }


                decimal amount = subtotal + tax;
                var listRecommentAmount = loadExpressButtonCash(amount);
                callBackCash.JSProperties["cpbtnCheckOutCashLabel"] = amount.ToString("c2");
                callBackCash.JSProperties["cpbtnExpressCash1"] = listRecommentAmount[0].ToString("c2");
                callBackCash.JSProperties["cpbtnExpressCash2"] = listRecommentAmount[1].ToString("c2");
                callBackCash.JSProperties["cpbtnExpressCash3"] = listRecommentAmount[2].ToString("c2");
                callBackCash.JSProperties["cpbtnExpressCash4"] = listRecommentAmount[3].ToString("c2");
                callBackCash.JSProperties["cpbtnExpressCash5"] = listRecommentAmount[4].ToString("c2");
            }



        }
        if (e.Parameter == "PrintCompletedTicket")
        {
            //string tID = callBackCash.JSProperties["cpCashChangeTicketID"].ToString();
            string tID = ticketIDCompletedTicket["ticketID"].ToString();
            SocketClient client = new SocketClient();
            //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            client.ConnectAndSendMsg("127.0.0.1", 8888, "printReceipt|Ticket|" + tID);
        }
        else
        //if (e.Parameter == "OK")
        {
            //handle complete ticket
            try
            {
                decimal amount = Convert.ToDecimal(e.Parameter);
                bool isNewRow = false;
                //if (amount >= obj.TotalAmount)
                //{
                System.Data.DataTable dt = obj.ListItems;
                //check new item not send kitchen yet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Status"].ToString() == "2")
                    {
                        isNewRow = true;
                        break;
                    }
                }
                if (!isNewRow)
                {
                    decimal totalAmount = completeCash();
                    if (totalAmount > 0)
                    {
                        callBackCash.JSProperties["cpOK"] = "OK";
                        callBackCash.JSProperties["cpCashChange"] = (amount - totalAmount).ToString("c2");
                        //callBackCash.JSProperties["cpCashChangeTicketID"] = obj.TicketID;
                        openDrawer();
                    }
                }
                else
                {
                    callBackCash.JSProperties["cpOK"] = "SendKitchen";
                }


                //}
                //else
                //{
                //    callBackCash.JSProperties["cpOK"] = "";
                //}
            }
            catch (System.Exception ex)
            {
                callBackCash.JSProperties["cpOK"] = "";
            }

        }
    }


    decimal completeCash()
    {
        try
        {
            ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            DataTable dt = obj.ListItems;
            string ticketID = obj.TicketID.ToString();


            var selections = gridReviewOrder.GetSelectedFieldValues("SaleItemID");
            //handle split bill
            if (selections.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string a = selections.ToString();
                    if (selections.Contains(dt.Rows[i]["SaleItemID"]))
                    {
                        ticketID = ticketID + "|" + dt.Rows[i]["SaleItemID"];
                        //subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ticketID = ticketID + "|" + dt.Rows[i]["SaleItemID"];
                    //subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                }
            }




            Dao dao = new Dao(false, true);
            string[] ticketIDSaleItemID = ticketID.ToString().Split('|');
            var listProcTickets = dao.GetAll<ProcTicket>();
            decimal totalAmount = 0;
            EntityFactory.getInstance().BeginTransactionEntities();
            ProcTicket procTicket = listProcTickets.FirstOrDefault(s => s.TicketID.ToString() == ticketIDSaleItemID[0]);


            if (procTicket != null)
            //if (ticketIDSaleItemID[0] == listProcTicket.TicketID.ToString())
            {

                //tableID = procTicket.TableID.ToString();
                //copy procticket to ticket table
                //ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(ticketIDSaleItemID[0]));
                //get tax, subtotal, credit amount, cash amount from Square to ProcTicket
                procTicket.Tax = Convert.ToDecimal((double)obj.SubTotal * 0.0875);
                procTicket.TotalP = obj.TotalAmount;
                procTicket.XferTo = "PhoMinh";
                //var tenders = json.SelectToken("orders[0].tenders");
                procTicket.PaidCash = obj.TotalAmount;
                totalAmount = obj.TotalAmount;
                Ticket ticket = procTicket.copyProcTicket2Ticket();

                var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));
                //handle ticket pay all
                if (ticketIDSaleItemID.Length - 1 == procSaleItems.Count)
                {
                    //copy procSaleItem to SaleItem table
                    //var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));

                    HashSet<decimal> discountPercent = new HashSet<decimal>();
                    decimal subtotal = 0;
                    decimal discount = 0;
                    ticket = dao.Add1<Ticket>(ticket);
                    callBackCash.JSProperties["cpCashChangeTicketID"] = ticket.TicketID;
                    foreach (var procSaleItem in procSaleItems)
                    {

                        SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                        ///handle discount
                        if (procSaleItem.TPrice < 0)
                        {
                            discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                        }
                        saleItem.TicketID = ticket.TicketID;
                        subtotal += Convert.ToDecimal(procSaleItem.TPrice);
                        dao.Add<SaleItem>(saleItem);
                        dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                    }
                    //calc discount 
                    foreach (var discountItem in discountPercent)
                    {
                        discount += subtotal * (discountItem / 100);
                    }
                    ticket.Discount = discount;
                    dao.Update<Ticket>(ticket);
                    //delete ProcTicket and ProcSaleItem
                    dao.Delete<ProcTicket>(procTicket.TicketID);
                    EntityFactory.getInstance().commit();
                }

                //hand ticket  split bill
                else
                {

                    //copy procSaleItem to SaleItem table
                    int counSaleItemCommit = 0;
                    //var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));

                    HashSet<decimal> discountPercent = new HashSet<decimal>();

                    //add ticket
                    ticket = dao.Add1<Ticket>(ticket);
                    callBackCash.JSProperties["cpCashChangeTicketID"] = ticket.TicketID;
                    
                    List<SaleItem> listSaleItem = new List<SaleItem>();
                    foreach (var procSaleItem in procSaleItems)
                    {
                        if (ticketIDSaleItemID.Contains(procSaleItem.SaleItemID.ToString()))
                        {

                            SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                            saleItem.TicketID = ticket.TicketID;

                            ///handle discount
                            if (procSaleItem.TPrice < 0)
                            {
                                discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                            }

                            dao.Add<SaleItem>(saleItem);
                            dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                            listSaleItem.Add(saleItem);
                            counSaleItemCommit++;
                        }
                    }
                    decimal subtotal = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string a = selections.ToString();
                        if (selections.Contains(dt.Rows[i]["SaleItemID"]))
                        {
                            //ticketID = ticketID + "|" + dt.Rows[i]["SaleItemID"];
                            subtotal += Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                        }
                    }
                    //calc discount 

                    decimal discount = 0;
                    foreach (var discountItem in discountPercent)
                    {
                        discount += subtotal * (discountItem / 100);
                    }
                    ticket.Tax = Convert.ToDecimal((double)subtotal * 0.0875);
                    ticket.TotalP = subtotal;
                    ticket.PaidCash = subtotal + ticket.Tax;
                    ticket.Discount = discount;
                    totalAmount = subtotal + Convert.ToDecimal((double)subtotal * 0.0875);
                    dao.Update<Ticket>(ticket);
                    if (procSaleItems.Count == counSaleItemCommit)
                    {
                        //delete ProcTicket and ProcSaleItem
                        try
                        {
                            dao.Delete<ProcTicket>(procTicket.TicketID);
                        }
                        catch (System.Exception ex)
                        {
                            dao.Delete<Ticket>(ticket.TicketID);
                        }
                        // saleitem = 3 = count
                    }
                    if (counSaleItemCommit > 0)
                    {
                        //if (listSaleItem.Count==0)
                        //{
                        //    dao.Delete<Ticket>(ticket.TicketID);      
                        //}
                        EntityFactory.getInstance().commit();

                    }
                    else
                    {
                        EntityFactory.getInstance().rollBack();
                    }

                }
            }

            //if (tableID != "")
            //    return 1;
            //Response.Redirect("default.aspx?tableid=" + tableID + "&type=modify");
            //else
            return totalAmount;
            //Response.Redirect("table.aspx");

        }
        catch (System.Exception ex)
        {
            ClsPublic.WriteException(ex);
            return 0;
        }
    }


    decimal[] loadExpressButtonCash(decimal Amount)
    {
        decimal[] list = new decimal[6];
        list[0] = Amount;
        list[1] = 5;
        if (Amount >= 1 && Amount <= 5)
        {
            list[2] = 5;
            list[3] = 10;
            list[4] = 20;
        }
        else if (Amount >= 5 && Amount <= 10)
        {
            list[2] = 10;
            list[3] = 20;
            list[4] = 50;
        }
        else if (Amount >= 10 && Amount <= 15)
        {
            list[1] = 15;
            list[2] = 20;
            list[3] = 40;
            list[4] = 50;
        }
        else if (Amount >= 15 && Amount <= 20)
        {
            list[1] = 20;
            list[2] = 30;
            list[3] = 40;
            list[4] = 100;
        }
        else if (Amount >= 20 && Amount <= 30)
        {
            list[1] = 25;
            list[2] = 30;
            list[3] = 40;
            list[4] = 100;
        }
        else if (Amount >= 30 && Amount <= 100)
        {
            int temp = (int)Amount;
            int mod = (temp % 10) > 5 ? 10 : 5;
            list[1] = temp - (temp % 10) + mod;
            list[2] = list[1] + 5;
            list[3] = list[2] + 10;
            list[4] = 100;
        }
        else if (Amount > 100)
        {
            int temp = (int)Amount;
            int mod = (temp % 10) > 5 ? 10 : 5;
            list[1] = temp - (temp % 10) + mod;
            list[2] = list[1] + 5;
            list[3] = list[2] + 10;
            list[4] = 100;
        }
        return list;
    }


    protected void btnExpressCash1_Click(object sender, EventArgs e)
    {

    }
    protected void callBackCash_Init(object sender, EventArgs e)
    {
        callBackCash.JSProperties["cpOK"] = "";
        callBackCash.JSProperties["cpbtnCheckOutCashLabel"] = "";
        callBackCash.JSProperties["cpbtnExpressCash1"] = "";
        callBackCash.JSProperties["cpbtnExpressCash2"] = "";
        callBackCash.JSProperties["cpbtnExpressCash3"] = "";
        callBackCash.JSProperties["cpbtnExpressCash4"] = "";
        callBackCash.JSProperties["cpbtnExpressCash5"] = "";
    }
}