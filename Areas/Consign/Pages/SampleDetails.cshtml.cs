using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;
using th.onlineconsign.Services;
using static BasePageModelForConsign;

public partial class SampleDetailsPageModel : BasePageModelForConsign
{
    ISampleConsignService sampleConsignService;
    SampleStorageDbContext dbSampleStorage;
    ISampleUcControler sampleUcControler;
    ITools tools;

    public SampleDetailsPageModel(ISampleConsignService service3, SampleStorageDbContext db2, ISampleUcControler service1, ITools service2)
    {
        sampleConsignService = service3;
        dbSampleStorage = db2;
        sampleUcControler = service1;
        tools = service2;
    }

    [BindProperty]
    public SampleStorageMainExt SampleStorageExt { get; set; } = new SampleStorageMainExt();

    public SampleDetailsPageCtl SampleDetailsPageCtl { get; set; } = new SampleDetailsPageCtl();

    public SelectList Specs { get; set; }

    public SelectList Grades { get; set; }

    public SelectList DelegateQuanUnit { get; set; }

    public async Task OnGetAsync()
    {
        SampleStorageExt.SampleId = int.Parse(RouteData.Values["handler"].ToString());

        var tuple = await sampleConsignService.GetInitDataForSampleDetailPage(SampleStorageExt.SampleId.ToString(), null);
        SampleStorageExt.KindName = tuple.Item1;
        SampleStorageExt.ItemName = tuple.Item2;
        SampleStorageExt.SampleName = tuple.Item3;
        SampleStorageExt.Parameters = tuple.Item6.Select(x => tools.EntityCopy<ItemParameter, ItemParameterExt>(x)).ToList();
        Specs = new SelectList(tuple.Item4, nameof(ItemSpec.SpecId), nameof(ItemSpec.SpecName), null, null);
        Grades = new SelectList(tuple.Item5, nameof(ItemGrade.GradeId), nameof(ItemGrade.GradeName), null, null);
        DelegateQuanUnit = new SelectList(tuple.Rest.Item1, nameof(DpDelegateQuanUnit.Nam), nameof(DpDelegateQuanUnit.Nam), null, null);
        var sampleUcName = tuple.Rest.Item2;

        var sampleucinfo = sampleUcControler.GetSampleUcViewComponentInfo(sampleUcName, SampleStorageExt.SampleId.ToString());
        SampleStorageExt.SampleUcDbTableName = sampleucinfo.Item1;// the same as sampleuc table name
        SampleStorageExt.SampleUcViewComponentViewName = sampleucinfo.Item2;

        SampleDetailsPageCtl.IfShouldAddScript = sampleUcControler.GetIfShouldAddScript(sampleUcName, SampleStorageExt.SampleId.ToString());
        SampleDetailsPageCtl.ShowSpecManual = Specs.Count() <= 0 ? ShowHideCssClass.show : ShowHideCssClass.hide;
        SampleDetailsPageCtl.ShowSpecSelect = Specs.Count() <= 0 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        SampleDetailsPageCtl.ShowGradeManual = Grades.Count() <= 0 ? ShowHideCssClass.show : ShowHideCssClass.hide;
        SampleDetailsPageCtl.ShowGradeSelect = Grades.Count() <= 0 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        SampleDetailsPageCtl.ShowProductorManual = tuple.Item7 ? ShowHideCssClass.show : ShowHideCssClass.hide;
        SampleDetailsPageCtl.ShowProductorSelect = tuple.Item7 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        SampleDetailsPageCtl.ReadonlyProductorManual = tuple.Item7 ? ReadonlyCssClass.na : ReadonlyCssClass.ReadOnly;
    }

    public async Task<JsonResult> OnGetSearchProductor(string sampleId, string searchkey)
    {
        return new JsonResult(await sampleConsignService.GetSearchProductor(sampleId, searchkey));
    }

    public async Task<RedirectToPageResult> OnPostSave()
    {
        var parmIds = string.Empty;
        var parmNames = string.Empty;
        SampleStorageExt.Parameters.ForEach((x) =>
        {
            if (x.Checked)
            {
                parmIds += $"{x.ParameterId};";
                parmNames += $"{x.ParameterName};";
            }
        });
        var sampleNo = await sampleConsignService.GetNewSampleConsignId();

        var sampleStorageMain = tools.EntityCopyForParent<SampleStorageMainExt, SampleStorageMain>(SampleStorageExt);
        sampleStorageMain.ContractSignNumber = string.Empty;
        sampleStorageMain.JzcertificateNo = string.Empty;
        sampleStorageMain.QycertificateNo = string.Empty;

        sampleStorageMain.OperatorUserId = Guid.Empty;
        sampleStorageMain.MoldingDate = DateTime.Parse("1753-01-01 00:00:00.000");
        sampleStorageMain.AgeTime = -1;

        sampleStorageMain.Id = Guid.NewGuid();
        sampleStorageMain.SampleNo = sampleNo;
        sampleStorageMain.DetectonDate = DateTime.Now;
        sampleStorageMain.KindId = int.Parse(sampleStorageMain.SampleId.ToString().Substring(0, 2));
        sampleStorageMain.ItemId = int.Parse(sampleStorageMain.SampleId.ToString().Substring(0, 4));

        sampleStorageMain.ExamParameter = parmIds;
        sampleStorageMain.CreateDateTime = DateTime.Now;
        sampleStorageMain.LastEditDateTime = DateTime.Now;

        sampleStorageMain.ExamParameterCn = parmNames;
        await dbSampleStorage.SampleStorageMain.AddAsync(sampleStorageMain);
        var ir = await dbSampleStorage.SaveChangesAsync();

        return RedirectToPage(pageName: "SampleDetails", routeValues: new { handler = sampleStorageMain.SampleId, area = "Consign" });
    }
}
public class SampleStorageMainExt : SampleStorageMain
{
    public List<ItemParameterExt> Parameters { get; set; }
    public string SampleUcViewComponentViewName { get; set; }
}

public class ItemParameterExt : ItemParameter
{
    public bool Checked { get; set; }
}

public class SampleDetailsPageCtl
{
    public ShowHideCssClass ShowSpecManual { get; set; }

    public ShowHideCssClass ShowSpecSelect { get; set; }

    public ShowHideCssClass ShowGradeManual { get; set; }

    public ShowHideCssClass ShowGradeSelect { get; set; }

    public ShowHideCssClass ShowProductorManual { get; set; }

    public ShowHideCssClass ShowProductorSelect { get; set; }

    public ReadonlyCssClass ReadonlyProductorManual { get; set; }

    public bool IfShouldAddScript { get; set; }
}

