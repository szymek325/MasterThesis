using System;

namespace Domain.Configuration
{
    public interface IGuidProvider
    {
        Guid NewGuid { get; }
        string NewGuidAsString { get; }
    }
}