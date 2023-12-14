using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeInfo
/// </summary>
public class EmployeeInfo
{
    public EmployeeInfo()
    {

    }
    string empName;
    int empID;
    string passcode;
    public EmployeeInfo(string name, string pass, int id)
    {
        empID = id;
        empName = name;
        passcode = pass;
    }
}