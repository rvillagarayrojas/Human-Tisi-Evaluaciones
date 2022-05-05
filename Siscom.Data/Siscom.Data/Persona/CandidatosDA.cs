using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Siscom.Entity.Persona;
using Siscom.Library.Common;
using System.Net.Mail;
using System.Text;
using System.Net;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Siscom.Data.Interface;

namespace Siscom.Data.Persona
{
    public class CandidatosDA : BaseAbstractDA
    {

        public IList<CandidatoBE> List(CandidatoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_HISTO_CANDIDATOS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CandidatosDA.List()", ex);
            }
        }

        public IList<CandidatoBE> ListPruebas(CandidatoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_USO_PRUEBAS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, 2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CandidatosDA.List()", ex);
            }
        }

        private IList<CandidatoBE> MapDataReaderToList(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<CandidatoBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private CandidatoBE MapDataReaderToEntity(IDataReader oR, decimal nOpcion)
        {
            var item = new CandidatoBE();
            if (nOpcion == 1)
            {
                item.cant_candidatos    = oR["CANT_CANDIDATOS"].ToDecimal();
                item.mes_reg            = oR["MES_REG"].ToText();
                item.anno_reg           = oR["ANNO_REG"].ToDecimal();
            }
            if (nOpcion == 2)
            {
                item.cant_pruebas       = oR["CANT_PRUEBAS"].ToDecimal();
                item.vc_desc_prueba     = oR["VC_DESC_PRUEBA"].ToText();
            }
            return item;
        }

    }
}
