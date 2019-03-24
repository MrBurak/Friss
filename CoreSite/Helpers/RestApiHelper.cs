
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CoreSite.Helpers
{
    public class RestApiHelper: IRestApiHelper
    {
        IConfigurationRoot _configuration;
        readonly string ApiUrl;
        public RestApiHelper()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ApiUrl = _configuration.GetSection("ApiSettings:Url").Value;
        }
        public TResponseModel ApiServicePostRequest<TRequestModel, TResponseModel>(TRequestModel model, string url, string token)
          where TRequestModel : class, new()
          where TResponseModel : class, new()
        {
            try
            {
                url = ApiUrl + url;
                var postData = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var data = System.Text.Encoding.UTF8.GetBytes(postData);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;

                if (token.Length > 0)
                {
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
                }

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = data.Length;

                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);

                    using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string pageContent = streamReader.ReadToEnd();

                            TResponseModel responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponseModel>(pageContent);

                            return responseModel;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        string strErr = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                    }
                }
                else
                {
                    string strErr = ex.ToString();
                }

                //throw;
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                //throw;
                //loging
            }

            return default(TResponseModel);
        }
        public TResponseModel ApiServiceGetRequest<TResponseModel>(string url, string parameters, string token)
        {
            try
            {
                url = ApiUrl + url +  parameters;

                WebRequest webRequest = WebRequest.Create(url);

                webRequest.Headers.Add("Authorization", "Bearer " + token);

                WebResponse resp = webRequest.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());

                string pageContent = sr.ReadToEnd();

                TResponseModel responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponseModel>(pageContent);

                return responseModel;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse err)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        string strErr = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                    }
                }
                else
                {
                    string strErr = ex.ToString();
                }
                //throw;
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                //throw;
            }

            return default(TResponseModel);
        }



    }
}
