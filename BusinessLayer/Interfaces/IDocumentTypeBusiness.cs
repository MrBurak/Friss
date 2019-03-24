using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IDocumentTypeBusiness
    {
        Result<List<DocumentType>> Get();
    }
}
