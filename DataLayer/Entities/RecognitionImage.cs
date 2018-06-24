using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class RecognitionImage
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public int? RecognitionId { get; set; }
        public Recognition Recognition { get; set; }
    }
}
