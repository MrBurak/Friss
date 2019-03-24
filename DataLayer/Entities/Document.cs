using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime LastDownloadDate { get; set; }
        public string FileSize { get; set; }
    }
}
