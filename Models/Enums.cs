namespace RestoranRezervasyonSistemi.Models
{
    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Waiter = "Garson";
        public const string Cleaner = "TemizlikPersoneli";
    }

    public static class TableStatus
    {
        public const string Available = "Temiz";
        public const string Occupied = "Dolu";
        public const string Dirty = "Kirli";
    }
}
