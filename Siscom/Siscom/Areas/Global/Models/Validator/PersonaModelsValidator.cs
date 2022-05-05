using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Areas.Global.Models;

namespace Siscom.Areas.Global.Models.Validator
{
    public class PersonaModelsValidator: ValidationBase,
        IModelValidator<PersonaModels>
    {
        public List<KeyValuePair<string, string>> Validate(PersonaModels model)
        {
            var validator = new PersonaModelsValidator();
            validator.RequiredFieldValidation("Persona.nu_id_cuenta", model.Persona.nu_id_cuenta, "Cuenta ID");


            return validator.ValidationList;
        }
    
    }
}