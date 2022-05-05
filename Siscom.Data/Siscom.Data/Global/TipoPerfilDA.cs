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
    public class TipoPerfilDA : BaseAbstractDA
    {
        public IList<TipoPerfilBE> List(TipoPerfilBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PERFIL");    //SP_WEB_SEL_TipoMoneda
                db.AddInParameter(cmd, "@VC_CRITERIO", DbType.String, oItem.nu_id_perfil);
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TipoPerfilDA.List()", ex);
            }
        }

        private IList<TipoPerfilBE> MapDataReaderToList(IDataReader oR)
        {
            var lista = new List<TipoPerfilBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR));
            }

            return lista;
        }

        private TipoPerfilBE MapDataReaderToEntity(IDataReader oR)
        {
            var item = new TipoPerfilBE();

            item.nu_id_perfil   = oR["NU_ID_PERFIL"].ToInt32();
            item.vc_desc_perfil = oR["VC_DESC_PERFIL"].ToText();
            item.ch_status      = oR["CH_STATUS"].ToText();
            
            return item;
        }
    }
}
