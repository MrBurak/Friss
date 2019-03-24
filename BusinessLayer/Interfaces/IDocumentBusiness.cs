using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IDocumentBusiness
    {
        Result<List<Document>> Get();
        Result<Document> Insert(Document document);
        Result<string> Update(int Id);
        Result<List<DocumentItem>> GetAll();
    }
}
