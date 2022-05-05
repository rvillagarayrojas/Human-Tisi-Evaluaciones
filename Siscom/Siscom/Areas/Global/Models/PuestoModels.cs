using System;
using System.Collections.Generic;
using Siscom.Entity.Persona;
using System.Web.Mvc;
using Siscom.Models.Base;
using Siscom.Entity.Global;
using MultiEntidad.A_Seleccion;

namespace Siscom.Areas.Global.Models
{
    public class PuestoModels
    {
        public PuestoBE     Puesto                          { get; set; }
        public PersonaBE    Persona                         { get; set; }

        public List<SelectListItem> ListTipoPrueba          { get; set; }
        public List<SelectListItem> ListNivelPrueba         { get; set; }
        public List<SelectListItem> ListaCuenta             { get; set; }
        public List<SelectListItem> ListaSubCuenta          { get; set; }

        public IList<PuestoBE>  ListaPuesto                 { get; set; }
        public IList<PuestoBE>  ListaPrueba                 { get; set; }
        public IList<PuestoBE>  ListaPruebas_Asignadas      { get; set; }
        public IList<PuestoBE>  ListaCandidatos_Asignadas   { get; set; }

        public MME_Prueba       mme_datos                   { get; set; }

        //---------------------analisis de brechas-------------------//

        public MME_Prueba       mme_prueba                  { get; set; }
        public List<MME_Prueba> ls_disc                     { get; set; }

        public PuestoModels()
        {
            Puesto      = new PuestoBE();
            Persona     = new PersonaBE();

            this.mme_prueba     = new MME_Prueba();
            this.ls_disc        = new List<MME_Prueba>();
            this.mme_datos      = new MME_Prueba();
        }

    }
}