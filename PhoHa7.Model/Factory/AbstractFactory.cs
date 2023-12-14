using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Model.Factory
{
    public abstract class AbstractFactory
    {



        public abstract Entities CreateEntities();
        public abstract Entities CreateLazyEntities();
        public abstract Entities CreateUpdateTransactionEntities();
        public abstract void commit();
        public abstract void rollBack();


        //public abstract sumenEntitiesSystem CreateSystemEntities();
        //public abstract sumenEntitiesSystem CreateSystemLazyEntities();
        //public abstract sumenEntitiesSystem CreateSystemUpdateTransactionEntities();
        //public abstract void commitSystem();
        //public abstract void rollBackSystem();





    }
}
