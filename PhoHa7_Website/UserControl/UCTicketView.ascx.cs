using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using PhoHa7.Library.Classes.Connection;
using System.Data;
using DevExpress.Web.ASPxDataView;
using PhoMac.Model.Data;

public partial class UCTicketView : System.Web.UI.UserControl
{
    int categoryid;
    public int CategoryId
    {
        get { return categoryid; }
        set { categoryid = value; }
    }
    public object DataSource
    {
        get { return DataView.DataSource; }
        set
        {
            DataView.DataSource = value;
            DataView.DataBind();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@CategoryID"].Value = CategoryId;
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ASPxButton btn = (ASPxButton)sender;
        int tableID = Convert.ToInt32(btn.CommandName);
        string type = btn.CommandArgument;
        if (type == "new")
        {
            if (checkAvaibleTable(tableID))
            {
                Response.Redirect("default.aspx?tableid=" + tableID + "&type=modify");
            }
            else
            {
                Session[tableID.ToString()] = null;
            }
        }
        Response.Redirect("default.aspx?tableid=" + tableID + "&type=" + type);
    }

    bool checkAvaibleTable(int tableID)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select count(*) from ProcTickets where TableID = @TableID";
        int count = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TableID" }, new object[] { tableID }));
        if (count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.Conn);
        //string sql = "SELECT t.[TableID], t.[CategoryID], t.[Active], t.[TableName], t.[TakeOut],p.tableid as pTable,p.CustomerName as CustomerName FROM [Tables] as t " +
        //"left join ProcTickets as p on t.tableid=p.tableid " +
        //"WHERE [CategoryID] = @CategoryID and t.Active = 1 order by t.orderby asc";
        //DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@CategoryID" }, new object[] { categoryid });
        DataView.DataSource = loadTable();
        //DataView.DataSource = dt;
        DataView.DataBind();
    }

    object loadTable()
    {
        Dao dao = new Dao();
        var listTable = dao.FindByMultiColumnAnd<PhoMac.Model.Table>(new[] { "CategoryID", "Active" }, categoryid, true);
        var listProcTicket = dao.GetAll<PhoMac.Model.ProcTicket>();
        var list = (from p in listTable
                    orderby p.OrderBy ascending
                    join p1 in listProcTicket on p.TableID equals p1.TableID into Inners
                    from i in Inners.DefaultIfEmpty(new PhoMac.Model.ProcTicket() { TableID = -1, CustomerName = "" })
                    select new PhoMac.Model.Table()
                    {
                        TableID = p.TableID,
                        CategoryID = p.CategoryID,
                        Active = p.Active,
                        TableName = p.TableName,
                        TakeOut = Convert.ToBoolean(p.TakeOut == null ? false : p.TakeOut),
                        Description = i.TableID.ToString(),
                        CustomerName = i.CustomerName,
                        OnDate = i.DateTimeIssue
                    }).ToList();
        return list;
    }


    protected void DataView_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //ASPxDataView data = (ASPxDataView)sender;
        //data.DataSource = SqlDataSource2;
        //data.DataBind();
    }
}