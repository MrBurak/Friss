using System;
using CommonLayer;
using CommonLayer.DocumentModels;


namespace StorageLayer.Interfaces
{
    public interface ISaver
    {
        Result<DocumentInfo> Save(UploadRequest request);
    }
}
