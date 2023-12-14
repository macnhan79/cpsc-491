using PhoHa7.Library.Enum;
using PhoMac.Model;
using PhoMac.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter.Permission
{
    public class SystemPermission
    {
        public string _FormCode;
        private Dao dao;

        public Employee Users { get; set; }
        private PhoHa7_Sys_Object SysObject { get; set; }

        private PhoHa7_Sys_User_Permission sysUserPermission;
        private List<PhoHa7_Sys_Role_Permission> listSysRolePermission;


        public SystemPermission(string pFormCode, int pUserID)
        {
            dao = new Dao();
            _FormCode = pFormCode;

            //Users.EmployeeID = pUserID;


            SysObject = dao.GetById<PhoHa7_Sys_Object>(pFormCode);
            sysUserPermission = dao.GetById<PhoHa7_Sys_User_Permission>(pUserID, _FormCode);

            listSysRolePermission = new List<PhoHa7_Sys_Role_Permission>();

            Users = dao.GetById<Employee>(pUserID);
            if (Users != null)
            {
                dao.CollectionObject(Users, "PhoHa7_UserRole");
                ICollection<PhoHa7_UserRole> listUserroles = Users.PhoHa7_UserRole;
                if (listUserroles != null)
                {
                    foreach (var listUserrole in listUserroles)
                    {
                        dao.ReferenceObject(listUserrole, "sys_role");
                        dao.CollectionObject(listUserrole.PhoHa7_Sys_Role, "sys_role_permission");
                        listSysRolePermission.AddRange(dao.FindByMultiColumnAnd<PhoHa7_Sys_Role_Permission>(new[] { "RP_Object_ID" }, _FormCode));
                        //listSysRolePermission.AddRange(listUserrole.sys_role.sys_role_permission.Where(p => p.RP_Object_ID == _FormCode));
                    }
                }
            }
        }


        protected bool getPermission(EnumFormStatus pEnumFormStatus)
        {
            //root permission
            PhoHa7_Sys_Role newRole = dao.GetById<PhoHa7_Sys_Role>(Users.SecureLevel);
            if (newRole !=null)
            {
                if (newRole.Role_Level == 0)
                    return true;
            }
            


            if (sysUserPermission != null)
            {
                if (sysUserPermission.UP_Permission == null)
                {
                    return false;
                }

                var abc = sysUserPermission.UP_Permission & (int)pEnumFormStatus;
                if ((sysUserPermission.UP_Permission & (int)pEnumFormStatus) != 0)
                    return true;
                else
                {
                    return false;
                }
            }
            else
            {
                if (listSysRolePermission.Count > 0)
                {
                    List<PhoHa7_Sys_Role_Permission> obj =
                        listSysRolePermission.Where(p => (p.RP_Permission & (int)pEnumFormStatus) == 0).ToList();
                    if (obj.Count > 0)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool PermissionView()
        {
            return getPermission(EnumFormStatus.View);
        }

        public bool PermissionView(EnumFormCode frmCode)
        {
            SysObject = dao.GetById<PhoHa7_Sys_Object>(frmCode.ToString());
            sysUserPermission = dao.GetById<PhoHa7_Sys_User_Permission>(Users, frmCode.ToString());
            return getPermission(EnumFormStatus.View);
        }

        public bool PermissionAdd()
        {
            return getPermission(EnumFormStatus.Add);
        }

        public bool PermissionUpdate()
        {
            return getPermission(EnumFormStatus.Modify);
        }

        public bool PermissionDelete()
        {
            return getPermission(EnumFormStatus.Delete);
        }

        public bool PermissionPrint()
        {
            return getPermission(EnumFormStatus.Print);
        }

        public bool PermissionWord()
        {
            return getPermission(EnumFormStatus.Word);
        }

        public bool PermissionExcel()
        {
            return getPermission(EnumFormStatus.Excel);
        }

        public bool PermissionApproved()
        {
            return getPermission(EnumFormStatus.Approved);
        }

        public bool PermissionReport()
        {
            return getPermission(EnumFormStatus.Report);
        }


        public int PermissionAll()
        {
            int uP_Permission = 0;
            uP_Permission += PermissionView() ? (int)EnumFormStatus.View : 0;
            uP_Permission += PermissionAdd() ? (int)EnumFormStatus.Add : 0;
            uP_Permission += PermissionUpdate() ? (int)EnumFormStatus.Modify : 0;
            uP_Permission += PermissionDelete() ? (int)EnumFormStatus.Delete : 0;
            uP_Permission += PermissionPrint() ? (int)EnumFormStatus.Print : 0;
            uP_Permission += PermissionWord() ? (int)EnumFormStatus.Word : 0;
            uP_Permission += PermissionExcel() ? (int)EnumFormStatus.Excel : 0;
            uP_Permission += PermissionReport() ? (int)EnumFormStatus.Report : 0;
            uP_Permission += PermissionApproved() ? (int)EnumFormStatus.Approved : 0;
            return uP_Permission;
        }









    }
}
