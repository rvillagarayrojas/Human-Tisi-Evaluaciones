using System;
using System.Collections.Generic;
using Siscom.Entity.Persona;
using System.Web.Mvc;
using Siscom.Models.Base;
using Siscom.Entity.Global;

namespace Siscom.Areas.Global.Models
{
    public class CuentaModels
    {
        public CuentaBE Cuenta { get; set; }
        public PersonaBE Persona { get; set; }

        public List<SelectListItem> ListCuenta { get; set; }

        public IList<CuentaBE> ListaCuenta { get; set; }

        public CuentaModels()
        {
            Cuenta = new CuentaBE();
            Persona = new PersonaBE();
        }
    }
}