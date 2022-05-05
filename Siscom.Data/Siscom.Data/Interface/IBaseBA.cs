using System.Collections.Generic;

namespace Siscom.Data.Interface
{
    public interface IBaseBA<T>
    {
        IList<T> List(T oItem);

        //T Get(T oItem);

        //int Insert(T oItem);

        //int Update(T oItem);

        //int Delete(T oItem);
    }
}
