using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataLayer.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Guid { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}