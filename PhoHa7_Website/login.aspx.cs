using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PhoMac.Model;
using PhoMac.Model.Data;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using PhoHa7.Library.Enum;
using PhoMac.Model.Presenter.Permission;


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PhoHa7_Machine machineSession = loadMachineInfoSession();
            //lblInfo.Text = "Name: " + machineSession.MachineName + ". UserHostAddress: " + machineSession.MachineIpAdress;
        }

    }

    //ICollection<PhoHa7_Machine> loadListMachine()
    //{
    //    //if (Session["MachineList"] == null)
    //    //{

    //    //    Session["MachineList"] = listMachine;
    //    //    return listMachine;
    //    //}
    //    //else
    //    //{
    //    //    ICollection<PhoHa7_Machine> listMachine = (ICollection<PhoHa7_Machine>)Session["MachineList"];
    //    //    return listMachine;
    //    //}
    //}

    PhoHa7_Machine loadMachineInfoSession()
    {
        if (Session["MachineInfo"]==null)
        {
            PhoHa7_Machine ma = GetVisitorIPAddress();
            Session["MachineInfo"] = ma;
            return ma;
        }
        else
        {
            PhoHa7_Machine ma = (PhoHa7_Machine)Session["MachineInfo"];
            return ma;
        }
    }

    /// <summary>
    /// method to get Client ip address
    /// </summary>
    /// <param name="GetLan"> set to true if want to get local(LAN) Connected ip address</param>
    /// <returns></returns>
    public PhoHa7_Machine GetVisitorIPAddress(bool GetLan = false)
    {
        PhoHa7_Machine ma = new PhoHa7_Machine();
        string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (String.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (string.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
        {
            GetLan = true;
            visitorIPAddress = string.Empty;
        }


        string stringHostName = "";
        //if (GetLan)
        //{
            //This is for Local(LAN) Connected ID Address
            try
            {
                stringHostName = Dns.GetHostEntry(Request.ServerVariables["REMOTE_ADDR"]).HostName.Split('.').ToList().First();
            }
            catch (System.Exception ex)
            {

            }
            visitorIPAddress = Request.UserHostAddress;
        //}

        ma.MachineName = stringHostName;
        ma.MachineIpAdress = visitorIPAddress;
        return ma;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Dao dao = new Dao(false, false);
        var listUser = dao.FindByMultiColumnAnd<Employee>(new[] { "securitycode" }, uname.Value);
        if (listUser.Count > 0)
        {
            Employee emp = listUser.First();
            bool active = Convert.ToBoolean(emp.Active == null ? false : emp.Active);
            if (active)
            {
                //PhoHa7_Machine machineSession = loadMachineInfoSession();
                //ICollection<PhoHa7_Machine> listMachine = dao.GetAll<PhoHa7_Machine>();

                //if (checkPermissionUser(EnumFormStatus.View, emp))
                //{
                    Session["LoginEmp"] = listUser.First();
                    Response.Redirect("Table.aspx");
                //}
                //else
                //{
                    //if (listMachine != null)
                    //{
                        //foreach (var item in listMachine)
                        //{
                            //bool isActive = Convert.ToBoolean(item.MachineActive == null ? false : item.MachineActive);
                            //if ((item.MachineIpAdress == machineSession.MachineIpAdress) && (item.MachineName == machineSession.MachineName) && isActive)
                            //{
                                //Session["LoginEmp"] = listUser.First();
                                //Response.Redirect("Table.aspx");
                            //}
                        //}
                        //lblInfo.Text = "Your device do NOT has permission.";
                        //lblInfo.ForeColor = Color.Red;
                    //}
                    //else
                    //{
                        //lblInfo.Text = "Your device do NOT has permission.";
                        //lblInfo.ForeColor = Color.Red;
                    //}
                //}

            }
            else
            {
                lblInfo.Text = "Your account has been deactivated.";
                lblInfo.ForeColor = Color.Red;
            }
        }

        //SqlHelperWeb sqlHelper = new SqlHelperWeb(DA_Connection.Conn);
        //string sql = "select * from Employees where SecurityCode = @SecurityCode";
        //DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@SecurityCode" }, new object[] { uname.Value });
        //if (dt.Rows.Count > 0)
        //{
        //    Session["LoginEmp"] = dt;
        //    Response.Redirect("Table.aspx");
        //}
    }


    bool checkPermissionUser(EnumFormStatus status, Employee emp)
    {
        EnumFormCode FormCode = EnumFormCode.FrmLogin;
        //Employee emp = (Employee)Session["LoginEmp"];
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

}