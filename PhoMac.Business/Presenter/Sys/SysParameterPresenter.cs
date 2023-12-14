using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter.Sys
{
    public class SysParameterPresenter
    {
        private Entities _sumenEntities;// = new sumenEntitiesSystem();

        public SysParameterPresenter(Entities pContext)
        {
            _sumenEntities = pContext;
        }

        public string GetScalar(string pOptionID)
        {

            var query = _sumenEntities.PhoHa7_Sys_Option.FirstOrDefault(p => p.Opt_ID == pOptionID);
            if (query != null)
                return query.Opt_Value;
            else
                return "";


        }


    }
}
