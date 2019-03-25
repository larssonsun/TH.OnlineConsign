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
    public string KindName { get; set; }
    [BindProperty]
    public string ItemName { get; set; }
    [BindProperty]
    public string SampleNameExt { get; set; }

    public string SampleUcName { get; set; }

    public string SampleUcViewComponentName { get; set; }

    public string SampleUcViewComponentViewName { get; set; }

    public bool IfShouldAddScript { get; set; }

    [BindProperty]
    public string SampleId { get; set; }

    public SelectList Specs { get; set; }

    public SelectList Grades { get; set; }

    [BindProperty]
    public List<ItemParameterExt> Parameters { get; set; }

    [BindProperty]
    public SampleStorageMainExt SampleStorageExt { get; set; } = new SampleStorageMainExt();
    public SelectList DelegateQuanUnit { get; set; }

    public ShowHideCssClass ShowSpecManual { get; set; }

    public ShowHideCssClass ShowSpecSelect { get; set; }

    public ShowHideCssClass ShowGradeManual { get; set; }

    public ShowHideCssClass ShowGradeSelect { get; set; }

    public ShowHideCssClass ShowProductorManual { get; set; }

    public ShowHideCssClass ShowProductorSelect { get; set; }

    public ReadonlyCssClass ReadonlyProductorManual { get; set; }

    public async Task OnGetAsync()
    {
        // TODO: c# / 2019-03-06 10_52 / part of the steel bar sample here shows the record certificate and does not include the license
        SampleId = RouteData.Values["handler"].ToString();
        var tuple = await sampleConsignService.GetInitDataForSampleDetailPage(SampleId, null);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        SampleNameExt = tuple.Item3;
        Parameters = tuple.Item6.Select(x => tools.EntityCopy<ItemParameter, ItemParameterExt>(x)).ToList();
        Specs = new SelectList(tuple.Item4, nameof(ItemSpec.SpecId), nameof(ItemSpec.SpecName), null, null);
        Grades = new SelectList(tuple.Item5, nameof(ItemGrade.GradeId), nameof(ItemGrade.GradeName), null, null);
        DelegateQuanUnit = new SelectList(tuple.Rest.Item1, nameof(DpDelegateQuanUnit.Nam), nameof(DpDelegateQuanUnit.Nam), null, null);
        SampleUcName = tuple.Rest.Item2;
        var sampleucinfo = sampleUcControler.GetSampleUcViewComponentInfo(SampleUcName, SampleId);
        SampleUcViewComponentName = sampleucinfo.Item1;
        SampleUcViewComponentViewName = sampleucinfo.Item2;
        IfShouldAddScript = sampleUcControler.GetIfShouldAddScript(SampleUcName, SampleId);

        ShowSpecManual = Specs.Count() <= 0 ? ShowHideCssClass.show : ShowHideCssClass.hide;
        ShowSpecSelect = Specs.Count() <= 0 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        ShowGradeManual = Grades.Count() <= 0 ? ShowHideCssClass.show : ShowHideCssClass.hide;
        ShowGradeSelect = Grades.Count() <= 0 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        ShowProductorManual = tuple.Item7 ? ShowHideCssClass.show : ShowHideCssClass.hide;

        ShowProductorSelect = tuple.Item7 ? ShowHideCssClass.hide : ShowHideCssClass.show;
        ReadonlyProductorManual = tuple.Item7 ? ReadonlyCssClass.na : ReadonlyCssClass.ReadOnly;
    }

    public async Task<JsonResult> OnGetSearchProductor(string sampleId, string searchkey)
    {
        return new JsonResult(await sampleConsignService.GetSearchProductor(sampleId, searchkey));
    }

    public async Task<RedirectToPageResult> OnPostSave()
    {
        var parmIds = string.Empty;
        var parmNames = string.Empty;
        Parameters.ForEach((x) =>
        {
            if (x.Checked)
            {
                parmIds += $"{x.ParameterId};";
                parmNames += $"{x.ParameterName};";
            }
        });
        var sampleNo = await sampleConsignService.GetNewSampleConsignId();
        var aaa = new SampleStorageMain()
        {

            ContractSignNumber = string.Empty,
            JzcertificateNo = string.Empty,
            QycertificateNo = string.Empty,
            SampleUcDbTableName = string.Empty,
            OperatorUserId = Guid.Empty,
            MoldingDate = DateTime.Parse("1753-01-01 00:00:00.000"),
            AgeTime = -1,

            Id = Guid.NewGuid(),
            SampleNo = sampleNo,
            DetectonDate = DateTime.Now,
            KindId = int.Parse(SampleId.Substring(0, 2)),
            ItemId = int.Parse(SampleId.Substring(0, 4)),
            SampleId = int.Parse(SampleId),
            ExamParameter = parmIds,
            CreateDateTime = DateTime.Now,
            LastEditDateTime = DateTime.Now,
            KindName = KindName,
            ItemName = ItemName,
            SampleName = SampleNameExt,
            ExamParameterCn = parmNames,

            ProJectPart = SampleStorageExt.ProJectPart,
            ProduceFactory = SampleStorageExt.ProduceFactory,
            RecordCertificate = SampleStorageExt.RecordCertificate,
            DelegateQuan = SampleStorageExt.DelegateQuan,
            DelegateQuanUnit = SampleStorageExt.DelegateQuanUnit,
            SpecId = SampleStorageExt.SpecId,
            GradeId = SampleStorageExt.GradeId,
            SpecName = SampleStorageExt.SpecName,
            GradeName = SampleStorageExt.GradeName
        };
        Console.WriteLine("------------------------" + (SampleStorageExt.ProJectPart is null ? "null" : "not null"));
        await dbSampleStorage.SampleStorageMain.AddAsync(aaa);
        var ir = await dbSampleStorage.SaveChangesAsync();

        return RedirectToPage(pageName: "SampleDetails", routeValues: new { handler = SampleId, area = "Consign" });
    }
}

public class SampleStorageMainExt : SampleStorageMain
{
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public new string ProJectPart { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public new string GradeName { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public new string SpecName { get; set; }
}

public class ItemParameterExt : ItemParameter
{
    public bool Checked { get; set; }
}