using Conexiones.SQLServer;
using Entidad.A_Seleccion;
using MultiEntidad.A_Seleccion;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaccion.A_Seleccion;

namespace Procedimiento.A_Seleccion
{
    public class P_Reportes
    {
        T_Reportes _T_Reportes;

        public P_Reportes()
        {
            _T_Reportes = new T_Reportes();
        }

        public List<MME_Prueba> Sel_Candidatos_Evaluacion(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Sel_Candidatos_Evaluacion(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Wonderlik(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Wonderlic(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Get_Prc_Excel(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Excel(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }
        public List<MME_Prueba> Sel_Prc_Barsit(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Barsit(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Monedas1(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Monedas1(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Gatb(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Gatb(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Ic(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Ic(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Disc(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Disc(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Analisis_Puesto(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Analisis_Puesto(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Analisis_Puesto_Candidato(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Analisis_Puesto_Candidato(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Cps(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Cps(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Kostick(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Kostick(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Ipv(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Ipv(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Zavic(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Zavic(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Domino(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Domino(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_TIG1_Domino(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_TIG1_Domino(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Raven(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Raven(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Ice_Baron(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Ice_Baron(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Neo_Pir(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Neo_Pir(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_BFQ(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_BFQ(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_16pf(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_16pf(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Minimult(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Minimult(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Ingles(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Ingles(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_SIV(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_SIV(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Prc_Habil_General(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Prc_Habil_General(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_DatosPersonales(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_DatosPersonales_Ficha(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Conocimiento(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Conocimiento_Ficha(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Educacion(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Educacion_Ficha(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }

        public List<MME_Prueba> Sel_Experiencia(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_ExperienciaLaboral_Ficha(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }
        public List<MME_Prueba> Sel_Familiares(MME_Prueba modelo)
        {
            List<MME_Prueba> ListaModeloEntidad = null;
            try
            {
                ListaModeloEntidad = _T_Reportes.Get_Familiares_Ficha(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaModeloEntidad;
        }


        public List<E_PruebaCandidato> Get_Pruebas_Candidato(MME_Prueba modelo)
        {
            List<E_PruebaCandidato> ListaPruebasCandidato = null;
            try
            {
                ListaPruebasCandidato = _T_Reportes.Get_Pruebas_Candidato(modelo);
            }
            catch (Exception ex) { throw ex; }
            return ListaPruebasCandidato;
        }
    }
}
