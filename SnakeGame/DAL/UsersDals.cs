using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class UsersDal : IUsersDal
    {
        private List<User> users = new List<User>() {
            new User() { Id = 1, Name = "Ivan", Age = 20, Phone = "123454" , Password = "123456"},
            new User() { Id = 2, Name = "Alena", Age = 20, Phone = "674523" , Password = "674523"},
            new User() { Id = 3, Name = "Ivan", Age = 20, Phone = "123454" },
            new User() { Id = 4, Name = "Ivan", Age = 20, Phone = "123454" },
        };

        public User GetById(int id)
        {
            return users.FirstOrDefault(item => item.Id == id);
        }
        public User GetByLogin(string login)
        {
            return users.FirstOrDefault(item => item.Name == login);
        }
        public List<User> GetAllUsers()
        {
            return users;
        }
    }
}
