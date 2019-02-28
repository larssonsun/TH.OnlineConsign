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
        Kinds = await db.ItemKind
        .Where(x => x.CanConsign == 1)
        .OrderBy(x => x.KindId).ToListAsync();

        
    }
}