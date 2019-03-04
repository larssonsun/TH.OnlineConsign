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

    [BindProperty]
    public string KindId { get; set; }

    [BindProperty]
    public string SearchKey { get; set; }

    public async Task OnGetAsync()
    {
        if (KindId == null)
            KindId = RouteData.Values["handler"].ToString();

        await InitPage(KindId);
    }

    public async Task OnPostSearchItem()
    {
        await InitPage(KindId);
    }

    public async Task InitPage(string kindId)
    {
        // get kindname
        KindName = await db.ItemKind
        .Where(x => x.KindId.ToString() == kindId)
        .Select(x => x.KindName).FirstAsync();

        // get items
        Items = await db.ItemItem
        .Where(x => x.CanConsign == 1
            && x.KindId.ToString() == kindId
            && (SearchKey != null ? x.ItemName.Contains(SearchKey) : true))
        .Select(x => new ItemItem
        {
            ItemId = x.ItemId,
            ItemName = x.ItemName,
            ItemDescription = x.ItemDescription
        })
        .OrderBy(x => x.ItemId).ToListAsync();
    }
}