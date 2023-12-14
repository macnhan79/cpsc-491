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
using DevExpress.Web.ASPxCallback;

public partial class TicketManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            DateTime date = DateTime.Today;
            dateTicket.Date = date;
            loadUsers(date);
        }
        else
        {
            DateTime date =dateTicket.Date ;
            dateTicket.Date = date;
            loadUsers(date);
        }
        //else
        //{
            //loadUsers();
        //}

        //if (!checkPermissionUser(EnumFormStatus.View))
        //{
        //    Response.Redirect("Default.aspx");
        //}
    }

    void loadUsers(DateTime date)
    {
        Employee currentEmp = (Employee)Session["LoginEmp"];
        Dao dao = new Dao();
        PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);

        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DateTime dateStart = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        DateTime dateEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        string sql = "select * from Tickets where DateTimeIssue >= @DateStart and DateTimeIssue <= @DateEnd order by DateTimeIssue desc";
        gridTicket.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@DateStart", "@DateEnd" }, new object[] { dateStart, dateEnd });
        gridTicket.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmEmployee;
        Employee emp = (Employee)Session["LoginEmp"];
        if (emp != null)
        {
            SystemPermission permission = new SystemPermission(FormCode.ToString(), emp.EmployeeID);
            if (status == EnumFormStatus.View)
                return permission.PermissionView();
            else if (status == EnumFormStatus.Add)
                return permission.PermissionAdd();
            else if (status == EnumFormStatus.Modify)
                return permission.PermissionUpdate();
            else if (status == EnumFormStatus.Delete)
                return permission.PermissionDelete();
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    protected void gridUsersManagement_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            Employee emp = new Employee();

            emp.FullName = e.NewValues["FullName"] + string.Empty;
            emp.SecurityCode = e.NewValues["SecurityCode"] + string.Empty;
            emp.SecureLevel = 4;

            //check double security code
            var tempEmp = dao.FindByMultiColumnAnd<Employee>(new[] { "SecurityCode" }, emp.SecurityCode);
            if (tempEmp.Count > 0)
            {
                throw new Exception("Passcode has already been used.");
            }

            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            emp.Active = active;
            emp.Administrator = false;



            dao.Add<Employee>(emp);

            gridView.CancelEdit();
            e.Cancel = true;
            //loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridUsersManagement_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int empID = (int)e.Keys["EmployeeID"];
            Dao dao = new Dao();
            Employee emp = dao.GetById<Employee>(empID);
            Employee currentEmp = (Employee)Session["LoginEmp"];
            PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);



            PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(emp.SecureLevel);
            if (currentRole.Role_Level > newRole.Role_Level)
            {
                throw new Exception("Your permission level settings prevent you from accessing this user.");
            }



            emp.FullName = e.NewValues["FullName"] + string.Empty;
            string newPass = e.NewValues["SecurityCode"] + string.Empty;
            if (newPass != "")
            {
                emp.SecurityCode = newPass;
            }
            //check double security code
            var tempEmp = dao.FindByMultiColumnAnd<Employee>(new[] { "SecurityCode" }, emp.SecurityCode);
            if (tempEmp.Count > 0)
            {
                foreach (var item in tempEmp)
                {
                    if (item.EmployeeID != emp.EmployeeID)
                    {
                        throw new Exception("Passcode has already been used.");
                    }
                }
            }


            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            emp.Active = active;

            //emp.Administrator = Convert.ToBoolean(e.NewValues["Administrator"] == null ? false : e.NewValues["Administrator"]);
            //UserPresenter userPresenter = new UserPresenter();
            //userPresenter.Update(emp, emp.EmployeeID);

            dao.Update<Employee>(emp);

            string oldName = e.OldValues["FullName"].ToString();
            if (oldName != emp.FullName)
            {
                var listAttendance = dao.FindByMultiColumnAnd<PhoHa7_Attendance>(new[] { "Att_EmployeeID" }, emp.EmployeeID);
                foreach (var item in listAttendance)
                {
                    item.Att_EmployeeName = emp.FullName;
                    dao.Update<PhoHa7_Attendance>(item);
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            //loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }
    protected void gridUsersManagement_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["EmployeeID"];
            Employee emp = dao.GetById<Employee>(id);


            //check role level
            Employee currentEmp = (Employee)Session["LoginEmp"];
            PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);
            PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(emp.SecureLevel);
            if (currentRole.Role_Level > newRole.Role_Level)
            {
                throw new Exception("Your permission level settings prevent you from accessing this user.");
            }

            Entities obj = EntityFactory.getInstance().CreateEntities();
            var attendanceList = dao.FindByMultiColumnAnd<PhoHa7_Attendance>(new string[] { "Att_EmployeeID" }, id);
            if (attendanceList.Count > 0)
            {
                foreach (var item in attendanceList)
                {
                    obj.PhoHa7_Attendance.Remove(obj.PhoHa7_Attendance.FirstOrDefault(p => p.Att_AttendanceID == item.Att_AttendanceID));
                    obj.SaveChanges();
                }
            }

            obj.Employees.Remove(obj.Employees.FirstOrDefault(p => p.EmployeeID == id));
            obj.SaveChanges();
            //dao.Delete<Employee>(emp.EmployeeID);

            gridView.CancelEdit();
            e.Cancel = true;
            //loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }


    protected void dateTicket_DateChanged(object sender, EventArgs e)
    {
        DateTime date = DateTime.Today;
        date = dateTicket.Date;
        loadUsers(date);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        dateTicket.Date = dateTicket.Date.AddDays(1);
        loadUsers(dateTicket.Date);
    }
    protected void btnToday_Click(object sender, EventArgs e)
    {
        dateTicket.Date = DateTime.Today;
        loadUsers(dateTicket.Date);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        dateTicket.Date = dateTicket.Date.AddDays(-1);
        loadUsers(dateTicket.Date);
    }
    protected void btnTicketID_Click(object sender, EventArgs e)
    {
        string abc = "a";
    }
    protected void gridReviewOrder_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        string ticketID = e.Parameters;
        if (ticketID == "PrintBill")
        {
            string tID = hiddenTicketID["ticketID"].ToString();
            SocketClient client = new SocketClient();
            //ManagerOrder obj = (ManagerOrder)Session[Request.QueryString["tableid"]];
            client.ConnectAndSendMsg("127.0.0.1", 8888, "printReceipt|Ticket|" + tID);
        }
        if (ticketID == "DeleteTicket")
        {
            Employee currentEmp = (Employee)Session["LoginEmp"];
            int tID = Convert.ToInt32(hiddenTicketID["ticketID"].ToString());
            Dao dao = new Dao();

            Ticket t = dao.GetById<Ticket>(tID);
            var listSaleItem = dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, tID);
            using (var context = new PhoMac.Model.Entities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    foreach (var item in listSaleItem)
                    {
                        context.SaleItems.Attach(item);
                        item.TPrice = 0;
                        item.SPrice = 0;
                        item.Description = "Void by " + currentEmp.FullName + " - " + item.Description;
                        context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    t.PaidCash = 0;
                    t.PaidCredit = 0;
                    t.Tips = 0;
                    t.Voided = true;
                    context.Tickets.Attach(t);
                    context.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
        }
        else
        {
            Dao dao = new Dao();
            var saleItems = dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketID));
            var ticket = dao.GetById<Ticket>(Convert.ToInt32(ticketID));
            gridReviewOrder.JSProperties["cpTableName"] = "Table " + ticket.TableName;
            if (saleItems != null)
            {
                gridReviewOrder.DataSource = saleItems;
                gridReviewOrder.DataBind();


                gridReviewOrder.JSProperties["cplblSubTotal"] = string.Format("{0:C}", ticket.TotalP);
                gridReviewOrder.JSProperties["cplblSaleTax"] = string.Format("{0:C}", ticket.Tax);
                gridReviewOrder.JSProperties["cplblTip"] = string.Format("{0:C}", ticket.Tips);
                gridReviewOrder.JSProperties["cplblTotal"] = string.Format("{0:C}", ticket.PaidCash + ticket.PaidCredit+ ticket.Tips);
                gridReviewOrder.JSProperties["cplblCash"] = string.Format("{0:C}", ticket.PaidCash);
                gridReviewOrder.JSProperties["cplblCredit"] = string.Format("{0:C}", ticket.PaidCredit);
                gridReviewOrder.JSProperties["cplblCreditCode"] = ticket.CardCode;
            }
        }
        
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
                    //decimal totalAmount = completeCash();
                    //if (totalAmount > 0)
                    //{
                    //    callBackCash.JSProperties["cpOK"] = "OK";
                    //    callBackCash.JSProperties["cpCashChange"] = (amount - totalAmount).ToString("c2");
                    //    //callBackCash.JSProperties["cpCashChangeTicketID"] = obj.TicketID;
                    //    openDrawer();
                    //}
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


    protected void callBackPayButton_Init(object sender, EventArgs e)
    {
        ASPxCallback calBack = sender as ASPxCallback;
        calBack.JSProperties["cpOK"] = "";
    }
























}