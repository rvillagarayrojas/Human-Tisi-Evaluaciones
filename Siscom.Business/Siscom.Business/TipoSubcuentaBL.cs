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
    public class TipoSubcuentaBL
    {
        private TipoSubCuentaDA oTipoSubCuentaDA;

        public TipoSubcuentaBL()
        {
            oTipoSubCuentaDA = new TipoSubCuentaDA();
        }

        public IList<TipoSubCuentaBE> List(TipoSubCuentaBE oItem)
        {
            try
            {
                return oTipoSubCuentaDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("TipoSubcuentaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
