/**
 * created by larsson on 3.12 2019
 * js for component view "d_sampleuc_gangjin_zl_xkz"
 */
$(document).ready(function () {
    var productorSel = $("#productorSource");
    var xkzSel = $("#Gj_Xk_No");
    var xkzProducerName = $("#xlzProductorName");
    var getXkz = function () {
        var porp = productorSel.find("option:selected");
        if (porp.index() <= 0) {
            xkzSel.children("option:gt(0)").remove();
            return;
        }
        $.getJSON("/Consign/SampleDetails/SearchXkz?putOnRecordsPassport=" + porp.text(), function (data) {
            xkzSel.children("option:gt(0)").remove();
            xkzProducerName.val("");
            if (data) {
                if (data.length <= 1)
                    xkzSel.attr("disabled", true);
                else
                    xkzSel.attr("disabled", false);
                $.each(data, function (i, elm) {
                    xkzSel.append("<option value=" + elm.name + (i == 0 ? " selected>" : ">") + elm.putOnRecordsPassport + "</option>");
                    if (i == 0)
                        xkzProducerName.val(elm.name);
                });
            }
        }, function (XMLHttpRequest, textStatus) {
            alert(textStatus);
        });
    }
    var setKxzProducerName = function () {
        xkzProducerName.val($(this).val());
    };

    productorSel.change(getXkz);
    xkzSel.change(setKxzProducerName);

});