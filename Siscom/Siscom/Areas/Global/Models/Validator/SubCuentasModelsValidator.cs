using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Areas.Global.Models;


namespace Siscom.Areas.Global.Models.Validator
{
    public class SubCuentasModelsValidator : ValidationBase,
        IModelValidator<SubCuentasModels>
    {
        public List<KeyValuePair<string, string>> Validate(SubCuentasModels model)
        {
            var validator = new SubCuentasModelsValidator();
            validator.RequiredFieldValidation("SubCuenta.nu_id_subcuenta", model.SubCuenta.nu_id_subcuenta, "SubCuenta ID");


            return validator.ValidationList;
        }
    }
}