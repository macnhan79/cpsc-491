using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_AttendanceCheckBoxList : System.Web.UI.UserControl
{
    DateTime date;
    public DateTime Date
    {
        get { return date; }
        set
        {
            date = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public object DataSource
    {
        get { return ListView1.DataSource; }
        //get { return null; }
        set
        {
            //attendanceDataView.DataSource = value;
            ListView1.DataSource = value;
            Session["Attendance"] = value;
            List<PhoHa7_Attendance> list = (List<PhoHa7_Attendance>)value;
            double total = 0;
            for (int i = 0; i < list.Count; i++)
            {
                total += list[i].AttendanceValue;
            }
            //attendanceDataView.DataBind();
            ListView1.DataBind();
            lblTotal.Value = "Total: " + total;
            lblTotal.DataBind();
        }
    }

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        double value = Convert.ToDouble(e.Parameter.Split('|')[0]);
        int id = Convert.ToInt32(e.Parameter.Split('|')[1]);
        List<PhoHa7_Attendance> list = (List<PhoHa7_Attendance>)Session["Attendance"];
        double total = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Att_AttendanceID == id)
            {
                list[i].AttendanceValue = value;
                list[i].setAttendanceDateValue(Date.Day, value);
            }
            total += list[i].AttendanceValue;
        }
        Session["Attendance"] = list;
        ListView1.DataSource = list;
        ListView1.DataBind();
        lblTotal.Text = "Total: " + total;
        lblTotal.DataBind();
    }




}