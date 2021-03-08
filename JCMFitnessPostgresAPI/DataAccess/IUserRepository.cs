using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string id);
        User GetUserByID(string id);
        List<User> GetAllUsers();
    }
}
