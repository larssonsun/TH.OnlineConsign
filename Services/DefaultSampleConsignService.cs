using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

namespace th.onlineconsign.Services
{
    public class DefaultSampleConsignService : ISampleConsignService
    {
        ItemDbContext db;
        public DefaultSampleConsignService(ItemDbContext db)
        {
            this.db = db;
        }

        public async Task<string> GetNewSampleConsignId()
        {
            var result  = await Task.Run<string>(()=>{
                return Guid.NewGuid().ToString();
            });
            return result;
        }

        public async Task<Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>, bool, Tuple<List<DpDelegateQuanUnit>, string>>> GetInitDataForSampleDetailPage(string sampleId, string searchkey)
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

        public async Task<List<UnitProductionUnit>> GetSearchProductor(string sampleId, string searchkey)
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

            return productors;
        }

        public async Task<List<UnitProductionUnit>> GetXkzSearchResult(string putOnRecordsPassport)
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

            return productors;
        }

        public async Task<UnitProductionUnit> GetXkzSurface(string xkzProductorName)
        {
            var xkzSurfaceFlag = await db.UnitProductionUnit
            .Where(x => x.PutOnRecordsPassport == xkzProductorName)
            .Select(x => new UnitProductionUnit
            {
                SurfaceFlagType = x.SurfaceFlagType,
                SurfaceFlagText = x.SurfaceFlagText,
                SurfaceFlagPicture = x.SurfaceFlagPicture
            }).FirstOrDefaultAsync();

            return xkzSurfaceFlag;
        }
    }
}