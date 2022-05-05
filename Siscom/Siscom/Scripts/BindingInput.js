function cleanUpModals()
{
    $("body > div").each(function ()
    {
        if ($(this).attr("id") != "ui-datepicker-div" && $(this).attr("id") != "pageContainerDiv")
        {
            $(this).empty();
            $(this).remove();
        }
    });
}

function GetValueFromItem(item) {

    if (item.attr("dataType") == "Date")
    {
        var dtObj = item.datepicker("getDate");
        if (dtObj != null)
        {
            return (dtObj.getMonth() + 1) + "/" + dtObj.getDate() + "/" + dtObj.getFullYear();
        }
    }
    else if (item.attr("dataType") == "Percent")
    {
        var valWithPercent = item.val();
        return valWithPercent.replace("%", "");
    }
    if (item.prop("type") == "checkbox") {
        var isChecked;

        if (item.prop("checked") == true || item.prop("checked") == false) {
            isChecked = item.prop("checked");
        }
        else {
            isChecked = item.attr("checked");
        }

        return isChecked;
    }
    return item.val();
}

function replacer(key, value) {
    if (typeof value === 'number' && !isFinite(value)) {
        return String(value);
    }
    return value;
}

function getPropertyValueIdentifierTuple(element, id) {
    var data =
    {
        fieldName: $(element).attr("propertyName"),
        fieldValue: window.GetValueFromItem($(element)),
        fieldIdentifier: $(element).attr("propertyIdentifier"),
        id: id
    };
    return data;
}

function getPropertyListValuePair(element, id) {
    var textvalues = [];
    var realvalues = $(element).val();
    var textCounter = 0;
    if (realvalues != null)
    {
        $(element).children().each(function (i, option)
        {
            if ($.inArray(option.value, realvalues) >= 0)
            {
                textvalues[textCounter] = option.text;
                textCounter++;
            }
        });
    }
    else
    {
        realvalues = [];
    }
    var data =
    {
        fieldName: $(element).attr("propertyName"),
        fieldValues: realvalues,
        fieldTexts: textvalues,
        id: id
    };
    return data;
}

function GetPropertyValuePair(element, id) {
    var data =
    {
        fieldName: $(element).attr("propertyName"),
        fieldValue: window.GetValueFromItem($(element)),
        id: id
    };

    return data;
}

function getPropertyValuePairWithFormat(element, id, toUpper) {
    var setterFormatOptions = $(element).attr("setterFormatOptions");
    if (setterFormatOptions == null) {
        setterFormatOptions = "None";
    }
    var data =
    {
        fieldName: $(element).attr("propertyName"),
        fieldValue: window.GetValueFromItem($(element)),        
        id: id,
        toUpper: toUpper
    };

    return data;
}

function GetValuesWithJSONFormat() {
    var data = $("[propertyName]");
    var hash = {};

    $.each(data, function (index) {
        var element = $(data[index]);
        var propertyName = element.attr("propertyName");
        if (propertyName == null) {
            alert(element.attr("id"));
        }
        var val = window.GetValueFromItem(element);

        var array = propertyName.split(".");

        if (array.length > 1) {
            buildHashAndSetValue(hash, array, val, 0);
        }
        else {
            hash[propertyName] = val;
        }
    });

    var jsonHash = JSON.stringify(hash, replacer);

    return JSON.parse(jsonHash);
}


function buildHashAndSetValue(hash, list, value, idx) {

    if (list.length > (idx+1)) {

        if (hash[list[idx]] == null) {
            hash[list[idx]] = {};
        }
        
        buildHashAndSetValue(hash[list[idx]], list, value, idx+1);

    }
    else {
        hash[list[list.length-1]] = value;
    }
}

function BuildObjectGraph(propName) {
    var list = propName.split(".");
    alert(list.length);

}



function SetValuesWithJSONFormat(jsonObject) {
    var data = $("[propertyName]");

    $.each(jsonObject, function(jsonIdx) {
        var json = jsonObject[jsonIdx];
        findElementSetValue(json, data);
    });
}

function NumCheck(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[1-9]{1}[0-9]{4}.[0-9]{2}$/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    // other key
    return false

}


function NumCheckInt(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[1-9]{1}[0-9]{5}$/
        return !(regexp.test(field.value))
    }
    else { return false }

}


function NumCheckRuc(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[0-9]{12}$/
        return !(regexp.test(field.value))
    }
    else { return false }

}