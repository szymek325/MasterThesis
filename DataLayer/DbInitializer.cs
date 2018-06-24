using System.Linq;
using DataLayer.Entities;

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