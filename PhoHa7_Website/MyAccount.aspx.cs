using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoMac.Model;
using PhoMac.Model.Data;

public partial class MyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginEmp"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            Employee emp = (Employee)Session["LoginEmp"];
            txtFullName.Text = emp.FullName;
            //DevExpress.Web.ASPxEditors.ListEditItem itemLanguage = new DevExpress.Web.ASPxEditors.ListEditItem();
            //itemLanguage.Value = emp.Language;
            //itemLanguage.Text = "abc";
            cbLanguage.Value = emp.Language;
        }
    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Dao dao = new Dao();
        Employee emp = (Employee)Session["LoginEmp"];
        if (txtOldPassCode.Text == emp.SecurityCode)
        {
            bool allow = true;
            if (txtNewPasscode.Text != "" || txtNewPasscode.Text != string.Empty)
            {
                if (txtNewPasscode.Text == txtNewPasscodeConfirm.Text)
                {
                    //check double passcode
                    var tempEmp = dao.FindByMultiColumnAnd<Employee>(new[] { "SecurityCode" }, emp.SecurityCode);
                    
                    foreach (var item in tempEmp)
                    {
                        if (item.EmployeeID != emp.EmployeeID && item.SecurityCode == txtNewPasscode.Text)
                        {
                            lblError.Text = "Passcode has already been used.";
                            allow = false;
                        }
                    }
                    emp.SecurityCode = txtNewPasscode.Text;
                }
            }
            if (allow)
            {
                //emp.SecurityCode = txtNewPasscode.Text;
                emp.FullName = txtFullName.Text;
                emp.Language = Convert.ToInt32(cbLanguage.Value);
                dao.Update<Employee>(emp);
                txtOldPassCode.Text = "";
                txtNewPasscode.Text = "";
                txtNewPasscodeConfirm.Text = "";
                lblError.Text = "";
            }
        }
        else
        {
            lblError.Text = "Please enter your current passcode.";
        }
    }
}