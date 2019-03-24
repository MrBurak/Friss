using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using CoreApi.Models;

namespace CoreApi.Helpers
{
    public class FileLogger
    {
        public async Task Log(ApiLogItem logitem)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Time [");
            sb.Append(string.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.UtcNow));
            sb.Append("]");
            sb.Append(Environment.NewLine);

            
            sb.Append("Path [");
            sb.Append(logitem.Path);
            sb.Append("]");
            sb.Append(Environment.NewLine);

            sb.Append("Method [");
            sb.Append(logitem.Method);
            sb.Append("]");
            sb.Append(Environment.NewLine);


            if (logitem.Method == "GET")
            {
                if (!string.IsNullOrEmpty(logitem.QueryString))
                {
                    sb.Append("Query Srting [");
                    sb.Append(logitem.QueryString);
                    sb.Append("]");
                    sb.Append(Environment.NewLine);
                }
            }
            else
            {
               
                sb.Append("Request Body [");
                sb.Append(logitem.RequestBody);
                sb.Append("]");
                sb.Append(Environment.NewLine);
            }

            sb.Append("Response Body [");
            sb.Append(logitem.ResponseBody);
            sb.Append("]");
            sb.Append(Environment.NewLine);

            sb.Append(Environment.NewLine);
            sb.Append("---------------------------------------------------------------");
            sb.Append(Environment.NewLine);


            var filename = "Log_" + string.Format("{0:dd/MM/yyyy}", DateTime.UtcNow) + ".txt";
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Log", filename);
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            File.AppendAllText(filepath, sb.ToString());
            sb.Clear();
        }
        
    }
}
