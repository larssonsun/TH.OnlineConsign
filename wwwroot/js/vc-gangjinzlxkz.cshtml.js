/**
 * created by larsson on 3.12 2019
 * js for component view "d_sampleuc_gangjin_zl_xkz"
 * last edited by larsson on 3.18 2019
 */
$(document).ready(function () {
    var productorSel = $("#SampleStorageExt_RecordCertificate");
    var xkzSel = $("#Gj_Xk_No");
    var xkzSelTxt = $("#Gj_Xk_NoHidden");
    var xkzProducerName = $("#Gj_Xk_Name");
    var getXkz = function () {
        var porp = productorSel.find("option:selected");
        if (porp.index() <= 0) {
            xkzSel.children("option:gt(0)").remove();
            xkzSel.change();
            return;
        }

        $.getJSON("/Consign/SampleDetails/SearchXkz?putOnRecordsPassport=" + encodeURI(porp.text()), function (data) {
            xkzSel.children("option:gt(0)").remove();
            xkzProducerName.val("");
            xkzSelTxt.val("");
            if (data) {
                if (data.length <= 1)
                    xkzSel.attr("disabled", true);
                else
                    xkzSel.attr("disabled", false);
                $.each(data, function (i, elm) {
                    xkzSel.append("<option value=" + elm.name + (i == 0 ? " selected>" : ">") + elm.putOnRecordsPassport + "</option>");
                    if (i == 0) {
                        xkzProducerName.val(elm.name);
                        xkzSelTxt.val(elm.putOnRecordsPassport);
                    }
                });
                xkzSel.change();
            }

        }, function (XMLHttpRequest, textStatus) {
            alert(textStatus);
        });
    }
    var setKxzProducerName = function () {
        xkzProducerName.val($(this).val());
        xkzSelTxt.val($(this).find("option:selected").text());
    };
    var setSurfaceFlag = function () {
        var xkzPorp = $(this).find("option:selected");
        var xkzText = $("#GJ_BianMiaoBiaoShi_GangJinZlXkz");
        var xkzImg = $("#Gj_BianMiaoBiaoShi_Image");
        var xkzImgHidden = $("#GJ_BianMiaoBiaoShi_ImageHidden");
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
                xkzImgHidden.val(data.surfaceFlagPicture);
                xkzText.css("display", "none")
                xkzImg.css("display", "")
            }
        });
    };

    productorSel.change(getXkz);
    xkzSel.change(setKxzProducerName);
    xkzSel.change(setSurfaceFlag);
});