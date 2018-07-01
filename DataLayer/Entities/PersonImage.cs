using System.IO;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class PersonImage : EntityBase, IImage
    {
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string GetPath()
        {
            return $"{nameof(PersonImage)}/{Id}";
        }
    }
}