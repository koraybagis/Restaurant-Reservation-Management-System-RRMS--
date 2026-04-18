using System.Data.SqlClient;
using System.Collections.Generic;
using RestoranRezervasyonSistemi.Models;
using System;
using System.Data;
using System.Configuration;

namespace RestoranRezervasyonSistemi.Controllers
{
    public class TableController
    {
        DbConnection db = new DbConnection();

        public List<Table> GetAllTables()
        {
            List<Table> tables = new List<Table>();
            try
            {
                using (var conn = db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    // Sadece temel bilgileri çekiyoruz (Hata riskini azaltmak için)
                    string sql = "SELECT id, table_name, capacity, location, Status, reservation_time FROM MasaDurumlari";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                Table masa = new Table();
                                masa.Id = dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0;
                                masa.TableName = dr["table_name"]?.ToString() ?? "İsimsiz";
                                masa.Capacity = dr["capacity"] != DBNull.Value ? Convert.ToInt32(dr["capacity"]) : 0;
                                masa.Location = dr["location"]?.ToString() ?? "";
                                // GetAllTables içindeki o satırı tam olarak böyle yap:
                                masa.Status = dr["status"] != DBNull.Value ? dr["status"].ToString().Trim() : "Bos";

                                // Saat okuma kısmını en güvenli hale getirdik
                                if (dr["reservation_time"] != DBNull.Value)
                                {
                                    masa.ReservationTime = (TimeSpan)dr["reservation_time"];
                                }

                                tables.Add(masa);
                            }
                            catch (Exception exInner)
                            {
                                // Tek bir masada hata varsa onu atla ama diğerlerini getir
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // EĞER HALA MASALAR GELMEZSE BURASI SANA NEDENİNİ SÖYLEYECEK:
                System.Windows.Forms.MessageBox.Show("Veritabanı Hatası: " + ex.Message);
            }
            return tables;
        }

        public bool AddTable(Table t)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "INSERT INTO tables (table_name, capacity, location, status) VALUES (@name, @cap, @loc, @status)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", t.TableName);
                    cmd.Parameters.AddWithValue("@cap", t.Capacity);
                    cmd.Parameters.AddWithValue("@loc", t.Location);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(t.Status) ? "Bos" : t.Status);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception) { return false; }
        }

        public bool DeleteTable(int id)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "DELETE FROM tables WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception) { return false; }
        }

        public bool UpdateTable(Table t)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "UPDATE tables SET table_name=@name, capacity=@cap, location=@loc, status=@status WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", t.TableName);
                    cmd.Parameters.AddWithValue("@cap", t.Capacity);
                    cmd.Parameters.AddWithValue("@loc", t.Location);
                    cmd.Parameters.AddWithValue("@status", t.Status);
                    cmd.Parameters.AddWithValue("@id", t.Id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception) { return false; }
        }
    }
}