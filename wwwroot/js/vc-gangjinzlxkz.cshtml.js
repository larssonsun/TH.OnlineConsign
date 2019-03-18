/**
 * created by larsson on 3.12 2019
 * js for component view "d_sampleuc_gangjin_zl_xkz"
 */
$(document).ready(function () {
    var productorSel = $("#productorSource");
    var xkzSel = $("#Gj_Xk_No");
    var xkzProducerName = $("#Gj_Xk_Name");
    var getXkz = function () {

        var porp = productorSel.find("option:selected");
        if (porp.index() <= 0) {
            xkzSel.children("option:gt(0)").remove();
            return;
        }

        $.getJSON("/Consign/SampleDetails/SearchXkz?putOnRecordsPassport=" + encodeURI(porp.text()), function (data) {
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
                xkzSel.change();
            }
        }, function (XMLHttpRequest, textStatus) {
            alert(textStatus);
        });
    }
    var setKxzProducerName = function () {
        xkzProducerName.val($(this).val());
    };
    var setSurfaceFlag = function () {
        var xkzPorp = $(this).find("option:selected");
        var xkzText = $("#GJ_BianMiaoBiaoShi_GangJinZlXkz");
        var xkzImg = $("#Gj_BianMiaoBiaoShi_Image");
        $.getJSON("/Consign/SampleDetails/GetSurfaceFlag?xkzProductorName=" + encodeURI(xkzPorp.text()), function (data) {
            xkzText.val("");
            xkzImg.attr("src", "");
            if (data.surfaceFlagType == 1) {
                xkzText.val(data.surfaceFlagText);
                xkzImg.css("display", "none")
                xkzText.css("display", "")
            }
            else if (data.surfaceFlagType == 2) {
                xkzImg.attr("src", "data:image/gif;base64," + data.surfaceFlagPicture);
                xkzText.css("display", "none")
                xkzImg.css("display", "")
            }
        });
    };

    productorSel.change(getXkz);
    xkzSel.change(setKxzProducerName);
    xkzSel.change(setSurfaceFlag);
});