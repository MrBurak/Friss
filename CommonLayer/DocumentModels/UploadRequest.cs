using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DocumentModels
{
    public class UploadRequest
    {
        public string Base64String { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
    }
}
