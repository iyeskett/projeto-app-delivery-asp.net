using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryApp.ViewComponents
{
    public class Products : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            

            return View(product);
        }
    }
}
