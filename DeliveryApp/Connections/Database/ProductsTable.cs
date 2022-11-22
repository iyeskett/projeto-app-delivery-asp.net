using DeliveryApp.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace DeliveryApp.Connections.Database
{
    public class ProductsTable
    {
        public async Task<List<Product>> GetProductsAsync()
        {
            DataTable dataTable;

            string sqlQuery = "SELECT id, name, details, price, image FROM products";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var da = new MySqlDataAdapter(sqlQuery, cn))
                    {
                        using (dataTable = new DataTable())
                        {
                            da.Fill(dataTable);
                            var serialize = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                            var products = JsonConvert.DeserializeObject<List<Product>>(serialize);
                            return products;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Product> GetProducts()
        {
            DataTable dataTable;

            string sqlQuery = "SELECT id, name, details, price, image FROM products";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var da = new MySqlDataAdapter(sqlQuery, cn))
                    {
                        using (dataTable = new DataTable())
                        {
                            da.Fill(dataTable);
                            var serialize = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                            var products = JsonConvert.DeserializeObject<List<Product>>(serialize);
                            return products;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void NewProduct(Product product)
        {
            string sqlQuery = "INSERT INTO products(name,details,price, image) VALUES(@Name,@Details,@Price, @Image)";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Details", product.Details);
                        cmd.Parameters.AddWithValue("@Price", Math.Round(product.Price, 2));
                        cmd.Parameters.AddWithValue("@Image", product.Image);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            string sqlQuery = $"UPDATE products set name = @Name, details = @Details, price = @Price WHERE id = {product.Id}";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Details", product.Details);
                        cmd.Parameters.AddWithValue("@Price", Math.Round(product.Price, 2));


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateProductImage(Product product)
        {
            string sqlQuery = $"UPDATE products set image = @Image WHERE id = {product.Id}";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.Parameters.AddWithValue("@Image", product.Image);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product GetProduct(int id)
        {
            Product product = null;
            DataTable dataTable;
            var sqlQuery = $"SELECT id, name, details, price, image FROM products WHERE id = {id}";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                                if (dr.Read())
                                {
                                    product = new Product();
                                    product.Id = dr.GetInt32("id");
                                    product.Name = Convert.ToString(dr["name"]);
                                    product.Details = Convert.ToString(dr["details"]);
                                    product.Price = Convert.ToDouble(dr["price"]);
                                    product.Image = Convert.ToString(dr["image"]);
                                }
                        }
                    }
                }
                return product;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteProduct(int id)
        {
            string sqlQuery = $"DELETE FROM products WHERE id = {id}";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
