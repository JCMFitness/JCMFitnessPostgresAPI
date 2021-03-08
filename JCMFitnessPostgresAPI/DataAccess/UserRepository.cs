using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDBContext _context;

        public UserRepository(ApiDBContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var entity = _context.Users.FirstOrDefault(t => t.UserID == id);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public User GetUserByID(string id)
        {
            return _context.Users.FirstOrDefault(t => t.UserID == id);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
