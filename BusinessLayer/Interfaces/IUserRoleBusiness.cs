using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using DataLayer.Entities;
using CommonLayer.UserModels;

namespace BusinessLayer.Interfaces
{
    public interface IUserRoleBusiness
    {
        Result<List<UserRole>> Get();
        Result<UserRole> Get(int Id);
        
    }
}
