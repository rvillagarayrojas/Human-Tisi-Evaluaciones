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
    public class TipoPerfilBL
    {
        private TipoPerfilDA oTipoPerfilDA;

        public TipoPerfilBL()
        {
            oTipoPerfilDA = new TipoPerfilDA();
        }

        public IList<TipoPerfilBE> List(TipoPerfilBE oItem)
        {
            try
            {
                return oTipoPerfilDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPerfilBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
