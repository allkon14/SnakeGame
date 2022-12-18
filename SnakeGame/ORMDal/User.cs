using System;
using System.Collections.Generic;
using System.Text;

namespace ORMDal
{
    public class User
    {
        public User()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        //public int Age { get; set; }
        //public string Phone { get; set; }
        public string Password { get; set; }
        //public DateTime? CreationDate { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
