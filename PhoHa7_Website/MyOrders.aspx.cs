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
using DevExpress.Web.ASPxEditors;

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

        if (!IsPostBack)
        {
            DataView.DataSource = loadTable(dtEmp.EmployeeID);
            DataView.DataBind();
        }
    }

    object loadTable(int empID)
    {
        Dao dao = new Dao();
        var listTable = dao.GetAll<PhoMac.Model.Table>();
        var listProcTicket = dao.FindByMultiColumnAnd<PhoMac.Model.ProcTicket>(new[] { "EmployeeID" }, empID);
        var list = (from p in listProcTicket
                    orderby p.TicketID ascending
                    join t in listTable on p.TableID equals t.TableID into Inners
                    from i in Inners
                    select new PhoMac.Model.Table()
                    {
                        TableID = i.TableID,
                        CategoryID = i.CategoryID,
                        Active = i.Active,
                        TableName = i.TableName,
                        TakeOut = i.TakeOut,
                        Description = p.TableID.ToString(),
                        CustomerName = p.CustomerName
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

}