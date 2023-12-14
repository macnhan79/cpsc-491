using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model.Data;

namespace PhoMac.Model.Presenter.Sys
{
    public class UserPresenter
    {
        public int Update(object pObjectValue, object pId)
        {
            using (Entities objEntities = new Entities())
            {
                Employee newUser = pObjectValue as Employee;
                ICollection<Employee> list = GetAll();
                Employee oldUser = objEntities.Employees.FirstOrDefault(p => p.EmployeeID == (int)pId);
                if (newUser.SecurityCode != "")
                {
                    //newUser.SecurityCode = ClsPublic.EncryptSHA512(newUser.User_Password);
                    oldUser.SecurityCode = newUser.SecurityCode;
                }
                oldUser.FullName = newUser.FullName;
                //oldUser.User_Description = newUser.User_Description;
                ////oldUser.User_Role_ID = newUser.User_Role_ID;
                //oldUser.User_Phone = newUser.User_Phone;
                //oldUser.User_Address = newUser.User_Address;
                //oldUser.User_Email = newUser.User_Email;
                //oldUser.User_Contact = newUser.User_Contact;
                oldUser.Active = newUser.Active;
                return objEntities.SaveChanges();
            }
        }

        public int Add(object pObjectValue)
        {
            using (Entities objEntities = new Entities())
            {
                Employee newUser = pObjectValue as Employee;
                //newUser.SecurityCode = ClsPublic.EncryptSHA512(newUser.SecurityCode);
                //newUser.SecurityCode = newUser.SecurityCode;
                objEntities.Employees.Add(newUser);
                return objEntities.SaveChanges();
            }
        }

        public int Delete(object pObjectValue)
        {
            using (Entities objEntities = new Entities())
            {
                Dao dao = new Dao();
                int user = (int)pObjectValue;
                Employee emp = objEntities.Employees.FirstOrDefault(p => p.EmployeeID == user);
                foreach (var item in emp.PhoHa7_Attendance)
                {
                    dao.Delete<PhoHa7_Attendance>(item.Att_AttendanceID);
                }

                objEntities.Employees.Remove(emp);
                return objEntities.SaveChanges();
            }
        }

        public ICollection<Employee> GetAll()
        {
            using (Entities objEntities = new Entities())
            {
                return objEntities.Employees.ToList();
            }
        }

        public Employee GetById(object pId)
        {
            using (Entities objEntities = new Entities())
            {
                int user = (int)pId;
                return objEntities.Employees.FirstOrDefault(p => p.EmployeeID == user);
            }
        }


    }
}
