using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageLayer;
using CommonLayer;
using CoreApi.Helpers;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase 
    {
        //public ValuesController(ILogger logger) : base(logger)
        //{

        //}
        
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Started"; 
        }

    }
}
