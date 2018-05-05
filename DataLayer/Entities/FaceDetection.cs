using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class FaceDetection : EntityBase
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int DnnFaces { get; set; }
        public int HaarFaces { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}