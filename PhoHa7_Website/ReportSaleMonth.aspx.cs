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

public partial class ReportSaleMonth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!checkPermissionUser(EnumFormStatus.View))
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            loadcbYear();
            loadReport();
        }
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.Report;
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



    void loadcbYear()
    {
        int year = DateTime.Today.Year;
        for (int i = year - 10; i <= year + 10; i++)
        {
            cbYear.Items.Add(i.ToString(), i);
        }
        cbYear.Value = DateTime.Today.Year;
        cbYear.DataBind();
    }

    public class ReportSale
    {
        public ReportSale(string a, double b)
        {
            DayOfWeek = a;
            Value = b;
        }
        public string DayOfWeek;
        public double Value;
    }

    double setTotal(int month, int year)
    {
        double cash = getCash(getStartDate(year, month), getEndDate(year, month));
        double credit = getCredit(getStartDate(year, month), getEndDate(year, month));
        double withdraw = getWithdraw(getStartDate(year, month), getEndDate(year, month));
        double total = cash + credit - withdraw;
        return total;
    }

    double getCash(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        double cash = 0;
        try
        {
            string sql = "select sum(paidcash) from Tickets where DateTimeIssue >= @dateBegin and DateTimeIssue <= @dateEnd";
            cash = Convert.ToDouble(sqlHelper.ExecuteScalar(sql, CommandType.Text,
                new object[] { "@dateBegin", "@dateEnd" }, new object[] { start, end }).ToString());

        }
        catch { }
        return cash;
    }

    double getCredit(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        double credit = 0;
        try
        {
            string sql = "select sum(paidcredit) from Tickets where DateTimeIssue >= @dateBegin and DateTimeIssue <= @dateEnd";
            credit = Convert.ToDouble(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@dateBegin", "@dateEnd" }, new object[] { start, end }).ToString());
        }
        catch { }
        return credit;
    }

    double getWithdraw(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        double credit = 0;
        try
        {
            string sql = "select sum(Amount) from DailyDraw where DrawDate >= @dateBegin and DrawDate <= @dateEnd";
            credit = Convert.ToDouble(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@dateBegin", "@dateEnd" }, new object[] { start, end }).ToString());
        }
        catch { }
        return credit;
    }

    DateTime getStartDate(int year, int month)
    {
        string day = "1";
        string dayStart = month + "/" + day + "/" + year + " 12:00:00 AM";
        DateTime date = Convert.ToDateTime(dayStart);
        return date;
    }

    DateTime getEndDate(int year, int month)
    {
        string day = DateTime.DaysInMonth(year, month).ToString();
        string dayEnd = month + "/" + day + "/" + year + " 11:59:59 pm";
        DateTime dateEnd = Convert.ToDateTime(dayEnd);
        return dateEnd;
    }

    void loadReport()
    {
        int year = Convert.ToInt32(cbYear.Value);
        List<ReportSale> list = new List<ReportSale>();
        for (int i = 1; i <= 12; i++)
        {
            switch (i)
            {
                case 1:
                    list.Add(new ReportSale("Jan", setTotal(i, year)));
                    break;
                case 2:
                    list.Add(new ReportSale("Feb", setTotal(i, year)));
                    break;
                case 3:
                    list.Add(new ReportSale("Mar", setTotal(i, year)));
                    break;
                case 4:
                    list.Add(new ReportSale("Apr", setTotal(i, year)));
                    break;
                case 5:
                    list.Add(new ReportSale("May", setTotal(i, year)));
                    break;
                case 6:
                    list.Add(new ReportSale("June", setTotal(i, year)));
                    break;
                case 7:
                    list.Add(new ReportSale("Jul", setTotal(i, year)));
                    break;
                case 8:
                    list.Add(new ReportSale("Aug", setTotal(i, year)));
                    break;
                case 9:
                    list.Add(new ReportSale("Sep", setTotal(i, year)));
                    break;
                case 10:
                    list.Add(new ReportSale("Oct", setTotal(i, year)));
                    break;
                case 11:
                    list.Add(new ReportSale("Nov", setTotal(i, year)));
                    break;
                case 12:
                    list.Add(new ReportSale("Dev", setTotal(i, year)));
                    break;
                default:
                    break;
            }

        }
        var categorySales = (from c in list
                             select new
                             {
                                 DayOfWeek = c.DayOfWeek,
                                 Value = c.Value,
                             }
                           ).ToList();
        WebChartControl1.DataSource = categorySales;
        WebChartControl1.Series["Month"].ArgumentDataMember = "DayOfWeek";
        WebChartControl1.Series["Month"].ValueDataMembers.AddRange(new string[] { "Value" });
        WebChartControl1.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadReport();
    }
}