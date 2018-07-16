using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PeopleUploader.Models
{
    public class ParsedFile
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public Stream FileStream { get; set; }
        public string PersonName { get; set; }
    }
}
