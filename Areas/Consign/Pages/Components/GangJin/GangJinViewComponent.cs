using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using th.onlineconsign.Model;
using th.onlineconsign.Services;

namespace th.onlineconsign.Components
{
    public class GangJinViewComponent : ViewComponent
    {
        ItemDbContext db;
        ISampleUcControler sampleUcControler;
        
        public GangJinViewComponent(ItemDbContext db, ISampleUcControler service1)
        {
            this.db = db;
            sampleUcControler = service1;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName, string sampleId)
        {
            // TODO: c# / 2019-03-06 10_52 / A view with a surface identifier needs to control the display switching of text or image

            return await Task<IViewComponentResult>.Run(() =>
            {
                return View(viewName, new GangJinViewModel {  });
            });
        }
    }

    public class GangJinViewModel
    {
        // public bool UseXkzForGangJin { get; set; }
    }
}