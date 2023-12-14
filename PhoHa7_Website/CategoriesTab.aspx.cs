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
using DevExpress.Web.ASPxEditors;

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
        ICollection<Category> listCat = dao.GetAll<Category>();
        ICollection<PhoHa7_ProductType> listType = dao.GetAll<PhoHa7_ProductType>();
        foreach (var itemCat in listCat)
        {
            string WebShowType = itemCat.WebShowType + string.Empty;
            string[] arrItemCat = WebShowType.Split(',');
            foreach (var itemArrItemCat in arrItemCat)
            {
                foreach (var itemType in listType)
                {
                    string target = itemArrItemCat + string.Empty;
                    string source = itemType.ProductTypeCode + string.Empty;
                    if (target == source)
                    {
                        itemCat.WebShowTypeName += itemType.ProductTypeName + ",";
                        break;
                    }
                }
            }

        }
        gridTabItems.DataSource = listCat;
        gridTabItems.DataBind();
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmTabItems;
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

    protected void gridTabItems_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Add))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            Dao dao = new Dao();
            Category cat = new Category();

            cat.CategoryName = e.NewValues["CategoryName"] + string.Empty;
            cat.WebName = e.NewValues["WebName"] + string.Empty;
            cat.WebShowType = e.NewValues["WebShowType"] + string.Empty;
            cat.WebOrderBy = Convert.ToInt32(e.NewValues["WebOrderBy"] == null ? 0 : e.NewValues["WebOrderBy"]);
            cat.WebFlag = e.NewValues["WebFlag"] + string.Empty;
            cat.WebActive = Convert.ToBoolean(e.NewValues["WebActive"] == null ? false : e.NewValues["WebActive"]);

            //check null
            if (cat.CategoryName == string.Empty)
            {
                throw new Exception("Please enter category name");
            }
            if (cat.WebName == string.Empty)
            {
                throw new Exception("Please enter web name");
            }
            if (cat.WebShowType == string.Empty)
            {
                throw new Exception("Please enter type");
            }

            dao.Add<Category>(cat);

            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }
    }

    protected void gridTabItems_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Modify))
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["CategoryID"];
            Dao dao = new Dao();

            Category cat = dao.GetById<Category>(id);
            cat.CategoryID = id;
            cat.CategoryName = e.NewValues["CategoryName"] + string.Empty;
            cat.WebName = e.NewValues["WebName"] + string.Empty;
            cat.WebShowType = e.NewValues["WebShowType"] + string.Empty;
            

            string cbH = cbHidden.Value;
            cat.WebShowType = cbH;
            // var item = list.SelectedValues;

            cat.WebOrderBy = Convert.ToInt32(e.NewValues["WebOrderBy"] == null ? 0 : e.NewValues["WebOrderBy"]);
            cat.WebFlag = e.NewValues["WebFlag"] + string.Empty;
            cat.WebActive = Convert.ToBoolean(e.NewValues["WebActive"] == null ? false : e.NewValues["WebActive"]);


            //check null
            if (cat.CategoryName == string.Empty)
            {
                throw new Exception("Please enter category name");
            }
            if (cat.WebName == string.Empty)
            {
                throw new Exception("Please enter web name");
            }
            if (cat.WebShowType == string.Empty)
            {
                throw new Exception("Please enter type");
            }

            dao.Update<PhoMac.Model.Category>(cat);



            gridView.CancelEdit();
            e.Cancel = true;
            loadUsers();
        }
        else
        {
            throw new Exception("You do NOT have permission.");
        }

    }

    protected void gridTabItems_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (checkPermissionUser(EnumFormStatus.Delete))
        {
            Dao dao = new Dao();
            ASPxGridView gridView = (ASPxGridView)sender;
            int id = (int)e.Keys["CategoryID"];

            Entities obj = EntityFactory.getInstance().CreateEntities();

            obj.Categories.Remove(obj.Categories.FirstOrDefault(p => p.CategoryID == id));
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

    private ASPxDropDownEdit dropDown;
    protected void gridTabItems_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "WebShowType")
            dropDown = (e.Editor as ASPxDropDownEdit);
    }

    protected void lbOnInit_Init(object sender, EventArgs e)
    {
        ASPxGridView lb = (sender as ASPxGridView);
        Dao dao = new Dao();
        lb.DataSource = dao.GetAll<PhoHa7_ProductType>();
        lb.DataBind();
    }

    protected void UserList_Init(object sender, EventArgs e)
    {
        ASPxListBox combo = (ASPxListBox)sender;
        Dao dao = new Dao();
        combo.DataSource = dao.GetAll<PhoHa7_ProductType>();
        combo.TextField = "ProductTypeName";
        combo.ValueField = "ProductTypeCode";
        combo.DataBind();
    }

    protected void listBox_DataBound(object sender, EventArgs e)
    {
        var listBox = (ASPxListBox)sender;

        int editingRowVisibleIndex = gridTabItems.EditingRowVisibleIndex;
        string rowValue = gridTabItems.GetRowValues(editingRowVisibleIndex, "WebShowType") + string.Empty;
        string[] rowValueItems = rowValue.Split(';');

        List<string> rowValueItemsAsList = new List<string>();
        rowValueItemsAsList.AddRange(rowValueItems);

        string text = "";
        foreach (ListEditItem item in listBox.Items)
        {
            if (rowValueItemsAsList.Contains(item.Value.ToString()))
            {
                item.Selected = true;
                text += item.Text + ",";
            }
        }
        if (dropDown != null)
        {
            dropDown.Text = text;
        }

    }
}