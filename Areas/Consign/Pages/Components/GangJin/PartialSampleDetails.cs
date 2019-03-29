using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Components;
using th.onlineconsign.ExtViewModels;
using th.onlineconsign.Model;

public partial class SampleDetailsPageModel : BasePageModelForConsign
{
    [BindProperty]
    public SampleStorageAddonGangJinExt SampleStorageAddonGangJinExt { get; set; }

    // TODO: c# / 2019-03-05 14:51 / should use cache
    public async Task<JsonResult> OnGetSearchXkz(string putOnRecordsPassport)
    {
        return new JsonResult(await sampleConsignService.GetXkzSearchResult(putOnRecordsPassport));
    }

    public async Task<JsonResult> OnGetGetSurfaceFlag(string xkzProductorName)
    {
        return new JsonResult(await sampleConsignService.GetXkzSurface(xkzProductorName));
    }

    public async Task GangJin_AddonSave(Guid pid)
    {
        var sampleStorageAddonGangJin = tools.EntityCopyForParent<SampleStorageAddonGangJinExt, SampleStorageAddonGangJin>(SampleStorageAddonGangJinExt);
        sampleStorageAddonGangJin.Id = Guid.NewGuid();
        sampleStorageAddonGangJin.ParentId = pid;
        sampleStorageAddonGangJin.GjBianMiaoBiaoShiImage = Convert.FromBase64String(SampleStorageAddonGangJinExt.GjBianMiaoBiaoShiImageBase64);
        await dbSampleStorage.SampleStorageAddonGangJin.AddAsync(sampleStorageAddonGangJin);
    }
}