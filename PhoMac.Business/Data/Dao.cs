using PhoMac.Model;
using PhoMac.Model.Factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PhoMac.Business.Data
{
    public class Dao : IDao
    {
        //public DbContext _dbContext;



        private bool HasTransaction;
        private bool IsLazy;


        public Dao(bool isLazy = false, bool hasTransaction = false)
        {
            IsLazy = isLazy;
            HasTransaction = hasTransaction;
            //CheckConnection();
        }

        /// <summary>
        /// Add or update auto destroy entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int AddOrUpdate<T>(params T[] pObject) where T : class
        {

            using (DbContext _dbContext = GetContext<T>())
            {
                DbSet<T> abc = _dbContext.Set<T>();
                abc.AddOrUpdate(pObject);
                int save = _dbContext.SaveChanges();
                return save;
            }
        }

        /// <summary>
        /// Add or update with para entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int AddOrUpdate<T>(DbContext _dbContext, params T[] pObject) where T : class
        {
            _dbContext = GetContext<T>();
            DbSet<T> abc = _dbContext.Set<T>();
            abc.AddOrUpdate(pObject);
            int save = _dbContext.SaveChanges();
            return save;
        }

        /// <summary>
        /// auto destroy entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int Add<T>(T pObject) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {
                //_dbContext = GetContext<T>();
                DbSet<T> abc = _dbContext.Set<T>();
                abc.Add((T)pObject);
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int Add<T>(DbContext _dbContext, T pObject) where T : class
        {
            //_dbContext = GetContext<T>();
            DbSet<T> abc = _dbContext.Set<T>();
            abc.Add((T)pObject);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int AddRange<T>(params T[] pObject) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {
                //_dbContext = GetContext<T>();
                DbSet<T> abc = _dbContext.Set<T>();
                abc.AddRange(pObject);
                int result = _dbContext.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int AddRange<T>(DbContext _dbContext, params T[] pObject) where T : class
        {
            //_dbContext = GetContext<T>();
            DbSet<T> abc = _dbContext.Set<T>();
            abc.AddRange(pObject);
            int result = _dbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int Update<T>(T pObject) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {//_dbContext = GetContext<T>();
                _dbContext.Entry<T>((T)pObject).State = EntityState.Modified;
                int result = _dbContext.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public int Update<T>(DbContext _dbContext, T pObject) where T : class
        {
            _dbContext.Entry<T>((T)pObject).State = EntityState.Modified;
            int result = _dbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        public int Delete<T>(params object[] pId) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {//_dbContext = GetContext<T>();
                _dbContext.Entry<T>(GetById<T>(pId)).State = EntityState.Deleted;
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public int Delete<T>(DbContext _dbContext, params object[] pId) where T : class
        {
            _dbContext.Entry<T>(GetById<T>(pId)).State = EntityState.Deleted;
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetAll<T>() where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {//_dbContext = GetContext<T>();
                DbSet<T> dbSet = _dbContext.Set<T>();
                return dbSet.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ICollection<T> GetAll<T>(DbContext _dbContext) where T : class
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return dbSet.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T GetById<T>(params object[] keyValues) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {
                return _dbContext.Set<T>().Find(keyValues);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T GetById<T>(DbContext _dbContext, params object[] keyValues) where T : class
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        public ICollection<T> FindByMultiColumnAnd<T>(string[] columnName, params object[] paraValue) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {//_dbContext = GetContext<T>();
                DbSet<T> dbSet = _dbContext.Set<T>();

                PropertyInfo[] propertyInfos = typeof(T).GetProperties();

                Expression<Func<T, bool>> lamda = null;
                var parameter = Expression.Parameter(typeof(T));
                //lay ten cot
                for (int i = 0; i < columnName.Length; i++)
                {
                    string name = columnName[i];
                    PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
                    if (propertyInfo == null)
                    {
                        return null;
                    }
                    //PropertyInfo propertyInfo = typeof(T).GetProperty(columnName[i]);
                    ConstantExpression constantValue = Expression.Constant(paraValue[i], propertyInfo.PropertyType);
                    if (i == 0)
                    {
                        lamda =
                                Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue),
                                    parameter);
                    }
                    else
                    {
                        var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue), parameter);
                        var binaryExpression = Expression.And(lamda.Body, expression.Body);
                        lamda = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                    }
                }
                var abc = dbSet.Where(lamda).ToList();
                return abc;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="columnName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        public ICollection<T> FindByMultiColumnAnd<T>(DbContext _dbContext, string[] columnName, params object[] paraValue) where T : class
        {
            DbSet<T> dbSet = _dbContext.Set<T>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            Expression<Func<T, bool>> lamda = null;
            var parameter = Expression.Parameter(typeof(T));
            //lay ten cot
            for (int i = 0; i < columnName.Length; i++)
            {
                string name = columnName[i];
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
                if (propertyInfo == null)
                {
                    return null;
                }
                //PropertyInfo propertyInfo = typeof(T).GetProperty(columnName[i]);
                ConstantExpression constantValue = Expression.Constant(paraValue[i], propertyInfo.PropertyType);
                if (i == 0)
                {
                    lamda =
                            Expression.Lambda<Func<T, bool>>(
                                Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue),
                                parameter);
                }
                else
                {
                    var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue), parameter);
                    var binaryExpression = Expression.And(lamda.Body, expression.Body);
                    lamda = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                }
            }
            var abc = dbSet.Where(lamda).ToList();
            return abc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        public ICollection<T> FindByMultiColumnOr<T>(string[] columnName, params object[] paraValue) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {//_dbContext = GetContext<T>();
                DbSet<T> dbSet = _dbContext.Set<T>();

                PropertyInfo[] propertyInfos = typeof(T).GetProperties();

                Expression<Func<T, bool>> lamda = null;
                var parameter = Expression.Parameter(typeof(T));
                //lay ten cot
                for (int i = 0; i < columnName.Length; i++)
                {
                    string name = columnName[i];
                    PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
                    if (propertyInfo == null)
                    {
                        return null;
                    }
                    //PropertyInfo propertyInfo = typeof(T).GetProperty(columnName[i]);
                    ConstantExpression constantValue = Expression.Constant(paraValue[i], propertyInfo.PropertyType);
                    if (i == 0)
                    {
                        lamda =
                                Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue),
                                    parameter);
                    }
                    else
                    {
                        var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue), parameter);
                        var binaryExpression = Expression.Or(lamda.Body, expression.Body);
                        lamda = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                    }
                }
                var abc = dbSet.Where(lamda).ToList();
                return abc;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="columnName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        public ICollection<T> FindByMultiColumnOr<T>(DbContext _dbContext, string[] columnName, params object[] paraValue) where T : class
        {
            DbSet<T> dbSet = _dbContext.Set<T>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            Expression<Func<T, bool>> lamda = null;
            var parameter = Expression.Parameter(typeof(T));
            //lay ten cot
            for (int i = 0; i < columnName.Length; i++)
            {
                string name = columnName[i];
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
                if (propertyInfo == null)
                {
                    return null;
                }
                //PropertyInfo propertyInfo = typeof(T).GetProperty(columnName[i]);
                ConstantExpression constantValue = Expression.Constant(paraValue[i], propertyInfo.PropertyType);
                if (i == 0)
                {
                    lamda =
                            Expression.Lambda<Func<T, bool>>(
                                Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue),
                                parameter);
                }
                else
                {
                    var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.MakeMemberAccess(parameter, propertyInfo), constantValue), parameter);
                    var binaryExpression = Expression.Or(lamda.Body, expression.Body);
                    lamda = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                }
            }
            var abc = dbSet.Where(lamda).ToList();
            return abc;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="referenceObject"></param>
        public void ReferenceObject<T>(T obj, string referenceObject) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(referenceObject.ToLower()));
                if (propertyInfo != null)
                {
                    _dbContext.Entry<T>(obj).Reference(referenceObject).Load();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="referenceObject"></param>
        public void ReferenceObject<T>(DbContext _dbContext, T obj, string referenceObject) where T : class
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(referenceObject.ToLower()));
            if (propertyInfo != null)
            {
                _dbContext.Entry<T>(obj).Reference(referenceObject).Load();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="referenceObject"></param>
        public void CollectionObject<T>(T obj, string referenceObject) where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(referenceObject.ToLower()));
                if (propertyInfo != null)
                {
                    _dbContext.Entry<T>(obj).Collection(propertyInfo.Name).Load();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <param name="obj"></param>
        /// <param name="referenceObject"></param>
        public void CollectionObject<T>(DbContext _dbContext,T obj, string referenceObject) where T : class
        {
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(p => p.Name.ToLower().Contains(referenceObject.ToLower()));
                if (propertyInfo != null)
                {
                    _dbContext.Entry<T>(obj).Collection(propertyInfo.Name).Load();
                }
                else
                {
                    throw new ArgumentNullException();
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<string> GetListNamePrimaryKey<T>() where T : class
        {
            using (DbContext _dbContext = GetContext<T>())
            {// _dbContext = GetContext<T>();
                var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_dbContext).ObjectContext;
                System.Data.Entity.Core.Objects.ObjectSet<T> set =
                                   objectContext.CreateObjectSet<T>();
                IEnumerable<string> keyNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name);
                return keyNames;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dbContext"></param>
        /// <returns></returns>
        public IEnumerable<string> GetListNamePrimaryKey<T>(DbContext _dbContext) where T : class
        {
                var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_dbContext).ObjectContext;
                System.Data.Entity.Core.Objects.ObjectSet<T> set =
                                   objectContext.CreateObjectSet<T>();
                IEnumerable<string> keyNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name);
                return keyNames;
        }


        public DbContext GetContext<T>()
        {
            if (typeof(T) == typeof(Appointment) ||
                typeof(T) == typeof(CardInfo) ||
                typeof(T) == typeof(Category) ||
                 typeof(T) == typeof(Customer) ||
                  typeof(T) == typeof(CustomerType) ||
                   typeof(T) == typeof(DailyDraw) ||
                typeof(T) == typeof(DelItem) ||
                typeof(T) == typeof(Employee) ||
                 typeof(T) == typeof(Error) ||
                 typeof(T) == typeof(ExpCategory) ||
                 typeof(T) == typeof(ExpProduct) ||
                 typeof(T) == typeof(GiftCert) ||
                 typeof(T) == typeof(GiftHistory) ||
                 typeof(T) == typeof(Message) ||
                typeof(T) == typeof(PaymentType) ||
                typeof(T) == typeof(PhoHa7_Attendance) ||
                typeof(T) == typeof(PhoHa7_CardProcess) ||
                typeof(T) == typeof(PhoHa7_FilterSaleItem) ||
                typeof(T) == typeof(PhoHa7_Machine) ||
                typeof(T) == typeof(PhoHa7_ProcSaleItem) ||
                typeof(T) == typeof(PhoHa7_ProcTickets) ||
                typeof(T) == typeof(PhoHa7_ProductType) ||
                typeof(T) == typeof(PhoHa7_Sys_Object) ||
                typeof(T) == typeof(PhoHa7_Sys_Option) ||
                typeof(T) == typeof(PhoHa7_Sys_Role) ||
                typeof(T) == typeof(PhoHa7_Sys_Role_Permission) ||
                typeof(T) == typeof(PhoHa7_Sys_User_Permission) ||
                typeof(T) == typeof(PhoHa7_UserRole) ||
                typeof(T) == typeof(ProcSaleItem) ||
                typeof(T) == typeof(ProcTicket) ||
                typeof(T) == typeof(Product) ||
                typeof(T) == typeof(Product_Size) ||
                typeof(T) == typeof(Product_SizeDetails) ||
                typeof(T) == typeof(ProductType) ||
                 typeof(T) == typeof(ReasonItem) ||
                typeof(T) == typeof(SaleItem) ||
                typeof(T) == typeof(TabCategory) ||
                typeof(T) == typeof(Table) ||
                 typeof(T) == typeof(DaySale) ||
                 typeof(T) == typeof(Dictionary) ||
                typeof(T) == typeof(Ticket))
            {
                if (!HasTransaction)
                {
                    if (IsLazy)
                    {
                        return new Entities();
                        //return EntityFactory.getInstance().CreateLazyEntities();
                    }
                    else
                    {
                        Entities obj = new Entities();

                        obj.Configuration.LazyLoadingEnabled = false;
                        obj.Configuration.ProxyCreationEnabled = false;
                        return obj;
                        //return EntityFactory.getInstance().CreateEntities();
                    }

                }

                else
                    return EntityFactory.getInstance().CreateUpdateTransactionEntities();
            }
            else
            {
                throw new Exception("Table does not exist!");
            }
            //else
            //{
            //    if (!HasTransaction)
            //    {
            //        if (IsLazy)
            //        {
            //            return EntityFactory.getInstance().CreateSystemLazyEntities();
            //        }
            //        else
            //        {
            //            return EntityFactory.getInstance().CreateSystemEntities();
            //        }

            //    }
            //    else return EntityFactory.getInstance().CreateSystemUpdateTransactionEntities();
            //}
        }





    }
}
