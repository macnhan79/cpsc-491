using DevExpress.Web.ASPxGridView;
using PhoHa7.Library.Enum;
using PhoMac.Model;
using PhoMac.Model.Presenter.Permission;
using PhoMac.Model.Presenter.Sys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;

public partial class UsersManagementAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }

            loadUsers();

        
        if (!checkPermissionUser(EnumFormStatus.View))
        {
            Response.Redirect("Default.aspx");
        }
    }

    void loadUsers()
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Employees order by EmployeeID desc";
        gridUsersManagementAdmin.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
        gridUsersManagementAdmin.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmEmployeeAdmin;
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

    protected void gridUsersManagementAdmin_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();

            //string SecureLevel = e.NewValues["SecureLevel"] + string.Empty;
            Employee emp = new Employee();
            Employee currentEmp = (Employee)Session["LoginEmp"];
            PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);


            emp.FullName = e.NewValues["FullName"] + string.Empty;

            if (e.NewValues["SecureLevel"] == null || e.NewValues["SecureLevel"].ToString() == "")
            {
                throw new Exception("Please choose level.");
            }
            emp.SecureLevel = Convert.ToInt32(e.NewValues["SecureLevel"]);
            PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(emp.SecureLevel);
            if (currentRole.Role_Level > newRole.Role_Level)
            {
                throw new Exception("Your permission level settings prevent you from accessing this user.");
            }


            //check double security code
            emp.SecurityCode = e.NewValues["SecurityCode"] + string.Empty;
            var tempEmp = dao.FindByMultiColumnAnd<Employee>(new[] { "SecurityCode" }, emp.SecurityCode);
            if (tempEmp.Count > 0)
            {
                throw new Exception("Passcode has already been used.");
            }


            //rate pay
            decimal rate = 0;
            decimal.TryParse(e.NewValues["Rate"].ToString(), out rate);
            emp.Rate = rate;
            //rate pay check
            decimal checkPayRate = 0;
            decimal.TryParse(e.NewValues["CheckPayRate"].ToString(), out checkPayRate);
            emp.CheckPayRate = checkPayRate;
            //active
            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            emp.Active = active;
            //admin
            //bool admin = Convert.ToBoolean(e.NewValues["Administrator"] == null ? false : e.NewValues["Active"]);
            emp.Administrator = false;


            dao.Add<Employee>(emp);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }
    protected void gridUsersManagementAdmin_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int empID = (int)e.Keys["EmployeeID"];
            Dao dao = new Dao();
            Employee emp = dao.GetById<Employee>(empID);
            Employee currentEmp = (Employee)Session["LoginEmp"];
            PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);


            emp.FullName = e.NewValues["FullName"] + string.Empty;
            if (e.NewValues["SecureLevel"] == null || e.NewValues["SecureLevel"].ToString() == "")
            {
                throw new Exception("Please choose level.");
            }
            emp.SecureLevel = Convert.ToInt32(e.NewValues["SecureLevel"]);
            PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(emp.SecureLevel);
            if (currentRole.Role_Level > newRole.Role_Level)
            {
                throw new Exception("Your permission level settings prevent you from accessing this user.");
            }
            string securityCode = e.NewValues["SecurityCode"] + string.Empty;
            if (securityCode != "")
            {
                emp.SecurityCode = e.NewValues["SecurityCode"] + string.Empty;
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
            }
            

            //emp.EmployeeID = (int)e.Keys["EmployeeID"];
            //rate pay
            decimal rate = 0;
            decimal.TryParse(e.NewValues["Rate"].ToString(), out rate);
            emp.Rate = rate;
            //rate pay check
            decimal checkPayRate = 0;
            decimal.TryParse(e.NewValues["CheckPayRate"].ToString(), out checkPayRate);
            emp.CheckPayRate = checkPayRate;
            //active
            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            emp.Active = active;
            //admin
            //bool admin = Convert.ToBoolean(e.NewValues["Administrator"] == null ? false : e.NewValues["Administrator"]);
            emp.Administrator = false;
            //UserPresenter userPresenter = new UserPresenter();
            //userPresenter.Update(emp, emp.EmployeeID);

            dao.Update<Employee>(emp);

            //update rate and checkrate
            decimal oldRate = 0;
            decimal oldCheckPayRate = 0;
            decimal.TryParse(e.OldValues["Rate"].ToString(), out oldRate);
            decimal.TryParse(e.OldValues["CheckPayRate"].ToString(), out oldCheckPayRate);
            string oldName = e.OldValues["FullName"].ToString();

            int month = DateTime.Today.Month;
            int year = DateTime.Today.Year;
            if (oldRate != rate || oldCheckPayRate != checkPayRate || oldName != emp.FullName)
            {
                var listAttendance = dao.FindByMultiColumnAnd<PhoHa7_Attendance>(new[] { "Att_EmployeeID", "Att_Month", "Att_Year" }, emp.EmployeeID, month, year);
                foreach (var item in listAttendance)
                {
                    item.Att_Rate = rate;
                    item.Att_AmountCheck = checkPayRate;
                    item.Att_AmountTotal = (decimal)item.totalAmount();
                    item.Att_AmountCash = (decimal)item.totalCash();
                    item.Att_EmployeeName = emp.FullName;
                    dao.Update<PhoHa7_Attendance>(item);
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            //gridView.DataSource = userPresenter.GetAll();
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }
    protected void gridUsersManagementAdmin_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["EmployeeID"];
            //  Employee emp = new Employee();
            Employee emp = dao.GetById<Employee>(id);
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
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridUsersManagementAdmin_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
    {
        if (e.FieldName.Equals("Rate"))
        {
            double rate = 0;
            if (e.Value == null || !double.TryParse(e.Value.ToString(), out rate))
                throw new Exception("Pay Rate must be a number.");
        }
        else if (e.FieldName.Equals("CheckPayRate"))
        {
            double rate = 0;
            if (e.Value == null || !double.TryParse(e.Value.ToString(), out rate))
                throw new Exception("Pay Rate Check must be a number.");
        }
    }

}