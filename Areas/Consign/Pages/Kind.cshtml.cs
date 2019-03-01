using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

public class KindPageModel : PageModel
{
    private readonly ItemDbContext db;
    public KindPageModel(ItemDbContext db)
    {
        this.db = db;
    }

    public List<ItemKind> Kinds { get; set; }

    public async Task OnGetAsync()
    {
        var q = db.ItemKind
        .Where(x => x.CanConsign == 1)
        .Select(x => new ItemKind
        {
            KindId = x.KindId,
            KindName = x.KindName,
            KindDescription = x.KindDescription
        })
        .OrderBy(x => x.KindId);

        System.Console.WriteLine(q.ToString());

        Kinds = await q.ToListAsync();
    }
}