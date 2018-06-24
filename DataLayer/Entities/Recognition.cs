﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Recognition : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int? NeuralNetworkId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public IEnumerable<RecognitionImage> Images { get; set; }
    }
}