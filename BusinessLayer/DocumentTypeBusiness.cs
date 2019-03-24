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
    public class DocumentTypeBusiness : IDocumentTypeBusiness
    {
        ISqlRepository<DocumentType> _repository;
        public DocumentTypeBusiness(ISqlRepository<DocumentType> repository)
        {
            _repository = repository;
        }
        public Result<List<DocumentType>> Get()
        {
            Result<List<DocumentType>> result = new Result<List<DocumentType>>();
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
    }
}
