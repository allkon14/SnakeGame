using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMDal
{
    public class OrmUsersDal : IUsersDal
    {
        public Entities.User GetByLogin(string login)
        {
            var context = new DefaultDbContext();
            try
            {
                ////Users - название таблицы
                var user = context.Users.FirstOrDefault(item => item.Name == login);

                if (user == null)
                {
                    return null;
                }
                var entity = new Entities.User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password
                    
                };
                return entity;
            }
            finally
            {
                context.Dispose();
            }
        }

        public Entities.User GetById(int id)
        {
            var context = new DefaultDbContext();
            try
            {
                var user = context.Users.FirstOrDefault(item => item.Id == id);

                if (user == null)
                {
                    return null;
                }
                var entity = new Entities.User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password
                };
                return entity;
            }
            finally
            {
                context.Dispose();
            }

        }

        public List<Entities.User> GetAllUsers()
        {
            var context = new DefaultDbContext();
            try
            {
                var user = context.Users;

                if (user == null)
                {
                    return null;
                }
                //user.
                //var entity = new Entities.User()
                //{
                //    Id = user.Id,
                //    Age = user.Age,
                //    Name = user.Name,
                //    Phone = user.Phone,
                //    Password = user.Password
                //};
                List<Entities.User> entities = new List<Entities.User>()
                {};
                foreach (var item in user)
                {
                    var entity = new Entities.User()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Password = item.Password
                    };
                    entities.Add(entity);
                }
                return entities;
            }
            finally
            {
                context.Dispose();
            }
        }

        public void PutUser(Entities.User user)
        {
            var context = new DefaultDbContext();
            try
            {
                ////Games -> Game
                context.Users.Add(new User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password

                });
                context.SaveChanges();
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}
