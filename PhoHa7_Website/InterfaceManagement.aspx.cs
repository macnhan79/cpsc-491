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

        gridInterface.DataSource = dao.GetAll<PhoHa7_Sys_Object>();
        gridInterface.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmObject;
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

    protected void gridCategoriesTab_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            PhoHa7_Sys_Object obj = new PhoHa7_Sys_Object();

            obj.Syso_ID = e.NewValues["Syso_ID"] + string.Empty;
            obj.Syso_Name = e.NewValues["Syso_Name"] + string.Empty;
            obj.Syso_NameEn = e.NewValues["Syso_NameEn"] + string.Empty;
            obj.Syso_Description = e.NewValues["Syso_Description"] + string.Empty;
            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);

            //check null
            if (obj.Syso_ID==string.Empty)
            {
                throw new Exception("Please enter ID");
            }
            if (obj.Syso_Name==string.Empty)
            {
                throw new Exception("Please enter name");
            }
            //check double security code
            var tempObj = dao.GetById<PhoHa7_Sys_Object>(obj.Syso_ID);
            if (tempObj!=null)
            {
                throw new Exception("ID has already been used.");
            }
            obj.Syso_Actived = active;

            dao.Add<PhoHa7_Sys_Object>(obj);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridCategoriesTab_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string empID = (string)e.Keys["Syso_ID"];
            Dao dao = new Dao();
            PhoHa7_Sys_Object obj = dao.GetById<PhoHa7_Sys_Object>(empID);

            obj.Syso_ID = e.NewValues["Syso_ID"] + string.Empty;
            obj.Syso_Name = e.NewValues["Syso_Name"] + string.Empty;
            obj.Syso_NameEn = e.NewValues["Syso_NameEn"] + string.Empty;
            obj.Syso_Description = e.NewValues["Syso_Description"] + string.Empty;
            bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            obj.Syso_Actived = active;


            //check null
            if (obj.Syso_ID == string.Empty)
            {
                throw new Exception("Please enter ID");
            }
            if (obj.Syso_Name == string.Empty)
            {
                throw new Exception("Please enter name");
            }
            //check double id
            //var tempObj = dao.GetById<PhoHa7_Sys_Object>(obj.Syso_ID);
            //if (tempObj != null)
            //{
            //    throw new Exception("ID has already been used.");
            //}

            dao.Update<PhoHa7_Sys_Object>(obj);
            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }
    protected void gridCategoriesTab_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            string id = (string)e.Keys["Syso_ID"];
            PhoHa7_Sys_Object obj = dao.GetById<PhoHa7_Sys_Object>(id);

            Entities entity = EntityFactory.getInstance().CreateEntities();
            entity.PhoHa7_Sys_Object.Remove(entity.PhoHa7_Sys_Object.FirstOrDefault(p => p.Syso_ID == id));
            entity.SaveChanges();

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