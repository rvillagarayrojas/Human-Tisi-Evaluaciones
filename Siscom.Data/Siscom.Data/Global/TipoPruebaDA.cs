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
    public class TipoPruebaDA : BaseAbstractDA
    {
        public IList<TipoPruebaBE> List(TipoPruebaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_TIPO_PRUEBA");
                db.AddInParameter(cmd, "@NU_ID_NIVEL_PRUEBA", DbType.Decimal, oItem.nu_id_nivel_prueba);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPruebaDA.List()", ex);
            }
        }

        private IList<TipoPruebaBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<TipoPruebaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private TipoPruebaBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new TipoPruebaBE();

            item.nu_id_tipo_prueba      = oR["NU_ID_TIPO_PRUEBA"].ToDecimal();
            item.vc_desc_tipo_prueba    = oR["VC_DESC_TIPO_PRUEBA"].ToText();

            return item;
        }
    }
}
