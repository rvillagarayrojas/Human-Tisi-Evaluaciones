using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using Siscom.Entity.Persona;
using Siscom.Data.Persona;
using Siscom.Business.Interface;
using System.Transactions;
using Siscom.Data.Global;
using Siscom.Entity.Global;

namespace Siscom.Business
{
    public class CuentaBL
    {
         private CuentaDA oCuentaDA;

         public CuentaBL()
        {
            oCuentaDA = new CuentaDA();
        }

         public int Insert(CuentaBE oItem)
         {
             try
             {
                 return oCuentaDA.Insert(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }

        public int Update(CuentaBE oItem)
        {
            try
            {
                return oCuentaDA.Update(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public CuentaBE ChangePassword(CuentaBE oItem)
        {
            try
            {
                return oCuentaDA.ChangePassword(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
         public IList<CuentaBE> List(CuentaBE oItem)
         {
             try
             {
                 return oCuentaDA.List(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("CuentaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }

         public CuentaBE Get(CuentaBE oItem)
         {
             try
             {
                 return oCuentaDA.Get(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("CuentaBL.Get()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
    }
}
