using System;
using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface ISensorsReadingRepository : IGenericRepository<SensorsReading>
    {
        IEnumerable<SensorsReading> GetByDay(DateTime day);
    }
}