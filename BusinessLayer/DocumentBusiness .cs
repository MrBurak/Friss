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
    public class DocumentBusiness : IDocumentBusiness
    {
        ISqlRepository<Document> _repository;
        ISqlRepository<User> _repositoryUser;
        public DocumentBusiness(ISqlRepository<Document> repository, ISqlRepository<User> repositoryUser)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
        }
        public Result<List<Document>> Get()
        {
            Result<List<Document>> result = new Result<List<Document>>();
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

        public Result<Document> Insert(Document document)
        {
            Result<Document> result = new Result<Document>();
            try
            {
                result.ResultEntity = _repository.Add(document);
                result.ResultMessage = "Saved";
                result.ResultStatus = true;
                result.ResultCode = ResultCodes.OK.GetHashCode();

            }
            catch(Exception ex)
            {
                result.ResultEntity = null;
                result.ResultStatus = false;
                result.ResultMessage = "Error";
                result.ResultInnerMessage = ex.ToString();
                result.ResultCode = ResultCodes.Error.GetHashCode();
            }
            return result;
        }


        public Result<string> Update(int Id)
        {
            Result<string> result = new Result<string>();
            try
            {
                var document = _repository.GetEntity(x => x.Id==Id);
                if (document != null)
                {
                    document.LastDownloadDate = DateTime.UtcNow;
                    _repository.Update(document);
                    result.ResultEntity = document.FilePath;
                    result.ResultStatus = true;
                    result.ResultMessage = "Updated";
                    result.ResultCode = ResultCodes.OK.GetHashCode();
                }
                else
                {
                    result.ResultEntity = null;
                    result.ResultStatus = false;
                    result.ResultMessage = "No document found";
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
        public Result<List<DocumentItem>> GetAll()
        {

            Result<List<DocumentItem>> result = new Result<List<DocumentItem>>();
            try
            {
                var obj = from d in _repository.GetList(x => x.Id > 0)
                          join u in _repositoryUser.GetList(x => x.refUserRole == 1) on d.CreatedUserId equals u.Id
                          select new { d.Id, d.Name, d.FilePath, d.CreatedDate, u.FirstName, u.LastName, d.LastDownloadDate, d.FileSize };
                List<DocumentItem> items = new List<DocumentItem>();
                foreach (var item in obj)
                {
                    items.Add(new DocumentItem
                    {
                        Id = item.Id,
                        FilePath = item.FilePath,
                        CreatedDate = item.CreatedDate,
                        FileSize = item.FileSize,
                        Fullname = item.
                        FirstName + " " + item.LastName,
                        LastDownloadDate = item.LastDownloadDate,
                        Name = item.Name
                    });
                }
                result.ResultEntity = items.OrderByDescending(x=> x.LastDownloadDate).ToList();
                result.ResultStatus = true;
                result.ResultMessage = "Listed";
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            catch (Exception ex)
            {
                result.ResultEntity = null;
                result.ResultStatus = false;
                result.ResultMessage = "Error";
                result.ResultCode = ResultCodes.Error.GetHashCode();
                result.ResultInnerMessage = ex.ToString();
            }
            return result;
        }



    }
}
