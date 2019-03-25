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
        return new JsonResult(await sampleConsignService.GetXkzSearchResult(putOnRecordsPassport));
    }

    public async Task<JsonResult> OnGetGetSurfaceFlag(string xkzProductorName)
    {
        return new JsonResult(await sampleConsignService.GetXkzSurface(xkzProductorName));
    }

}