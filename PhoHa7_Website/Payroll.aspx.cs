using PhoHa7.Library.Enum;
using PhoMac.Model;
using PhoMac.Model.Presenter.Permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoHa7.Library.Interface;

public partial class Payroll : System.Web.UI.Page
{
    private EnumFormCode _formCode;
    public EnumFormCode FormCode
    {
        get { return _formCode; }
        set { _formCode = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        checkPermissionUser();

        if (!IsPostBack)
        {
            loadcbYear();
            loadGrid115();
            load1631();
        }
    }

    void checkPermissionUser()
    {

        FormCode = EnumFormCode.FrmPayroll;
        Employee emp = (Employee)Session["LoginEmp"];
        if (emp != null)
        {
            SystemPermission permission = new SystemPermission(FormCode.ToString(), emp.EmployeeID);
            if (!permission.PermissionView())
                Response.Redirect("Default.aspx");
            if (!permission.PermissionReport())
            {
                gridPayRoll115.Columns["Att_Rate"].Visible = false;
                gridPayRoll115.Columns["Att_AmountCheck"].Visible = false;
                gridPayRoll115.Columns["Cash"].Visible = false;
                gridPayRoll115.Columns["AmountTotal"].Visible = false;

                gridPayRoll1631.Columns["Att_Rate"].Visible = false;
                gridPayRoll1631.Columns["Att_AmountCheck"].Visible = false;
                gridPayRoll1631.Columns["Cash"].Visible = false;
                gridPayRoll1631.Columns["AmountTotal"].Visible = false;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    void loadGrid115()
    {
        string sql = "select *, (Day1+Day2+Day3+Day4+Day5+Day6+Day7+Day8+Day9+Day10+Day11+Day12+Day13+Day14+Day15) pDays, (((Day1+Day2+Day3+Day4+Day5+Day6+Day7+Day8+Day9+Day10+Day11+Day12+Day13+Day14+Day15)*Att_Rate) - Att_AmountCheck) AmountTotal, " +
            " ((((Day1+Day2+Day3+Day4+Day5+Day6+Day7+Day8+Day9+Day10+Day11+Day12+Day13+Day14+Day15)*Att_Rate)) - Att_AmountCheck) Cash from PhoHa7_Attendance where Att_Month = @Month and Att_Year = @Year order by Att_TotalDays desc, Att_EmployeeID asc";
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@Month", "@Year" }, new object[] { cbMonth.Value, cbYear.Value });
        gridPayRoll115.DataSource = dt;
        gridPayRoll115.DataBind();

    }

    void load1631()
    {
        string sql = "select *, (Day16+Day17+Day18+Day19+Day20+Day21+Day22+Day23+Day24+Day25+Day26+Day27+Day28+Day29+Day30+Day31) pDays, (((Day16+Day17+Day18+Day19+Day20+Day21+Day22+Day23+Day24+Day25+Day26+Day27+Day28+Day29+Day30+Day31)*Att_Rate) - Att_AmountCheck) AmountTotal, " +
            " ((((Day16+Day17+Day18+Day19+Day20+Day21+Day22+Day23+Day24+Day25+Day26+Day27+Day28+Day29+Day30+Day31)*Att_Rate)) - Att_AmountCheck) Cash from PhoHa7_Attendance where Att_Month = @Month and Att_Year = @Year order by Att_TotalDays desc, Att_EmployeeID asc";
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@Month", "@Year" }, new object[] { cbMonth.Value, cbYear.Value });
        gridPayRoll1631.DataSource = dt;
        gridPayRoll1631.DataBind();
    }

    void loadcbYear()
    {
        int year = DateTime.Today.Year;
        for (int i = year - 10; i <= year + 10; i++)
        {
            cbYear.Items.Add(i.ToString(), i);
        }
        cbYear.Value = DateTime.Today.Year;
        cbMonth.Value = DateTime.Today.Month;
        cbYear.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadGrid115();
        load1631();
    }

    protected void btnExport115_Click(object sender, EventArgs e)
    {
        //cbMonth.Value, cbYear.Value
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=1-15(" + cbMonth.Value + "-" + cbYear.Value + ").xls");
        Response.AddHeader("Content-Type", "application/vnd.ms-excel");
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gridPayRoll115.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnExport1631_Click(object sender, EventArgs e)
    {
        //cbMonth.Value, cbYear.Value
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=16-31(" + cbMonth.Value + "-" + cbYear.Value + ").xls");
        Response.AddHeader("Content-Type", "application/vnd.ms-excel");
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gridPayRoll1631.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}