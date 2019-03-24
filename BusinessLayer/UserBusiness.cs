using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLayer.Interfaces;
using CommonLayer;
using DataLayer.Entities;
using DataLayer.Abstract;
using CommonLayer.UserModels;

namespace BusinessLayer
{
    public class UserBusiness : IUserBusiness
    {
        ISqlRepository<User> _repository;
        IUserRoleBusiness _userrolerepository;
        public UserBusiness(ISqlRepository<User> repository, IUserRoleBusiness userrolerepository)
        {
            _repository = repository;
            _userrolerepository = userrolerepository;
        }
        public Result<List<User>> Get()
        {
            Result<List<User>> result = new Result<List<User>>();
            try
            {
                result.ResultEntity = _repository.GetList(x => !x.IsDeleted).ToList();
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
        public Result<User> Login(LoginModel request)
        {
            Result<User> result = new Result<User>();
            try
            {
                var user= _repository.GetEntity(x => !x.IsDeleted && x.UserName == request.UserName && x.Password == Utility.MD5Crypt(request.Password));
                if (user != null)
                {
                    result.ResultEntity = user;
                    result.ResultStatus = true;
                    result.ResultMessage = "Loged In";
                    result.ResultCode = ResultCodes.OK.GetHashCode();
                }
                else
                {
                    result.ResultEntity =null;
                    result.ResultStatus = false;
                    result.ResultMessage = "Login failed";
                    result.ResultCode = ResultCodes.Failed.GetHashCode();

                }
               
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
        public Result<User> UpdateToken(int Id, string token)
        {
            Result<User> result = new Result<User>();
            try
            {
                var user = _repository.GetEntity(x => x.Id==Id);
                if (user != null)
                {
                    user.Token = token;
                    result.ResultEntity = user;
                    result.ResultStatus = _repository.Update(user);
                    result.ResultMessage = "Loged In";
                    result.ResultCode = ResultCodes.OK.GetHashCode();
                }
                else
                {
                    result.ResultEntity = null;
                    result.ResultStatus = false;
                    result.ResultMessage = "Login failed";
                    result.ResultCode = ResultCodes.Failed.GetHashCode();
                }

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
        public Result<User> GetUserByToken(string token)
        {
            Result<User> result = new Result<User>();
            try
            {
                var user = _repository.GetEntity(x => !x.IsDeleted && x.Token == token);
                if (user != null)
                {
                    result.ResultEntity = user;
                    result.ResultStatus = true;
                    result.ResultMessage = "Record Found";
                    result.ResultCode = ResultCodes.OK.GetHashCode();
                }
                else
                {
                    result.ResultEntity = null;
                    result.ResultStatus = false;
                    result.ResultMessage = "Record Not Found";
                    result.ResultCode = ResultCodes.NoRecord.GetHashCode();
                }

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
