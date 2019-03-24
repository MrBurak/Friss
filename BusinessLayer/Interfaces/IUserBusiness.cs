using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using DataLayer.Entities;
using CommonLayer.UserModels;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        Result<List<User>> Get();
        Result<User> Login(LoginModel request);
        Result<User> UpdateToken(int Id, string token);
        Result<User> GetUserByToken(string token);
    }
}
