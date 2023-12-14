using DevExpress.Web.ASPxGridView;
using PhoHa7.Library.Enum;
using PhoMac.Model;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;
using PhoMac.Model.Presenter.Permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MachineManagement : System.Web.UI.Page
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
        string sql = "select * from PhoHa7_Machine order by MachineID desc";
        gridMachineManagement.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
        gridMachineManagement.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmMachineManagement;
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




    protected void gridMachineManagement_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();


            PhoHa7_Machine emp = new PhoHa7_Machine();
            emp.MachineIpAdress = e.NewValues["MachineIpAdress"] + string.Empty;
            emp.MachineName = e.NewValues["MachineName"] + string.Empty;

            bool active = Convert.ToBoolean(e.NewValues["MachineActive"] == null ? true : e.NewValues["MachineActive"]);
            emp.MachineActive = active;

            dao.Add<PhoHa7_Machine>(emp);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }
    protected void gridMachineManagement_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int machineID = (int)e.Keys["MachineID"];
            Dao dao = new Dao();
            PhoHa7_Machine emp = dao.GetById<PhoHa7_Machine>(machineID);

            emp.MachineIpAdress = e.NewValues["MachineIpAdress"] + string.Empty;
            emp.MachineName = e.NewValues["MachineName"] + string.Empty;
            bool active = Convert.ToBoolean(e.NewValues["MachineActive"] == null ? true : e.NewValues["MachineActive"]);
            emp.MachineActive = active;

            dao.Update<PhoHa7_Machine>(emp);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }
    protected void gridMachineManagement_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["MachineID"];
            PhoHa7_Machine machine = dao.GetById<PhoHa7_Machine>(id);

            Entities obj = EntityFactory.getInstance().CreateEntities();
            obj.PhoHa7_Machine.Remove(obj.PhoHa7_Machine.FirstOrDefault(p => p.MachineID == id));
            obj.SaveChanges();
            //EntityFactory.getInstance().CreateEntities().SaveChanges();
            //int count = dao.Delete<PhoHa7_Machine>(machine.MachineID);

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