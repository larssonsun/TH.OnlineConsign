/**
 * created by larsson on 3.5 2019
 * js for page sampledetails
 */

$(document).ready(function () {


    // //   -----------------   // //
    // //   productor factroy   // //
    // //   -----------------   // //

    var productorSel = $("#SampleStorageExt_RecordCertificate");
    var productorNameTxt = $("#produceFactory");
    var productorNameTxtHidden = $("#SampleStorageExt_RecordCertificateHidden");
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
            productorNameTxtHidden.val("");
            if (data) {
                $.each(data, function (i, elm) {
                    productorSel.append("<option value=" + elm.name + (i == 0 ? " selected>" : ">") + elm.putOnRecordsPassport + "</option > ");
                    if (i == 0)
                    {
                        productorNameTxt.val(elm.name);
                        productorNameTxtHidden.val(elm.putOnRecordsPassport);
                    }
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
        productorNameTxtHidden.val($(this).find("option:selected").text());
    });


    // //   -------------------   // //
    // //   spec and grade name   // //
    // //   -------------------   // //

    var specName = $("#SampleStorageExt_SpecName");
    $("#SampleStorageExt_SpecId").change(function () {
        if ($(this).val() == 0)
            specName.val("");
        else {
            var specTxt = $(this).find("option:selected").text();
            specName.val(specTxt);
        }
    });

    var gradeName = $("#SampleStorageExt_GradeName");
    $("#SampleStorageExt_GradeId").change(function () {
        if ($(this).val() == 0)
            gradeName.val("");
        else {
            var gradeTxt = $(this).find("option:selected").text();
            gradeName.val(gradeTxt);
        }
    });
});