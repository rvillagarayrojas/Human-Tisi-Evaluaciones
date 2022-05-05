using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Siscom.Controllers;
using Siscom.Entity.Global;
using Siscom.Models;
using Siscom.SClient.Global;
using Siscom.WebLib.MvcShared;
using Siscom.Utility;
using Siscom.Controllers.Base;
using Siscom.Areas.Persona.Models;
using Siscom.Areas.Persona.Models.Validator;
using Elmah;

namespace Siscom.Areas.Persona.Controllers
{
    public class PersonaController : BaseModelController<PersonaModels>
    {
        PersonaRestClient oPersonaRestClient;

        public PersonaController()
            : base(new PersonaModelValidator())
        {
            oPersonaRestClient = new PersonaRestClient();
        }
        public ActionResult Index()
        {
            try
            {
                var PersonaObj = new PersonaModels();
                var model = new PersonaModels();                       
                cacheProvider.Set(model.Id.Value.ToString(), model);
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

    }
}
