using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoMac.Model;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Employee dtEmp = (Employee)Session["LoginEmp"];
        btnLogout.Text = "Hi "+ dtEmp.FullName + "(Log Out)";



        //bool isAdmin = (bool)dtEmp.Administrator;
        //ASPxNavBar1.Groups.FindByName("gReport").Visible = isAdmin;
        //ASPxNavBar1.Groups.FindByName("gAdmin").Visible = isAdmin;
    }



    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["LoginEmp"] = null;
        Session["MachineList"] = null;
        Session["MachineInfo"] = null;
        Response.Redirect("Login.aspx");
    }
}
