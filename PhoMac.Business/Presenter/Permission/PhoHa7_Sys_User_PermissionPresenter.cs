using PhoHa7.Library.Enum;
using PhoMac.Business.Data;
using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter.Sys
{
    public class PhoHa7_Sys_User_PermissionPresenter
    {
        PhoHa7_Sys_User_Permission dic;
        public List<PhoHa7_Sys_User_PermissionPresenter> ListSys_User_Permission;


        public PhoHa7_Sys_User_Permission Sys_User_Permission
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;
                //this.UP_Object_Parent_ID = dic.UP_Object_Parent_ID;
                //this.UP_Object_Name = dic.UP_Object_Name;
                this.UP_User_ID = dic.UP_User_ID;
                this.UP_Object_ID = dic.UP_Object_ID;
                this.UP_Permission = Convert.ToInt32(dic.UP_Permission);
            }
        }

        public PhoHa7_Sys_User_PermissionPresenter()
        {
            dic = new PhoHa7_Sys_User_Permission();
            ListSys_User_Permission = new List<PhoHa7_Sys_User_PermissionPresenter>();
        }

        public void CopyToList(List<PhoHa7_Sys_User_Permission> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                PhoHa7_Sys_User_PermissionPresenter obj = new PhoHa7_Sys_User_PermissionPresenter();
                obj.Sys_User_Permission = pListDic[i];
                ListSys_User_Permission.Add(obj);
            }
        }

        void copyInstance()
        {
            //dic.UP_Object_Parent_ID = UP_Object_Parent_ID;
            //dic.UP_Object_Name = UP_Object_Name;
            dic.UP_User_ID = UP_User_ID;
            dic.UP_Object_ID = UP_Object_ID;
            dic.UP_Permission = UP_Permission;
        }

#region Property

        public string UP_Object_Parent_ID { get; set; }
        public string UP_Object_Name { get; set; }
        public int UP_User_ID { get; set; }
        public string UP_Object_ID { get; set; }
        public int UP_Permission
        {
            get
            {
                uP_Permission = 0;
                uP_Permission += View ? (int)EnumFormStatus.View : 0;
                uP_Permission += Add ? (int)EnumFormStatus.Add : 0;
                uP_Permission += Update ? (int)EnumFormStatus.Modify : 0;
                uP_Permission += Delete ? (int)EnumFormStatus.Delete : 0;
                uP_Permission += Print ? (int)EnumFormStatus.Print : 0;
                uP_Permission += Word ? (int)EnumFormStatus.Word : 0;
                uP_Permission += Excel ? (int)EnumFormStatus.Excel : 0;
                uP_Permission += Report ? (int)EnumFormStatus.Report : 0;
                uP_Permission += Approved ? (int)EnumFormStatus.Approved : 0;
                return uP_Permission;
            }
            set
            {

                uP_Permission = value;
                View = PermissionView();
                Add = PermissionAdd();
                Update = PermissionUpdate();
                Delete = PermissionDelete();
                Print = PermissionPrint();
                Word = PermissionWord();
                Excel = PermissionExcel();
                Report = PermissionReport();
                Approved = PermissionApproved();
            }
        }

#endregion

#region Permission

        public bool PermissionView()
        {
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

        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool Word { get; set; }
        public bool Excel { get; set; }
        public bool Report { get; set; }
        public bool Approved { get; set; }
        private int uP_Permission;
        protected bool getPermission(EnumFormStatus pEnumFormStatus)
        {
            Dao dao = new Dao();
            Employee Users = dao.GetById<Employee>(UP_User_ID);
            if (Users != null)
            {
                if (Users.Administrator == null ? false : (bool)Users.Administrator)
                    return true;
            }
            if (uP_Permission == null)
            {
                return false;
            }
            else
            {
                return (uP_Permission & (int)pEnumFormStatus) != 0;
            }
        }

#endregion


    }
}
