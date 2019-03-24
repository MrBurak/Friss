using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSite.Models.RestApiModels
{
    public class DocumentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Fullname { get; set; }
        public DateTime LastDownloadDate { get; set; }
        public string FileSize { get; set; }
    }
}
