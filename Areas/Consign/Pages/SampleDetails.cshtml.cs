using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task OnGetAsync()
    {
        SampleId = RouteData.Values["handler"].ToString();
        var tuple = await InitPage(SampleId, null);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        SampleNameExt = tuple.Item3;
    }

    private async Task<Tuple<string, string, string>> InitPage(string sampleId, string searchkey)
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

        return new Tuple<string, string, string>(kindName, itemName, sampleNameExt);
    }
}