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
        Employee currentEmp = (Employee)Session["LoginEmp"];
        Dao dao = new Dao();
        gridDictionary.DataSource = dao.GetAll<Error>().OrderByDescending(x=>x.ErrorDate);
        gridDictionary.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmError;
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


}