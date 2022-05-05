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
    public class TipoPruebaBL
    {
        private TipoPruebaDA oTipoPruebaDA;

        public TipoPruebaBL()
        {
            oTipoPruebaDA = new TipoPruebaDA();
        }

        public IList<TipoPruebaBE> List(TipoPruebaBE oItem)
        {
            try
            {
                return oTipoPruebaDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPruebaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
