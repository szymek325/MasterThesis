using System;

namespace Domain.Configuration
{
    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid => Guid.NewGuid();
        public string NewGuidAsString => Guid.NewGuid().ToString();
    }
}