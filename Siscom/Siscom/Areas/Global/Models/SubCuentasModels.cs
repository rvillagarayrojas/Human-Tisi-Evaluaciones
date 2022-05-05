using System;
using System.Collections.Generic;
using Siscom.Entity.Persona;
using System.Web.Mvc;
using Siscom.Models.Base;
using Siscom.Entity.Global;

namespace Siscom.Areas.Global.Models
{
    public class SubCuentasModels
    {
        public SubCuentaBE SubCuenta { get; set; }
        public PersonaBE Persona { get; set; }
        
        public List<SelectListItem> ListCuenta { get; set; }

        public IList<SubCuentaBE> ListaSubCuenta { get; set; }

        public SubCuentasModels()
        {
            SubCuenta = new SubCuentaBE();
            Persona = new PersonaBE();

        }

    }
}