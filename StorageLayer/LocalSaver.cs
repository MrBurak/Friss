using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.DocumentModels;
using StorageLayer.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace StorageLayer
{
    public class LocalSaver : ISaver
    {
        IConfigurationRoot _configuration;
        IHttpContextAccessor _httpContextAccessor;
        IValidation _validation;
        IHostingEnvironment _env;
        readonly string localpath;
        public LocalSaver(IValidation validation, IHostingEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            localpath = _configuration.GetSection("LocalStorage:Url").Value;
            _validation = validation;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public Result<DocumentInfo> Save(UploadRequest request)
        {
            Result<DocumentInfo> saveresult = new Result<DocumentInfo>();
            var result = _validation.Base64ToStream(request.Base64String);
            if (!result.ResultStatus)
            {
                saveresult.ResultMessage = result.ResultMessage;
                saveresult.ResultInnerMessage = result.ResultInnerMessage;
                saveresult.ResultCode = ResultCodes.Validation.GetHashCode();
            }
            Stream stream = result.ResultEntity;
            result = _validation.ValidateFileSize(result.ResultEntity);
            if (!result.ResultStatus)
            {
                saveresult.ResultMessage = result.ResultMessage;
                saveresult.ResultInnerMessage = result.ResultInnerMessage;
                saveresult.ResultCode = ResultCodes.Validation.GetHashCode();
                return saveresult;
            }
            result = _validation.ValidateExtension(result.ResultEntity, request.Extension);
            if (!result.ResultStatus)
            {
                saveresult.ResultMessage = result.ResultMessage;
                saveresult.ResultInnerMessage = result.ResultInnerMessage;
                saveresult.ResultCode = ResultCodes.Validation.GetHashCode();
                return saveresult;
            }
            try
            {
                var file = Guid.NewGuid().ToString("N") + request.Extension;
                var webrootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
               
                var path = Path.Combine(webrootFolder, localpath, file).ToLower();
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
                HttpRequest hrequest = _httpContextAccessor.HttpContext.Request;
                


                saveresult.ResultEntity = new DocumentInfo { FilePath="http://" + hrequest.Host + "/" + localpath + "/" + file, FileSize=stream.Length.ToString() };
                saveresult.ResultStatus = true;
                saveresult.ResultMessage = "Saved";
                saveresult.ResultCode = ResultCodes.OK.GetHashCode();
            }
            catch (Exception ex)
            {
                saveresult.ResultEntity = null;
                saveresult.ResultStatus = false;
                saveresult.ResultMessage = "Error";
                saveresult.ResultInnerMessage = ex.ToString();
                saveresult.ResultCode = ResultCodes.Error.GetHashCode();
            }
            return saveresult;
        }
    }
}
