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
        bool valid = true;
        int id = 0;
        if (Request.QueryString["id"] == null)
        {
            valid = false;
        }
        else
        {
            try
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            catch (System.Exception ex)
            {
                valid = false;
            }
            Dao dao = new Dao();
            Product pro = dao.GetById<Product>(id);
            if (pro == null)
            {
                valid = false;
            }
        }

        if (!valid)
        {
            Response.Redirect("Products.aspx");
        }
    }

    void loadUsers()
    {
        Dao dao = new Dao();
        //product id
        int id = Convert.ToInt32(Request.QueryString["id"]);
        //load grid with

        gridWith.DataSource = dao.FindByMultiColumnAnd<Product>(new[] { "ExpandCategoryID", "MType" }, id, 99).OrderBy(p => p.OrderBy).ToList();
        gridWith.DataBind();

        //load grid without
        gridWithout.DataSource = dao.FindByMultiColumnAnd<Product>(new[] { "ExpandCategoryID", "MType" }, id, 98).OrderBy(p => p.OrderBy).ToList();
        gridWithout.DataBind();

        //load grid custom select
        gridCustom.DataSource = dao.FindByMultiColumnAnd<Product>(new[] { "ExpandCategoryID", "MType" }, id, 97).OrderBy(p => p.OrderBy).ToList();
        gridCustom.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.Enity;
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

    #region Grid With

    protected void gridWith_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            Product pro = new Product();
            

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 99;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;


            dao.Add<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridWith_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Dao dao = new Dao();
            Product pro = dao.GetById<Product>(id);

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 99;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;
            dao.Update<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }

    protected void gridWith_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Product emp = dao.GetById<Product>(id);


            Entities obj = EntityFactory.getInstance().CreateEntities();
            obj.Products.Remove(obj.Products.FirstOrDefault(p => p.ProductID == id));
            obj.SaveChanges();

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    #endregion

    #region Grid Without

    protected void gridWithout_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            Product pro = new Product();

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 98;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;


            dao.Add<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridWithout_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Dao dao = new Dao();
            Product pro = dao.GetById<Product>(id);

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 98;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;
            dao.Update<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }

    protected void gridWithout_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Product emp = dao.GetById<Product>(id);


            Entities obj = EntityFactory.getInstance().CreateEntities();
            obj.Products.Remove(obj.Products.FirstOrDefault(p => p.ProductID == id));
            obj.SaveChanges();

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    #endregion

    #region Grid Custom Select

    protected void gridCustom_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            Product pro = new Product();

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 97;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;


            dao.Add<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridCustom_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Dao dao = new Dao();
            Product pro = dao.GetById<Product>(id);

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.BarCode = "TT";
            //pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            //var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            //if (tempPro.Count > 0)
            //{
            //    foreach (var item in tempPro)
            //    {
            //        if (item.BarCode == pro.BarCode)
            //        {
            //            throw new Exception("BarCode has already been used.");
            //        }
            //    }
            //}
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);
            pro.MType = 97;
            int extendId = Convert.ToInt32(Request.QueryString["id"]);
            pro.ExpandCategoryID = extendId;

            dao.Update<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }

    protected void gridCustom_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Product emp = dao.GetById<Product>(id);


            Entities obj = EntityFactory.getInstance().CreateEntities();
            obj.Products.Remove(obj.Products.FirstOrDefault(p => p.ProductID == id));
            obj.SaveChanges();

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    #endregion




}