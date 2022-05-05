using System.Collections.Generic;

namespace Siscom.Business.Interface
{
    public interface IBaseBL<T>
    {
        IList<T> List(T oItem);

        //T Get(T oItem);

        //int Insert(T oItem);

        //int Update(T oItem);

        //int Delete(T oItem);
    }
}
