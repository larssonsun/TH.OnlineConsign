using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public class SampleDetailsPageModel : BasePageModelForConsign
{
    ItemDbContext db;
    public SampleDetailsPageModel(ItemDbContext db)
    {
        this.db = db;
    }

    public string KindName { get; set; }

    public string ItemName { get; set; }

    public string SampleNameExt { get; set; }

    public string SampleUcName { get; set; }

    public string SampleUcViewComponentName { get; set; }

    public string SampleId { get; set; }

    public SelectList Specs { get; set; }

    public SelectList Grades { get; set; }

    public List<ItemParameter> Parameters { get; set; }

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
        var tuple = await InitPage(SampleId, null);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        SampleNameExt = tuple.Item3;
        Parameters = tuple.Item6;
        Specs = new SelectList(tuple.Item4, nameof(ItemSpec.SpecId), nameof(ItemSpec.SpecName), null, null);
        Grades = new SelectList(tuple.Item5, nameof(ItemGrade.GradeId), nameof(ItemGrade.GradeName), null, null);
        DelegateQuanUnit = new SelectList(tuple.Rest.Item1, nameof(DpDelegateQuanUnit.Nam), nameof(DpDelegateQuanUnit.Nam), null, null);
        SampleUcName = tuple.Rest.Item2;
        SampleUcViewComponentName = base.GetSampleUcViewComponentName(SampleUcName);

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
        // TODO: c# / 2019-03-05 14:51 / should use cache
        var typeCode = await db.DpProductionUnitType
            .Where(x => x.ItemId.ToString() == sampleId)
            .Select(x => x.TypeCode)
            .FirstOrDefaultAsync();

        var productors = await db.UnitProductionUnit
            .Where(x => x.ProductionUnitType == typeCode
                && (searchkey != null ? x.Name.Contains(searchkey) ||
                    x.PutOnRecordsPassport.Contains(searchkey) : true))
            .Select(x => new UnitProductionUnit { Name = x.Name, PutOnRecordsPassport = x.PutOnRecordsPassport })
            .OrderBy(x => x.PutOnRecordsPassport)
            .ToListAsync();

        return new JsonResult(productors);
    }

    private async Task<Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>, bool,
        Tuple<List<DpDelegateQuanUnit>, string>>> InitPage(string sampleId, string searchkey)
    {
        // get kindname for breadcrumb
        var kindName = await db.ItemKind
            .Where(x => x.KindId.ToString() == sampleId.Substring(0, 2))
            .Select(x => x.KindName).FirstAsync();

        // get itemname for breadcrumb
        var itemName = await db.ItemItem
            .Where(X => X.ItemId.ToString() == sampleId.Substring(0, 4))
            .Select(x => x.ItemName).FirstAsync();

        // get samplename for breadcrumb
        var sampleSource = await db.ItemSample
            .Where(x => x.SampleId.ToString() == sampleId)
            .Select(x => new { ExtName = x.SampleJudge.Length > 0 ? $"{x.SampleName}({x.SampleJudge})" : x.SampleName, Sun = x.SampleUc })
            .FirstAsync();
        var sampleNameExt = sampleSource.ExtName;
        var sampleUcName = sampleSource.Sun;

        // get spec for sample details
        var specs = await db.ItemSpec
            .Where(x => x.SampleId.ToString() == sampleId)
            .Select(x => new ItemSpec { SpecId = x.SpecId, SpecName = x.SpecName })
            .OrderBy(x => x.SpecId)
            .ToListAsync();

        // get grade for sample details
        var grades = await db.ItemGrade
            .Where(x => x.SampleId.ToString() == sampleId)
            .Select(x => new ItemGrade { GradeId = x.GradeId, GradeName = x.GradeName })
            .OrderBy(x => x.GradeId)
            .ToListAsync();

        // get parms for sample details
        var parms = await db.ItemParameter
            .Where(x => x.SampleId.ToString() == sampleId)
            .Select(x => new ItemParameter { ParameterId = x.ParameterId, ParameterName = x.ParameterName, IsDefault = x.IsDefault.Value })
            .OrderBy(x => x.ParameterId)
            .ToListAsync();

        // check if there are manufacturer resources
        var productorManual = true;
        var typeCode = await db.DpProductionUnitType
            .Where(x => x.ItemId.ToString() == sampleId)
            .Select(x => x.TypeCode)
            .FirstOrDefaultAsync();

        if (!String.IsNullOrEmpty(typeCode))
        {
            var tmp = await db.UnitProductionUnit
                .Where(x => x.ProductionUnitType == typeCode)
                .Select(x => x.Name)
                .CountAsync();

            productorManual = tmp <= 0;
        }

        // get delegate quan unit
        var deleQuanUnit = await db.DpDelegateQuanUnit
            .Select(x => new DpDelegateQuanUnit { Nam = x.Nam, Ord = x.Ord.Value })
            .OrderBy(x => x.Ord.Value)
            .ToListAsync();


        return new Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>, bool, Tuple<List<DpDelegateQuanUnit>, string>>
            (kindName, itemName, sampleNameExt, specs, grades, parms, productorManual, new Tuple<List<DpDelegateQuanUnit>, string>(deleQuanUnit, sampleUcName));
    }
}