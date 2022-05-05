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
    public class SubCuentaBL
    {
        private SubCuentaDA oSubCuentaDA;

        public SubCuentaBL()
        {
            oSubCuentaDA = new SubCuentaDA();
        }

        public int Insert(SubCuentaBE oItem)
        {
            try
            {
                return oSubCuentaDA.Insert(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public int Update(SubCuentaBE oItem)
        {
            try
            {
                return oSubCuentaDA.Update(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public int UpdateUsuario(SubCuentaBE oItem)
        {
            try
            {
                return oSubCuentaDA.UpdateUsuario(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public IList<SubCuentaBE> List(SubCuentaBE oItem)
        {
            try
            {
                return oSubCuentaDA.List(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("SubCuentaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

        public SubCuentaBE Get(SubCuentaBE oItem)
        {
            try
            {
                return oSubCuentaDA.Get(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("SubCuentaBL.Get()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

    }
}
