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
    public class TipoPuestoDA : BaseAbstractDA
    {
        public IList<TipoPuestoBE> List(TipoPuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PUESTO_SUBCUENTA");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPuestoDA.List()", ex);
            }
        }

        private IList<TipoPuestoBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<TipoPuestoBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private TipoPuestoBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new TipoPuestoBE();

            item.nu_id_puesto       = oR["NU_ID_PUESTO"].ToInt32();
            item.nu_id_subcuenta    = oR["NU_ID_SUBCUENTA"].ToInt32();
            item.nu_id_cuenta       = oR["NU_ID_CUENTA"].ToInt32();
            item.vc_desc_puesto     = oR["VC_DESC_PUESTO"].ToText();
            item.ch_status          = oR["CH_STATUS"].ToText();
            item.vc_usr_reg         = oR["VC_USR_REG"].ToText();
            item.dt_fec_reg         = oR["DT_FEC_REG"].ToDateTime();

            return item;
        }
    }
}
