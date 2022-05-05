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
    public class NivelPruebaBL
    {
        private NivelPruebaDA oNivelPruebaDA;

        public NivelPruebaBL()
        {
            oNivelPruebaDA = new NivelPruebaDA();
        }

        public IList<NivelPruebaBE> List(NivelPruebaBE oItem)
        {
            try
            {
                return oNivelPruebaDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("NivelPruebaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}
