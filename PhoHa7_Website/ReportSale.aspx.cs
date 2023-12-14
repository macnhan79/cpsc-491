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
using System.Web.Caching;
using PhoMac.Model.Data;

public partial class ReportSale : System.Web.UI.Page
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
            ASPxCalendar1.SelectedDate = DateTime.Today;
            setTotal();
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
    protected void ASPxCalendar1_SelectionChanged(object sender, EventArgs e)
    {
        setTotal();
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

    double getSaleTax(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        double credit = 0;
        try
        {
            string sql = "select sum(tax) from Tickets where DateTimeIssue >= @dateBegin and DateTimeIssue <= @dateEnd";
            credit = Convert.ToDouble(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@dateBegin", "@dateEnd" }, new object[] { start, end }).ToString());
        }
        catch { }
        return credit;
    }

    double getCreditTip(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        double credit = 0;
        try
        {
            string sql = "select sum(tips) from Tickets where DateTimeIssue >= @dateBegin and DateTimeIssue <= @dateEnd";
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

    object getListWithdraw(DateTime start, DateTime end)
    {
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataTable credit = null;
        try
        {
            string sql = "select Description, Amount from DailyDraw where DrawDate >= @dateBegin and DrawDate <= @dateEnd";
            credit = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@dateBegin", "@dateEnd" }, new object[] { start, end });
        }
        catch { }
        return credit;
    }


    void setTotal()
    {
        Dao dao = new Dao();
        double cash = getCash(getStartDate(), getEndDate());
        double credit = getCredit(getStartDate(), getEndDate());
        double withdraw = getWithdraw(getStartDate(), getEndDate());
        double tips = getCreditTip(getStartDate(), getEndDate());
        double saleTax = getSaleTax(getStartDate(), getEndDate());

        string day = ASPxCalendar1.SelectedDate.Day.ToString();
        string month = ASPxCalendar1.SelectedDate.Month.ToString();
        string year = ASPxCalendar1.SelectedDate.Year.ToString();
        string key = day + month + year;
        //DaySale daySale = getTotalSale(key);
        //if (daySale.PaidCash == 0)
        //{
        //    daySale.PaidCash = Convert.ToDecimal(cash);
        //}
        //else
        //{
        //    double saleCash = Convert.ToDouble(daySale.PaidCash);
        //    if (cash < saleCash)
        //    {
        //        cash = saleCash;
        //    }
        //    else
        //    {
        //        daySale.PaidCash = Convert.ToDecimal(cash);
        //    }
        //}
       
        
        double totalSale = cash + credit - withdraw;

        lblTotal.Text = String.Format("{0:c}",totalSale);
        lblCash.Text = String.Format("{0:c}",cash);
        lblCredit.Text = String.Format("{0:c}",credit);
        lblWithdraw.Text = String.Format("{0:c}",withdraw);
        lblCash1.Text = String.Format("{0:c}",(cash - withdraw));
        lblTips.Text = String.Format("{0:c}",tips);
        lblSaleTax.Text = String.Format("{0:c}",saleTax);
        lblNetTotal.Text = String.Format("{0:c}",(totalSale - saleTax));

        //daySale.PaidCredit = Convert.ToDecimal(credit);
        //daySale.TotalSale = Convert.ToDecimal(totalSale);
        //dao.Update<DaySale>(daySale);
        gridWithdraw.DataSource = getListWithdraw(getStartDate(), getEndDate());
        gridWithdraw.DataBind();
    }

    DateTime getStartDate()
    {
        string dayStart = ASPxCalendar1.SelectedDate.Date.ToString();
        DateTime dateStart = Convert.ToDateTime(dayStart);
        return dateStart;
    }

    DateTime getEndDate()
    {
        string day = ASPxCalendar1.SelectedDate.Day.ToString();
        string month = ASPxCalendar1.SelectedDate.Month.ToString();
        string year = ASPxCalendar1.SelectedDate.Year.ToString();
        string dayEnd = month + "/" + day + "/" + year + " 11:59:59 pm";
        DateTime dateEnd = Convert.ToDateTime(dayEnd);
        return dateEnd;
    }

    DaySale getTotalSale(string dateKey)
    {
        Dao dao = new Dao();
        DaySale daySale = null;
        ICollection<DaySale> daySaleList = dao.FindByMultiColumnAnd<DaySale>(new[] { "DateKey" }, dateKey);
        if (daySaleList!=null && daySaleList.Count>0)
        {
            daySale = daySaleList.First();
        }
        if (daySale == null)
        {
            daySale = new DaySale();
            daySale.DateKey = dateKey;
            daySale.PaidCash = 0;
            daySale.PaidCredit = 0;
            daySale.TotalSale = 0;
            dao.Add<DaySale>(daySale);
        }
        return daySale;
    }

}