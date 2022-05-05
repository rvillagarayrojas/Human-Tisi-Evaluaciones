using System;
using System.Collections.Generic;
using Siscom.Entity.Persona;
using System.Web.Mvc;
using Siscom.Models.Base;

namespace Siscom.Areas.Persona.Models
{
    public class PersonaModels : BaseModel
    {
        public PersonaBE Persona { get; set; }

        public PersonaModels()
        {
            Id = Guid.NewGuid();
            Persona = new PersonaBE();

        }
    }
}