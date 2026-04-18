using System;

namespace RestoranRezervasyonSistemi.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public string Status { get; set; }

        public bool IsOccupied { get; set; }

        public TimeSpan? ReservationTime { get; set; }
    }
}