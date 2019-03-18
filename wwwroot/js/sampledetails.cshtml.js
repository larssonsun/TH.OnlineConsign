/**
 * created by larsson on 3.5 2019
 * js for page sampledetails
 */

$(document).ready(function () {

    var productorSel = $("#productorSource");
    var productorNameTxt = $("#productorName");
    // TODO: js / 2019-03-05 14:51 / should addd searchkey validate
    var validateSearchProductor = function () {
        return true;
    };

    // opator productor and it's recordspassport
    $("#searchProductor").click(function () {
        if (!validateSearchProductor())
            return;
        var sampleId = $("#hiddenSampleId").val();
        var searchProductorSearchKey = $.trim($("#searchProductorSearchKey").val());
        $.getJSON("/Consign/SampleDetails/SearchProductor?sampleid=" + sampleId + "&searchkey=" + encodeURI(searchProductorSearchKey), function (data) {
            productorSel.children("option:gt(0)").remove();
            productorNameTxt.val("");
            if (data) {
                $.each(data, function (i, elm) {
                    productorSel.append("<option value=" + elm.name + (i == 0 ? " selected>" : ">") + elm.putOnRecordsPassport + "</option > ");
                    if (i == 0)
                        productorNameTxt.val(elm.name);
                });
                productorSel.change();
            }
        }, function (XMLHttpRequest, textStatus) {
            alert(textStatus);
        });
    });

    // when productor passport sel changed
    productorSel.change(function () {
        productorNameTxt.val($(this).val());
    });
});