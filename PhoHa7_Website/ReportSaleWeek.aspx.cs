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

public partial class ReportSaleWeek : System.Web.UI.Page
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
            dateEdit.Date = DateTime.Today;
            //setTotal();
            loadReport(dateEdit.Date);
        }

    }

    void loadReport(DateTime date)
    {
        int dayOfWeek = (int)dateEdit.Date.DayOfWeek;
        DateTime monday;
        DateTime tuesday;
        DateTime webnesday;
        DateTime thursday;
        DateTime friday;
        DateTime saturday;
        DateTime sunday;
        switch (dayOfWeek)
        {
            case 1:
                monday = dateEdit.Date;
                tuesday = dateEdit.Date.AddDays(1);
                webnesday = dateEdit.Date.AddDays(2);
                thursday = dateEdit.Date.AddDays(3);
                friday = dateEdit.Date.AddDays(4);
                saturday = dateEdit.Date.AddDays(5);
                sunday = dateEdit.Date.AddDays(6);
                break;
            case 2:
                monday = dateEdit.Date.AddDays(-1);
                tuesday = dateEdit.Date;
                webnesday = dateEdit.Date.AddDays(1);
                thursday = dateEdit.Date.AddDays(2);
                friday = dateEdit.Date.AddDays(3);
                saturday = dateEdit.Date.AddDays(4);
                sunday = dateEdit.Date.AddDays(5);
                break;
            case 3:
                monday = dateEdit.Date.AddDays(-2);
                tuesday = dateEdit.Date.AddDays(-1);
                webnesday = dateEdit.Date.AddDays(0);
                thursday = dateEdit.Date.AddDays(1);
                friday = dateEdit.Date.AddDays(2);
                saturday = dateEdit.Date.AddDays(3);
                sunday = dateEdit.Date.AddDays(4);
                break;
            case 4:
                monday = dateEdit.Date.AddDays(-3);
                tuesday = dateEdit.Date.AddDays(-2);
                webnesday = dateEdit.Date.AddDays(-1);
                thursday = dateEdit.Date.AddDays(0);
                friday = dateEdit.Date.AddDays(1);
                saturday = dateEdit.Date.AddDays(2);
                sunday = dateEdit.Date.AddDays(3);
                break;
            case 5:
                monday = dateEdit.Date.AddDays(-4);
                tuesday = dateEdit.Date.AddDays(-3);
                webnesday = dateEdit.Date.AddDays(-2);
                thursday = dateEdit.Date.AddDays(-1);
                friday = dateEdit.Date.AddDays(0);
                saturday = dateEdit.Date.AddDays(1);
                sunday = dateEdit.Date.AddDays(2);
                break;
            case 6:
                monday = dateEdit.Date.AddDays(-5);
                tuesday = dateEdit.Date.AddDays(-4);
                webnesday = dateEdit.Date.AddDays(-3);
                thursday = dateEdit.Date.AddDays(-2);
                friday = dateEdit.Date.AddDays(-1);
                saturday = dateEdit.Date.AddDays(0);
                sunday = dateEdit.Date.AddDays(1);
                break;
                //sunday
            case 0:
                monday = dateEdit.Date.AddDays(-6);
                tuesday = dateEdit.Date.AddDays(-5);
                webnesday = dateEdit.Date.AddDays(-4);
                thursday = dateEdit.Date.AddDays(-3);
                friday = dateEdit.Date.AddDays(-2);
                saturday = dateEdit.Date.AddDays(-1);
                sunday = dateEdit.Date.AddDays(0);
                break;
            default:
                monday = dateEdit.Date;
                tuesday = dateEdit.Date;
                webnesday = dateEdit.Date;
                thursday = dateEdit.Date;
                friday = dateEdit.Date;
                saturday = dateEdit.Date;
                sunday = dateEdit.Date;
                break;
        }
        List<ReportSale> list = new List<ReportSale>();
        list.Add(new ReportSale("Monday", setTotal(monday)));
        list.Add(new ReportSale("Tuesday", setTotal(tuesday)));
        list.Add(new ReportSale("Wednesday", setTotal(webnesday)));
        list.Add(new ReportSale("Thursday", setTotal(thursday)));
        list.Add(new ReportSale("Friday", setTotal(friday)));
        list.Add(new ReportSale("Saturday", setTotal(saturday)));
        list.Add(new ReportSale("Sunday", setTotal(sunday)));
        var categorySales = (from c in list
                             select new
                             {
                                 DayOfWeek = c.DayOfWeek,
                                 Value = c.Value,
                             }
                            ).ToList();
        //WebChartControl2.Series["week"].ArgumentDataMember = "Monday";
        WebChartControl1.DataSource = categorySales;
        WebChartControl1.Series["Week"].ArgumentDataMember = "DayOfWeek";
        WebChartControl1.Series["Week"].ValueDataMembers.AddRange(new string[] { "Value" });
        WebChartControl1.DataBind();
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

    double setTotal(DateTime date)
    {
        double cash = getCash(getStartDate(date), getEndDate(date));
        double credit = getCredit(getStartDate(date), getEndDate(date));
        double withdraw = getWithdraw(getStartDate(date), getEndDate(date));
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

    DateTime getStartDate(DateTime date)
    {
        return date;
    }

    DateTime getEndDate(DateTime date)
    {
        string day = date.Day.ToString();
        string month = date.Month.ToString();
        string year = date.Year.ToString();
        string dayEnd = month + "/" + day + "/" + year + " 11:59:59 pm";
        DateTime dateEnd = Convert.ToDateTime(dayEnd);
        return dateEnd;
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

    protected void btnNext_Click(object sender, EventArgs e)
    {
        dateEdit.Date = dateEdit.Date.AddDays(7);
        loadReport(dateEdit.Date);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        dateEdit.Date = dateEdit.Date.AddDays(-7);
        loadReport(dateEdit.Date);
    }

    protected void btnToday_Click(object sender, EventArgs e)
    {
        dateEdit.Date = DateTime.Today;
        loadReport(dateEdit.Date);
    }

    protected void dateEditAttendance_DateChanged(object sender, EventArgs e)
    {
        loadReport(dateEdit.Date);
    }




}