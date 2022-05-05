using Conexiones.SQLServer;
using Entidad.A_General;
using Entidad.A_Seleccion;
using MultiEntidad.A_Seleccion;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaccion.A_Seleccion;

namespace Procedimiento.A_Seleccion
{
    public class P_Candidato_Evaluacion
    {
        T_Candidato_Evaluacion _T_Candidato_Evaluacion;

        public P_Candidato_Evaluacion()
        {
            _T_Candidato_Evaluacion = new T_Candidato_Evaluacion();
        }

        public List<MME_Prueba> Sel(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Candidato_Evaluacion.Get_Candidato_Evaluacion(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }



        public int Upd_Candidato_Evaluacion(MME_Prueba modelo)
        {
            var status = 0;
            try
            {
                status = _T_Candidato_Evaluacion.Upd_Candidato_Evaluacion(modelo);
            }
            catch (Exception ex) { throw ex; }
            return status;
        }


        public  MME_Prueba AC_Envio(MME_Prueba m)
        {
            using (TransactionScope Tran = new TransactionScope())
            {
                try
                {


                    m = _T_Candidato_Evaluacion.AC_Envio_Correo(m);
                    if (m.nu_status == 0)
                    {
                        Tran.Dispose();
                        return m;
                    }

                    Tran.Complete();
                }
                catch (Exception ex) { Tran.Dispose(); throw ex; }
            }
            return m;
        }


        public Tuple<Int32, String> Ins_Candidato(MME_Prueba M)
        {
            Tuple<Int32, String> ParameterOutPut;
            List<MME_Prueba> itemCorreos = new List<MME_Prueba>();
            DbCommand cmd = null;
            using (TransactionScope Tran = new TransactionScope())
            {
                try
                {
                    ParameterOutPut = _T_Candidato_Evaluacion.Ins_Candidato(ref cmd, M, out itemCorreos);
                    if (ParameterOutPut.Item1 == 0)
                    {
                        Tran.Dispose();
                        return ParameterOutPut;
                    }

                    Tran.Complete();
                }
                catch (Exception ex) { Tran.Dispose(); throw ex; }
                finally { cmd.Connection.Close(); }
            }

            try
            {
                ParameterOutPut = _T_Candidato_Evaluacion.Ins_Candidato_EnvioCorreo(itemCorreos, ParameterOutPut);
            }
            catch (Exception)
            {
                
                throw;
            }            

            return ParameterOutPut;
        }

        public List<E_Seguimiento> Get_Candidato_Seguimiento(E_Candidato modelo)
        {
            List<E_Seguimiento> ListaModeloSeguimniento = null;
            try
            {
                ListaModeloSeguimniento = _T_Candidato_Evaluacion.Get_Candidato_Seguimiento(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloSeguimniento;
        }

        public E_Candidato_Evaluacion ChangePassword(E_Candidato_Evaluacion oItem)
        {
            try
            {
                return _T_Candidato_Evaluacion.ChangePassword(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<E_RespuestaPreguntaFile> Get_RespuestaPreguntaFile(E_Candidato modelo, out String ServidorDescarga)
        {
            List<E_RespuestaPreguntaFile> ListaListaE_RespuestaPreguntaFile = null;
            try
            {
                ListaListaE_RespuestaPreguntaFile = _T_Candidato_Evaluacion.Get_RespuestaPreguntaFile(modelo, out ServidorDescarga);
            }
            catch (Exception ex) { throw ex; }
            return ListaListaE_RespuestaPreguntaFile;
        }

        public String Set_RespuestaPregunta_Upd(E_RespuestaPreguntaFile modelo)
        {
            String vc_Mensaje_Out = "";
            try
            {
                vc_Mensaje_Out = _T_Candidato_Evaluacion.Set_RespuestaPregunta_Upd(modelo);
            }
            catch (Exception ex) { throw ex; }
            return vc_Mensaje_Out;
        }
    }
}
