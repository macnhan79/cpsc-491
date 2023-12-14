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
        PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);

        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        gridDictionary.DataSource = dao.GetAll<Dictionary>();
        //string sql = "select * from Employees e left join PhoHa7_Sys_Role r on e.SecureLevel = r.Role_ID where r.Role_Level > @RoleLevel order by EmployeeID desc";
        //gridDictionary.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@RoleLevel" }, new object[] { currentRole.Role_Level });
        gridDictionary.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmDictionary;
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
            Dictionary dic = new Dictionary();

            dic.Dic_Keyword = e.NewValues["Dic_Keyword"] + string.Empty;
            dic.Dic_SourceLanguage = e.NewValues["Dic_SourceLanguage"] + string.Empty;
            dic.Dic_TargetLanguage = e.NewValues["Dic_TargetLanguage"] + string.Empty;

            //check double security code
            var tempEmp = dao.FindByMultiColumnAnd<Dictionary>(new[] { "Dic_Keyword" }, dic.Dic_Keyword);
            if (tempEmp.Count > 0)
            {
                throw new Exception("Keyword has already been used.");
            }

            dao.Add<Dictionary>(dic);

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
            string keyword = (string)e.Keys["Dic_Keyword"];
            Dao dao = new Dao();
            Dictionary dic = dao.GetById<Dictionary>(keyword);

            //Employee currentEmp = (Employee)Session["LoginEmp"];
            //PhoHa7_Sys_Role currentRole = dao.GetById<PhoHa7_Sys_Role>(currentEmp.SecureLevel);



            //PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(emp.SecureLevel);
            //if (currentRole.Role_Level > newRole.Role_Level)
            //{
            //    throw new Exception("Your permission level settings prevent you from accessing this user.");
            //}


            dic.Dic_Keyword = keyword;
            dic.Dic_SourceLanguage = e.NewValues["Dic_SourceLanguage"] + string.Empty;
            dic.Dic_TargetLanguage = e.NewValues["Dic_TargetLanguage"] + string.Empty;
            //check double keyword
            var tempEmp = dao.FindByMultiColumnAnd<Dictionary>(new[] { "Dic_Keyword" }, keyword);
            if (tempEmp.Count > 0)
            {
                //foreach (var item in tempEmp)
                //{
                //    if (item.EmployeeID != emp.EmployeeID)
                //    {
                throw new Exception("Keyword has already been used.");
                //    }
                //}
            }


            //bool active = Convert.ToBoolean(e.NewValues["Active"] == null ? false : e.NewValues["Active"]);
            //emp.Active = active;

            //emp.Administrator = Convert.ToBoolean(e.NewValues["Administrator"] == null ? false : e.NewValues["Administrator"]);
            //UserPresenter userPresenter = new UserPresenter();
            //userPresenter.Update(emp, emp.EmployeeID);

            dao.Update<Dictionary>(dic);

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
            string keyword = (string)e.Keys["Dic_Keyword"];
            Dictionary dic = dao.GetById<Dictionary>(keyword);


            

            Entities obj = EntityFactory.getInstance().CreateEntities();

            obj.Dictionaries.Remove(obj.Dictionaries.FirstOrDefault(p => p.Dic_Keyword == keyword));
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