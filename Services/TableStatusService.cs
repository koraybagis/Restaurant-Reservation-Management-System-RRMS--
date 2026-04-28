using System.Drawing;
using RestoranRezervasyonSistemi.Models;

namespace RestoranRezervasyonSistemi.Services
{
    public static class TableStatusService
    {
        public static string Normalize(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return TableStatus.Available;

            var value = status.Trim().ToLowerInvariant();
            if (value == "temiz" || value == "bos" || value == "boş" || value == "available" || value == "empty")
                return TableStatus.Available;
            if (value == "dolu" || value == "occupied" || value == "rezerv" || value == "rezerve")
                return TableStatus.Occupied;
            if (value == "kirli" || value == "dirty")
                return TableStatus.Dirty;
            return status.Trim();
        }

        public static Color GetColor(string normalizedStatus)
        {
            switch (normalizedStatus)
            {
                case TableStatus.Available:
                    return Color.ForestGreen;
                case TableStatus.Occupied:
                    return Color.LightCoral;
                case TableStatus.Dirty:
                    return Color.LightGray;
                default:
                    return Color.Gray;
            }
        }
    }
}
