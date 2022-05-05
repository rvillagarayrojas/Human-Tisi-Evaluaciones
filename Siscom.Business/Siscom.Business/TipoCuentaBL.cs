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
    public class TipoCuentaBL
    {
        private TipoCuentaDA oTipoCuentaDA;

        public TipoCuentaBL()
        {
            oTipoCuentaDA = new TipoCuentaDA();
        }

        public IList<TipoCuentaBE> List(TipoCuentaBE oItem)
        {
            try
            {
                return oTipoCuentaDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("TipoCuentaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
