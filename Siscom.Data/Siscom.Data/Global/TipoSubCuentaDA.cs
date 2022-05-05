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
    public class TipoSubCuentaDA : BaseAbstractDA
    {
        public IList<TipoSubCuentaBE> List(TipoSubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_TIPO_SUBCUENTAS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TipoSubCuentaDA.List()", ex);
            }
        }

        private IList<TipoSubCuentaBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<TipoSubCuentaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private TipoSubCuentaBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new TipoSubCuentaBE();

            item.nu_id_cuenta       = oR["NU_ID_CUENTA"].ToInt32();
            item.nu_id_subcuenta    = oR["NU_ID_SUBCUENTA"].ToInt32();
            item.vc_desc_sub_cuenta = oR["VC_DESC_SUB_CUENTA"].ToText();

            return item;
        }
    }
}
