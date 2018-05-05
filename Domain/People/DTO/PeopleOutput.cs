using System;
using System.Collections.Generic;
using System.Text;
using Domain.Files.DTO;

namespace Domain.People.DTO
{
    public class PersonOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public IEnumerable<FileLink> FileLinks { get; set; }
    }
}
