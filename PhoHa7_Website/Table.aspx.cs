using DevExpress.Web.ASPxTabControl;
using PhoHa7.Library.Classes.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoMac.Model;
using PhoMac.Model.Data;

public partial class Table : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Employee dtEmp = (Employee)Session["LoginEmp"];
        btnLogout.Text = "Hi " + dtEmp.FullName + "(Log Out)";
        //CashDrawer.OpenCashDrawer("MCP30 - Ethernet:TCP:");

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        string sql = "select CategoryID, CategoryName from TabCategories where Active = 1";
        DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TabPage tab = new TabPage();
                tab.Name = dt.Rows[i]["CategoryID"] + string.Empty;
                tab.Text = dt.Rows[i]["CategoryName"] + string.Empty;

                UserControl_TableList obj = LoadControl("~/UserControl/TableList.ascx") as UserControl_TableList;
                obj.CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryID"]);
                obj.DataSource = loadTable(Convert.ToInt32(dt.Rows[i]["CategoryID"]));
                //obj.DataSource = tableList;
                tab.Controls.Add(obj);
                carTabPage.TabPages.Add(tab);
            }

        }

    }

    object loadTable(int categoryid)
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
                        TakeOut = p.TakeOut,
                        Description = i.TableID.ToString(),
                        CustomerName = i.CustomerName,
                        OnDate = i.DateTimeIssue
                    }).ToList();
        return list;
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["LoginEmp"] = null;
        Session["MachineList"] = null;
        Session["MachineInfo"] = null;
        Response.Redirect("Login.aspx");
    }



}