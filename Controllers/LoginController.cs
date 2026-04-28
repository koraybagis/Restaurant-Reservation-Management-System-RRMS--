using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Data;
using System;
using System.Collections.Generic;

namespace RestoranRezervasyonSistemi.Controllers
{
    public class LoginController
    {
        private readonly UserRepository _userRepository;

        public LoginController(UserRepository userRepository = null)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        public User CheckLogin(string userOrFullName, string pass)
        {
            if (string.IsNullOrWhiteSpace(userOrFullName) || string.IsNullOrWhiteSpace(pass))
                return null;

            try
            {
                return _userRepository.AuthenticateUser(userOrFullName, pass);
            }
            catch (Exception ex)
            {
                // Loglama burada yapılabilir
                throw new Exception("Giriş işlemi sırasında hata oluştu.", ex);
            }
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            
            users.AddRange(_userRepository.GetAllUsers());
            
            return users;
        }
    }
}