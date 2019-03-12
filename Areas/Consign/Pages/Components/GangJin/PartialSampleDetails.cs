using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public partial class SampleDetailsPageModel : BasePageModelForConsign
{
    public async Task<JsonResult> OnGetSearchXkz(string putOnRecordsPassport)
    {
        // TODO: c# / 2019-03-05 14:51 / should use cache
        var bindp = await db.UnitProductionUnit
            .Where(x => x.PutOnRecordsPassport == putOnRecordsPassport)
            .Select(x => x.Bindlicences).FirstAsync();

        IQueryable<UnitProductionUnit> iqUnitProductionUnit;
        if (!string.IsNullOrEmpty(bindp))
            iqUnitProductionUnit = db.UnitProductionUnit.Where(x => x.PutOnRecordsPassport == bindp);
        else
            iqUnitProductionUnit = db.UnitProductionUnit.Where(x => x.ProductionUnitType == "u");

        var productors = await iqUnitProductionUnit
            .Select(x => new UnitProductionUnit { Name = x.Name, PutOnRecordsPassport = x.PutOnRecordsPassport })
            .OrderBy(x => x.PutOnRecordsPassport)
            .ToListAsync();

        return new JsonResult(productors);
    }

}