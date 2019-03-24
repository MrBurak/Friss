using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonLayer;
using StorageLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using System.Linq;

namespace StorageLayer
{
    public class Validation : IValidation
    {
        IConfigurationRoot _configuration;
        IDocumentTypeBusiness _repository;

        readonly int maxFileSize;
        public Validation(IDocumentTypeBusiness repository)
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            maxFileSize = Convert.ToInt32(_configuration.GetSection("UploadProperties:MaxFileSize").Value);
            _repository = repository;

        }
        public Result<Stream> ValidateExtension(Stream stream, string extension)
        {
            Result<Stream> result = new Result<Stream>();
            var documenttypes = _repository.Get().ResultEntity;
            List<string> ExtensionList = new List<string>();
            foreach (DocumentType dt in documenttypes)
            {
                ExtensionList.AddRange(dt.Extentions.Split(Convert.ToChar(";")));
            }
            if (ExtensionList.Contains(extension))
            {

                result.ResultStatus = true;
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            else
            {
                result.ResultStatus = false;
                result.ResultMessage = "File extention have to be in [" + string.Join(" ", ExtensionList) + "]";
                result.ResultCode = ResultCodes.Validation.GetHashCode();
            }
            return result;
            
        }



        public Result<Stream> Base64ToStream(string base64)
        {
            Result<Stream> result = new Result<Stream>(); 
            try
            {
                byte[] data = Convert.FromBase64String(base64);
                result.ResultEntity= new MemoryStream(data);
                result.ResultStatus = true;
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            catch(Exception ex)
            {
                result.ResultEntity = null;
                result.ResultStatus = false;
                result.ResultMessage = "Unable to read your file";
                result.ResultInnerMessage = ex.ToString();
                result.ResultCode = ResultCodes.Validation.GetHashCode();
            }
            return result;
        }

        public Result<Stream> ValidateFileSize(Stream stream)
        {
            var maxSizeMb = maxFileSize * 1024 * 1024;
            Result<Stream> result = new Result<Stream>();
            if (stream.Length.Equals(0))
            {
                result.ResultStatus = false;
                result.ResultMessage = "Please choose file";
                result.ResultCode = ResultCodes.Validation.GetHashCode();

            }
            if (stream.Length > maxSizeMb)
            {
                result.ResultStatus = false;
                result.ResultMessage = "File size can not be bigger then " + maxFileSize + " Mb";
                result.ResultCode = ResultCodes.Validation.GetHashCode();

            }
            else
            {
                result.ResultStatus = true;
                result.ResultMessage = "File is valid";
                result.ResultCode = ResultCodes.OK.GetHashCode();
            }
            return result;
        }


    }
}
