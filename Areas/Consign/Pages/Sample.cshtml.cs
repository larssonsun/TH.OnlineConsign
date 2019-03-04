using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public class SamplePageModel : PageModel
{
    ItemDbContext db;
    public SamplePageModel(ItemDbContext db)
    {
        this.db = db;
    }

    public string KindName { get; set; }
    public string ItemName { get; set; }

    public List<ItemSample> Samples { get; set; }

    [BindProperty]
    public string SearchKey { get; set; }

    [BindProperty(SupportsGet = true)]
    public string ItemId { get; set; }

    public async Task OnGetAsync()
    {
        if (ItemId == null)
            ItemId = RouteData.Values["handler"].ToString();

        var tuple = await InitPage(ItemId, null);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        Samples = tuple.Item3;
    }

    public async Task OnPostSearchSampleAsync()
    {
        var tuple = await InitPage(ItemId, SearchKey);
        KindName = tuple.Item1;
        ItemName = tuple.Item2;
        Samples = tuple.Item3;
    }

    private async Task<Tuple<string, string, List<ItemSample>>> InitPage(string itemId, string searchkey)
    {
        // get kindname for breadcrumb
        var kingName = await db.ItemKind
            .Where(x => x.KindId.ToString() == itemId.Substring(0, 2))
            .Select(x => x.KindName).FirstAsync();

        // get itemname for breadcrumb
        var itemName = await db.ItemItem
            .Where(X => X.ItemId.ToString() == itemId)
            .Select(x => x.ItemName).FirstAsync();

        // get samples by itemid
        var samples = await db.ItemSample
            .Where(
                x => x.CanConsign == 1
                && x.ItemId.ToString() == itemId
                && (searchkey != null ? x.SampleName.Contains(searchkey) ||
                    x.SampleJudge.Contains(searchkey) : true))
            .Select(x => new ItemSample()
            {
                SampleId = x.SampleId,
                SampleName = x.SampleName,
                SampleJudge = x.SampleJudge
            })
            .OrderBy(x => x.SampleId).ToListAsync();

        return new Tuple<string, string, List<ItemSample>>(kingName, itemName, samples);
    }
}