/// <reference path="jquery-1.4.4-vsdoc.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

jQuery.validator.addMethod("min", function (value, element, param) {
    return value >= param;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("min", "value");
