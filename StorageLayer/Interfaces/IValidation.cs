using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonLayer;

namespace StorageLayer
{
    public interface IValidation
    {
        Result<Stream> ValidateFileSize(Stream stream);
        Result<Stream> ValidateExtension(Stream stream, string extension);
        Result<Stream> Base64ToStream(string base64);
    }
}
