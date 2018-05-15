using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class DbInitializer
    {
        public static void Seed(MasterContext context)
        {
            if (!context.Statuses.Any())
            {
                context.Statuses.Add(new Status
                {
                    Name = "New"
                });
                context.Statuses.Add(new Status
                {
                    Name = "In Progress"
                });
                context.Statuses.Add(new Status
                {
                    Name = "Completed"
                });
                context.Statuses.Add(new Status
                {
                    Name = "Error"
                });
            }

            context.SaveChanges();
        }
    }
}
