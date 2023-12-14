using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string pass = MD5(TextBox1.Text);
        if (pass == "2bca263c477699a775926d9828d14103" && txtUsername.Text == "admin")
        {
            Session["LoginCu"] = "2bca263c477699a775926d9828d14103";
            Response.Redirect("admin.aspx");
        }

    }


    private static string MD5(string Metin)
    {
        MD5CryptoServiceProvider MD5Code = new MD5CryptoServiceProvider();
        byte[] byteDizisi = Encoding.UTF8.GetBytes(Metin);
        byteDizisi = MD5Code.ComputeHash(byteDizisi);
        StringBuilder sb = new StringBuilder();
        foreach (byte ba in byteDizisi)
        {
            sb.Append(ba.ToString("x2").ToLower());
        }
        return sb.ToString();
    }
}