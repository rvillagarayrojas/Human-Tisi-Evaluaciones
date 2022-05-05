using System;
using System.Collections.Generic;
using Siscom.Entity.Persona;
using Siscom.Entity.Global;
using System.Web.Mvc;
using Siscom.Models.Base;
using MultiEntidad.A_Seleccion;
using Entidad.A_Seleccion;

namespace Siscom.Areas.Global.Models
{
    public class PersonaModels
    {
        public PersonaBE Persona    { get; set; }
        public PersonaBE Puesto     { get; set; } 

        //Combos
        public List<SelectListItem> ListaPerfil     { get; set; }
        public List<SelectListItem> ListaCuenta     { get; set; }
        public List<SelectListItem> ListaSubCuenta  { get; set; }

        public IList<PersonaBE>     ListaPuesto     { get; set; }
        public IList<PersonaBE>     ListaPersona    { get; set; }

        public List<PersonaBE>      ls_Persona      { get; set; }

        public List<CandidatoBE> ls_histo_candidatos { get; set; }
        public List<CandidatoBE> ls_uso_pruebas { get; set; }
        public List<E_Seguimiento> Ls_seguimiento { get; set; }
        public List<E_RespuestaPreguntaFile> Ls_RespuestaPreguntaFileo { get; set; }


        //-----------------------PRUEBAS-------------------------//

        public MME_Prueba       mme_prueba          { get; set; }
        public List<MME_Prueba> ls_mme_prueba       { get; set; }
        public List<MME_Prueba> ls_preguntas        { get; set; }

        public List<MME_Prueba> ls_candidatos       { get; set; }
        public List<MME_Prueba> ls_wonderlik        { get; set; }
        public List<MME_Prueba> ls_excel            { get; set; }
        public List<MME_Prueba> ls_barsit           { get; set; }
        public List<MME_Prueba> ls_Monedas1         { get; set; }        
        public List<MME_Prueba> ls_gatb             { get; set; }
        public List<MME_Prueba> ls_ic               { get; set; }
        public List<MME_Prueba> ls_disc             { get; set; }
        public List<MME_Prueba> ls_cps              { get; set; }
        public List<MME_Prueba> ls_kostick          { get; set; }
        public List<MME_Prueba> ls_ipv              { get; set; }
        public List<MME_Prueba> ls_zavic            { get; set; }
        public List<MME_Prueba> ls_domino           { get; set; }
        public List<MME_Prueba> ls_tig1_domino      { get; set; }        
        public List<MME_Prueba> ls_raven            { get; set; }
        public List<MME_Prueba> ls_ice_baron        { get; set; }
        public List<MME_Prueba> ls_neo_pir          { get; set; }
        public List<MME_Prueba> ls_bfq              { get; set; }
        public List<MME_Prueba> ls_16pf             { get; set; }
        public List<MME_Prueba> ls_siv              { get; set; }
        public List<MME_Prueba> ls_habil_general    { get; set; }
        public List<MME_Prueba> ls_minimult         { get; set; }
        public List<MME_Prueba> ls_ingles           { get; set; }

        public List<MME_Prueba> ls_datosPersonales  { get; set; }
        public List<MME_Prueba> ls_educacion        { get; set; }
        public List<MME_Prueba> ls_conocimiento     { get; set; }
        public List<MME_Prueba> ls_experiencia      { get; set; }
        public List<MME_Prueba> ls_familiares       { get; set; }

        public MME_Prueba       mme_datos       { get; set; }

        public String ServidorDescarga { get; set; }

        public PersonaModels()
        {
            Persona = new PersonaBE();
            Puesto  = new PersonaBE();

            this.mme_prueba         = new MME_Prueba();
            this.ls_mme_prueba      = new List<MME_Prueba>();
            this.ls_preguntas       = new List<MME_Prueba>();

            this.ls_histo_candidatos = new List<CandidatoBE>();
            this.ls_uso_pruebas     = new List<CandidatoBE>();

            this.ls_candidatos      = new List<MME_Prueba>();
            this.ls_wonderlik       = new List<MME_Prueba>();
            this.ls_barsit          = new List<MME_Prueba>();
            this.ls_gatb            = new List<MME_Prueba>();
            this.ls_ic              = new List<MME_Prueba>();
            this.ls_disc            = new List<MME_Prueba>();
            this.ls_cps             = new List<MME_Prueba>();
            this.ls_kostick         = new List<MME_Prueba>();
            this.ls_ipv             = new List<MME_Prueba>();
            this.ls_zavic           = new List<MME_Prueba>();
            this.ls_domino          = new List<MME_Prueba>();
            this.ls_tig1_domino     = new List<MME_Prueba>();
            this.ls_raven           = new List<MME_Prueba>();
            this.ls_ice_baron       = new List<MME_Prueba>();
            this.ls_neo_pir         = new List<MME_Prueba>();
            this.ls_bfq             = new List<MME_Prueba>();
            this.ls_16pf            = new List<MME_Prueba>();
            this.ls_siv             = new List<MME_Prueba>();
            this.ls_habil_general   = new List<MME_Prueba>();
            this.ls_minimult        = new List<MME_Prueba>();
            this.ls_ingles          = new List<MME_Prueba>();

            this.ls_datosPersonales = new List<MME_Prueba>();
            this.ls_educacion       = new List<MME_Prueba>();
            this.ls_conocimiento    = new List<MME_Prueba>();
            this.ls_experiencia     = new List<MME_Prueba>();
            this.ls_familiares      = new List<MME_Prueba>();

            this.mme_datos      = new MME_Prueba();
            
            this.ls_Persona     = new List<PersonaBE>();
        }
    }
}