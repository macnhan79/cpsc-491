using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Model.Presenter.Sys
{
    public class SysRolePresenter
    {




        public ICollection<PhoHa7_Sys_Role> GetAll()
        {
            using (Entities objSumenEntitiesSystem = new Entities())
            {
                return objSumenEntitiesSystem.PhoHa7_Sys_Role.ToList();
            }

        }

    }
}
