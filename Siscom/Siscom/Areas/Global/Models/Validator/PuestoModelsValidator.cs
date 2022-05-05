using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Areas.Global.Models;

namespace Siscom.Areas.Global.Models.Validator
{
    public class PuestoModelsValidator : ValidationBase,
        IModelValidator<PuestoModels>
    {
        public List<KeyValuePair<string, string>> Validate(PuestoModels model)
        {
            var validator = new PuestoModelsValidator();
            validator.RequiredFieldValidation("Puesto.nu_id_cuenta", model.Puesto.nu_id_cuenta, "Cuenta ID");
            
            return validator.ValidationList;
        }
    }
}