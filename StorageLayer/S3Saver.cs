using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.DocumentModels;
using StorageLayer.Interfaces;

namespace StorageLayer
{
    public class S3Saver : ISaver
    {
        public Result<DocumentInfo> Save(UploadRequest request)
        {
            return new Result<DocumentInfo>();
        }
    }
}
