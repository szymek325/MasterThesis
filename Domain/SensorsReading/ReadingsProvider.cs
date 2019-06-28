using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.SensorsReading.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.SensorsReading
{
    public class ReadingsProvider : IReadingsProvider
    {
        private readonly ILogger<ReadingsProvider> logger;
        private readonly IMapper mapper;
        private readonly ISensorsReadingRepository readingsRepo;


        public ReadingsProvider(ISensorsReadingRepository readingsRepo, ILogger<ReadingsProvider> logger,
            IMapper mapper)
        {
            this.readingsRepo = readingsRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IEnumerable<Reading> GetAllReadings()
        {
            var sensorsReadings = readingsRepo.GetAll().ToList();
            logger.LogInformation($"{sensorsReadings.Count} retrieved from db");
            var readings = mapper.Map<IEnumerable<Reading>>(sensorsReadings);
            return readings;
        }

        public IEnumerable<DateOutput> GetDistinctDates()
        {
            var distinctDates = readingsRepo.GetAll().Select(x =>new DateOutput{
                Date = x.CreationTime.Date}
            ).Distinct().OrderByDescending(x=>x.Date).ToList();
            return distinctDates;
        }

        public IEnumerable<Reading> GetReadingsFromDay(DateTime day)
        {
            var sensorReadings = readingsRepo.GetByDay(day).OrderByDescending(x=>x.CreationTime);
            var readings = mapper.Map<IEnumerable<Reading>>(sensorReadings);
            return readings;
        }

        public IEnumerable<Reading> GetReadingsFromDay(string day)
        {
            var parsedDay = DateTime.Parse(day);
            var sensorReadings = readingsRepo.GetByDay(parsedDay).OrderByDescending(x => x.CreationTime);
            var readings = mapper.Map<IEnumerable<Reading>>(sensorReadings);
            return readings;
        }
    }
}