using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using StorageLayer.Interfaces;
using CommonLayer.DocumentModels;
using CoreApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Entities;


namespace CoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        IUserBusiness _repository;
        IDocumentBusiness _repositoryDocument;
        IHelper _helper;
        ISaver _saver;

        public DocumentController(IUserBusiness repository, IHelper helper, IDocumentBusiness repositoryDocument)
        {
            _repository = repository;
            _repositoryDocument = repositoryDocument;
            _helper = helper;
            _saver = _helper.GetSaver();
        }

        [HttpPost]
        [Route("v1")]
        public async Task<IActionResult> Post([FromBody] UploadRequest request)
        {
            string authHeader = this.HttpContext.Request.Headers["Authorization"];
            string token = authHeader.Substring("bearer ".Length).Trim();
            var userresult = _repository.GetUserByToken(token);
            if (!userresult.ResultStatus)
            {
                return Unauthorized();
            }
            if (userresult.ResultEntity.refUserRole!=1)
            {
                return Unauthorized();
            }
            
            var result = _saver.Save(request);
            if (!result.ResultStatus) return Ok(await Task.Run(() => result));
            return Ok(await Task.Run(() => _repositoryDocument.Insert(new Document
            {
                CreatedUserId = userresult.ResultEntity.Id,
                CreatedDate = DateTime.UtcNow,
                FilePath = result.ResultEntity.FilePath,
                FileSize = result.ResultEntity.FileSize,
                Name = request.Name
            })));
        }

        [HttpGet]
        [Route("v1")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.Run(() => _repositoryDocument.GetAll()));
        }

        [HttpGet]
        [Route("v1/download")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await Task.Run(() => _repositoryDocument.Update(Id)));
        }

    }
}