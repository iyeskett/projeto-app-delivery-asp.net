using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryApp.ViewComponents
{
    public class ProductsCart : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            

            return View(product);
        }
    }
}
