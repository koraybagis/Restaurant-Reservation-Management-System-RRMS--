using System.Data.SqlClient;
using RestoranRezervasyonSistemi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RestoranRezervasyonSistemi.Controllers
{
    public class LoginController
    {
        DbConnection db = new DbConnection();

        public User CheckLogin(string user, string pass)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM users WHERE username=@u AND password=@p";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new User
                        {
                            Username = dr["username"].ToString(),
                            Role = dr["role"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}