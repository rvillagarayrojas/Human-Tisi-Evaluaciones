using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Siscom.Controllers;
using Siscom.Entity.Global;
using Siscom.Entity.Persona;
using Siscom.SClient.Global;


namespace Siscom.Utility
{
    public class MetodosApp
    {
        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaTipoPerfil()
        {
            try
            {
                TipoPerfilRestClient _TipoPerfil = new TipoPerfilRestClient();

                var list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });

                var tipoperfilObj = new TipoPerfilBE() { };

                var listtipoperfil = _TipoPerfil.GetByFilters(tipoperfilObj);

                foreach (var item in listtipoperfil)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_perfil, Value = item.nu_id_perfil.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }


        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaTipoCuenta(PersonaBE oItem)
        {
            try
            {
                TipoCuentaRestClient _TipoCuenta = new TipoCuentaRestClient();

                var list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });


                var listtipocuenta = _TipoCuenta.GetByFilters(oItem);

                foreach (var item in listtipocuenta)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_cuenta, Value = item.nu_id_cuenta.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }

        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaTipoSubCuenta(PersonaBE oItem)
        {
            try
            {
                TipoSubCuentaRestClient _TipoSubCuenta = new TipoSubCuentaRestClient();

                var list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });

               
                var listtiposubcuenta = _TipoSubCuenta.GetByFilters(oItem);

                foreach (var item in listtiposubcuenta)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_sub_cuenta, Value = item.nu_id_subcuenta.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }

        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaTipoPuesto(PersonaBE oItem)
        {
            try
            {
                TipoPuestoRestClient _TipoPuesto= new TipoPuestoRestClient();

                var list = new List<SelectListItem>();

                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });


                var listtipopuesto = _TipoPuesto.GetByFilters(oItem);

                foreach (var item in listtipopuesto)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_puesto, Value = item.nu_id_puesto.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }


        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaTipoPrueba(PuestoBE oItem)
        {
            try
            {
                TipoPruebaRestClient _TipoPrueba = new TipoPruebaRestClient();

                var list = new List<SelectListItem>();

                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });


                var listtipopuesto = _TipoPrueba.GetByFilters(oItem);

                foreach (var item in listtipopuesto)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_tipo_prueba, Value = item.nu_id_tipo_prueba.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }

        // Carga Combo de Tipo de Perfil
        public static List<SelectListItem> ListaNivelPrueba(PuestoBE oItem)
        {
            try
            {
                NivelPruebaRestClient _NivelPrueba = new NivelPruebaRestClient();

                var list = new List<SelectListItem>();

                list.Add(new SelectListItem() { Text = "Seleccione", Value = "-1" });


                var listtipopuesto = _NivelPrueba.GetByFilters(oItem);

                foreach (var item in listtipopuesto)
                {
                    list.Add(new SelectListItem() { Text = item.vc_desc_nivel_prueba, Value = item.nu_id_nivel_prueba.ToString() });
                }

                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}