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

namespace Siscom.Business
{
    public class CandidatosBL
    {
        private CandidatosDA oCandidatosDA;
        public CandidatosBL()
        {
            oCandidatosDA = new CandidatosDA();
        }

        public IList<CandidatoBE> List(CandidatoBE oItem)
        {
            try
            {
                if (oItem.opcion == 0)
                {
                    return oCandidatosDA.List(oItem);
                }
                else
                {
                    return oCandidatosDA.ListPruebas(oItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CandidatosBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

    }
}
