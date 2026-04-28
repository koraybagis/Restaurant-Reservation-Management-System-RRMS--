using System.Collections.Generic;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestoranRezervasyonSistemi.Controllers
{
    public class TableController
    {
        private readonly DbConnection _db = new DbConnection();

        public List<Table> GetAllTables()
        {
            var tables = new List<Table>();

            try
            {
                using (var conn = _db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    const string sql = "SELECT * FROM tables";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        var schema = dr.GetSchemaTable();
                        bool hasId = HasColumn(schema, "id");
                        bool hasTableName = HasColumn(schema, "table_name");
                        bool hasCapacity = HasColumn(schema, "capacity");
                        bool hasLocation = HasColumn(schema, "location");
                        bool hasStatus = HasColumn(schema, "status");
                        bool hasReservationTime = HasColumn(schema, "reservation_time");

                        while (dr.Read())
                        {
                            var table = new Table
                            {
                                Id = hasId && dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0,
                                TableName = hasTableName ? dr["table_name"]?.ToString() ?? "İsimsiz" : "İsimsiz",
                                Capacity = hasCapacity && dr["capacity"] != DBNull.Value ? Convert.ToInt32(dr["capacity"]) : 0,
                                Location = hasLocation ? dr["location"]?.ToString() ?? string.Empty : string.Empty,
                                Status = hasStatus && dr["status"] != DBNull.Value ? dr["status"].ToString().Trim() : TableStatus.Available,
                            };

                            if (hasReservationTime && dr["reservation_time"] != DBNull.Value)
                            {
                                table.ReservationTime = dr["reservation_time"] is TimeSpan ts
                                    ? ts
                                    : TimeSpan.TryParse(dr["reservation_time"].ToString(), out var parsedTime)
                                        ? parsedTime
                                        : (TimeSpan?)null;
                            }

                            tables.Add(table);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Masalar yüklenirken veritabanı hatası. SQL Hata Kodu: {ex.Number}. Hata: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Masalar yüklenirken hata oluştu: {ex.Message}", ex);
            }

            return tables;
        }

        public bool UpdateTableStatus(int tableId, string status)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    const string sql = "UPDATE Tables SET Status = @Status WHERE Id = @Id";
                    
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Id", tableId);
                        
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Masa durumu güncellenemedi: " + ex.Message);
            }
        }

        public bool AddTable(Table t)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "INSERT INTO tables (table_name, capacity, location, status) VALUES (@name, @cap, @loc, @status)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", t.TableName);
                        cmd.Parameters.AddWithValue("@cap", t.Capacity);
                        cmd.Parameters.AddWithValue("@loc", t.Location);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(t.Status) ? TableStatus.Available : t.Status);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Masa ekleme işlemi başarısız oldu.", ex);
            }
        }

        public bool DeleteTable(int id)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "DELETE FROM tables WHERE id = @id";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Masa silme işlemi başarısız oldu.", ex);
            }
        }

        public bool UpdateTable(Table t)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string sql = "UPDATE tables SET table_name=@name, capacity=@cap, location=@loc, status=@status WHERE id=@id";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", t.TableName);
                        cmd.Parameters.AddWithValue("@cap", t.Capacity);
                        cmd.Parameters.AddWithValue("@loc", t.Location);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(t.Status) ? TableStatus.Available : t.Status);
                        cmd.Parameters.AddWithValue("@id", t.Id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Masa güncelleme işlemi başarısız oldu.", ex);
            }
        }

        private static bool HasColumn(DataTable schemaTable, string columnName)
        {
            if (schemaTable == null || string.IsNullOrWhiteSpace(columnName))
                return false;

            foreach (DataRow row in schemaTable.Rows)
            {
                if (string.Equals(row["ColumnName"]?.ToString(), columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}