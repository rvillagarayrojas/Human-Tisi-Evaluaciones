function SetValuesWithJSONFormat(jsonObject) {
    var data = $("[propertyName]");

    $.each(jsonObject, function (jsonIdx) {
        var json = jsonObject[jsonIdx];
        findElementSetValue(json, data);
    });
}

function findElementSetValue(json, data) {
    $.each(data, function (dataIdx) {
        var element = $(data[dataIdx]);
        var propertyName = element.attr("propertyName");

        if (json.Key == propertyName) {
            setValueFactory(json.Value, element);
        }
    });
}


function setValueFactory(val, element) {
    if (element.is("div,span")) {
        if (val != null) {
            element.text(val);
        }
        return;
    }
    switch (element.prop("type")) {
        case "radio":
        case "checkbox":
            var setToChecked = (val != null && val.toLowerCase() == 'true');
            if (setToChecked) {
                if (!element.is(":checked")) {
                    element.attr("checked", "checked");
                }
            }
            else {
                if (element.is(":checked")) {
                    element.removeAttr("checked");
                }
            }
            break;
        default:

            if (hasElementChanged(val, element)) {
                if (element.attr("disabled") != true) {
                    element.css("backgroundColor", "#F4A83D");
                    element.animate({ backgroundColor: "White" }, 1000, null, function () { element.css("backgroundColor", ""); });
                }
                if (element.attr("dataType") == "Date") {
                    if (val == "" || val == null) {
                        element.val(" ");
                    }
                    else {
                        var date;
                        if (val.substr(0, 6) == "/Date(") {
                            date = new Date(parseInt(val.substr(6),10));
                        }
                        else {
                            date = new Date(val);
                        }
                        element.val($.datepicker.formatDate("dd-M-yy", date));

                    }

                }
                else {
                    if (val == null) {
                        element.val("");
                    }
                    else {
                        element.val(val);
                    }
                }
            }
            break;
    }
    

    function hasElementChanged(val, element) {
        var v = element.val();
        if ((val == null || val == "") && v == "") {
            return false;
        }
        return v != val;
    }
}   

