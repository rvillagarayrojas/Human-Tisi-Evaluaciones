using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Areas.Global.Models;

namespace Siscom.Areas.Global.Models.Validator
{
    public class CuentaModelsValidator : ValidationBase,
        IModelValidator<CuentaModels>
    {
        public List<KeyValuePair<string, string>> Validate(CuentaModels model)
        {
            var validator = new CuentaModelsValidator();
            validator.RequiredFieldValidation("Cuenta.nu_id_cuenta", model.Cuenta.nu_id_cuenta, "Cuenta ID");


            return validator.ValidationList;
        }
    }
}