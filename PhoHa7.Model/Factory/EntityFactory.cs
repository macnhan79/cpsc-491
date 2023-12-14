using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Model.Factory
{
    public class EntityFactory : AbstractFactory
    {
        static EntityFactory entityFactory = new EntityFactory();

        public static EntityFactory getInstance()
        {
            return entityFactory;
        }

        private EntityFactory() { }


        private Entities sumenEntityTrans;// = new sumenEntities();
      //  private sumenEntitiesSystem sumenEntitySystemTrans;// = new sumenEntitiesSystem();


        public DbContextTransaction SumenTransaction { get; set; }
       // public DbContextTransaction SystemTransaction { get; set; }


        public override Entities CreateEntities()
        {
            Entities obj = new Entities();
            
            obj.Configuration.LazyLoadingEnabled = false;
            obj.Configuration.ProxyCreationEnabled = false;
            return obj;
        }

        public override Entities CreateLazyEntities()
        {
            Entities obj = new Entities();
            return obj;
        }

        public override Entities CreateUpdateTransactionEntities()
        {
            if (sumenEntityTrans == null)
            {
                sumenEntityTrans = new Entities();
            }
            return sumenEntityTrans;
        }

        public DbContextTransaction BeginTransactionEntities()
        {
            if (sumenEntityTrans == null)
            {
                sumenEntityTrans = new Entities();
            }
            if (SumenTransaction == null)
            {
                SumenTransaction = sumenEntityTrans.Database.BeginTransaction();
            }
            return SumenTransaction;
        }

        public override void commit()
        {
            try
            {
                SumenTransaction.Commit();
            }
            catch
            {
                rollBack();
            }
            finally
            {
                sumenEntityTrans = null;
                SumenTransaction = null;
                CreateUpdateTransactionEntities();
            }
        }

        public override void rollBack()
        {
            SumenTransaction.Rollback();
            sumenEntityTrans = null;
            SumenTransaction = null;
            CreateUpdateTransactionEntities();
        }

    }
}
