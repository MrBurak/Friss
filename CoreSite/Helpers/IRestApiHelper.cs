using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSite.Helpers
{
    public interface IRestApiHelper
    {
        TResponseModel ApiServicePostRequest<TRequestModel, TResponseModel>(TRequestModel model, string url, string token)
          where TRequestModel : class, new()
          where TResponseModel : class, new();
        TResponseModel ApiServiceGetRequest<TResponseModel>(string url, string parameters, string token);
    }
}
