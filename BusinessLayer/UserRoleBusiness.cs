using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLayer.Interfaces;
using CommonLayer;
using DataLayer.Entities;
using DataLayer.Abstract;

namespace BusinessLayer
{
    public class UserRoleBusiness : IUserRoleBusiness
    {
        ISqlRepository<UserRole> _repository;
        public UserRoleBusiness(ISqlRepository<UserRole> repository)
        {
            _repository = repository;
        }
        public Result<List<UserRole>> Get()
        {
            Result<List<UserRole>> result = new Result<List<UserRole>>();
            try
            {
                result.ResultEntity = _repository.GetList(x => x.Id > 0).ToList();
                result.ResultStatus = true;
                result.ResultMessage = "Listed";
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            catch (Exception ex)
            {
                result.ResultEntity = null;
                result.ResultStatus = false;
                result.ResultMessage = "Error";
                result.ResultInnerMessage = ex.ToString();
                result.ResultCode = ResultCodes.Error.GetHashCode();
            }
            return result;
        }

        public Result<UserRole> Get(int Id)
        {
            Result<UserRole> result = new Result<UserRole>();
            try
            {
                result.ResultEntity = _repository.GetEntity(x => x.Id ==Id);
                result.ResultStatus = true;
                result.ResultMessage = "Found";
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            catch (Exception ex)
            {
                result.ResultEntity = null;
                result.ResultStatus = false;
                result.ResultMessage = "Error";
                result.ResultInnerMessage = ex.ToString();
                result.ResultCode = ResultCodes.Error.GetHashCode();
            }
            return result;
        }
    }
}
