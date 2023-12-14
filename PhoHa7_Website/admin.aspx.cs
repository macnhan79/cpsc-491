using PhoHa7.Library.Classes.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1;

public partial class admin1 : System.Web.UI.Page
{
   DateTime dateStart;
    DateTime dateEnd;
    SqlDataHelper da;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginCu"] != "2bca263c477699a775926d9828d14103")
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            da = new SqlDataHelper();
            if (Session["AmountTotal"] == null)
            {
                Session["AmountTotal"] = 0;
            }
            //else
            //{
            //    total = Convert.ToDouble(Session["AmountTotal"]);
            //}
            //Calendar1.SelectedDate = DateTime.Now;
            Label1.Text = Calendar1.SelectedDate.ToString();
            SqlDataHelper da1 = new SqlDataHelper();
            SqlCommand cmd = new SqlCommand();
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Label1.Text = Calendar1.SelectedDate.ToString();
        string dayStart = Calendar1.SelectedDate.Date.ToString();
        string day = Calendar1.SelectedDate.Day.ToString();
        string month = Calendar1.SelectedDate.Month.ToString();
        string year = Calendar1.SelectedDate.Year.ToString();
        //string dayStart = day + "-" + month + "-" + year + " 00:00:00";
        string dayEnd = month + "/" + day + "/" + year + " 11:59:59 pm";
        dateStart = Convert.ToDateTime(dayStart);
        dateEnd = Convert.ToDateTime(dayEnd);
        SqlDataSource1.SelectParameters["dateBegin"].DefaultValue = dayStart;
        SqlDataSource1.SelectParameters["dateEnd"].DefaultValue = dayEnd;
        GridView1.DataBind();
        setTotal();
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = (GridViewRow)GridView1.Rows[e.NewSelectedIndex];
        int saleID = Convert.ToInt32(row.Cells[7].Text);
        int ticketID = Convert.ToInt32(row.Cells[6].Text);
        //double amount = Convert.ToDouble(row.Cells[2].Text);

        SqlDataHelper da = new SqlDataHelper();
        SqlParameter pSaleID = new SqlParameter("@saleID", saleID);
        SqlParameter pTicketID = new SqlParameter("@ticketID", ticketID);
        da.ExecuteNonQuery("updateTicket", pSaleID, pTicketID);

        ////////////////////////////////////////
        double amount = Convert.ToDouble(row.Cells[3].Text);
        double total = Convert.ToDouble(Session["AmountTotal"]);
        total += amount;
        Session["AmountTotal"] = total;
        string abc = Session["AmountTotal"].ToString();
        lbTotal.Text = total + "";
        setTotal();

        GridView1.DataBind();
    }

    void setDatetime()
    {
        Label1.Text = Calendar1.SelectedDate.ToString();
        string dayStart = Calendar1.SelectedDate.Date.ToString();
        string day = Calendar1.SelectedDate.Day.ToString();
        string month = Calendar1.SelectedDate.Month.ToString();
        string year = Calendar1.SelectedDate.Year.ToString();
        //string dayStart = day + "-" + month + "-" + year + " 00:00:00";
        string dayEnd = month + "/" + day + "/" + year + " 11:59:59 pm";
        dateStart = Convert.ToDateTime(dayStart);
        dateEnd = Convert.ToDateTime(dayEnd);
    }

    double getCash()
    {
        setDatetime();
        SqlParameter start = new SqlParameter("@dateBegin", dateStart);
        SqlParameter end = new SqlParameter("@dateEnd", dateEnd);
        double cash = 0;
        try
        {
            cash = Convert.ToDouble(da.ExecuteScalar("totalCash", start, end).ToString());
        }
        catch { }
        return cash;
    }

    double getCredit()
    {
        setDatetime();
        SqlParameter start = new SqlParameter("@dateBegin", dateStart);
        SqlParameter end = new SqlParameter("@dateEnd", dateEnd);
        double credit = 0;
        try
        {
            credit = Convert.ToDouble(da.ExecuteScalar("totalCredit", start, end).ToString());
        }
        catch { }
        return credit;
    }

    double getTotal()
    {
        setDatetime();
        SqlParameter start = new SqlParameter("@dateBegin", dateStart);
        SqlParameter end = new SqlParameter("@dateEnd", dateEnd);
        double total = 0;
        try
        {
            total = Convert.ToDouble(da.ExecuteScalar("totalDay", start, end).ToString());
        }
        catch { }
        return total;
    }

    void setTotal()
    {
        double cash = getCash();
        double credit = getCredit();
        lblCash.Text = cash.ToString();
        lblCredit.Text = credit.ToString();
        lblTotal.Text = getTotal().ToString();
    }
}