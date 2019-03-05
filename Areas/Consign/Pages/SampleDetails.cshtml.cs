using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public class SampleDetailsPageModel : PageModel
{
    ItemDbContext db;
    public SampleDetailsPageModel(ItemDbContext db)
    {
        this.db = db;
    }

    public string KindName { get; set; }

    public string ItemName { get; set; }

    public string SampleNameExt { get; set; }

    public string SampleId { get; set; }

    public SelectList Specs { get; set; }

    public SelectList Grades { get; set; }

    public List<ItemParameter> Parameters { get; set; }

    public async Task OnGetAsync()
    {
        // TODO: c# / 2019-03-05 16:27 / productor name editable ctl
        SampleId = RouteData.Values["handler"].ToString();
        var tuple = await InitPage(SampleId, null);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        SampleNameExt = tuple.Item3;
        Parameters = tuple.Item6;
        Specs = new SelectList(tuple.Item4, nameof(ItemSpec.SpecId), nameof(ItemSpec.SpecName), null, null);
        Grades = new SelectList(tuple.Item5, nameof(ItemGrade.GradeId), nameof(ItemGrade.GradeName), null, null);
        
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

    private async Task<Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>>> InitPage(string sampleId, string searchkey)
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
        var sampleNameExt = await db.ItemSample
            .Where(x => x.SampleId.ToString() == sampleId)
            .Select(x => x.SampleJudge.Length > 0 ? $"{x.SampleName}({x.SampleJudge})" : x.SampleName)
            .FirstAsync();

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

        return new Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>>
            (kindName, itemName, sampleNameExt, specs, grades, parms);
    }
}