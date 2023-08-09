using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SHITPurchase.Models;

namespace SHITPurchase.Data
{
    public class DbWebAPIRepo : IWebAPIRepo
    {
        private readonly WebAPIDbContext _dbContext;
        public DbWebAPIRepo(WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //for users controllers
        public bool ValidLogin(string username, string password)
        {
            User u = _dbContext.Users.FirstOrDefault(
                e => e.UserName == username && e.Password == password);
            if (u == null)
                return false;
            else
                return true;
        }

        public User GetUserByUsername(string username)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public User RegisterUser(User new_user)
        {
            EntityEntry<User> e = _dbContext.Users.Add(new_user);
            User u = e.Entity;
            _dbContext.SaveChanges();
            return u;
        }

        public string GetVersion(User user)
        {
            if (user != null)
                return "v1";
            else
                return null;
        }

        //for orders controllers
        public Product GetProductByID(int productID)
        {
            Product i = _dbContext.Products.FirstOrDefault(e => e.Id == productID);
            return i;
        }

        public Order AddOrder(Order new_order)
        {
            EntityEntry<Order> e = _dbContext.Orders.Add(new_order);
            Order o = e.Entity;
            _dbContext.SaveChanges();
            return o;
        }

        public Order GetLastOrder()
        {
            IEnumerable<Order> allorders = _dbContext.Orders.ToList<Order>();
            Order lastorder = allorders.Last();
            return lastorder;
        }
    }
}