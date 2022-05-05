using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Siscom.Entity.Global;
using Siscom.Library.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Siscom.Data.Interface;

namespace Siscom.Data.Global
{
    public class TipoCuentaDA : BaseAbstractDA
    {
        public IList<TipoCuentaBE> List(TipoCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_CUENTAS1");    //SP_WEB_SEL_TipoMoneda
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.String, oItem.nu_id_cuenta);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TipoCuentaDA.List()", ex);
            }
        }

        private IList<TipoCuentaBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<TipoCuentaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private TipoCuentaBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new TipoCuentaBE();

            item.nu_id_cuenta   = oR["NU_ID_CUENTA"].ToInt32();
            item.vc_desc_cuenta = oR["VC_DESC_CUENTA"].ToText();
            item.ch_status      = oR["CH_STATUS"].ToText();
            item.vc_usr_reg     = oR["VC_USR_REG"].ToText();
            item.dt_fec_reg     = oR["DT_FEC_REG"].ToDateTime();

            return item;
        }
    }
}
