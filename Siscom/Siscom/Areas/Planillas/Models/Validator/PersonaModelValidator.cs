using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Areas.Planilla.Models;

namespace Siscom.Areas.Persona.Models.Validator
{
    public class PersonaModelValidator : ValidationBase, IModelValidator<PersonaModels>
    {
        public List<KeyValuePair<string, string>> Validate(PersonaModels model)
        {
            var validator = new PersonaModelValidator();
            return validator.ValidationList;
        }
    }
}