using System;
using RestoranRezervasyonSistemi.Models;

namespace RestoranRezervasyonSistemi.Services
{
    public static class UserRoleService
    {
        public static string Normalize(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return string.Empty;

            var value = role.Trim().ToLowerInvariant().Replace(" ", string.Empty);

            if (value == "admin")
                return UserRole.Admin;

            if (value == "garson" || value == "waiter")
                return UserRole.Waiter;

            if (value == "temizlikpersoneli" || value == "cleaner")
                return UserRole.Cleaner;

            return role.Trim();
        }
    }
}
