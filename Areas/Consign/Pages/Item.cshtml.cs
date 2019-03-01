using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public class ItemPageModel : PageModel
{
    private readonly ItemDbContext db;
    public ItemPageModel(ItemDbContext db)
    {
        this.db = db;
    }

    public List<ItemItem> Items { get; set; }

    public string KindName { get; set; }

    [BindProperty(SupportsGet = true)]
    public string KindId { get; set; }

    [BindProperty]
    public string SearchKey { get; set; }

    public async Task OnGetAsync()
    {
        if (KindId == null)
            KindId = RouteData.Values["handler"].ToString();

        // route data
        // if (RouteData.Values.Keys.Contains("search"))
        //     SearchKey = RouteData.Values["search"].ToString();
        Console.WriteLine($"----------------------------------------{SearchKey}");

        // get kindname
        KindName = db.ItemKind.Single(x => x.KindId.ToString() == KindId).KindName;

        // get items
        var q = await db.ItemItem
        .Where(x => x.CanConsign == 1
            && x.KindId.ToString() == KindId
            && x.ItemName.IndexOf(SearchKey) >= 0)
        .Select(x => new ItemItem
        {
            ItemId = x.ItemId,
            ItemName = x.ItemName,
            ItemDescription = x.ItemDescription
        })
        .OrderBy(x => x.ItemId).ToListAsync();
        Console.WriteLine(q.ToString());
        Items = q;
    }

    public RedirectToPageResult OnPostSearchItem()
    {
        return RedirectToPage("Item", KindId, new { search = SearchKey });
    }
}