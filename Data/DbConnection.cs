using System;
using System.Data.SqlClient;

public class DbConnection
{
    // Başına @ koyarak slash işaretini garantiledik
    // Noktadan sonra port numarasını (1433) açıkça belirtiyoruz.
    private string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}