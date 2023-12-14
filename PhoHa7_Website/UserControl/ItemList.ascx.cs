using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxDataView;
using PhoMac.Model.Data;
using PhoMac.Model;

public partial class UserControl_ItemList : System.Web.UI.UserControl
{
    int categoryid;
    public int CategoryId
    {
        get { return categoryid; }
        set { categoryid = value; }
    }

    string mType;
    public string MType
    {
        get { return mType; }
        set { mType = value; }
    }
    string flag;
    public string Flag
    {
        get { return flag; }
        set { flag = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {


    }

    void addItem(int productID)
    {
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];
        //get product ID
        //Button btnAdd = (Button)sender;
        //int productID = Convert.ToInt32(btnAdd.CommandArgument);
        //get list order from cache
        DataTable dt = ((ManagerOrder)Session[managerOrder.TableID.ToString()]).ListItems;


        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Products where ProductID = @ProductID";
        //get product from database
        DataTable productRow = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { productID });
        // string itemName = productRow.Rows[0]["KitchenName"].ToString();
        if (productRow.Rows.Count > 0)
        {
            //add new row to DataTable
            DataRow dr = dt.NewRow();
            dr["ProductName"] = productRow.Rows[0]["ProductName"];
            dr["CustomKitchenName"] = productRow.Rows[0]["ProductName"];
            dr["Qty"] = 1;
            dr["BarCode"] = productRow.Rows[0]["BarCode"];
            dr["ProductID"] = Convert.ToInt32(productRow.Rows[0]["ProductID"]);
            dr["KitchenName"] = productRow.Rows[0]["KitchenName"];
            dr["CategoryID"] = productRow.Rows[0]["CategoryID"];
            dr["LPrice"] = productRow.Rows[0]["Price"];
            dr["SPrice"] = productRow.Rows[0]["CPrice"];
            dr["OptionRequire"] = "";
            dr["Extra"] = 0;
            dr["SmallSize"] = false;
            dr["TakeOut"] = false;
            dr["EmployeeName"] = "";
            dr["Status"] = EnumFormStatus.Add;
            dr["MType"] = Convert.ToInt32(productRow.Rows[0]["MType"]);

            dt.Rows.Add(dr);
            dt.AcceptChanges();
            //save to Session
            ((ManagerOrder)Session[Request.QueryString["tableid"]]).ListItems = dt;
        }
    }

    void addExtraItem(string productBarCode)
    {
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];

        DataTable dt = ((ManagerOrder)Session[managerOrder.TableID.ToString()]).ItemModify;
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select * from Products where BarCode = @BarCode";
        //get product from database
        DataTable productRow = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@BarCode" }, new object[] { productBarCode });

        string extraName = dt.Rows[0]["ExtraName"] + string.Empty;
        if (extraName == "")
        {
            dt.Rows[0]["ExtraName"] = dt.Rows[0]["ExtraName"].ToString() + productRow.Rows[0]["KitchenName"];
            dt.Rows[0]["ExtraPrice"] = dt.Rows[0]["ExtraPrice"].ToString() + productRow.Rows[0]["Price"];
        }
        else
        {
            dt.Rows[0]["ExtraName"] = dt.Rows[0]["ExtraName"].ToString() + "|" + productRow.Rows[0]["KitchenName"];
            dt.Rows[0]["ExtraPrice"] = dt.Rows[0]["ExtraPrice"].ToString() + "|" + productRow.Rows[0]["Price"];
        }


        dt.Rows[0]["Extra"] = Convert.ToDouble(dt.Rows[0]["Extra"]) + Convert.ToDouble(productRow.Rows[0]["Price"]);
        if (Request.QueryString["type"].Equals("modify"))
        {
            dt.Rows[0]["Status"] = EnumFormStatus.Modify;
        }
        // break;
        // }
        //}
        dt.AcceptChanges();
        managerOrder.ItemModify = dt;
        Session[managerOrder.TableID.ToString()] = managerOrder;
    }


    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@CategoryID"].Value = CategoryId;
        e.Command.Parameters["@MType"].Value = MType;
    }

    protected void DataView_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //get details list order
        ManagerOrder managerOrder = (ManagerOrder)Session[Request.QueryString["tableid"]];
        string ProductBarCode = e.Parameter;
        try
        {
            dataView.JSProperties["cpRefeshGrid"] = ProductBarCode;
        }
        catch (System.Exception ex)
        {
            dataView.JSProperties["cpRefeshGrid"] = "";
            dataView.JSProperties["cpRefeshListBox"] = "";
        }



    }

    protected void DataView_Init(object sender, EventArgs e)
    {
        ASPxDataView view = sender as ASPxDataView;

        string[] arrMType = MType.Split(',');
        string sql = "SELECT [ProductID], [ProductName], [CategoryID], [Active], [Price], [MType], [CPrice],[KitchenName],[BarCode],[ProductImage] " +
        "FROM [Products] where CategoryID = " + CategoryId + " and (";
        for (int i = 0; i < arrMType.Length; i++)
        {
            if (i > 0)
            {
                sql += " or ";
            }
            sql += " MType =  " + arrMType[i];
        }
        sql += ") order by orderby";
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataTable productRow = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null,null);
        view.DataSource = productRow;
        
        view.JSProperties["cpRefeshGrid"] = "";
        view.JSProperties["cpRefeshListBox"] = "";
        view.JSProperties["cpSubmitAddExtra"] = "";
        view.DataBind();
    }

}