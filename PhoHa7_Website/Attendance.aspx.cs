using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;
using PhoMac.Model;
using PhoHa7.Library.Enum;
using PhoMac.Model.Presenter.Permission;

public partial class Attendance : System.Web.UI.Page
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
            DateTime date = DateTime.Today;
            dateEditAttendance.Date = date;
            loadUserAttendanceFromDatabase(date);
        }
        else
        {
            if (Session["Attendance"] != null)
            {
                AttendanceCheckBoxList.Date = dateEditAttendance.Date;
                AttendanceCheckBoxList.DataSource = (List<PhoHa7_Attendance>)Session["Attendance"];
            }
            else
                loadUserAttendanceFromDatabase(dateEditAttendance.Date);
        }
    }

    bool checkPermissionUser(EnumFormStatus status)
    {
        EnumFormCode FormCode = EnumFormCode.FrmAttendance;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        List<PhoHa7_Attendance> list = (List<PhoHa7_Attendance>)AttendanceCheckBoxList.DataSource;
        Dao dao = new Dao(false, true);
        EntityFactory.getInstance().BeginTransactionEntities();
        foreach (var item in list)
        {
            item.Att_TotalDays = (double)item.totalDays();
            item.Att_AmountTotal = (decimal)item.totalAmount();
            item.Att_AmountCash = (decimal)item.totalCash();
            dao.AddOrUpdate<PhoHa7_Attendance>(item);
        }
        EntityFactory.getInstance().commit();

    }

    List<PhoHa7_Attendance> loadUserAttendanceFromDatabase(DateTime date)
    {
        string sql = "select e.EmployeeID, e.FullName,e.Rate, e.CheckPayRate, e.Active ,a.* from Employees e left join (select * from PhoHa7_Attendance where Att_Month = @Month and Att_Year = @Year) a on e.EmployeeID = a.Att_EmployeeID where Active = 1 and Administrator = 0 order by e.EmployeeID desc";
        SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
        DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@Month", "@Year" }, new object[] { date.Month, date.Year });
        List<PhoHa7_Attendance> listAttendance = new List<PhoHa7_Attendance>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            PhoHa7_Attendance att = new PhoHa7_Attendance();
            att.Att_EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"] == DBNull.Value ? 0 : dt.Rows[i]["EmployeeID"]);
            att.Att_EmployeeName = dt.Rows[i]["FullName"] + string.Empty;
            att.Att_Month = Convert.ToInt32(dt.Rows[i]["Att_Month"] == DBNull.Value ? date.Month : dt.Rows[i]["Att_Month"]);
            att.Att_Year = Convert.ToInt32(dt.Rows[i]["Att_Year"] == DBNull.Value ? date.Year : dt.Rows[i]["Att_Year"]);
            //att.AttendanceValue = att.loadAttendanceDateValue(date.Day);
            att.Att_AttendanceID = Convert.ToInt32(att.Att_EmployeeID.ToString() + att.Att_Month.ToString() + att.Att_Year.ToString());
            att.Day = date.Day;
            att.Day1 = Convert.ToDouble(dt.Rows[i]["Day1"] == DBNull.Value ? 0 : dt.Rows[i]["Day1"]);
            att.Day2 = Convert.ToDouble(dt.Rows[i]["Day2"] == DBNull.Value ? 0 : dt.Rows[i]["Day2"]);
            att.Day3 = Convert.ToDouble(dt.Rows[i]["Day3"] == DBNull.Value ? 0 : dt.Rows[i]["Day3"]);
            att.Day4 = Convert.ToDouble(dt.Rows[i]["Day4"] == DBNull.Value ? 0 : dt.Rows[i]["Day4"]);
            att.Day5 = Convert.ToDouble(dt.Rows[i]["Day5"] == DBNull.Value ? 0 : dt.Rows[i]["Day5"]);
            att.Day6 = Convert.ToDouble(dt.Rows[i]["Day6"] == DBNull.Value ? 0 : dt.Rows[i]["Day6"]);
            att.Day7 = Convert.ToDouble(dt.Rows[i]["Day7"] == DBNull.Value ? 0 : dt.Rows[i]["Day7"]);
            att.Day8 = Convert.ToDouble(dt.Rows[i]["Day8"] == DBNull.Value ? 0 : dt.Rows[i]["Day8"]);
            att.Day9 = Convert.ToDouble(dt.Rows[i]["Day9"] == DBNull.Value ? 0 : dt.Rows[i]["Day9"]);
            att.Day10 = Convert.ToDouble(dt.Rows[i]["Day10"] == DBNull.Value ? 0 : dt.Rows[i]["Day10"]);
            att.Day11 = Convert.ToDouble(dt.Rows[i]["Day11"] == DBNull.Value ? 0 : dt.Rows[i]["Day11"]);
            att.Day12 = Convert.ToDouble(dt.Rows[i]["Day12"] == DBNull.Value ? 0 : dt.Rows[i]["Day12"]);
            att.Day13 = Convert.ToDouble(dt.Rows[i]["Day13"] == DBNull.Value ? 0 : dt.Rows[i]["Day13"]);
            att.Day14 = Convert.ToDouble(dt.Rows[i]["Day14"] == DBNull.Value ? 0 : dt.Rows[i]["Day14"]);
            att.Day15 = Convert.ToDouble(dt.Rows[i]["Day15"] == DBNull.Value ? 0 : dt.Rows[i]["Day15"]);
            att.Day16 = Convert.ToDouble(dt.Rows[i]["Day16"] == DBNull.Value ? 0 : dt.Rows[i]["Day16"]);
            att.Day17 = Convert.ToDouble(dt.Rows[i]["Day17"] == DBNull.Value ? 0 : dt.Rows[i]["Day17"]);
            att.Day18 = Convert.ToDouble(dt.Rows[i]["Day18"] == DBNull.Value ? 0 : dt.Rows[i]["Day18"]);
            att.Day19 = Convert.ToDouble(dt.Rows[i]["Day19"] == DBNull.Value ? 0 : dt.Rows[i]["Day19"]);
            att.Day20 = Convert.ToDouble(dt.Rows[i]["Day20"] == DBNull.Value ? 0 : dt.Rows[i]["Day20"]);
            att.Day21 = Convert.ToDouble(dt.Rows[i]["Day21"] == DBNull.Value ? 0 : dt.Rows[i]["Day21"]);
            att.Day22 = Convert.ToDouble(dt.Rows[i]["Day22"] == DBNull.Value ? 0 : dt.Rows[i]["Day22"]);
            att.Day23 = Convert.ToDouble(dt.Rows[i]["Day23"] == DBNull.Value ? 0 : dt.Rows[i]["Day23"]);
            att.Day24 = Convert.ToDouble(dt.Rows[i]["Day24"] == DBNull.Value ? 0 : dt.Rows[i]["Day24"]);
            att.Day25 = Convert.ToDouble(dt.Rows[i]["Day25"] == DBNull.Value ? 0 : dt.Rows[i]["Day25"]);
            att.Day26 = Convert.ToDouble(dt.Rows[i]["Day26"] == DBNull.Value ? 0 : dt.Rows[i]["Day26"]);
            att.Day27 = Convert.ToDouble(dt.Rows[i]["Day27"] == DBNull.Value ? 0 : dt.Rows[i]["Day27"]);
            att.Day28 = Convert.ToDouble(dt.Rows[i]["Day28"] == DBNull.Value ? 0 : dt.Rows[i]["Day28"]);
            att.Day29 = Convert.ToDouble(dt.Rows[i]["Day29"] == DBNull.Value ? 0 : dt.Rows[i]["Day29"]);
            att.Day30 = Convert.ToDouble(dt.Rows[i]["Day30"] == DBNull.Value ? 0 : dt.Rows[i]["Day30"]);
            att.Day31 = Convert.ToDouble(dt.Rows[i]["Day31"] == DBNull.Value ? 0 : dt.Rows[i]["Day31"]);
            att.Att_Rate = Convert.ToDecimal(dt.Rows[i]["Rate"] == DBNull.Value ? 0 : dt.Rows[i]["Rate"]);
            att.Att_AmountCheck = Convert.ToDecimal(dt.Rows[i]["CheckPayRate"] == DBNull.Value ? 0 : dt.Rows[i]["CheckPayRate"]);
            listAttendance.Add(att);
        }
        AttendanceCheckBoxList.DataSource = listAttendance;
        AttendanceCheckBoxList.Date = date;
        AttendanceCheckBoxList.DataBind();
        return listAttendance;
    }



    protected void dateEditAttendance_DateChanged(object sender, EventArgs e)
    {
        DateTime date = DateTime.Today;
        date = dateEditAttendance.Date;
        loadUserAttendanceFromDatabase(date);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        dateEditAttendance.Date = dateEditAttendance.Date.AddDays(-1);
        loadUserAttendanceFromDatabase(dateEditAttendance.Date);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        dateEditAttendance.Date = dateEditAttendance.Date.AddDays(1);
        loadUserAttendanceFromDatabase(dateEditAttendance.Date);
    }

    protected void btnToday_Click(object sender, EventArgs e)
    {
        dateEditAttendance.Date = DateTime.Today;
        loadUserAttendanceFromDatabase(dateEditAttendance.Date);
    }

}