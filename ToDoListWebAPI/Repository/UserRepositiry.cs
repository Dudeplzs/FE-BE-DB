using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.Repository
{
    public class UserRepositiry
    {
        public static AppUser Get(string email, string username, string password)
        {
            var users = new List<AppUser>();
            users.Add(new AppUser { Id = 1, Email = "batman@gmail.com", Username = "batman", Password = "batman", Roles = "Admin" });
            users.Add(new AppUser { Id = 2, Email = "robin@gmail.com", Username = "robin", Password = "robin", Roles = "employee" });
            return users.Where(x => x.Username.ToLower() == username && x.Password == x.Password).FirstOrDefault();
        }
    }
}
