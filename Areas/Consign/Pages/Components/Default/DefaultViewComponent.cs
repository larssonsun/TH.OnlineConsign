using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace th.onlineconsign.Components
{
    public class DefaultViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string viewName)
        {
            // The default View "Default" is not used here for business structure information.
            return await Task<IViewComponentResult>.Run(() =>
            {
                // Get the name of the view according to the name of sampleuc
                return View(viewName);
            });
        }
    }
}