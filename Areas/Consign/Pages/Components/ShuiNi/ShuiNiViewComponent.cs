using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using th.onlineconsign.Model;

namespace th.onlineconsign.Components
{
    public class ShuiNiViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string sampleUcViewComponentName, string sampleUcName = "d_sampleuc_shuini")
        {

            // The default View "Default" is not used here for business structure information.
            return await Task<IViewComponentResult>.Run(() =>
            {
                // Get the name of the view according to the name of sampleuc
                return View(sampleUcName == "d_sampleuc_shuini" ? "ShuiNi" : "Default");
            });
        }
    }
}