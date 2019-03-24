using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUploadDocument { get; set; }
    }
}
