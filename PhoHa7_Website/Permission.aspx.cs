using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using PhoMac.Model.Presenter.Sys;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoHa7.Library.Enum;
using PhoMac.Model.Presenter.Permission;

public partial class Permission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!checkPermissionUser(EnumFormStatus.View))
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            loadUsers();
            loadPermission(0);
        }
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmPermissionUser;
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

    void loadUsers()
    {
        UserPresenter userPresenter = new UserPresenter();
        gridUsersPermission.DataSource = userPresenter.GetAll();
        if (gridUsersPermission.VisibleRowCount>0)
        {
            gridUsersPermission.Selection.SelectRow(0);
        }
        
        gridUsersPermission.DataBind();
    }

    void loadPermission(int empID)
    {
        Dao dao = new Dao(true, false);
        var listModule = dao.GetAll<PhoHa7_Sys_Object>();
        var listPermission = dao.FindByMultiColumnAnd<PhoHa7_Sys_User_Permission>(new[] { "user" }, empID);
        List<PhoHa7_Sys_User_Permission> listPermissions = (from p in listModule
                                                            join p1 in listPermission on p.Syso_ID equals p1.UP_Object_ID into Inners
                                                            from i in Inners.DefaultIfEmpty(new PhoHa7_Sys_User_Permission() { UP_Permission = 0 })
                                                            select new PhoHa7_Sys_User_Permission()
                                                            {
                                                                UP_Object_ID = p.Syso_ID,
                                                                UP_User_ID = empID,
                                                                UP_Permission = i.UP_Permission,
                                                                UP_Object_Parent_ID = p.Syso_Parent_ID,
                                                                UP_Object_Name = p.Syso_Name
                                                            }).ToList();
        gridPermission.DataSource = listPermissions;
        gridPermission.DataBind();
    }

    protected void gridPermission_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //int empID = Convert.ToInt32(e.Parameters);
        refreshGridPermission();
    }

    private void refreshGridPermission()
    {
        if (gridUsersPermission.Selection.Count > 0)
        {
            List<object> list = gridUsersPermission.GetSelectedFieldValues("EmployeeID");
            int empID = (int)gridUsersPermission.GetSelectedFieldValues("EmployeeID")[0];
            loadPermission(empID);
        }
    }
    protected void gridPermission_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        PhoHa7_Sys_User_Permission empPermission = new PhoHa7_Sys_User_Permission();

        empPermission.UP_Object_ID = (string)e.Keys["UP_Object_ID"];
        empPermission.UP_User_ID = (int)e.Keys["UP_User_ID"];
        empPermission.View = (bool)e.NewValues["View"];
        empPermission.Add = (bool)e.NewValues["Add"];
        empPermission.Update = (bool)e.NewValues["Update"];
        empPermission.Delete = (bool)e.NewValues["Delete"];
        empPermission.Report = (bool)e.NewValues["Report"];

        Dao dao = new Dao(true, false);
        dao.AddOrUpdate<PhoHa7_Sys_User_Permission>(empPermission);
        gridView.CancelEdit();
        e.Cancel = true;
        refreshGridPermission();
    }
}