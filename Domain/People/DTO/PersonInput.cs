using System.Collections.Generic;
using Domain.Files.DTO;

namespace Domain.People.DTO
{
    public  class PersonInput
    {
        public string Name { get; set; }
        public IEnumerable<FileToUpload> Files { get; set; }
    }
}