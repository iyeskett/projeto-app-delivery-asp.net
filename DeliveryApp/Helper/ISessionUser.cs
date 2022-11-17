using DeliveryApp.Models;

namespace DeliveryApp.Helper
{
    public interface ISessionUser
    {
        void CreateUserProductsSession(List<Product> products);
        void RemoveUserProductsSession();

        public void UpdateUserProductSession(List<Product> products);
        List<Product> SearchUserProductsSession();

        void AddProductCart(Product product);
        void RemoveProductCart(Product product);

        double SumTotalCart(List<Product> products);


        void CreateUserSession(UserLogin user);
        void RemoveUserSession();
        UserLogin SearchUserSession();



    }
}
