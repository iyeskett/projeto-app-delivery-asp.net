using DeliveryApp.Connections.Database;
using DeliveryApp.Helper;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DeliveryApp.Controllers
{
    public class ProductController : Controller
    {
        Random random = new Random();
        private readonly ISessionUser _session;
        internal ProductsTable _productsTable = new ProductsTable();
        IHostingEnvironment _appEnvironment;
        string pathWebRoot;
        string pathImg;


        public ProductController(IHostingEnvironment env, ISessionUser session)
        {
            _appEnvironment = env;
            pathWebRoot = _appEnvironment.WebRootPath;
            pathImg = $@"{pathWebRoot}\img";
            _session = session;

        }

        public IActionResult Index()
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            List<Product> products = _productsTable.GetProducts();

            return View(products);
        }

        public IActionResult New()
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult New(Product product)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                return View("Import", product);

            }

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            var product = _productsTable.GetProduct(id);
            if (product == null) Redirect("Index");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _productsTable.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = _productsTable.GetProduct(id);
            if (product == null) Redirect("Index");

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            var productDB = _productsTable.GetProduct(id);
            if (productDB == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(productDB);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            var productDB = _productsTable.GetProduct(product.Id);
            if (productDB != null)
            {
                _productsTable.DeleteProduct(productDB.Id);
                var directory = productDB.Image.Split('/');
                Directory.Delete($@"{pathImg}\{directory[2]}", true);
            }
            return Redirect("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Import(List<IFormFile> arquivos, Product product)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            Product productDB = _productsTable.GetProduct(product.Id);
            bool newProduct = false;

            if (productDB == null)
            {
                newProduct = true;
            }

            try
            {
                if (!newProduct && arquivos.Count == 0 && ModelState.IsValid)
                {
                    productDB.Price = product.Price;
                    Edit(productDB);
                    return Redirect("Index");
                }

                if (arquivos.Count != 0 && ModelState.IsValid)
                {
                    foreach (var arquivo in arquivos)
                    {
                        string path;
                        int randomInt;
                        var fileNameSplit = arquivo.FileName.Split('.');
                        string extension = fileNameSplit[fileNameSplit.Length-1];
                        do
                        {
                            
                            randomInt = random.Next();
                            path = $@"{pathImg}\{randomInt}\archive.{extension}";

                        } while (Directory.Exists(path));

                        Directory.CreateDirectory($@"{pathImg}\{randomInt}");

                        //copia o arquivo para o local de destino original
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await arquivo.CopyToAsync(stream);
                            if (newProduct)
                            {
                                product.Image = $"/img/{randomInt}/archive.{extension}";
                                _productsTable.NewProduct(product);
                            }
                            else
                            {
                                var directory = productDB.Image.Split('/');
                                Directory.Delete($@"{pathImg}\{directory[2]}", true);
                                productDB.Image = $@"/img/{randomInt}/archive.{extension}";
                                _productsTable.UpdateProductImage(productDB);
                            }
                        }

                    }

                    TempData["Message"] = $"Imagem enviada com sucesso";
                    return Redirect("Index");

                }



                TempData["ErroMessage"] = $"Selecione um arquivo.";
                TempData["ErroMessageNew"] = $"Obrigatório a escolha de uma imagem ao adicionar um novo produto";

                if (product.Id > 0) return View("Edit", product);
                else return View("New", product);



            }
            catch (Exception e)
            {

                TempData["ErroMessage"] = $"Ops, não conseguimos carregar o arquivo: {e.Message}";
                return View(product);
            }


        }
    }
}
