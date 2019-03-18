using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public partial class SampleDetailsPageModel : BasePageModelForConsign
{
    // TODO: c# / 2019-03-05 14:51 / should use cache
    public async Task<JsonResult> OnGetSearchXkz(string putOnRecordsPassport)
    {
        var bindp = await db.UnitProductionUnit
            .Where(x => x.PutOnRecordsPassport == putOnRecordsPassport)
            .Select(x => x.Bindlicences).FirstOrDefaultAsync();

        IQueryable<UnitProductionUnit> iqUnitProductionUnit;
        if (!string.IsNullOrEmpty(bindp))
            iqUnitProductionUnit = db.UnitProductionUnit.Where(x => x.PutOnRecordsPassport == bindp);
        else
            iqUnitProductionUnit = db.UnitProductionUnit.Where(x => x.ProductionUnitType == "u");

        var productors = await iqUnitProductionUnit
            .Select(x => new UnitProductionUnit
            {
                Name = x.Name,
                PutOnRecordsPassport = x.PutOnRecordsPassport
            })
            .OrderBy(x => x.PutOnRecordsPassport)
            .ToListAsync();

        return new JsonResult(productors);
    }

    public async Task<JsonResult> OnGetGetSurfaceFlag(string xkzProductorName)
    {
        var xkzSurfaceFlag = await db.UnitProductionUnit
        .Where(x => x.PutOnRecordsPassport == xkzProductorName)
        .Select(x => new UnitProductionUnit
        {
            SurfaceFlagType = x.SurfaceFlagType,
            SurfaceFlagText = x.SurfaceFlagText,
            SurfaceFlagPicture = x.SurfaceFlagPicture
        }).FirstOrDefaultAsync();

        return new JsonResult(xkzSurfaceFlag);
    }

}