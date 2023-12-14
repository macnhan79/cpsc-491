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
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxUploadControl;
using System.IO;

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

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsCallback && !IsPostBack)
            Session[SessionUploadedFileContent] = null;
    }

    void loadUsers()
    {
        Employee currentEmp = (Employee)Session["LoginEmp"];
        Dao dao = new Dao();

        gridProductsManagement.DataSource = dao.FindByMultiColumnAnd<Product>(new[] { "ExpandCategoryID" }, 0);
        gridProductsManagement.DataBind();
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


    #region Grid

    protected void gridProductsManagement_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            //int proID = (int)e.Keys["ProductID"];
            Dao dao = new Dao();
            Product pro = new Product();

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            if (tempPro.Count > 0)
            {
                foreach (var item in tempPro)
                {
                    if (item.BarCode == pro.BarCode)
                    {
                        throw new Exception("BarCode has already been used.");
                    }
                }
            }

            //save file
            if (Session[SessionUploadedFileContent] != null)
            {
                pro.ProductImage = "/image_products/" + pro.ProductID + Session[SessionUploadedFileExtension];
                uploadFile(pro.ProductID.ToString() + Session[SessionUploadedFileExtension] + string.Empty);

            }


            pro.Description = e.NewValues["KitchenName"] + string.Empty;
            pro.CategoryID = Convert.ToInt32(e.NewValues["CategoryID"] + string.Empty);
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Page = Convert.ToInt32(e.NewValues["Page"] + string.Empty);
            pro.Col = Convert.ToInt32(e.NewValues["Col"] + string.Empty);
            pro.Row = Convert.ToInt32(e.NewValues["Row"] + string.Empty);
            pro.Price = Convert.ToDecimal(e.NewValues["Price"] + string.Empty);
            pro.CPrice = Convert.ToDecimal(e.NewValues["CPrice"] + string.Empty);
            pro.ExpandCategoryID = 0;
            pro.Type = 0;
            pro.InItem = false;
            pro.Cost = 0;
            pro.InCatID = 0;
            pro.PrintBoth = false;
            pro.NoTax = false;
            pro.VendorID = 0;
            pro.MType = 0;
            pro.BPrice = 0;
            pro.LPrice = 0;
            pro.DPrice = 0;
            pro.Price1 = 0;
            pro.Price2 = 0;
            pro.KitchenName= e.NewValues["KitchenName"] + string.Empty;
            pro.OrderBy = 0;
            pro.ExpandCategoryID = 0;
            pro.SubLevel = 0;
            pro.ParentID = 0;

            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);


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

    protected void gridProductsManagement_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int proID = (int)e.Keys["ProductID"];
            Dao dao = new Dao();
            Product pro = dao.GetById<Product>(proID);

            pro.ProductName = e.NewValues["ProductName"] + string.Empty;
            pro.BarCode = e.NewValues["BarCode"] + string.Empty;
            //check double barcode
            var tempPro = dao.FindByMultiColumnAnd<Product>(new[] { "BarCode" }, pro.BarCode);
            if (tempPro.Count > 0)
            {
                foreach (var item in tempPro)
                {
                    if (item.BarCode == pro.BarCode)
                    {
                        if (item.ProductID != pro.ProductID)
                        {
                            throw new Exception("BarCode has already been used.");
                        }
                    }
                }
            }

            //save file
            if (Session[SessionUploadedFileContent] != null)
            {
                pro.ProductImage = "/image_products/" + pro.ProductID + Session[SessionUploadedFileExtension];
                uploadFile(pro.ProductID.ToString() + Session[SessionUploadedFileExtension] + string.Empty);

            }


            pro.KitchenName = e.NewValues["KitchenName"] + string.Empty;
            pro.CategoryID = Convert.ToInt32(e.NewValues["CategoryID"] + string.Empty);
            pro.OrderBy = Convert.ToInt32(e.NewValues["OrderBy"] + string.Empty);
            pro.Page = Convert.ToInt32(e.NewValues["Page"] + string.Empty);
            pro.Col = Convert.ToInt32(e.NewValues["Col"] + string.Empty);
            pro.Row = Convert.ToInt32(e.NewValues["Row"] + string.Empty);
            pro.Price = Convert.ToDecimal(e.NewValues["Price"] + string.Empty);
            pro.CPrice = Convert.ToDecimal(e.NewValues["CPrice"] + string.Empty);
            pro.PrintBoth = Convert.ToBoolean(e.NewValues["PrintBoth"] + string.Empty);
            pro.Active = Convert.ToBoolean(e.NewValues["Active"] == null ? true : e.NewValues["Active"]);

            using (Entities obj = new Entities())
            {
                obj.Configuration.LazyLoadingEnabled = false;
                obj.Configuration.ProxyCreationEnabled = false;
                obj.Entry<Product>(pro).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }
            //dao.Update<Product>(pro);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }

    protected void gridProductsManagement_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["ProductID"];
            Product pro = dao.GetById<Product>(id);
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

    #region upload file

    const string SessionUploadedFileContent = "UploadedFileContent";
    const string SessionUploadedFileExtension = "UploadedFileExtension";

    //upload complete
    protected void ASPxUploadControl1_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        if (e.IsValid)
        {
            Session[SessionUploadedFileContent] = e.UploadedFile.FileBytes;
            Session[SessionUploadedFileExtension] = Path.GetExtension(e.UploadedFile.FileName);
            e.CallbackData = e.UploadedFile.FileName;

            BinaryWriter Writer = null;
            try
            {
                byte[] file = e.UploadedFile.FileBytes;
                // Create a new stream to write to the file
                Writer = new BinaryWriter(File.OpenWrite(Server.MapPath("~/image_products/") + e.UploadedFile.FileName));

                // Writer raw data                
                Writer.Write(file);
                Writer.Flush();
                Writer.Close();
                //Session[SessionUploadedFileContent] = null;
                //Session[SessionUploadedFileExtension] = null;
            }
            catch
            {
            }
        }
    }

    //remove file from session
    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        //Session.Remove(SessionUploadedFileContent);

        //Session.Remove(SessionUploadedFileExtension);
        e.Result = "ok";
    }

    //save file
    protected bool uploadFile(string filename)
    {
        BinaryWriter Writer = null;
        try
        {
            byte[] file = (byte[])Session[SessionUploadedFileContent];
            // Create a new stream to write to the file
            Writer = new BinaryWriter(File.OpenWrite(Server.MapPath("~/image_products/") + filename));

            // Writer raw data                
            Writer.Write(file);
            Writer.Flush();
            Writer.Close();
            Session[SessionUploadedFileContent] = null;
            Session[SessionUploadedFileExtension] = null;
        }
        catch
        {
            //...
            return false;
        }

        return true;
    }

    #endregion








    protected void cbCategory_Init(object sender, EventArgs e)
    {
        ASPxComboBox cbCategory = (ASPxComboBox)sender;
        Dao dao = new Dao();
        cbCategory.DataSource = dao.GetAll<Category>().Where(p => p.Active == true);
        cbCategory.DataBind();
    }



    protected void UploadBtn_Click(object sender, EventArgs e)
    {

    }

    //protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    //{
    //    //if (FileUpload1.HasFile)
    //    //{
    //    //    hiddenImageName.Value = FileUpload1.FileName;
    //    //    uploadFile(FileUpload1);
    //    //}
    //    //if (FileUpload1.HasFile)
    //    //{
    //    //    lblMsg.Text = FileUpload1.FileName;
    //    //    hiddenImageName.Value = FileUpload1.FileName;
    //    //    Session[SessionUploadedFileContent] = FileUpload1.FileBytes;
    //    //    //uploadFile(FileUpload1);
    //    //}
    //}
}