using System;
using System.Collections.Generic;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class Detection : EntityBase
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? CompletionTime { get; set; }
        public ImageAttachment Image { get; set; }
        public IEnumerable<DetectionResult> Results { get; set; }
    }
}