using Newtonsoft.Json;
using DeliveryApp.Models;

namespace DeliveryApp.Helper
{
    public class SessionUser : ISessionUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessionUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public List<Product> SearchUserProductsSession()
        {
            List<Product> products = new List<Product>();
            string sessionUser = _httpContext.HttpContext.Session.GetString("sessionProductsUser");
            if (string.IsNullOrEmpty(sessionUser))
            {
                CreateUserProductsSession(products);
                return SearchUserProductsSession();
            }

            products = JsonConvert.DeserializeObject<List<Product>>(sessionUser);
            return products;
        }


        public void CreateUserProductsSession(List<Product> products)
        {
            string productsList = JsonConvert.SerializeObject(products);
            _httpContext.HttpContext.Session.SetString("sessionProductsUser", productsList);
        }

        public void UpdateUserProductSession(List<Product> products)
        {
            RemoveUserProductsSession();
            CreateUserProductsSession(products);
        }

        public void RemoveUserProductsSession()
        {
            _httpContext.HttpContext.Session.Remove("sessionProductsUser");
        }

        public void AddProductCart(Product product)
        {
            List<Product> products = SearchUserProductsSession();
            var search = products.Where(x => x.Id == product.Id);
            Product productInCart = search.Count() > 0 ?  search.ElementAt(0) : null;
            int quantityInCart;

            if (products.Where(x => x.Id == product.Id).Count() != 0)
            {
                int index = products.IndexOf(productInCart);
                quantityInCart = productInCart.QuantityInCart;
                products.Remove(productInCart);
                productInCart.Add1QuantityInCart();
                products.Insert(index, productInCart);

                UpdateUserProductSession(products);
            }
            else
            {
                product.Add1QuantityInCart();
                products.Add(product);
                UpdateUserProductSession(products);
            }
        }

        public void RemoveProductCart(Product product)
        {
            List<Product> products = SearchUserProductsSession();
            var search = products.Where(x => x.Id == product.Id);
            Product productInCart = search.Count() > 0 ? search.ElementAt(0) : null;
            int quantityInCart;

            if (products.Where(x => x.Id == productInCart.Id).Count() != 0 && productInCart.QuantityInCart > 1)
            {
                int index = products.IndexOf(productInCart);
                quantityInCart = productInCart.QuantityInCart;
                products.Remove(productInCart);
                productInCart.Remove1QuantityInCart();
                products.Insert(index, productInCart);

                UpdateUserProductSession(products);
            }
            else
            {
                products.Remove(productInCart);
                UpdateUserProductSession(products);
            }
        }

        public double SumTotalCart(List<Product> products)
        {
            double total = 0;
            foreach (var product in products)
            {
                total += product.Price * product.QuantityInCart;
            }
            return total;
        }

        public void CreateUserSession(UserLogin user)
        {
            string valor = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("loggedUserSession", valor);
        }

        public void RemoveUserSession()
        {
            _httpContext.HttpContext.Session.Remove("loggedUserSession");
        }

        public UserLogin SearchUserSession()
        {
            string userSession = _httpContext.HttpContext.Session.GetString("loggedUserSession");
            if (string.IsNullOrEmpty(userSession))  return null;
            return JsonConvert.DeserializeObject<UserLogin>(userSession);
        }
    }
}
