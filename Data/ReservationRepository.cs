using RestoranRezervasyonSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestoranRezervasyonSistemi.Data
{
    public sealed class ReservationRepository
    {
        private readonly DbConnection _db = new DbConnection();

        public DataTable GetReservationList()
        {
            using (var conn = _db.GetConnection())
            {
                const string sql = @"SELECT r.id, t.table_name as [Masa], r.customer_name as [Müşteri], 
                                   r.reservation_date as [Tarih], r.reservation_time as [Saat] 
                                   FROM reservations r 
                                   JOIN tables t ON r.table_id = t.id 
                                   ORDER BY r.reservation_date, r.reservation_time";
                using (var da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public bool DeleteReservationById(int reservationId)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = "DELETE FROM reservations WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", reservationId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteReservationByIdForUser(int reservationId, string customerEmail, string customerName)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = "DELETE FROM reservations WHERE id=@id AND customer_email=@mail AND customer_name=@name";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", reservationId);
                    cmd.Parameters.AddWithValue("@mail", customerEmail);
                    cmd.Parameters.AddWithValue("@name", customerName);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteReservationForUser(int tableId, DateTime date, TimeSpan time, string customerEmail, string customerName)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = "DELETE FROM reservations WHERE table_id=@tid AND reservation_date=@date AND reservation_time=@time AND customer_email=@mail AND customer_name=@name";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@mail", customerEmail);
                    cmd.Parameters.AddWithValue("@name", customerName);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ReservationExists(int tableId, DateTime date, TimeSpan time)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = "SELECT COUNT(1) FROM reservations WHERE table_id=@tid AND reservation_date=@date AND reservation_time=@time";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@time", time);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public (int ReservationId, TimeSpan ReservationTime)? GetNextReservationForUser(int tableId, DateTime date, string customerEmail, string customerName, TimeSpan? fromTime = null)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT TOP 1 id, reservation_time
                                     FROM reservations
                                     WHERE table_id=@tid AND reservation_date=@date AND customer_email=@mail AND customer_name=@name
                                       AND (@fromTime IS NULL OR reservation_time >= @fromTime)
                                     ORDER BY reservation_time";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@mail", customerEmail);
                    cmd.Parameters.AddWithValue("@name", customerName);
                    cmd.Parameters.AddWithValue("@fromTime", fromTime.HasValue ? (object)fromTime.Value : DBNull.Value);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                            return null;

                        var id = Convert.ToInt32(dr["id"]);
                        var t = (TimeSpan)dr["reservation_time"];
                        return (id, t);
                    }
                }
            }
        }

        public TimeSpan? GetNextReservationTime(int tableId, DateTime date, TimeSpan fromTime)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT TOP 1 reservation_time
                                     FROM reservations
                                     WHERE table_id=@tid AND reservation_date=@date
                                       AND reservation_time >= @fromTime
                                     ORDER BY reservation_time";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@fromTime", fromTime);

                    var value = cmd.ExecuteScalar();
                    if (value == null || value == DBNull.Value)
                        return null;

                    return (TimeSpan)value;
                }
            }
        }

        public TimeSpan GetEarliestAvailableTime(int tableId, DateTime date, TimeSpan fromTime, int durationMinutes)
        {
            var reservations = new List<TimeSpan>();

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT reservation_time
                                     FROM reservations
                                     WHERE table_id=@tid AND reservation_date=@date
                                     ORDER BY reservation_time";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["reservation_time"] != DBNull.Value)
                                reservations.Add((TimeSpan)dr["reservation_time"]);
                        }
                    }
                }
            }

            var duration = TimeSpan.FromMinutes(durationMinutes);
            var candidate = fromTime;

            foreach (var resStart in reservations)
            {
                // Candidate slot fits before the next reservation
                if (candidate + duration <= resStart)
                    return candidate;

                // Otherwise push candidate after this reservation block
                var blockEnd = resStart + duration;
                if (blockEnd > candidate)
                    candidate = blockEnd;
            }

            return candidate;
        }

        public bool HasConflict(int tableId, DateTime date, TimeSpan time, int conflictWindowMinutes)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                // Overlap rule:
                // [newStart, newEnd) and [existingStart, existingEnd) overlap iff
                // newStart < existingEnd AND newEnd > existingStart
                const string sql = @"SELECT COUNT(*)
                                     FROM reservations
                                     WHERE table_id = @tid
                                       AND reservation_date = @date
                                       AND @time < DATEADD(minute, @window, reservation_time)
                                       AND DATEADD(minute, @window, @time) > reservation_time";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@window", conflictWindowMinutes);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool HasActiveReservationForTable(int tableId, DateTime date, TimeSpan time, int durationMinutes)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT COUNT(1)
                                     FROM reservations
                                     WHERE table_id = @tid
                                       AND reservation_date = @date
                                       AND reservation_time <= @time
                                       AND DATEADD(minute, @duration, reservation_time) > @time";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@duration", durationMinutes);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public int InsertReservation(Reservation reservation)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"INSERT INTO reservations (table_id, customer_name, customer_phone, reservation_date, guest_count, reservation_time, customer_email) 
                                   VALUES (@tid, @name, @phone, @date, @count, @time, @mail);
                                   SELECT CAST(SCOPE_IDENTITY() as int)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", reservation.TableId);
                    cmd.Parameters.AddWithValue("@name", reservation.CustomerName);
                    cmd.Parameters.AddWithValue("@phone", reservation.CustomerPhone);
                    cmd.Parameters.AddWithValue("@date", reservation.ReservationDate.Date);
                    cmd.Parameters.AddWithValue("@count", reservation.GuestCount);
                    cmd.Parameters.AddWithValue("@time", reservation.ReservationTime);
                    cmd.Parameters.AddWithValue("@mail", reservation.CustomerEmail);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public List<Reservation> GetAllReservations()
        {
            var reservations = new List<Reservation>();
            
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT id, table_id, customer_name, customer_phone, reservation_date, guest_count, reservation_time, customer_email 
                                   FROM reservations 
                                   ORDER BY reservation_date, reservation_time";
                
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                TableId = Convert.ToInt32(dr["table_id"]),
                                CustomerName = dr["customer_name"]?.ToString(),
                                CustomerPhone = dr["customer_phone"]?.ToString(),
                                ReservationDate = Convert.ToDateTime(dr["reservation_date"]),
                                GuestCount = Convert.ToInt32(dr["guest_count"]),
                                ReservationTime = (TimeSpan)dr["reservation_time"],
                                CustomerEmail = dr["customer_email"]?.ToString()
                            });
                        }
                    }
                }
            }
            
            return reservations;
        }

        public int EnsureOpenReservationForTable(int tableId)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    const string selectSql = @"SELECT TOP 1 id
                                           FROM reservations
                                           WHERE table_id = @tid AND reservation_date = @date
                                           ORDER BY reservation_time DESC, id DESC";

                    using (var selectCmd = new SqlCommand(selectSql, conn, tx))
                    {
                        selectCmd.Parameters.AddWithValue("@tid", tableId);
                        selectCmd.Parameters.AddWithValue("@date", DateTime.Today);
                        var existingId = selectCmd.ExecuteScalar();
                        if (existingId != null && existingId != DBNull.Value)
                        {
                            tx.Commit();
                            return Convert.ToInt32(existingId);
                        }
                    }

                    const string insertSql = @"INSERT INTO reservations (table_id, customer_name, customer_phone, reservation_date, guest_count, reservation_time, customer_email)
                                           VALUES (@tid, @name, @phone, @date, @count, @time, @mail);
                                           SELECT CAST(SCOPE_IDENTITY() as int)";

                    using (var insertCmd = new SqlCommand(insertSql, conn, tx))
                    {
                        insertCmd.Parameters.AddWithValue("@tid", tableId);
                        insertCmd.Parameters.AddWithValue("@name", $"Masa {tableId} Adisyon");
                        insertCmd.Parameters.AddWithValue("@phone", "-");
                        insertCmd.Parameters.AddWithValue("@date", DateTime.Today);
                        insertCmd.Parameters.AddWithValue("@count", 1);
                        insertCmd.Parameters.AddWithValue("@time", DateTime.Now.TimeOfDay);
                        insertCmd.Parameters.AddWithValue("@mail", "walkin@local");
                        var newId = Convert.ToInt32(insertCmd.ExecuteScalar());
                        tx.Commit();
                        return newId;
                    }
                }
            }
        }

        public int? GetOpenReservationForTable(int tableId)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT TOP 1 id
                                     FROM reservations
                                     WHERE table_id = @tid
                                     ORDER BY reservation_date DESC, reservation_time DESC, id DESC";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    var id = cmd.ExecuteScalar();
                    if (id == null || id == DBNull.Value)
                        return null;

                    return Convert.ToInt32(id);
                }
            }
        }

        public int? GetActiveReservationIdForTable(int tableId, DateTime date, TimeSpan time, int durationMinutes)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT TOP 1 id
                                     FROM reservations
                                     WHERE table_id = @tid
                                       AND reservation_date = @date
                                       AND reservation_time <= @time
                                       AND DATEADD(minute, @duration, reservation_time) > @time
                                     ORDER BY reservation_time DESC, id DESC";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@duration", durationMinutes);

                    var id = cmd.ExecuteScalar();
                    if (id == null || id == DBNull.Value)
                        return null;

                    return Convert.ToInt32(id);
                }
            }
        }

        public bool CancelReservationWithMenuItems(int reservationId)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    const string deleteMenuSql = "DELETE FROM ReservationMenuItems WHERE ReservationId = @resId";
                    using (var menuCmd = new SqlCommand(deleteMenuSql, conn, tx))
                    {
                        menuCmd.Parameters.AddWithValue("@resId", reservationId);
                        menuCmd.ExecuteNonQuery();
                    }

                    const string deleteReservationSql = "DELETE FROM reservations WHERE id = @id";
                    using (var reservationCmd = new SqlCommand(deleteReservationSql, conn, tx))
                    {
                        reservationCmd.Parameters.AddWithValue("@id", reservationId);
                        var affected = reservationCmd.ExecuteNonQuery();
                        if (affected <= 0)
                        {
                            tx.Rollback();
                            return false;
                        }
                    }

                    tx.Commit();
                    return true;
                }
            }
        }
    }
}

