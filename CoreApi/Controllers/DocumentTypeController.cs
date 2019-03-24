using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        IDocumentTypeBusiness _repository;
        IConfigurationRoot _configuration;
        int maxFileSize;
        public DocumentTypeController(IDocumentTypeBusiness repository)
        {
            _repository = repository;
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            maxFileSize = Convert.ToInt32(_configuration.GetSection("UploadProperties:MaxFileSize").Value);
        }

        [HttpGet]
        [Route("v1")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Task.Run(() => _repository.Get()));
        }
        [HttpGet]
        [Route("maxfilesize")]
        public async Task<IActionResult> GetMaxFileSize()
        {

            return Ok(await Task.Run(() => maxFileSize));
        }

    }
}