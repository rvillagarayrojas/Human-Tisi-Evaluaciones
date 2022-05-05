function SetUpdateField(controller, toUpper) {
    $("[propertyname]").change(function () { UpdateField(this, controller, toUpper); });
}

function UpdateField(element, controller, afterCall) {
    var that = this;
    var data = getPropertyValuePairWithFormat(element, guid, false);
    $.ajax({
        type: "POST",
        async: true,
        url: window.GetControllerUrl("UpdateField", controller),
        dataType: "json",
        data: data,
        success: function (result) {
            UpdatePropertiesAndValidation(result);
          
            if (afterCall && typeof afterCall === 'function') {
                afterCall(element);
            }
        },
        error: function () {
            alert("Se ha producido un error inesperado al actualizar el campo, por favor actualice la página y vuelva a intentarlo");
        }
    });
};

function SetUpdateFieldDialog(controller, toUpper) {

}

function UpdateFieldDialog(element, controller, dialogId, toUpper) {
    var that = this;
    var data = getPropertyValuePairWithFormat(element, dialogId, toUpper);
    $.ajax({
        type: "POST",
        async: true,
        url: window.GetControllerUrl("UpdateField", controller),
        dataType: "json",
        data: data,
        success: function (result) {
            UpdatePropertiesAndValidationDialog(result);
        },
        error: function () {
            alert("Se ha producido un error inesperado al actualizar el campo, por favor actualice la página y vuelva a intentarlo");
        }
    });
};

function UpdatePropertiesAndValidation(updatedFields) {
    var validationList = updatedFields.PropertyValidationList;
    var propertyChangeList = updatedFields.PropertyChangeList;

    if (validationList != null) {
        window.UpdateValidation(validationList);
    }
    if (propertyChangeList != null) {
        window.SetValuesWithJSONFormat(propertyChangeList);
    }
}

function UpdatePropertiesAndValidationDialog(updatedFields) {
    var validationList = updatedFields.PropertyValidationList;
    var propertyChangeList = updatedFields.PropertyChangeList;
    if (validationList != null) {
        window.UpdateValidationDialog(validationList);
    }
    if (propertyChangeList != null) {
        window.SetValuesWithJSONFormat(propertyChangeList);
    }
}

/********* START VALIDATION ********************************/

function AddValidators() {
    var html = "";
    $("span[validationFor]").remove();

    var list = $("[propertyName]").not("[propertyIdentifier]");
    $.each(list, function (idx) {
        html1 = "<span validationFor='" + $(list[idx]).attr("propertyName") + "' style='display:none'><img class='validation' src='/Content/images/exclamation.gif' /></span>";
        html2 = "<span validationFor='" + $(list[idx]).attr("propertyName") + "' style='display:none;'><img class='validation' style='padding-top: 5px; padding-left: 3px;' src='/Content/images/exclamation.gif' /></span>";
        if (!$(list[idx]).is("select"))
            $(list[idx]).after(html1);
        else
            $(list[idx]).parent().after(html2);
    });

    var propIdentList = $("[propertyName][propertyIdentifier]");
    $.each(propIdentList, function (idxs) {
        html = "<span validationFor='" + $(propIdentList[idxs]).attr("propertyName") + "_" + $(propIdentList[idxs]).attr("propertyIdentifier") +
        "' style='display:none'><img class='validation' src='" + "/Content/images/exclamation.gif' /></span>";
        $(propIdentList[idxs]).after(html1);
    });
}

function AddValidatorsDialog() {
    var html = "";
    $(".ui-dialog span[validationFor]").remove();
    var list = $(".ui-dialog [propertyName]");

    $.each(list, function (idx) {
        html1 = "<span validationFor='" + $(list[idx]).attr("propertyName") + "' style='display:none'><img class='validation' src='/Content/images/exclamation.gif' /></span>";
        html2 = "<span validationFor='" + $(list[idx]).attr("propertyName") + "' style='display:none;'><img class='validation' style='padding-top: 5px; padding-left: 3px;' src='/Content/images/exclamation.gif' /></span>";
        if (!$(list[idx]).is("select"))
            $(list[idx]).after(html1);
        else
            $(list[idx]).parent().after(html2);
    });

    var propIdentList = $("[propertyName][propertyIdentifier]");
    $.each(propIdentList, function (idxs) {
        html = "<span validationFor='" + $(propIdentList[idxs]).attr("propertyName") + "_" + $(propIdentList[idxs]).attr("propertyIdentifier") +
        "' style='display:none'><img class='validation' src='" + "/Content/images/exclamation.gif' /></span>";
        $(propIdentList[idxs]).after(html);
    });
}


function AddValidatorsForPrefix(propertyNamePrefix) {
    var html = "";

    var list = $("[propertyName^='" + propertyNamePrefix + "']").not("[propertyIdentifier]");
    $.each(list, function (idx) {
        html = "<span validationFor='" + $(list[idx]).attr("propertyName") + "' style='display:none'><img class='validation' src='" + "/Content/images/exclamation.gif' /></span>";
        $(list[idx]).after(html);
    });

    var propIdentList = $("[propertyName^='" + propertyNamePrefix + "'][propertyIdentifier]");
    $.each(propIdentList, function (idxs) {
        html = "<span validationFor='" + $(propIdentList[idxs]).attr("propertyName") + "_" + $(propIdentList[idxs]).attr("propertyIdentifier") +
        "' style='display:none'><img class='validation' src='" + "/Content/images/exclamation.gif' /></span>";
        $(propIdentList[idxs]).after(html);
    });
}

function UpdateValidation(validationList, dontHideAll) {
    if (dontHideAll == null) {
        $("span[validationFor]").hide();
        $("span[validationFor]").removeAttr("message");
    }

    var validationSpan = null;
    $.each(validationList, function (idx, validation) {
        validationSpan = $("span[validationFor='" + validation.Key + "']");
        validationSpan.attr("message", validation.Value);
        validationSpan.children().attr("tool-tip", validation.Value);
        //validationSpan.children().tipsy({ gravity: 'sw' });
        validationSpan.show();
    });

    var multipleValidationsList = $("[multipleValidations]");
    $.each(multipleValidationsList, function (i, val) {
        var propName = $(val).attr("propertyName");

        var validationSpans = $("span[validationFor^='" + propName + "'][validationFor!='" + propName + "'][message]");
        var msg = null;
        $.each(validationSpans, function (ind, thespan) {
            if (msg == null) {
                msg = "";
            }
            else {
                msg += "<br />";
            }

            msg += $(thespan).attr("message");
        });

        if (msg != null) {
            var msgspans = $("span[validationFor='" + propName + "']");

            msgspans.attr("message", msg);
            msgspans.children().attr("tool-tip", msg);
            msgspans.children().tipsy({ gravity: 'nw', html: true });
            msgspans.show();
        }
    });
}

function UpdateValidationDialog(validationList, dontHideAll) {
    if (dontHideAll == null) {
        $(".ui-dialog span[validationFor]").hide();
        $(".ui-dialog span[validationFor]").removeAttr("message");
    }

    var validationSpan = null;
    $.each(validationList, function (idx, validation) {
        validationSpan = $(".ui-dialog span[validationFor='" + validation.Key + "']");

        validationSpan.attr("message", validation.Value);
        validationSpan.children().attr("tool-tip", validation.Value);
        validationSpan.children().tipsy({ gravity: 'sw' });
        validationSpan.show();
    });

    var multipleValidationsList = $("[multipleValidations]");
    $.each(multipleValidationsList, function (i, val) {
        var propName = $(val).attr("propertyName");

        var validationSpans = $(".ui-dialog span[validationFor^='" + propName + "'][validationFor!='" + propName + "'][message]");
        var msg = null;
        $.each(validationSpans, function (ind, thespan) {
            if (msg == null) {
                msg = "";
            }
            else {
                msg += "<br />";
            }

            msg += $(thespan).attr("message");
        });

        if (msg != null) {
            var msgspans = $(".ui-dialog span[validationFor='" + propName + "']");

            msgspans.attr("message", msg);
            msgspans.children().attr("tool-tip", msg);
            msgspans.children().tipsy({ gravity: 'nw', html: true });
            msgspans.show();
        }
    });
}

function SetValidation(controller) {
    AddValidators();
    var data =
    {
        id: guid
    };
    $.ajax({
        type: "POST",
        url: window.GetControllerUrl("GetValidation", controller),
        dataType: "json",
        data: data,
        success: function (validationList) {
            window.UpdateValidation(validationList.PropertyValidationList);
        }
    });
}

function SetValidationDialog(controller, dialogId) {
    AddValidatorsDialog();
    var data =
    {
        id: dialogId
    };
    $.ajax({
        type: "POST",
        url: window.GetControllerUrl("GetValidation", controller),
        dataType: "json",
        data: data,
        success: function (validationList) {
            window.UpdateValidationDialog(validationList.PropertyValidationList);
        }
    });
}

/********* END VALIDATION ********************************/


function parseAndSetDt(element) {
    try {
        var dt = $.datepicker.parseDate("dd/mm/yy", $(element).val());
        $(element).datepicker("setDate", dt);
    }
    catch (e) {
        $(element).datepicker("setDate", null);
    }
    window.UpdateField(element);
}