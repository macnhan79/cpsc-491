using System.Collections.Generic;

namespace PhoHa7.Library.Interface
{
    public interface IDao
    {
        int Add<T>(T pObject) where T : class;
        int AddRange<T>(params T[] pObject) where T : class;
        int AddOrUpdate<T>(params T[] pObject) where T : class;
        int Update<T>(T pObject) where T : class;
        int Delete<T>(params object[] pId) where T : class;
        ICollection<T> GetAll<T>() where T : class;
        T GetById<T>(params object[] keyValues) where T : class;
    }

}
