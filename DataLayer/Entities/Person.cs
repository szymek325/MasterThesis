using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Person : EntityBase
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}