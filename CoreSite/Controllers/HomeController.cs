using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreSite.Models;
using CoreSite.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;
using CoreSite.Models.RestApiModels;
using Microsoft.AspNetCore.Http;

namespace CoreSite.Controllers
{
    public class HomeController : Controller
    {
        IRestApiHelper _apiHelper;
        IConfigurationRoot _configuration;
        public HomeController(IRestApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        }
        public IActionResult Index()
        {
            if (!ControlSession()) return View("Login");
            var session= GetSession();
            ViewData["Session"] = session;
            
            Result<List<DocumentItem>> result = new Result<List<DocumentItem>>();
            var url = _configuration.GetSection("ApiSettings:GetDocument").Value;
            result = _apiHelper.ApiServiceGetRequest<Result<List<DocumentItem>>>(url, string.Empty, session.Token );
            if (result != null)
            {
                return View(result.ResultEntity);
            }
            else
            {
               return View(new List<DocumentItem>());
            }

            
        }

        [HttpGet]
        public JsonResult Download([FromQuery] int id)
        {
            
            var url = _configuration.GetSection("ApiSettings:DownloadDocument").Value;
            var result = _apiHelper.ApiServiceGetRequest<Result<string>>(url, "?Id=" + id, GetSession().Token);
            return Json(result);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login([FromBody] LoginModel model)
        {
            Result<TokenModel> result = new Result<TokenModel>();
            var url = _configuration.GetSection("ApiSettings:Login").Value;
            var tokenmodel= _apiHelper.ApiServicePostRequest<LoginModel, TokenModel>(model, url, string.Empty);
            if (tokenmodel != null)
            {
                result.ResultEntity = tokenmodel;
                var usertoken = Utility.ObjectToByteArray(tokenmodel);
                HttpContext.Session.Set("UserToken", usertoken);
                result.ResultStatus = true;
            }
            else
            {
                result.ResultMessage = "Unable to login";
                result.ResultStatus = false;
            }

            return Json(result);
        }

        public IActionResult Upload()
        {
            return Uploader();
            
        }

        [HttpPost]
        public IActionResult Upload(string upload)
        {
            string filename = Request.Form["filename"];
            var files= Request.Form.Files;
            if (files.Count == 0 || filename=="")
            {
                
                ViewData["Result"]= "Please choose file and give it a name";
                ViewData["Saved"] = false;

            }
            else
            {
                var file = files[0];
                UploadRequest model = new UploadRequest
                {
                    Extension = Path.GetExtension(file.FileName),
                    Base64String=Utility.ConvertToBase64(file.OpenReadStream()),
                    Name=filename
                };
                var url = _configuration.GetSection("ApiSettings:UploadDocument").Value;
                var result = _apiHelper.ApiServicePostRequest<UploadRequest, Result<Document>>(model, url, GetSession().Token);
                if (result != null)
                {
                    ViewData["Result"] = result.ResultMessage;
                    ViewData["Saved"] = result.ResultStatus;
                }
                else
                {
                    ViewData["Result"] = "Upload failed";
                    ViewData["Saved"] = false;
                }


            }
            return Uploader();
        }

        public IActionResult Uploader()
        {
            if (!ControlSession()) return View("Login");
            var session = GetSession();
            if (session.UserRoleId != 1) return View("Index");
            ViewData["Session"] = session;
            List<string> extensions = new List<string>();
            var sizeurl = _configuration.GetSection("ApiSettings:MaxFileSize").Value;
            ViewData["FileSize"] = _apiHelper.ApiServiceGetRequest<int>(sizeurl, string.Empty, session.Token);


            var url = _configuration.GetSection("ApiSettings:GetDocumentType").Value;
            var result = _apiHelper.ApiServiceGetRequest<Result<List<DocumentType>>>(url, string.Empty, session.Token);
            if (result != null)
            {
                foreach (DocumentType dt in result.ResultEntity)
                {
                    extensions.AddRange(dt.Extentions.Split(Convert.ToChar(";")));
                }
                return View(extensions);
            }
            else
            {
                return View(new List<string>());
            }
        }


        private bool ControlSession()
        {
            byte[] token;
            HttpContext.Session.TryGetValue("UserToken", out token);
            return token != null;
        }
        private TokenModel GetSession()
        {
            byte[] token;
            HttpContext.Session.TryGetValue("UserToken", out token);
            return Utility.FromByteArray<TokenModel>(token);
        }


    }
}
