using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using th.onlineconsign.Model;

public class GangJinViewComponent : ViewComponent
{
    ItemDbContext db;

    public GangJinViewComponent(ItemDbContext db)
    {
        this.db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync(string sampleUcViewComponentViewName)
    {
        // TODO: c# / 2019-03-06 10_52 / A view with a surface identifier needs to control the display switching of text or image
        return await Task<IViewComponentResult>.Run(() =>
        {
            // TODO:right now加载许可证
            return View(sampleUcViewComponentViewName);
        });
    }
}