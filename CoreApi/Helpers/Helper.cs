using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageLayer.Interfaces;
using StorageLayer;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CoreApi.Helpers
{
    public class Helper : IHelper
    {
        IConfigurationRoot _configuration;
        IValidation _validation;
        IHostingEnvironment _env;
        IHttpContextAccessor _httpContextAccessor;
        string _saverName;
        public Helper(IValidation validation, IHostingEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _saverName = _configuration.GetSection("UploadProperties:StorageType").Value;
            _validation = validation;
            _httpContextAccessor = httpContextAccessor;
        }
        public ISaver GetSaver()
        {
            ISaver saver;
            switch (_saverName)
            {
                case "LocalSaver":
                    saver = (ISaver)Activator.CreateInstance(typeof(LocalSaver), _validation, _env, _httpContextAccessor); 
                    break;
                default:
                    saver = (ISaver)Activator.CreateInstance(typeof(LocalSaver), _validation, _env, _httpContextAccessor);
                    break;
            }
            return saver;
        }
    }
}
