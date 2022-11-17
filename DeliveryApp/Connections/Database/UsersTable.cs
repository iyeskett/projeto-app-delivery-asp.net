using DeliveryApp.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace DeliveryApp.Connections.Database
{
    public class UsersTable
    {

        public UserLogin GetUser(string email)
        {
            DataTable dataTable;
            UserLogin login = new UserLogin();
            string sqlQuery = $"SELECT * FROM users WHERE email='{email}'";
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                if (dataReader.Read())
                                {
                                    login.Id = dataReader.GetInt32("id");
                                    login.Username = Convert.ToString(dataReader["username"]);
                                    login.Email = Convert.ToString(dataReader["email"]);
                                    login.Password = Convert.ToString(dataReader["password"]);
                                    login.Type = Convert.ToString(dataReader["type"]);


                                }
                            }

                        }
                    }
                }
                return login;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public UserLogin GetUserId(int id)
        {
            DataTable dataTable;
            UserLogin login = new UserLogin();
            string sqlQuery = $"SELECT * FROM users WHERE id='{id}'";
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                if (dataReader.Read())
                                {
                                    login.Id = dataReader.GetInt32("id");
                                    login.Username = Convert.ToString(dataReader["username"]);
                                    login.Email = Convert.ToString(dataReader["email"]);
                                    login.Password = Convert.ToString(dataReader["password"]);
                                    login.Type = Convert.ToString(dataReader["type"]);


                                }
                            }

                        }
                    }
                }
                return login;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public List<UserLogin> GetUsers()
        {
            DataTable dataTable;
            string sqlQuery = "SELECT * FROM users ORDER BY username";
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
                            var serializado = JsonConvert.SerializeObject(dataTable);

                            var users = JsonConvert.DeserializeObject<List<UserLogin>>(serializado);
                            return users;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void NewUser(UserLogin usuario)
        {
            string sqlQuery = "INSERT INTO users(username, email, password, type) VALUES (@Username, @Email, @Password, @Type)";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario.Username);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Password", usuario.Password);
                        cmd.Parameters.AddWithValue("@Type", usuario.Type);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void EditUser(UserLogin user)
        {
            var sqlQuery = $"UPDATE users SET username = @Username, email = @Email, password = @Password, type = @Type WHERE id = {user.Id}";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sqlQuery, cn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Type", user.Type);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteUser(int id)
        {
            var sqlQuery = $"DELETE FROM users WHERE id = {id}";

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
