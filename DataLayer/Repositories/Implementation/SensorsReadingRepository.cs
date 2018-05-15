using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class SensorsReadingRepository : GenericRepository<SensorsReading>, ISensorsReadingRepository
    {
        public SensorsReadingRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<SensorsReading> GetByDay(DateTime day)
        {
            return GetAll().Where(x => x.CreationTime.Date == day.Date);
        }
    }
}