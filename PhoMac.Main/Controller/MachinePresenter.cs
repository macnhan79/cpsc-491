using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model.Data;
using PhoMac.Model;

namespace PhoMac.Main.Controller
{
    public class MachinePresenter
    {
        Dao dao;
        PhoHa7_Machine machineInfo;
        string machineName = string.Empty;
        public MachinePresenter()
        {
            dao = new Dao();
            machineName = Environment.MachineName;
            var list = dao.FindByMultiColumnAnd<PhoHa7_Machine>(new[] { "MachineName" }, machineName);
            if (list.Count==0)
            {
                machineInfo = new PhoHa7_Machine();
                machineInfo.MachineName = machineName;
                machineInfo.MachineActive = true;
                dao.Add<PhoHa7_Machine>(machineInfo);
            }
            else
            {
                machineInfo = list.First();
            }
        }

        public PhoHa7_Machine getMachineInfo()
        {
            return machineInfo;
        }

        public void update(PhoHa7_Machine machine)
        {
            dao.Update<PhoHa7_Machine>(machine);
        }

    }
}
