using Siscom.WebLib.Validator;
using System.Collections.Generic;
using Siscom.Models;

namespace Siscom.Models.Validator
{
    public class CambioContrasenaModelValidator : ValidationBase, IModelValidator<CambioContrasenaModel>
    {
        public List<KeyValuePair<string, string>> Validate(CambioContrasenaModel model)
        {
            var validator = new CambioContrasenaModelValidator();
            return validator.ValidationList;
        }
    }

}