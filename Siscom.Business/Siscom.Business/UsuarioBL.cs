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
    public class UsuarioBL : IBaseBL<UsuarioBE>
    {
        private UsuarioDA oUsuarioDA;
        public UsuarioBL()
        {
            oUsuarioDA = new UsuarioDA();
        }
        public IList<UsuarioBE> List(UsuarioBE oItem)
        {
            try
            {
                return oUsuarioDA.List(oItem);   
            }
            catch (Exception ex)
            {
                throw new Exception("UsuarioBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

        public int Update(UsuarioBE oItem)
        {
            try
            {
                return oUsuarioDA.Update(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("UsuarioBL.Update()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

    }
}
