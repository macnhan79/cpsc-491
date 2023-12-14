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

public partial class UsersManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"]==null)
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
        Employee currentEmp = (Employee)Session["LoginEmp"];
        Dao dao = new Dao();
        PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);

        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Employees e left join PhoHa7_Sys_Role r on e.SecureLevel = r.Role_ID where r.Role_Level > @RoleLevel order by EmployeeID desc";
        gridUsersManagement.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@RoleLevel" }, new object[] { currentRole.Role_Level });
        gridUsersManagement.DataBind();
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
            loadUsers();
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
            loadUsers();
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
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }


}