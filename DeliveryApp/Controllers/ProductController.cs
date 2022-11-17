using DeliveryApp.Connections.Database;
using DeliveryApp.Helper;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DeliveryApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly ISessionUser _session;
        internal ProductsTable _productsTable = new ProductsTable();
        IHostingEnvironment _appEnvironment;
        string pathWebRoot;
        string pathImage;


        public ProductController(IHostingEnvironment env, ISessionUser session)
        {
            _appEnvironment = env;
            pathWebRoot = _appEnvironment.WebRootPath;
            pathImage = $@"{pathWebRoot}\img";
            _session = session;

        }

        public IActionResult Index()
        {
            var loggedUser = _session.SearchUserSession();
            if (loggedUser == null) return RedirectToAction("Index", "Login");


            List<Product> products = _productsTable.GetProducts();

            return View(products);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Product product)
        {
            if (ModelState.IsValid)
            {
                return View("Import", product);

            }

            return View(product);
        }

        public IActionResult Import(Product product)
        {
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Import(List<IFormFile> arquivos, Product product)
        {
            Product productDB = _productsTable.GetProduct(product.Id);
            bool newProduct = false;

            if (productDB == null)
            {
                newProduct = true;
            }

            try
            {
                if (arquivos.Count != 0)
                {
                    foreach (var arquivo in arquivos)
                    {
                        string path = $@"{pathImage}\{arquivo.FileName}";
                        //copia o arquivo para o local de destino original
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await arquivo.CopyToAsync(stream);
                            if (newProduct)
                            {
                                product.Image = $"/img/{arquivo.FileName}";
                                _productsTable.NewProduct(product);
                            }
                            else
                            {
                                productDB.Image = $@"/img/{arquivo.FileName}";
                                _productsTable.UpdateProductImage(productDB);
                            }
                        }

                    }

                    TempData["Message"] = $"Imagem enviada com sucesso";
                    return Redirect("Index");

                }
                TempData["ErroMessage"] = $"Selecione um arquivo.";
                return View(product);


            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos carregar o arquivo: {e.Message}";
                return RedirectToAction("Index");
            }


        }
    }
}
