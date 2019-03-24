using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.DocumentModels;
using StorageLayer.Interfaces;

namespace StorageLayer
{
    public class BlobSaver : ISaver
    {
        public Result<DocumentInfo> Save(UploadRequest request)
        {
            return new Result<DocumentInfo>();
        }
    }
}
