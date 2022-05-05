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
    public class NivelPruebaDA : BaseAbstractDA
    {
        public IList<NivelPruebaBE> List(NivelPruebaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_NIVEL_PRUEBA");
                db.AddInParameter(cmd, "@VC_CRITERIO", DbType.String, oItem.vc_criterio);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("NivelPruebaDA.List()", ex);
            }
        }

        private IList<NivelPruebaBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<NivelPruebaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private NivelPruebaBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new NivelPruebaBE();

            item.nu_id_nivel_prueba = oR["NU_ID_NIVEL_PRUEBA"].ToInt32();
            item.vc_desc_nivel_prueba = oR["VC_DESC_NIVEL_PRUEBA"].ToText();

            return item;
        }
    }
}
