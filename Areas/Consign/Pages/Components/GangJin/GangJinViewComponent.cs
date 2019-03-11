using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;

namespace th.onlineconsign.Components
{
    public class GangJinViewComponent : ViewComponent
    {
        ItemDbContext db;

        public GangJinViewComponent(ItemDbContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName, string sampleId)
        {
            // TODO: c# / 2019-03-06 10_52 / A view with a surface identifier needs to control the display switching of text or image

            // var productors = await InitDataForComponent();
            // var extProductors = new SelectList(productors.Item1, nameof(UnitProductionUnit.Name), nameof(UnitProductionUnit.PutOnRecordsPassport));

            return await Task<IViewComponentResult>.Run(() =>
            {
                return View(viewName, new GangJinViewModel { });
            });

        }

        // private async Task<Tuple<List<UnitProductionUnit>>> InitDataForComponent()
        // {
        //     var productors = await db.UnitProductionUnit
        //         .Where(x => x.ProductionUnitType == "u")
        //         .Select(x => new UnitProductionUnit { Name = x.Name, PutOnRecordsPassport = x.PutOnRecordsPassport })
        //         .OrderBy(x => x.PutOnRecordsPassport)
        //         .ToListAsync();

        //     return new Tuple<List<UnitProductionUnit>>(productors);
        // }
    }

    public class GangJinViewModel
    {
        public SelectList Productors { get; set; }
    }
}