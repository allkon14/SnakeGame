using Entities;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IUsersDal
    {
        User GetById(int id);
        User GetByLogin(string login);
        List<User> GetAllUsers();
    }
}
