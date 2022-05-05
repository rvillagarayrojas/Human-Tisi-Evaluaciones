using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using Siscom.Entity.Global;
using Siscom.Data.Global;
using Siscom.Business.Interface;
using System.Transactions;

namespace Siscom.Business
{
    public class TipoPuestoBL
    {
         private TipoPuestoDA oTipoPuestoDA;

         public TipoPuestoBL()
        {
            oTipoPuestoDA = new TipoPuestoDA();
        }

         public IList<TipoPuestoBE> List(TipoPuestoBE oItem)
        {
            try
            {
                return oTipoPuestoDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPuestoBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
