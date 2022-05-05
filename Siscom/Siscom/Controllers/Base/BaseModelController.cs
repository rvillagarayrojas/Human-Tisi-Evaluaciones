using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Siscom.WebLib.Validator;
using Siscom.WebLib.PropertySetter;
using Siscom.Web.Controllers;

namespace Siscom.Controllers.Base
{
    [SessionExpireFilter]
    public abstract class BaseModelController<T> : BaseController
    {   
        private readonly IModelValidator<T> _modelValidator = null;

        protected BaseModelController(IModelValidator<T> modelValidator)
        {
            _modelValidator = modelValidator;
        }

        public T GetModel(Guid id)
        {
            var key = id.ToString();
            var existingModel = cacheProvider.Get(key);
            if (existingModel == null)
            {
                existingModel = (T)Activator.CreateInstance(typeof(T),   new object[] { });
                cacheProvider.Set(key, existingModel);
            }
            return (T)existingModel;
        }        

        public ActionResult UpdateField(string fieldName, string fieldValue, Guid id, bool toUpper = true)
        {
            var model = GetModel(id);
            return UpdateFieldAjaxResult(model, fieldName, fieldValue, toUpper);
        }

        public ActionResult GetValidation(Guid id)
        {
            var model = GetModel(id);
            return GetValidationAjaxResult(model);
        }

        public JsonResult UpdateFieldAjaxResult(T model, string fieldName, string fieldValue, bool toUpper)
        {
            var validationList = new List<KeyValuePair<string, string>>();

            PropertySetter<T>.SetFinalObject(model, fieldName, fieldValue, validationList, toUpper);
            validationList.AddRange(_modelValidator.Validate(model));

            return Json
                (new
                {
                    result = true,
                    PropertyValidationList = validationList,
                }
                );
        }

        public JsonResult GetValidationAjaxResult(T model)
        {
            var validationList = new List<KeyValuePair<string, string>>();

            validationList.AddRange(_modelValidator.Validate(model));

            return Json(new
            {
                PropertyValidationList = validationList,
            });
        }

        public List<KeyValuePair<string, string>> UpdatePropertyList(object valueWasSet, string fieldName, SetterFormatOptions setterFormatOptionsEnum = SetterFormatOptions.None)
        {
            var propertyChangeList = new List<KeyValuePair<string, string>>();

            if (valueWasSet == null)
            {
                propertyChangeList.Add(new KeyValuePair<string, string>(fieldName, null));
            }
            else
            {
                if (setterFormatOptionsEnum != SetterFormatOptions.None)
                {
                    propertyChangeList.Add(new KeyValuePair<string, string>(fieldName, SetterFormatOptionsHelper.FormatAsString(valueWasSet, setterFormatOptionsEnum)));
                }
                else if (valueWasSet is DateTime)
                {
                    propertyChangeList.Add(new KeyValuePair<string, string>(fieldName, string.Format("{0:dd/MM/yyyy}", valueWasSet)));
                }
                else if (valueWasSet is bool)
                {
                    propertyChangeList.Add(new KeyValuePair<string, string>(fieldName, valueWasSet.ToString().ToLower()));
                }
                else
                {
                    propertyChangeList.Add(new KeyValuePair<string, string>(fieldName, string.Format("{0}", valueWasSet)));
                }
            }
            return propertyChangeList;
        }        
    }
}

